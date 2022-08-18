using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface ITicker
    {
        double Speed { get; set; }
        bool Gapless { get; set; }
        double LineHeight { get; set; }
        double Distance { get; set; }
    }
}
