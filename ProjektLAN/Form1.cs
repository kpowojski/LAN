using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using Org.BouncyCastle.X509;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using Fiddler;

namespace ProjektLAN
{
    public partial class Form1 : Form
    {
        private Proxy proxy;
        private Logs logs;
        public Form1()
        {
            
            InitializeComponent();
            logs = new Logs(this.listView1);
            proxy = new Proxy(this.logs);
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            FiddlerApplication.Shutdown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton0.Checked == true)
            {
                this.proxy.options = 0;
                this.proxy.start();
            }
            else if (radioButton1.Checked == true)
            {
                //block access to net
                this.proxy.options = 1;
                this.proxy.start();
            }
            else if (radioButton2.Checked == true)
            {
                //redirect from one page to another one 
                this.proxy.options = 2;
                this.proxy.blockedPage = textBox1.Text;
                this.proxy.redirectPage = textBox2.Text;
                this.proxy.start();
                textBoxEnabling(false);
            }
            else if (radioButton3.Checked == true)
            {
                //404 HTTP error
                this.proxy.options = 3;
                this.proxy.start();
            }
            else
            {
                this.logs.addLog("Select radio button and try again");
            }
            
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            radioButtonsEnabling(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.proxy.stop();
            
            radioButtonsEnabling(true);
            this.button2.Enabled = false;
            this.button1.Enabled = true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void radioButton0_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEnabling(false);
            this.button1.Enabled = true;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEnabling(false);
            this.button1.Enabled = true;

        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEnabling(true);
            this.button1.Enabled = true;
        }



        private void textBoxEnabling(bool var)
        {
            this.textBox1.Enabled = var;
            this.textBox2.Enabled = var;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            textBoxEnabling(false);
            this.button1.Enabled = true;
        }

        private void radioButtonsEnabling(bool var)
        {
            this.radioButton0.Enabled = var;
            this.radioButton1.Enabled = var;
            this.radioButton2.Enabled = var;
            this.radioButton3.Enabled = var;
        }

    }
}
