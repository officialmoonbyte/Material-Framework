using System;
using System.Windows.Forms;

namespace IndieGoat.MaterialFramework.Events
{
    public class GenericNewTabButtonClickArgs : EventArgs
    {
        public TabPage NewTabpage { get; set; }
    }
}
