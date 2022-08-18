using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IPosition
    {
        double PosX { get; set; }
        double PosY { get; set; }
        double Width { get; set; }
        double Height { get; set; }
    }
}
