public abstract class GamePlatform
{
	// Primitive
	public abstract int FloatToInt(float value);
	public abstract int FloatToIntCeiling(float value);
	public abstract float MathSin(float a);
	public abstract float MathCos(float a);
	public abstract float MathSqrt(float value);
	public abstract float MathAcos(float p);
	public abstract float MathTan(float p);
	public abstract float FloatModulo(float a, int b);

	public abstract int IntParse(string value);
	public abstract bool IntTryParse(string s, IntRef ret);
	public abstract float FloatParse(string value);
	public abstract string IntToString(int value);
	public abstract string FloatToString(float value);
	public abstract bool FloatTryParse(string s, FloatRef ret);
	public abstract string StringFormat(string format, string arg0);
	public abstract string StringFormat2(string format, string arg0, string arg1);
	public abstract string StringFormat3(string format, string arg0, string arg1, string arg2);
	public abstract string StringFormat4(string format, string arg0, string arg1, string arg2, string arg3);
	public abstract int[] StringToCharArray(string s, IntRef length);
	public abstract string CharArrayToString(int[] charArray, int length);
	public abstract bool StringEmpty(string data);
	public abstract bool StringContains(string a, string b);
	public abstract string StringReplace(string s, string from, string to);
	public abstract bool StringStartsWithIgnoreCase(string a, string b);
	public abstract int StringIndexOf(string s, string p);
	public abstract string StringTrim(string value);
	public abstract string StringToLower(string p);
	public abstract string StringFromUtf8ByteArray(byte[] value, int valueLength);
	public abstract byte[] StringToUtf8ByteArray(string s, IntRef retLength);
	public abstract string[] StringSplit(string value, string separator, IntRef returnLength);
	public abstract string StringJoin(string[] value, string separator);
	public abstract string StringSplitCamelCase(string s);

