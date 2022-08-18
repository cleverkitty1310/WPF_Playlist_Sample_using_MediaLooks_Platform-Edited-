using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IGlow
    {
        bool GlowEnable { get; set; }
        double GlowSizeX { get; set; }
        double GlowSizeY { get; set; }
        double GlowValue { get; set; }
    }
}
