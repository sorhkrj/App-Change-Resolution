using System;
using System.Windows.Forms;

namespace App_Change_Resolution
{
    static class Program
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
            string App = "App Change Resolution";
            IntPtr thiswindow = FindWindow(null, App);
            if (thiswindow != IntPtr.Zero)
            {
                MessageBox.Show("이미실행중입니다.", App, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