	// Misc
	public abstract string Timestamp();
	public abstract void ClipboardSetText(string s);
	public abstract void TextSize(string text, FontCi font, IntRef outWidth, IntRef outHeight);
	public abstract void Exit();
	public abstract bool ExitAvailable();
	public abstract string PathSavegames();
	public abstract string PathCombine(string part1, string part2);
	public abstract string[] DirectoryGetFiles(string path, IntRef length);
	public abstract string[] FileReadAllLines(string path, IntRef length);
	public abstract void WebClientDownloadDataAsync(string url, HttpResponseCi response);
	public abstract void ThumbnailDownloadAsync(string ip, int port, ThumbnailResponseCi response);
	public abstract string FileName(string fullpath);
	public abstract string FileImage(string fullpath);
	public abstract string FileImagePath(string fullpath);
	public abstract string FileLastWriteTime(string fullpath);
	public abstract bool FileExists(string fullpath);
	public abstract void FileRename(string oldpath, string newpath);
	public abstract void AddOnNewFrame(NewFrameHandler handler);
	public abstract void AddOnKeyEvent(KeyEventHandler handler);
	public abstract void AddOnMouseEvent(MouseEventHandler handler);
	public abstract void AddOnTouchEvent(TouchEventHandler handler);
	public abstract int GetCanvasWidth();
	public abstract int GetCanvasHeight();
	public abstract string GetLanguageIso6391();
	public abstract int TimeMillisecondsFromStart();
	public abstract void ThrowException(string message);
	public abstract BitmapCi BitmapCreate(int width, int height);
	public abstract void BitmapSetPixelsArgb(BitmapCi bmp, int[] pixels);
	public abstract BitmapCi CreateTextTexture(Text_ t);
	public abstract void SetTextRendererFont(int fontID);
	public abstract float BitmapGetWidth(BitmapCi bmp);
	public abstract float BitmapGetHeight(BitmapCi bmp);
	public abstract void BitmapDelete(BitmapCi bmp);
	public abstract void ConsoleWriteLine(string p);
	public abstract MonitorObject MonitorCreate();
	public abstract void MonitorEnter(MonitorObject monitorObject);
	public abstract void MonitorExit(MonitorObject monitorObject);
	public abstract void SaveScreenshot();
	public abstract void SaveScreenshotFile(string filename);
	public abstract BitmapCi GrabScreenshot();
	public abstract AviWriterCi AviWriterCreate();
	public abstract UriCi ParseUri(string uri);
	public abstract RandomCi RandomCreate();
	public abstract string PathStorage();
	public abstract void SetVSync(bool enabled);
	public abstract string GetGameVersion();
	public abstract void GzipDecompress(byte[] compressed, int compressedLength, byte[] ret);
	public abstract bool ChatLog(string servername, string p);
	public abstract bool IsValidTypingChar(int c);
	public abstract void WindowExit();
	public abstract void MessageBoxShowError(string text, string caption);
	public abstract int ByteArrayLength(byte[] arr);
	public abstract BitmapCi BitmapCreateFromPng(byte[] data, int dataLength);
	public abstract void BitmapGetPixelsArgb(BitmapCi bitmap, int[] bmpPixels);
	public abstract string[] ReadAllLines(string p, IntRef retCount);
	public abstract bool ClipboardContainsText();
	public abstract string ClipboardGetText();
	public abstract void SetTitle(string applicationname);
	public abstract bool Focused();
	public abstract void AddOnCrash(OnCrashHandler handler);
	public abstract string KeyName(int key);
	public abstract DisplayResolutionCi[] GetDisplayResolutions(IntRef resolutionsCount);
	public abstract WindowState GetWindowState();
	public abstract void SetWindowState(WindowState value);
	public abstract void ChangeResolution(int width, int height, int bitsPerPixel, float refreshRate);
	public abstract DisplayResolutionCi GetDisplayResolutionDefault();
	public abstract void WebClientUploadDataAsync(string url, byte[] data, int dataLength, HttpResponseCi response);
	public abstract string FileOpenDialog(string extension, string extensionName, string initialDirectory);
	public abstract void MouseCursorSetVisible(bool value);
	public abstract bool MouseCursorIsVisible();
	public abstract void ApplicationDoEvents();
	public abstract void ThreadSpinWait(int iterations);
	public abstract void ShowKeyboard(bool show);
	public abstract bool IsFastSystem();
	public abstract Preferences GetPreferences();
	public abstract void SetPreferences(Preferences preferences);
	public abstract ServerConfigCi GetServerConfig(string filename);
	public abstract bool IsMousePointerLocked();
	public abstract void RequestMousePointerLock();
	public abstract void ExitMousePointerLock();
	public abstract bool MultithreadingAvailable();
	public abstract void QueueUserWorkItem(Action_ action);
	public abstract void LoadAssetsAsyc(AssetList list, FloatRef progress);
	public abstract byte[] GzipCompress(byte[] data, int dataLength, IntRef retLength);
	public abstract bool IsDebuggerAttached();
	public abstract bool IsSmallScreen();
	public abstract void OpenLinkInBrowser(string url);
	public abstract void SaveAssetToCache(Asset tosave);
	public abstract Asset LoadAssetFromCache(string md5);
	public abstract bool IsCached(string md5);
	public abstract bool IsChecksum(string checksum);
	public abstract string DecodeHTMLEntities(string htmlencodedstring);
	public abstract string QueryStringValue(string key);
	public abstract void SetWindowCursor(int hotx, int hoty, int sizex, int sizey, byte[] imgdata, int imgdataLength);
	public abstract void RestoreWindowCursor();

	// Audio
	public abstract AudioData AudioDataCreate(byte[] data, int dataLength);
	public abstract bool AudioDataLoaded(AudioData data);
	public abstract AudioCi AudioCreate(AudioData data);
	public abstract void AudioPlay(AudioCi audio);
	public abstract void AudioPause(AudioCi audio);
	public abstract void AudioDelete(AudioCi audioCi);
	public abstract bool AudioFinished(AudioCi audio);
	public abstract void AudioSetPosition(AudioCi audio, float x, float y, float z);
	public abstract void AudioUpdateListener(float posX, float posY, float posZ, float orientX, float orientY, float orientZ);

	// Tcp
	public abstract bool TcpAvailable();
	public abstract void TcpConnect(string ip, int port, BoolRef connected);
	public abstract void TcpSend(byte[] data, int length);
	public abstract int TcpReceive(byte[] data, int dataLength);

