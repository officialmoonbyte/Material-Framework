using System;
using System.Windows.Forms;

namespace Moonbyte.MaterialFramework.Events
{
    public class GenericNewTabButtonClickArgs : EventArgs
    {
        public TabPage NewTabpage { get; set; } 
    }
}
