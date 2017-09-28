using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace nemonic
{
    //한글입력을 돕는 RichTextBox 클래스
    public class RichTextBoxIME : RichTextBox
    {

        public RichTextBoxIME()
        {
            //this.Handles.Add(this.Handle);
        }
        #region Consts
        private const int WM_USER = 0x0400;

        private const int WM_KEYDOWN = 256;
        private const int WM_IME_STARTCOMPOSITION = 269;
        private const int WM_IME_ENDCOMPOSITION = 270;
        private const int WM_IME_COMPOSITION = 271;

        private const int GCS_RESULTSTR = 2048;
        private const int GCS_COMPSTR = 8;

        private const int EM_GETTEXTEX = (WM_USER + 94);
        private const int EM_GETTEXTLENGTHEX = (WM_USER + 95);
        private const int GT_DEFAULT = 0;
        private const int GTL_CLOSE = 4;
        #endregion

        #region Structs
        [StructLayout(LayoutKind.Sequential)]
        private struct GETTEXTEX
        {
            public Int32 cb;
            public Int32 flags;
            public Int32 codepage;
            public IntPtr lpDefaultChar;
            public IntPtr lpUsedDefChar;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct GETTEXTLENGTHEX
        {
            public Int32 flags;
            public Int32 codepage;
        }
        #endregion

        #region DllImports
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, ref GETTEXTEX wParam, StringBuilder lParam);
        [DllImport("user32.dll", EntryPoint = "SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, ref GETTEXTLENGTHEX wParam, int lParam);

        [DllImport("imm32.dll")]
        private static extern IntPtr ImmGetContext(IntPtr hwnd);
        [DllImport("imm32.dll")]
        private static extern int ImmReleaseContext(IntPtr hwnd, IntPtr himc);
        [DllImport("imm32.dll")]
        private static extern int ImmGetCompositionString(IntPtr himc, int dw, StringBuilder lpv, int dw2);
        [DllImport("imm32.dll")]
        private static extern int ImmGetCompositionString(IntPtr himc, int dw, int lpv, int dw2);
        #endregion

        #region Enums
        private enum ImeState
        {
            /// <summary>
            /// 상태없음
            /// </summary>
            None,

            /// <summary>
            /// 조합중
            /// </summary>
            Compounding,

            /// <summary>
            /// 조합완료
            /// </summary>
            Complete,
        }
        #endregion

        private bool _BackSpaceKeyPressed = false;
        private ImeState _ImeState = ImeState.None;
        private string _ImeSaveString = string.Empty;

        #region WndProc (최적화 할것)

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_KEYDOWN)
            {
                if (m.LParam.ToInt32() == 917505)
                    _BackSpaceKeyPressed = true;
                else
                    _BackSpaceKeyPressed = false;
            }

            if (m.Msg == WM_IME_COMPOSITION || m.Msg == WM_IME_STARTCOMPOSITION || m.Msg == WM_IME_ENDCOMPOSITION)
                _ImeState = ImeState.None;

            if (m.Msg == WM_IME_ENDCOMPOSITION)
                OnTextChanged(EventArgs.Empty);

            if (m.Msg == WM_IME_COMPOSITION)
            {
                int comp = m.LParam.ToInt32();

                if ((comp & GCS_RESULTSTR) > 0)
                    _ImeState = ImeState.Complete;
                else if ((comp & GCS_COMPSTR) > 0)
                {
                    _ImeState = ImeState.Compounding;
                    OnTextChanged(EventArgs.Empty);
                }
            }

            base.WndProc(ref m);
        }

        #endregion

        #region Properties

        /// <summary>
        /// RichTextBoxIme의 현재 텍스트를 가져오거나 설정합니다.
        /// </summary>
        public override string Text
        {
            set
            {
                base.Text = value;
            }

            get
            {
                string thisText = string.Empty;
                #region  RichText 가져오기
                // 참조 : Paul Welter's Weblog
                GETTEXTLENGTHEX getLength = new GETTEXTLENGTHEX();
                getLength.flags = GTL_CLOSE;
                getLength.codepage = 1200;
                int textLength = SendMessage(this.Handle, EM_GETTEXTLENGTHEX, ref getLength, 0);

                GETTEXTEX getText = new GETTEXTEX();
                getText.cb = textLength + 2;
                getText.flags = GT_DEFAULT;
                getText.codepage = 1200;
                StringBuilder sb = new StringBuilder(getText.cb);
                SendMessage(this.Handle, EM_GETTEXTEX, ref getText, sb);

                thisText = sb.ToString();
                #endregion

                string imeText = string.Empty;
                #region IME 가져오기
                // 참조 : 여기저기
                int dwSize = 0;
                IntPtr intICHwnd = IntPtr.Zero;

                intICHwnd = ImmGetContext(this.Handle);
                dwSize = ImmGetCompositionString(intICHwnd, GCS_COMPSTR, 0, 0);
                if (dwSize != 0)
                {
                    StringBuilder tmp = new StringBuilder(dwSize);
                    dwSize = ImmGetCompositionString(intICHwnd, GCS_COMPSTR, tmp, dwSize);
                    imeText = tmp.ToString();
                }
                ImmReleaseContext(this.Handle, intICHwnd);

                if (imeText.Length > 0)
                    imeText = imeText[0].ToString();
                else
                    imeText = string.Empty;
                #endregion

                int currentPosition = this.SelectionStart;
                int thisTextLength = thisText.Length;

                bool textIsKoreanUnicode = false;
                bool imeIsKoreanUnicode = false;
                if (thisTextLength > 0)
                    textIsKoreanUnicode = IsKoreanUnicodeChar(thisText[thisTextLength - 1]);
                if (imeText.Length > 0)
                    imeIsKoreanUnicode = IsKoreanUnicodeChar(imeText[0]);

                string leftString = string.Empty;
                string rightString = string.Empty;
                #region 현재커서를 기준으로 우측과 좌측 문자열 분리 (최적화 할것)
                if (thisTextLength > 0)
                {
                    leftString = thisText.Substring(0, currentPosition);

                    if (_ImeState == ImeState.Compounding)
                    {
                        if (currentPosition < thisTextLength)
                        {
                            if (!imeIsKoreanUnicode)
                            {
                                if (!_BackSpaceKeyPressed)
                                    rightString = thisText.Substring(currentPosition, thisTextLength - currentPosition);
                                else
                                    rightString = thisText.Substring(currentPosition + 1, thisTextLength - currentPosition - 1);
                            }
                            else
                            {
                                rightString = thisText.Substring(currentPosition, thisTextLength - currentPosition - 1);
                                if (rightString.Length > 0 && imeText != rightString[0].ToString())
                                {
                                    if (_ImeSaveString.Length > 0)
                                        rightString = _ImeSaveString.Substring(currentPosition + 1, _ImeSaveString.Length - currentPosition - 1);
                                    else
                                        rightString = thisText.Substring(currentPosition + 1, thisTextLength - currentPosition - 1);
                                }
                            }
                        }
                    }
                    else
                    {
                        if (currentPosition < thisTextLength)
                            rightString = thisText.Substring(currentPosition, thisTextLength - currentPosition);
                    }
                }
                #endregion

                string rtn = string.Empty;
                #region 문자열 조합 (최적화 할것)
                if (rightString.Trim().Length == 0)
                {
                    // 일반입력
                    if (!textIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Compounding && textIsKoreanUnicode && imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Complete && textIsKoreanUnicode && imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Compounding && !textIsKoreanUnicode && imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Compounding && textIsKoreanUnicode && !imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else
                        rtn = leftString + imeText + rightString;
                }
                else
                {
                    // 끼워넣기
                    if (!textIsKoreanUnicode && leftString == string.Empty && rightString == string.Empty)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Complete && textIsKoreanUnicode && imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Compounding && !textIsKoreanUnicode && imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Compounding && textIsKoreanUnicode && !imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else if (_ImeState == ImeState.Compounding && textIsKoreanUnicode && imeIsKoreanUnicode)
                        rtn = leftString + imeText + rightString;
                    else
                        rtn = leftString + imeText + rightString;
                }
                #endregion

                if (_ImeState == ImeState.Complete)
                    _ImeSaveString = rtn;
                else
                    _ImeSaveString = string.Empty;

                return rtn;
            }
        }

        #endregion

        #region Helpers

        public bool IsKoreanUnicodeChar(char korChar)
        {
            int code = Convert.ToUInt16(korChar);

            // 완성형 한글 유니코드 범위 (3.0 버전)
            if (code >= 44032 && code <= 55203)
                return true;

            return false;
        }

        #endregion
    }
}