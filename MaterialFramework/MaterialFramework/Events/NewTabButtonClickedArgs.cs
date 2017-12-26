using IndieGoat.MaterialFramework.Controls;
using System;

namespace IndieGoat.MaterialFramework.Events
{
    public class NewTabButtonClickedArgs : EventArgs
    {
        public MaterialTabPage NewTabpage { get; set; }
    }
}
