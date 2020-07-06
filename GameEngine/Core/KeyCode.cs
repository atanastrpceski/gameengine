namespace GameEngine.Core
{
    //
    // Summary:
    // The available keyboard keys.
    public static class KeyCode
    {
        // A key outside the known keys.
        public static int Unknown = 0;
        /// The left shift key.
        public static int ShiftLeft = 1;
        /// The left shift key (equivalent to ShiftLeft).
        public static int LShift = 1;
        /// The right shift key.
        public static int ShiftRight = 2;
        /// The right shift key (equivalent to ShiftRight).
        public static int RShift = 2;
        /// The left control key.
        public static int ControlLeft = 3;
        /// The left control key (equivalent to ControlLeft).
        public static int LControl = 3;
        /// The right control key.
        public static int ControlRight = 4;
        /// The right control key (equivalent to ControlRight).
        public static int RControl = 4;
        /// The left alt key.
        public static int AltLeft = 5;
        /// The left alt key (equivalent to AltLeft.
        public static int LAlt = 5;
        /// The right alt key.
        public static int AltRight = 6;
        /// The right alt key (equivalent to AltRight).
        public static int RAlt = 6;
        /// The left win key.
        public static int WinLeft = 7;
        /// The left win key (equivalent to WinLeft).
        public static int LWin = 7;
        /// The right win key.
        public static int WinRight = 8;
        /// The right win key (equivalent to WinRight).
        public static int RWin = 8;
        /// The menu key.
        public static int Menu = 9;
        /// The F1 key.
        public static int F1 = 10;
        /// The F2 key.
        public static int F2 = 11;
        /// The F3 key.
        public static int F3 = 12;
        /// The F4 key.
        public static int F4 = 13;
        /// The F5 key.
        public static int F5 = 14;
        /// The F6 key.
        public static int F6 = 15;
        /// The F7 key.
        public static int F7 = 16;
        /// The F8 key.
        public static int F8 = 17;
        /// The F9 key.
        public static int F9 = 18;
        /// The F10 key.
        public static int F10 = 19;
        /// The F11 key.
        public static int F11 = 20;
        /// The F12 key.
        public static int F12 = 21;
        /// The F13 key.
        public static int F13 = 22;
        /// The F14 key.
        public static int F14 = 23;
        /// The F15 key.
        public static int F15 = 24;
        /// The F16 key.
        public static int F16 = 25;
        /// The F17 key.
        public static int F17 = 26;
        /// The F18 key.
        public static int F18 = 27;
        /// The F19 key.
        public static int F19 = 28;
        /// The F20 key.
        public static int F20 = 29;
        /// The F21 key.
        public static int F21 = 30;
        /// The F22 key.
        public static int F22 = 31;
        /// The F23 key.
        public static int F23 = 32;
        /// The F24 key.
        public static int F24 = 33;
        /// The F25 key.
        public static int F25 = 34;
        /// The F26 key.
        public static int F26 = 35;
        /// The F27 key.
        public static int F27 = 36;
        /// The F28 key.
        public static int F28 = 37;
        /// The F29 key.
        public static int F29 = 38;
        /// The F30 key.
        public static int F30 = 39;
        /// The F31 key.
        public static int F31 = 40;
        /// The F32 key.
        public static int F32 = 41;
        /// The F33 key.
        public static int F33 = 42;
        /// The F34 key.
        public static int F34 = 43;
        /// The F35 key.
        public static int F35 = 44;
        /// The up arrow key.
        public static int Up = 45;
        /// The down arrow key.
        public static int Down = 46;
        /// The left arrow key.
        public static int Left = 47;
        /// The right arrow key.
        public static int Right = 48;
        /// The enter key.
        public static int Enter = 49;
        /// The escape key.
        public static int Escape = 50;
        /// The space key.
        public static int Space = 51;
        /// The tab key.
        public static int Tab = 52;
        /// The backspace key.
        public static int BackSpace = 53;
        /// The backspace key (equivalent to BackSpace).
        public static int Back = 53;
        /// The insert key.
        public static int Insert = 54;
        // The delete key.
        public static int Delete = 55;
        // The page up key.
        public static int PageUp = 56;
        // The page down key.
        public static int PageDown = 57;
        // The home key.
        public static int Home = 58;
        // The end key.
        public static int End = 59;
        // The caps lock key.
        public static int CapsLock = 60;
        // The scroll lock key.
        public static int ScrollLock = 61;
        // The print screen key.
        public static int PrintScreen = 62;
        // The pause key.
        public static int Pause = 63;
        // The num lock key.
        public static int NumLock = 64;
        // The clear key (Keypad5 with NumLock disabled; on typical keyboards).
        public static int Clear = 65;
        // The sleep key.
        public static int Sleep = 66;
        // The keypad 0 key.
        public static int Keypad0 = 67;
        // The keypad 1 key.
        public static int Keypad1 = 68;
        // The keypad 2 key.
        public static int Keypad2 = 69;
        // The keypad 3 key.
        public static int Keypad3 = 70;
        /// The keypad 4 key.
        public static int Keypad4 = 71;
        /// The keypad 5 key.
        public static int Keypad5 = 72;
        /// The keypad 6 key.
        public static int Keypad6 = 73;
        /// The keypad 7 key.
        public static int Keypad7 = 74;
        /// The keypad 8 key.
        public static int Keypad8 = 75;
        /// The keypad 9 key.
        public static int Keypad9 = 76;
        /// The keypad divide key.
        public static int KeypadDivide = 77;
        /// The keypad multiply key.
        public static int KeypadMultiply = 78;
        /// The keypad subtract key.
        public static int KeypadSubtract = 79;
        /// The keypad minus key (equivalent to KeypadSubtract).
        public static int KeypadMinus = 79;
        /// The keypad add key.
        public static int KeypadAdd = 80;
        // The keypad plus key (equivalent to KeypadAdd).
        public static int KeypadPlus = 80;
        // The keypad decimal key.
        public static int KeypadDecimal = 81;
        // The keypad period key (equivalent to KeypadDecimal).
        public static int KeypadPeriod = 81;
        // The keypad enter key.
        public static int KeypadEnter = 82;
        // The A key.
        public static int A = 83;
        // The B key.
        public static int B = 84;
        // The C key.
        public static int C = 85;
        // The D key.
        public static int D = 86;
        // The E key.
        public static int E = 87;
        // The F key.
        public static int F = 88;
        // The G key.
        public static int G = 89;
        // The H key.
        public static int H = 90;
        // The I key.
        public static int I = 91;
        // The J key.
        public static int J = 92;
        // The K key.
        public static int K = 93;
        // The L key.
        public static int L = 94;
        // The M key.
        public static int M = 95;
        // The N key.
        public static int N = 96;
        // The O key.
        public static int O = 97;
        // The P key.
        public static int P = 98;
        // The Q key.
        public static int Q = 99;
        // The R key.
        public static int R = 100;
        // The S key.
        public static int S = 101;
        // The T key.
        public static int T = 102;
        // The U key.
        public static int U = 103;
        // The V key.
        public static int V = 104;
        // The W key.
        public static int W = 105;
        // The X key.
        public static int X = 106;
        // The Y key.
        public static int Y = 107;
        // The Z key.
        public static int Z = 108;
        // The number 0 key.
        public static int Number0 = 109;
        // The number 1 key.
        public static int Number1 = 110;
        // The number 2 key.
        public static int Number2 = 111;
        // The number 3 key.
        public static int Number3 = 112;
        // The number 4 key.
        public static int Number4 = 113;
        // The number 5 key.
        public static int Number5 = 114;
        // The number 6 key.
        public static int Number6 = 115;
        // The number 7 key.
        public static int Number7 = 116;
        // The number 8 key.
        public static int Number8 = 117;
        // The number 9 key.
        public static int Number9 = 118;
        // The tilde key.
        public static int Tilde = 119;
        //s     The grave key (equivaent to Tilde).
        public static int Grave = 119;
        // The minus key.
        public static int Minus = 120;
        // The plus key.
        public static int Plus = 121;
        // The left bracket key.
        public static int BracketLeft = 122;
        // The left bracket key (equivalent to BracketLeft).
        public static int LBracket = 122;
        // The right bracket key.
        public static int BracketRight = 123;
        // The right bracket key (equivalent to BracketRight).
        public static int RBracket = 123;
        // The semicolon key.
        public static int Semicolon = 124;
        // The quote key.
        public static int Quote = 125;
        // The comma key.
        public static int Comma = 126;
        // The period key.
        public static int Period = 127;
        // The slash key.
        public static int Slash = 128;
        // The backslash key.
        public static int BackSlash = 129;
        // The secondary backslash key.
        public static int NonUSBackSlash = 130;
        // Indicates the last available keyboard key.
        public static int LastKey = 131;
    }
}
