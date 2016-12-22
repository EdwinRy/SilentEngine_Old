using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Input;

namespace Silent.GameSystem
{
    public class Silent_Input
    {
        public Silent_Input()
        {

        }

        /*
        public enum Keys
        {
            A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,
            AltL,AltR,Back,BackSlash,BackSpace,BracketLeft,BracketRight,
            CapsLock,Clear,Comma,CtrlL,CtrlR,Delete,Down,End,Enter,Espace,
            F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,F12,Home,Insert,
            Keypad0,Keypad1,Keypad2,Keypad3,Keypad4,Keypad5,Keypad6,
            Keypad7,Keypad8,Keypad9,KeypadAdd,KeypadDecimal,KeypadDivide,
            KeypadEnter,KeypadMinus,KeypadMultiply,KeypadPeriod,KeypadPlus,
            KeypadSubtract,LShift,RShift,LWin,Left,Menu,Minus,
            Number0,Number1,Number2,Number3,Number4,Number5,Number6,Number7,Number8,Number9,
            NumLock,PageDown,PageUp,Pause,Period,Plus,PrintScreen,Quote,Right,RWin,
            ScrollLock,Semicolon,Slash,Sleep,Space,Tab,Tilde,Unknown,Up
        }
        */

        public enum Keys
        {
            Unknown = 0,ShiftLeft,
            LShift = ShiftLeft,ShiftRight,RShift = ShiftRight,ControlLeft,LControl = ControlLeft,
            ControlRight,RControl = ControlRight,AltLeft,LAlt = AltLeft,AltRight,RAlt = AltRight,
            WinLeft,LWin = WinLeft,WinRight,RWin = WinRight,Menu,F1,F2,F3,F4,F5,F6,F7,F8,F9,F10,F11,
            F12,F13,F14,F15,F16,F17,F18,F19,F20,F21,F22,F23,F24,F25,F26,F27,F28,F29,F30,F31,F32,F33,
            F34,F35,Up,Down,Left,Right,Enter,Escape,Space,Tab,BackSpace,Back = BackSpace,Insert,Delete,
            PageUp,PageDown,Home,End,CapsLock,ScrollLock,PrintScreen,Pause,NumLock,Clear,Sleep,Keypad0,
            Keypad1,Keypad2,Keypad3,Keypad4,Keypad5,Keypad6,Keypad7,Keypad8,Keypad9,KeypadDivide,KeypadMultiply,
            KeypadSubtract,KeypadMinus = KeypadSubtract,KeypadAdd,KeypadPlus = KeypadAdd,KeypadDecimal,KeypadEnter,
            A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z,
            Number0,Number1,Number2,Number3,Number4,Number5,Number6,Number7,Number8,Number9,
            Tilde,Minus,Plus,BracketLeft,LBracket = BracketLeft,BracketRight,RBracket = BracketRight,
            Semicolon,Quote,Comma,Period,Slash,BackSlash,LastKey
        }

        private static List<Keys> m_keysPressed = new List<Keys>();
        private static Dictionary<List<Keys>, Action> KeyBinds = new Dictionary<List<Keys>, Action>();

        //Keys previousKey;

        public void KeyDown(Key key)
        {
            if (!m_keysPressed.Contains((Keys)key)) { 
                m_keysPressed.Add((Keys)key);
            }
        }

        public void KeyUp(Key key)
        {
            m_keysPressed.Remove((Keys)key);
        }

        public void KeyPress(char keychar)
        {

        }

        public void Update()
        {
            foreach(List<Keys> keys in KeyBinds.Keys)
            {
                if(m_keysPressed.SequenceEqual(keys)) { KeyBinds[keys].Invoke(); }
            }           
        }

        public void Bind(List<Keys> keyboardState, Action ToRun)
        {
            KeyBinds.Add(keyboardState, ToRun);
        }        
    }
}