	// Enet
	public abstract bool EnetAvailable();
	public abstract EnetHost EnetCreateHost();
	public abstract bool EnetHostService(EnetHost host, int timeout, EnetEventRef enetEvent);
	public abstract bool EnetHostCheckEvents(EnetHost host, EnetEventRef event_);
	public abstract EnetPeer EnetHostConnect(EnetHost host, string hostName, int port, int data, int channelLimit);
	public abstract void EnetPeerSend(EnetPeer peer, byte channelID, byte[] data, int dataLength, int flags);
	public abstract void EnetHostInitialize(EnetHost host, IPEndPointCi address, int peerLimit, int channelLimit, int incomingBandwidth, int outgoingBandwidth);

	// WebSocket
	public abstract bool WebSocketAvailable();
	public abstract void WebSocketConnect(string ip, int port);
	public abstract void WebSocketSend(byte[] data, int dataLength);
	public abstract int WebSocketReceive(byte[] data, int dataLength);

	// OpenGl
	public abstract void GlViewport(int x, int y, int width, int height);
	public abstract void GlClearColorBufferAndDepthBuffer();
	public abstract void GlDisableDepthTest();
	public abstract void GlClearColorRgbaf(float r, float g, float b, float a);
	public abstract void GlEnableDepthTest();
	public abstract void GlDisableCullFace();
	public abstract void GlEnableCullFace();
	public abstract void GlEnableTexture2d();
	public abstract void GlDisableTexture2d();
	public abstract void GLLineWidth(int width);
	public abstract void GLDisableAlphaTest();
	public abstract void GLEnableAlphaTest();
	public abstract void GLDeleteTexture(int id);
	public abstract void GlClearDepthBuffer();
	public abstract void GlLightModelAmbient(int r, int g, int b);
	public abstract void GlEnableFog();
	public abstract void GlHintFogHintNicest();
	public abstract void GlFogFogModeExp2();
	public abstract void GlFogFogColor(int r, int g, int b, int a);
	public abstract void GlFogFogDensity(float density);
	public abstract int GlGetMaxTextureSize();
	public abstract void GlDepthMask(bool flag);
	public abstract void GlCullFaceBack();
	public abstract void GlEnableLighting();
	public abstract void GlEnableColorMaterial();
	public abstract void GlColorMaterialFrontAndBackAmbientAndDiffuse();
	public abstract void GlShadeModelSmooth();
	public abstract void GlDisableFog();
	public abstract void BindTexture2d(int texture);
	public abstract Model CreateModel(ModelData modelData);
	public abstract void DrawModel(Model model);
	public abstract void InitShaders();
	public abstract void SetMatrixUniformProjection(float[] pMatrix);
	public abstract void SetMatrixUniformModelView(float[] mvMatrix);
	public abstract void DrawModels(Model[] model, int count);
	public abstract void DrawModelData(ModelData data);
	public abstract void DeleteModel(Model model);
	public abstract int LoadTextureFromBitmap(BitmapCi bmp);
	public abstract void GlActiveTexture(int textureUnit);
	public abstract GlProgram GlCreateProgram();
	public abstract void GlDeleteProgram(GlProgram program);
	public abstract GlShader GlCreateShader(ShaderType shaderType);
	public abstract void GlShaderSource(GlShader shader, string source);
	public abstract void GlCompileShader(GlShader shader);
	public abstract bool GlGetShaderCompileStatus(GlShader shader);
	public abstract string GlGetShaderInfoLog(GlShader shader);
	public abstract void GlAttachShader(GlProgram program, GlShader shader);
	public abstract void GlUseProgram(GlProgram program);
	public abstract int GlGetUniformLocation(GlProgram program, string name);
	public abstract void GlLinkProgram(GlProgram program);
	public abstract bool GlGetProgramLinkStatus(GlProgram program);
	public abstract string GlGetProgramInfoLog(GlProgram program);
	public abstract string GlGetStringSupportedShadingLanguage();
	public abstract void GlUniform1i(int location, int v0);
	public abstract void GlUniform1f(int location, float v0);
	public abstract void GlUniform2f(int location, float v0, float v1);
	public abstract void GlUniform3f(int location, float v0, float v1, float v2);
	public abstract void GlUniform4f(int location, float v0, float v1, float v2, float v3);
	public abstract void GlUniformArray1f(int location, int count, float[] values);

