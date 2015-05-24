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
        private BlockPage blockpage;
        private BlockNet blockNet;
        private Unauthorized unauth;
        private Logs logs;
        public Form1()
        {
            
            InitializeComponent();
            logs = new Logs(this.listView1);

            
        }

        private void Form1_FormClosing_1(object sender, FormClosingEventArgs e)
        {
            FiddlerApplication.Shutdown();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                this.blockNet = new BlockNet(this.logs);
                this.blockNet.start();
            }
            else if (radioButton2.Checked == true)
            {
                this.blockpage = new BlockPage(this.logs, this.textBox1.Text, this.textBox2.Text);
                this.blockpage.start();
                textBoxEnabling(false);
            }
            else if (radioButton3.Checked == true)
            {
                this.unauth = new Unauthorized(this.logs);
                this.unauth.start();
            }
            else {
                this.logs.addLog("Select radio button and try again");
            }
            

            
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            
            radioButtonsEnabling(false);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                this.blockNet.stop();
                
            }
            else if (radioButton2.Checked == true)
            {
                this.blockpage.stop();
                
            }
            else
            {
                this.unauth.stop();
            }

            radioButtonsEnabling(true);
            this.button2.Enabled = false;
            this.button1.Enabled = true;
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
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
            this.radioButton1.Enabled = var;
            this.radioButton2.Enabled = var;
            this.radioButton3.Enabled = var;
        }
    }
}
