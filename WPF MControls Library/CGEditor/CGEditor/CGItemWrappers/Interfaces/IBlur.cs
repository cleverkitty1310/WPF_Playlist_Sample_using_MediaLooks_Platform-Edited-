using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IBlur
    {
        bool BlurEnable { get; set; }
        double BlurSizeX { get; set; }
        double BlurSizeY { get; set; }
        string BlurAlign { get; set; }
    }
}
