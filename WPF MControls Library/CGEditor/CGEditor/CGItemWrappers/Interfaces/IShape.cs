using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IShape
    {
        string Type { get; set; }
        int Sides { get; set; }
        double RoundCorners { get; set; }
        double Rotate { get; set; }
    }
}
