using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IOutline
    {
        string OutlineColor { get; set; }
        double OutlineWidth { get; set; }
    }

    public interface ITextOutline : IOutline
    {
        string TextOutlineColor { get; set; }
        double TextOutlineWidth { get; set; }
    }
}