	// Game
	public abstract bool SinglePlayerServerAvailable();
	public abstract void SinglePlayerServerStart(string saveFilename, ServerConfigCi config);
	public abstract void SinglePlayerServerExit();
	public abstract bool SinglePlayerServerLoaded();
	public abstract void SinglePlayerServerDisable();
	public abstract DummyNetwork SinglePlayerServerGetNetwork();
	public abstract PlayerInterpolationState CastToPlayerInterpolationState(InterpolatedObject a);

	// Translation
	public abstract bool LanguageNativeAvailable();
	public abstract Language GetLanguageHandler();
}

public class Asset
{
	public string name;
	public string md5;
	public byte[] data;
	public int dataLength;
}

public class AssetList
{
	internal Asset[] items;
	internal int count;

	public Asset[] GetItems() { return items; }
	public void SetItems(Asset[] value) { items = value; }
	public int GetCount() { return count; }
	public void SetCount(int value) { count = value; }
}

public enum WindowState
{
	Normal,
	Minimized,
	Maximized,
	Fullscreen
}

public abstract class RandomCi
{
	public abstract float NextFloat();
	public abstract int Next();
	public abstract int MaxNext(int range);
}

public class UriCi
{
	internal string url;
	internal string ip;
	internal int port;
	internal DictionaryStringString get;
	public string GetUrl() { return url; }
	public void SetUrl(string value_) { url = value_; }
	public string GetIp() { return ip; }
	public void SetIp(string value_) { ip = value_; }
	public int GetPort() { return port; }
	public void SetPort(int value_) { port = value_; }
	public DictionaryStringString GetGet() { return get; }
	public void SetGet(DictionaryStringString value_) { get = value_; }
}

public class EnetHost
{
}

public abstract class EnetEvent
{
	public abstract EnetEventType Type();
	public abstract EnetPeer Peer();
	public abstract EnetPacket Packet();
}

public class EnetEventRef
{
	internal EnetEvent e;

	public EnetEvent GetEvent() { return e; }
	public void SetEvent(EnetEvent value_) { e = value_; }
}

public enum EnetEventType
{
	None,
	Connect,
	Disconnect,
	Receive
}

public class EnetPacketFlags
{
	public const int None = 0;
	public const int Reliable = 1;
	public const int Unsequenced = 2;
	public const int NoAllocate = 4;
	public const int UnreliableFragment = 8;
}

public abstract class EnetPeer
{
	public abstract int UserData();
	public abstract void SetUserData(int value);
	public abstract IPEndPointCi GetRemoteAddress();
}

public abstract class EnetPacket
{
	public abstract int GetBytesCount();
	public abstract byte[] GetBytes();
	public abstract void Dispose();
}


public class FloatRef
{
	public static FloatRef Create(float value_)
	{
		FloatRef f = new FloatRef();
		f.value = value_;
		return f;
	}
	internal float value;

	public float GetValue() { return value; }
	public void SetValue(float value_) { value = value_; }
}

public class KeyEventArgs
{
	int keyCode;
	public int GetKeyCode() { return keyCode; }
	public void SetKeyCode(int value) { keyCode = value; }
	bool handled;
	public bool GetHandled() { return handled; }
	public void SetHandled(bool value) { handled = value; }
	bool modifierCtrl;
	public bool GetCtrlPressed() { return modifierCtrl; }
	public void SetCtrlPressed(bool value) { modifierCtrl = value; }
	bool modifierShift;
	public bool GetShiftPressed() { return modifierShift; }
	public void SetShiftPressed(bool value) { modifierShift = value; }
	bool modifierAlt;
	public bool GetAltPressed() { return modifierAlt; }
	public void SetAltPressed(bool value) { modifierAlt = value; }
}

