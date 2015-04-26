using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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


    }
}
