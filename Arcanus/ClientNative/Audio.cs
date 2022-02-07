using OpenTK.Mathematics;
using OpenTK.Audio.OpenAL;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace Arcanus.ClientNative
{
	public class AudioOpenAl
	{
		public GameExit d_GameExit;

		public AudioOpenAl()
		{
			try
			{
				// get all the devices and create a context for the first one
				List<string> devices = ALC.GetString(AlcGetStringList.DeviceSpecifier);
				context = ALC.CreateContext(ALC.OpenDevice(devices[0]), new ALContextAttributes());

				// enable the current device's context
				// this is required or we will hear no sound
				ALC.MakeContextCurrent(context);
			}
			catch (Exception e)
			{
				// try to install the OpenAL library
				string oalinst = "oalinst.exe";

				if (File.Exists(oalinst))
				{
					try
					{
						Process.Start(oalinst, "/s");
					} catch {}
				}

				Console.WriteLine(e);
			}
		}

		ALContext context;

		// Loads a wave/riff audio file.
		public static byte[] LoadWave(Stream stream, out int channels, out int bits, out int rate)
		{
			if (stream == null)
				throw new ArgumentNullException("stream");

			using (BinaryReader reader = new BinaryReader(stream))
			{
				// RIFF header
				string signature = new string(reader.ReadChars(4));
				if (signature != "RIFF")
					throw new NotSupportedException("Specified stream is not a wave file.");

				int riff_chunck_size = reader.ReadInt32();

				string format = new string(reader.ReadChars(4));
				if (format != "WAVE")
					throw new NotSupportedException("Specified stream is not a wave file.");

				// WAVE header
				string format_signature = new string(reader.ReadChars(4));
				if (format_signature != "fmt ")
					throw new NotSupportedException("Specified wave file is not supported.");

				int format_chunk_size = reader.ReadInt32();
				int audio_format = reader.ReadInt16();
				int num_channels = reader.ReadInt16();
				int sample_rate = reader.ReadInt32();
				int byte_rate = reader.ReadInt32();
				int block_align = reader.ReadInt16();
				int bits_per_sample = reader.ReadInt16();

				string data_signature = new string(reader.ReadChars(4));
				if (data_signature != "data")
					throw new NotSupportedException("Specified wave file is not supported.");

				int data_chunk_size = reader.ReadInt32();

				channels = num_channels;
				bits = bits_per_sample;
				rate = sample_rate;

				return reader.ReadBytes((int)reader.BaseStream.Length);
			}
		}
		public static ALFormat GetSoundFormat(int channels, int bits)
		{
			switch (channels)
			{
				case 1:
					return bits == 8 ? ALFormat.Mono8 : ALFormat.Mono16;
				case 2:
					return bits == 8 ? ALFormat.Stereo8 : ALFormat.Stereo16;
				default:
					throw new NotSupportedException("The specified sound format is not supported.");
			}
		}

		public class AudioTask : AudioCi
		{
			public AudioTask(GameExit gameexit, AudioDataCs sample, AudioOpenAl audio)
			{
				this.gameexit = gameexit;
				this.sample = sample;
				this.audio = audio;
			}
			AudioOpenAl audio;
			GameExit gameexit;
			AudioDataCs sample;
			public Vector3 position;
			public void Play()
			{
				if (started)
				{
					shouldplay = true;
					return;
				}
				started = true;
				ThreadPool.QueueUserWorkItem(delegate
				{
					play();
				});
			}
			//bool resume = true;
			bool started = false;
			void play()
			{
				try
				{
					DoPlay();
				}
				catch (Exception e)
				{
					Console.WriteLine(e.ToString());
				}
			}

			unsafe private void DoPlay()
			{
				int state;

				// create the buffers for playback
				int source = AL.GenSource();
				int buffer = AL.GenBuffer();

				// copy the audio data into unmanaged memory so we can buffer it
				int pcmSize = Marshal.SizeOf(typeof(IntPtr)) * sample.Pcm.Length;
				IntPtr pcm = Marshal.AllocHGlobal(pcmSize);
				Marshal.Copy(sample.Pcm, 0, pcm, sample.Pcm.Length);

				// buffer the data so we can play it
				AL.BufferData(buffer, GetSoundFormat(sample.Channels, sample.BitsPerSample), pcm, pcmSize, sample.Rate);

				// various sound effects
				AL.DistanceModel(ALDistanceModel.InverseDistance);
				AL.Source(source, ALSourcef.RolloffFactor, 0.3f);
				AL.Source(source, ALSourcef.ReferenceDistance, 1);
				AL.Source(source, ALSourcef.MaxDistance, (int)(64 * 1));

				// copy the buffer to our playable source
				AL.Source(source, ALSourcei.Buffer, buffer);

				// play the audio
				AL.SourcePlay(source);

				// the audio plays in a new thread and so we need to
				// query its state to know when the sound has stopped
				// and only after can we run the clean up methods
				for (;;)
				{
					// get the audio state (i.e. playing, paused, etc)
					AL.GetSource(source, ALGetSourcei.SourceState, out state);

					if (!loop && (ALSourceState)state != ALSourceState.Playing)
					{
						break;
					}

					if (stop)
					{
						break;
					}

					if (gameexit.GetExit())
					{
						break;
					}

					if (loop)
					{
						if (state == (int)ALSourceState.Playing && !shouldplay)
						{
							AL.SourcePause(source);
						}

						if (state != (int)ALSourceState.Playing && shouldplay)
						{
							if (restart)
							{
								AL.SourceRewind(source);
								restart = false;
							}

							AL.SourcePlay(source);
						}
					}

					AL.Source(source, ALSource3f.Position, position.X, position.Y, position.Z);

					/*
                    if (stop)
                    {
                        AL.SourcePause(source);
                        resume = false;
                    }
                    if (resume)
                    {
                        AL.SourcePlay(source);
                        resume = false;
                    }
                    */

					Thread.Sleep(1);
				}

				Finished = true;
				AL.SourceStop(source);
				AL.DeleteSource(source);
				AL.DeleteBuffer(buffer);
			}

			public bool loop = false;
			bool stop;

			public void Stop()
			{
				stop = true;
			}

			public bool shouldplay;
			public bool restart;

			public void Restart()
			{
				restart = true;
			}

			internal void Pause()
			{
				shouldplay = false;
			}

			internal bool Finished;
		}

		public AudioDataCs GetSampleFromArray(byte[] data)
		{
			Stream stream = new MemoryStream(data);
			if (stream.ReadByte() == 'R'
				&& stream.ReadByte() == 'I'
				&& stream.ReadByte() == 'F'
				&& stream.ReadByte() == 'F')
			{
				stream.Position = 0;
				int channels, bits_per_sample, sample_rate;
				byte[] sound_data = LoadWave(stream, out channels, out bits_per_sample, out sample_rate);
				AudioDataCs sample = new AudioDataCs()
				{
					Pcm = sound_data,
					BitsPerSample = bits_per_sample,
					Channels = channels,
					Rate = sample_rate,
				};
				return sample;
			}
			else
			{
				stream.Position = 0;
				AudioDataCs sample = new OggDecoder().OggToWav(stream);
				return sample;
			}
		}
		Vector3 lastlistener;
		public AudioTask CreateAudio(AudioDataCs sample)
		{
			return new AudioTask(d_GameExit, sample, this);
		}
		public void UpdateListener(Vector3 position, Vector3 orientation)
		{
			lastlistener = position;
			AL.Listener(ALListener3f.Position, position.X, position.Y, position.Z);
			Vector3 up = Vector3.UnitY;
			AL.Listener(ALListenerfv.Orientation, ref orientation, ref up);
		}
	}
}
