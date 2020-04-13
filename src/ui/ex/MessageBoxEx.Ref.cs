using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Utils {

    // https://www.cnblogs.com/code1992/p/9719856.html

    public class MessageBoxEx {
        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons, string[] buttonTitles = null) {
            MessageForm frm = new MessageForm(buttons, buttonTitles);
            frm.Show();
            frm.WatchForActivate = true;
            DialogResult result = MessageBoxEx.Show(frm, text, caption, buttons);
            frm.Close();

            return result;
        }

        public static DialogResult Show(string text, string caption, MessageBoxButtons buttons,
            MessageBoxIcon icon, MessageBoxDefaultButton defaultButton, string[] buttonTitles = null) {
            MessageForm frm = new MessageForm(buttons, buttonTitles);
            frm.WatchForActivate = true;
            DialogResult result = MessageBoxEx.Show(frm, text, caption, buttons, icon, defaultButton);
            frm.Close();

            return result;
        }

        class MessageForm : Form {
            IntPtr _handle;
            MessageBoxButtons _buttons;
            string[] _buttonTitles = null;

            bool _watchForActivate = false;

            public bool WatchForActivate {
                get { return _watchForActivate; }
                set { _watchForActivate = value; }
            }

            public MessageForm(MessageBoxButtons buttons, string[] buttonTitles) {
                _buttons = buttons;
                _buttonTitles = buttonTitles;

                this.Text = "";
                this.StartPosition = FormStartPosition.CenterScreen;
                this.Location = new Point(-32000, -32000);
                this.ShowInTaskbar = false;
            }

            protected override void OnShown(EventArgs e) {
                base.OnShown(e);
                NativeWin32API.SetWindowPos(this.Handle, IntPtr.Zero, 0, 0, 0, 0, 659);
            }

            protected override void WndProc(ref System.Windows.Forms.Message m) {
                if (_watchForActivate && m.Msg == 0x0006) {
                    _watchForActivate = false;
                    _handle = m.LParam;
                    CheckMsgbox();
                }
                base.WndProc(ref m);
            }

            private void CheckMsgbox() {
                if (_buttonTitles == null || _buttonTitles.Length == 0)
                    return;

                int buttonTitleIndex = 0;
                IntPtr h = NativeWin32API.GetWindow(_handle, GW_CHILD);

                while (h != IntPtr.Zero) {
                    if (NativeWin32API.GetWindowClassName(h).Equals("Button")) {
                        if (_buttonTitles.Length > buttonTitleIndex) {
                            NativeWin32API.SetWindowText(h, _buttonTitles[buttonTitleIndex]);

                            buttonTitleIndex++;
                        }
                    }

                    h = NativeWin32API.GetWindow(h, GW_HWNDNEXT);
                }
            }
        }

        public const int GW_CHILD = 5;
        public const int GW_HWNDNEXT = 2;

        public class NativeWin32API {
            [DllImport("user32.dll", CharSet = CharSet.Auto)]
            public static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int Width, int Height, int flags);
            [DllImport("user32.dll")]
            public static extern IntPtr GetWindow(IntPtr hWnd, Int32 wCmd);
            [DllImport("user32.dll")]
            public static extern bool SetWindowText(IntPtr hWnd, string lpString);
            [DllImport("user32.dll")]
            public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)]StringBuilder lpString, int nMaxCount);

            public static string GetWindowClassName(IntPtr handle) {
                StringBuilder sb = new StringBuilder(256);

                GetClassNameW(handle, sb, sb.Capacity);
                return sb.ToString();
            }
        }
    }
}
