using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProjektLAN
{
    public partial class Form1 : Form
    {
        private Capture capture;
        private Logs logs;
        public Form1()
        {
            
            InitializeComponent();
            logs = new Logs(this.listView1);
            capture = new Capture(this.logs);
            
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            Fiddler.FiddlerApplication.Shutdown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.capture.start();
            this.button1.Enabled = false;
            this.button2.Enabled = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.capture.stop();
            this.button2.Enabled = false;
            this.button1.Enabled = true;
        }
    }
}
