using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace ZK_Lymytz.TOOLS
{
    class MyCheckBox : MyComponent
    {
        public MyCheckBox(CheckBox component) : base(component) { }

        public delegate void delegateChecked(bool value);
        public void Checked(bool value)
        {
            if (component != null)
            {
                if (((CheckBox)component).InvokeRequired)
                {
                    delegateChecked deleg = new delegateChecked(Checked);
                    ((CheckBox)component).Invoke(deleg, new object[] { value });
                }
                else
                {
                    ((CheckBox)component).Checked = value;
                }
            }
        }
    }
}