public class KeyPressEventArgs
{
	int keyChar;
	public int GetKeyChar() { return keyChar; }
	public void SetKeyChar(int value) { keyChar = value; }
	bool handled;
	public bool GetHandled() { return handled; }
	public void SetHandled(bool value) { handled = value; }
}

// this maps to the OpenTK.Windowing.GraphicsLibraryFramework.Keys enum
public class GlKeys
{
	public const int Unknown = -1;
	public const int LShift = 340;
	public const int ShiftLeft = 340;
	public const int RShift = 344;
	public const int ShiftRight = 344;
	public const int LControl = 341;
	public const int ControlLeft = 341;
	public const int RControl = 345;
	public const int ControlRight = 345;
	public const int AltLeft = 342;
	public const int LAlt = 342;
	public const int AltRight = 346;
	public const int RAlt = 346;
	public const int WinLeft = 348;
	public const int LWin = 348;
	public const int RWin = 348;
	public const int WinRight = 348;
	public const int Menu = 348;
	public const int F1 = 290;
	public const int F2 = 291;
	public const int F3 = 292;
	public const int F4 = 293;
	public const int F5 = 294;
	public const int F6 = 295;
	public const int F7 = 296;
	public const int F8 = 297;
	public const int F9 = 298;
	public const int F10 = 299;
	public const int F11 = 300;
	public const int F12 = 301;
	public const int F13 = 302;
	public const int F14 = 303;
	public const int F15 = 304;
	public const int F16 = 305;
	public const int F17 = 306;
	public const int F18 = 307;
	public const int F19 = 308;
	public const int F20 = 309;
	public const int F21 = 310;
	public const int F22 = 311;
	public const int F23 = 312;
	public const int F24 = 313;
	public const int F25 = 314;
	public const int Up = 265;
	public const int Down = 264;
	public const int Left = 263;
	public const int Right = 262;
	public const int Enter = 257;
	public const int Escape = 256;
	public const int Space = 32;
	public const int Tab = 258;
	public const int Back = 259;
	public const int BackSpace = 259;
	public const int Insert = 260;
	public const int Delete = 261;
	public const int PageUp = 266;
	public const int PageDown = 267;
	public const int Home = 268;
	public const int End = 269;
	public const int CapsLock = 280;
	public const int ScrollLock = 281;
	public const int PrintScreen = 283;
	public const int Pause = 284;
	public const int NumLock = 282;
	public const int Keypad0 = 320;
	public const int Keypad1 = 321;
	public const int Keypad2 = 322;
	public const int Keypad3 = 323;
	public const int Keypad4 = 324;
	public const int Keypad5 = 325;
	public const int Keypad6 = 326;
	public const int Keypad7 = 327;
	public const int Keypad8 = 328;
	public const int Keypad9 = 329;
	public const int KeypadDivide = 331;
	public const int KeypadMultiply = 332;
	public const int KeypadMinus = 333;
	public const int KeypadSubtract = 333;
	public const int KeypadAdd = 334;
	public const int KeypadPlus = 334;
	public const int KeypadDecimal = 330;
	public const int KeypadEnter = 335;
	public const int A = 65;
	public const int B = 66;
	public const int C = 67;
	public const int D = 68;
	public const int E = 69;
	public const int F = 70;
	public const int G = 71;
	public const int H = 72;
	public const int I = 73;
	public const int J = 74;
	public const int K = 75;
	public const int L = 76;
	public const int M = 77;
	public const int N = 78;
	public const int O = 79;
	public const int P = 80;
	public const int Q = 81;
	public const int R = 82;
	public const int S = 83;
	public const int T = 84;
	public const int U = 85;
	public const int V = 86;
	public const int W = 87;
	public const int X = 88;
	public const int Y = 89;
	public const int Z = 90;
	public const int Number0 = 48;
	public const int Number1 = 49;
	public const int Number2 = 50;
	public const int Number3 = 51;
	public const int Number4 = 52;
	public const int Number5 = 53;
	public const int Number6 = 54;
	public const int Number7 = 55;
	public const int Number8 = 56;
	public const int Number9 = 57;
	public const int Tilde = 96;
	public const int Minus = 45;
	public const int Plus = 43;
	public const int LBracket = 91;
	public const int BracketLeft = 91;
	public const int BracketRight = 93;
	public const int RBracket = 93;
	public const int Semicolon = 59;
	public const int Quote = 34;
	public const int Comma = 44;
	public const int Period = 46;
	public const int Slash = 47;
	public const int BackSlash = 92;
	public const int SuperLeft = 343;
	public const int SuperRight = 347;
	public const int LastKey = Menu;
}

