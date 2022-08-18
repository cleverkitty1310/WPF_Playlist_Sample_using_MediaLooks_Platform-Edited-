using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IGradient
    {
        string Color {get; set;}
        double ColorAngle { get; set; }
    }

    public interface ITextGradient : IGradient
    {
        string TextColor { get; set; }
        double TextColorAngle { get; set; }
    }
}
