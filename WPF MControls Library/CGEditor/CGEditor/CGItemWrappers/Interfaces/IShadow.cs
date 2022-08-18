using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IShadow
    {
        bool ShadowEnable { get; set; }
        double ShadowBlur { get; set; }
        double ShadowAlpha { get; set; }
        string ShadowColor { get; set; }
        double ShadowXOffset { get; set; }
        double ShadowYOffset { get; set; }
    }
}