public abstract class NewFrameHandler
{
	public abstract void OnNewFrame(NewFrameEventArgs args);
}

public abstract class ImageOnLoadHandler
{
	public abstract void OnLoad();
}

public abstract class KeyEventHandler
{
	public abstract void OnKeyDown(KeyEventArgs e);
	public abstract void OnKeyPress(KeyPressEventArgs e);
	public abstract void OnKeyUp(KeyEventArgs e);
}

public class MouseEventArgs
{
	int x;
	int y;
	int movementX;
	int movementY;
	int button;
	public int GetX() { return x; }
	public void SetX(int value) { x = value; }
	public int GetY() { return y; }
	public void SetY(int value) { y = value; }
	public int GetMovementX() { return movementX; }
	public void SetMovementX(int value) { movementX = value; }
	public int GetMovementY() { return movementY; }
	public void SetMovementY(int value) { movementY = value; }
	public int GetButton() { return button; }
	public void SetButton(int value) { button = value; }
	bool handled;
	public bool GetHandled() { return handled; }
	public void SetHandled(bool value) { handled = value; }
	bool forceUsage;
	public bool GetForceUsage() { return forceUsage; }
	public void SetForceUsage(bool value) { forceUsage = value; }
	bool emulated;
	public bool GetEmulated() { return emulated; }
	public void SetEmulated(bool value) { emulated = value; }
}

public class MouseWheelEventArgs
{
	int delta;
	float deltaPrecise;
	public int GetDelta() { return delta; }
	public void SetDelta(int value) { delta = value; }
	public float GetDeltaPrecise() { return deltaPrecise; }
	public void SetDeltaPrecise(float value) { deltaPrecise = value; }
}

public class MouseButtonEnum
{
	public const int Left = 0;
	public const int Middle = 2;
	public const int Right = 1;
}

public abstract class MouseEventHandler
{
	public abstract void OnMouseDown(MouseEventArgs e);
	public abstract void OnMouseUp(MouseEventArgs e);
	public abstract void OnMouseMove(MouseEventArgs e);
	public abstract void OnMouseWheel(MouseWheelEventArgs e);
}

public class TouchEventArgs
{
	int x;
	int y;
	int id;
	bool handled;
	public int GetX() { return x; }
	public void SetX(int value) { x = value; }
	public int GetY() { return y; }
	public void SetY(int value) { y = value; }
	public int GetId() { return id; }
	public void SetId(int value) { id = value; }
	public bool GetHandled() { return handled; }
	public void SetHandled(bool value) { handled = value; }
}

public abstract class TouchEventHandler
{
	public abstract void OnTouchStart(TouchEventArgs e);
	public abstract void OnTouchMove(TouchEventArgs e);
	public abstract void OnTouchEnd(TouchEventArgs e);
}

public class NewFrameEventArgs
{
	float dt;
	public float GetDt()
	{
		return dt;
	}
	public void SetDt(float p)
	{
		this.dt = p;
	}
}

// Empty value classes for use in JavaScript. Native implementation derives from this.
public class BitmapCi
{
	public virtual void Dispose() { }
}
public class GlProgram { }
public class GlShader { }

public enum TextAlign
{
	Left,
	Center,
	Right
}

public enum TextBaseline
{
	Top,
	Middle,
	Bottom
}

public class IntRef
{
	public static IntRef Create(int value_)
	{
		IntRef intref = new IntRef();
		intref.value = value_;
		return intref;
	}
	internal int value;
	public int GetValue() { return value; }
	public void SetValue(int value_) { value = value_; }
}

public class AudioSampleCi
{
}

public abstract class AudioData
{
}

public abstract class AudioCi
{
}

public abstract class Action_
{
	public abstract void Run();
}
