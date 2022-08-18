using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IMisc
    {
        double PixelAR { get; set; }
        double EdgesSmooth { get; set; }
        string PlayMode { get; set; }
        string Scale { get; set; }
        string Align { get; set; }
        string Interlace { get; set; }
    }
}
