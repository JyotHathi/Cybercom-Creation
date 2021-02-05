using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Threading.Tasks;

namespace Digital_Clock
{
    public partial class Form1 : Form
    {
        static int total = 0;
        static object obj = new object();
        public Form1()
        {
            InitializeComponent();

            label1.Text = DateTime.Now.ToLocalTime().ToLongTimeString();
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Start();
            timer.Interval = 1000;
            timer.Enabled = true;
            timer.AutoReset = true;
            timer.Elapsed += TimeTick;

        }
        private void TimeTick(object e, EventArgs args)
        {
            Action action = () => label1.Text = DateTime.Now.ToLocalTime().ToLongTimeString();
            BeginInvoke(action);
        }

        private /*async*/ void button1_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(
                () =>
                {
                    AddNumber();
                    Action action = () => label2.Text = total.ToString();
                    BeginInvoke(action);
                }
                    );
            //Task thread = new Task(AddNumber);
            thread.Start();
            //await thread;

        }
        public void AddNumber()
        {
            for (int i = 0; i < 100000000; i++)
            {
                lock (obj)
                {
                    total++;
                }
            }
            
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private /*async*/ void button2_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(
                () =>
                {
                    AddNumber();
                    Action action = () => label3.Text = total.ToString();
                    BeginInvoke(action);
                }
                    );
            //Task thread = new Task(AddNumber);
            thread.Start();
            //await thread;

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
