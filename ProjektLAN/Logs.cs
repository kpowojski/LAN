using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace ProjektLAN
{
    class Logs
    {
        private ListView listView;
        public Logs(ListView listView)
        {
            this.listView = listView;
            
        }

        public bool addLog(String text)
        {
            if (text == null || text.Equals(""))
                return false;

            ListViewItem newItem = new ListViewItem(this.listView.Items.Count.ToString());
            newItem.SubItems.Add(text);

            this.listView.Invoke(new MethodInvoker(delegate()
                {
                    this.listView.Items.Add(newItem);
                    this.listView.Items[listView.Items.Count - 1].EnsureVisible();

                }));
            return true;
        }


        public bool addLog(String type, String url, String color)
        {
            ListViewItem newItem = new ListViewItem(this.listView.Items.Count.ToString());
            newItem.SubItems.Add(type);
            newItem.SubItems.Add(url);
            ListViewItem item = setItemColor(newItem, color);

            this.listView.Invoke(new MethodInvoker(delegate()
                {
                    this.listView.Items.Add(item);
                    this.listView.Items[listView.Items.Count - 1].EnsureVisible();
                }));
            return true;
        }


        private ListViewItem setItemColor(ListViewItem item, String color)
        {
            if (color.Equals("red") || color.Equals("Red") || color.Equals("RED"))
            {
                item.ForeColor = Color.Red;
                return item;
            }
            else if (color.Equals("green") || color.Equals("Green") || color.Equals("GREEN"))
            {
                item.ForeColor = Color.Green;
                return item;
            }

            else
            {
                item.ForeColor = Color.Black;
                return item;
            }

        }


    }
}
