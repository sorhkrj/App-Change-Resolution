using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace App_Change_Resolution
{
    internal enum SHOW_WINDOW_COMMANDS : int
    {
        HIDE = 0,
        NORMAL = 1,
        MINIMIZED = 2,
        MAXIMIZED = 3,
    }

    internal struct WINDOWPLACEMENT
    {
        public int lengh;
        public int flags;
        public SHOW_WINDOW_COMMANDS show_cmd;
        public Point min_position;
        public Point max_postion;
        public Rectangle normal_position;
    }

    public partial class Form1 : Form
    {
        [DllImport("user32.dll")]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowPlacement(IntPtr handle, ref WINDOWPLACEMENT placement);

        [DllImport("user32.dll")]
        internal static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInserAfter, int x, int y, int width, int height, int wFlags);

        private const int SWP_NOZORDER = 0x04;

        private Stopwatch stopwatch = new Stopwatch();

        private int x, y = 0;
        private int width, height = 0;

        private int Fx, Fy;

        private int Fwidth, Fheight;

        private void Form1_Load(object sender, EventArgs e)
        {
            GetAllPrc();
            stopwatch.Reset();
            stopwatch.Start();
            timer1.Start();
            checkBox1.Checked = false;
            checkBox2.Checked = true;
            textBox1.Enabled = false;
            textBox2.Enabled = false;
        }
        

        private void comboBox1_MouseDown(object sender, MouseEventArgs e)
        {
            GetAllPrc();
        }

        private void comboBox1_MouseHover(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(comboBox1, comboBox1.Text);
        }

        private void comboBox1_MouseWheel(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            textBox3.Text = "0";
            textBox4.Text = "0";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            TimeSpan elapsed = stopwatch.Elapsed;
            Point point = new Point();
            Size size = new Size();
            if (button1.Enabled == true)
            {
                GetWindowPos(findwindow(), ref point, ref size);
            }
            if (checkBox1.Checked == false)
            {
                textBox1.Text = point.X.ToString();
                textBox2.Text = point.Y.ToString();
            }
            if (checkBox2.Checked == false)
            {
                textBox3.Text = size.Width.ToString();
                textBox4.Text = size.Height.ToString();
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            }
            else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                if (button1.Enabled == true)
                {
                    Point point = new Point();
                    Size size = new Size();
                    GetWindowPos(findwindow(), ref point, ref size);
                    textBox1.Text = point.X.ToString();
                    textBox2.Text = point.Y.ToString();
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                textBox3.Enabled = true;
                textBox4.Enabled = true;
            }
            else
            {
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                if (button1.Enabled == true)
                {
                    Point point = new Point();
                    Size size = new Size();
                    GetWindowPos(findwindow(), ref point, ref size);
                    textBox3.Text = size.Width.ToString();
                    textBox4.Text = size.Height.ToString();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button2.Enabled = true;
            x = Fx;
            y = Fy;
            width = Fwidth;
            height = Fheight;
            if (textBox1.Text == "" || textBox1.Text == "-")
            {
                textBox1.Text = "0";
            }
            if (textBox2.Text == "" || textBox2.Text == "-")
            {
                textBox2.Text = "0";
            }
            if (textBox3.Text == "" || textBox3.Text == "-")
            {
                textBox3.Text = "0";
            }
            if (int.Parse(textBox3.Text) < 0)
            {
                textBox3.Text = "0";
            }
            if (textBox4.Text == "" || textBox4.Text == "-")
            {
                textBox4.Text = "0";
            }
            if (int.Parse(textBox4.Text) < 0)
            {
                textBox4.Text = "0";
            }
            Fx = int.Parse(textBox1.Text);
            Fy = int.Parse(textBox2.Text);
            Fwidth = int.Parse(textBox3.Text);
            Fheight = int.Parse(textBox4.Text);
            textBox1.Text = Fx.ToString();
            textBox2.Text = Fy.ToString();
            textBox3.Text = Fwidth.ToString();
            textBox4.Text = Fheight.ToString();
            SetWindowPos(findwindow(), -1, Fx, Fy, Fwidth, Fheight, SWP_NOZORDER);
            Point point = new Point();
            Size size = new Size();
            GetWindowPos(findwindow(), ref point, ref size);
            textBox1.Text = point.X.ToString();
            textBox2.Text = point.Y.ToString();
            textBox3.Text = size.Width.ToString();
            textBox4.Text = size.Height.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            Fx = x;
            Fy = y;
            Fwidth = width;
            Fheight = height;
            textBox1.Text = x.ToString();
            textBox2.Text = y.ToString();
            textBox3.Text = width.ToString();
            textBox4.Text = height.ToString();
            SetWindowPos(findwindow(), -1, x, y, width, height, SWP_NOZORDER);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                button1.Enabled = true;
                Point point = new Point();
                Size size = new Size();
                GetWindowPos(findwindow(), ref point, ref size);
                x = point.X;
                y = point.Y;
                width = size.Width;
                height = size.Height;
                Fx = x;
                Fy = y;
                Fwidth = width;
                Fheight = height;
                textBox1.Text = x.ToString();
                textBox2.Text = y.ToString();
                textBox3.Text = width.ToString();
                textBox4.Text = height.ToString();
                SetWindowPos(findwindow(), -1, x, y, width, height, SWP_NOZORDER);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            int i;
            if(int.TryParse(textBox1.Text.Replace(",",""),out i))
            {
                textBox1.Text = i.ToString();
            }
            else if (textBox1.Text == "-")
            {
                textBox1.Text = "-";
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(textBox1.Text.Replace(",", ""), out i))
            {
                textBox1.Text = i.ToString();
            }
            else if (textBox1.Text == "-")
            {
                textBox1.Text = "-";
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(textBox1.Text.Replace(",", ""), out i))
            {
                textBox1.Text = i.ToString();
            }
            else if (textBox1.Text == "-")
            {
                textBox1.Text = "-";
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            int i;
            if (int.TryParse(textBox1.Text.Replace(",", ""), out i))
            {
                textBox1.Text = i.ToString();
            }
            else if(textBox1.Text == "-")
            {
                textBox1.Text = "-";
            }
            else
            {
                textBox1.Text = "0";
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private IntPtr findwindow()
        {
            return FindWindow(null, comboBox1.SelectedItem.ToString());
        }

        private void GetWindowPos(IntPtr hwnd, ref Point point, ref Size size)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.lengh = Marshal.SizeOf(placement);
            GetWindowPlacement(hwnd, ref placement);
            size = new Size(placement.normal_position.Right - (placement.normal_position.Left * 2), placement.normal_position.Bottom - (placement.normal_position.Top * 2));
            point = new Point(placement.normal_position.Left, placement.normal_position.Top);
        }

        private void GetAllPrc()
        {
            button1.Enabled = false;
            button2.Enabled = false;
            comboBox1.Items.Clear();
            Process[] allPrc = Process.GetProcesses();
            foreach (Process p in allPrc)
            {
                if (p.MainWindowTitle == "")
                {
                    continue;
                }
                comboBox1.Items.Add(p.MainWindowTitle);
            }
            textBox3.Text = "0";
            textBox4.Text = "0";
        }
    }
}
