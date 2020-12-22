using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZK_Lymytz.TOOLS
{
    class MyTextBox : MyComponent
    {
        public MyTextBox(TextBox component) : base(component) { }

        public delegate void delegateText(string value);
        public void Text(string value)
        {
            if (component != null)
            {
                if (((TextBox)component).InvokeRequired)
                {
                    delegateText deleg = new delegateText(Text);
                    ((TextBox)component).Invoke(deleg, new object[] { value });
                }
                else
                {
                    ((TextBox)component).Text = value;
                }
            }
        }
    }
}
