namespace Arcanus.Mods
{
	/// <summary>
	/// This class contains core settings for the Arcanus server
	/// </summary>
	public class Core : IMod
	{
		public void PreStart(ModManager m) { }
		public void Start(ModManager m)
		{
			//Render hint to send to clients
			m.RenderHint(RenderHint.Fast);

			//Different serverside view distance if singleplayer
			if (m.IsSinglePlayer())
			{
				m.SetPlayerAreaSize(512);
			}
			else
			{
				m.SetPlayerAreaSize(256);
			}

			//Set up server time
			m.SetGameDayRealHours(1);
			m.SetDaysPerYear(24);

			//Set up day/night cycle
			m.SetSunLevels(sunLevels);
			m.SetLightLevels(lightLevels);
		}

		float[] lightLevels = new float[]
		{
			0.0351843721f,
			0.0439804651f,
			0.0549755814f,
			0.0687194767f,
			0.0858993459f,
			0.1073741824f,
			0.134217728f,
			0.16777216f,
			0.2097152f,
			0.262144f,
			0.32768f,
			0.4096f,
			0.512f,
			0.64f,
			0.8f,
			1f,
		};

		int[] sunLevels = new int[]
		{
			07,//00:00
			07,
			07,
			07,
			07,//01:00
			07,
			07,
			07,
			07,//02:00
			07,
			07,
			07,
			07,//03:00
			07,
			07,
			07,
			07,//04:00
			07,
			07,
			07,
			08,//05:00
			08,
			09,
			09,
			10,//06:00
			10,
			11,
			11,
			12,//07:00
			12,
			13,
			13,
			14,//08:00
			14,
			15,
			15,
			15,//09:00
			15,
			15,
			15,
			15,//10:00
			15,
			15,
			15,
			15,//11:00
			15,
			15,
			15,
			15,//12:00
			15,
			15,
			15,
			15,//13:00
			15,
			15,
			15,
			15,//14:00
			15,
			15,
			15,
			15,//15:00
			15,
			15,
			15,
			15,//16:00
			15,
			15,
			15,
			15,//17:00
			15,
			15,
			15,
			15,//18:00
			15,
			15,
			15,
			15,//19:00
			15,
			15,
			15,
			15,//20:00
			15,
			14,
			14,
			13,//21:00
			13,
			12,
			12,
			11,//22:00
			11,
			10,
			10,
			09,//23:00
			09,
			08,
			08,
		};
	}
}
