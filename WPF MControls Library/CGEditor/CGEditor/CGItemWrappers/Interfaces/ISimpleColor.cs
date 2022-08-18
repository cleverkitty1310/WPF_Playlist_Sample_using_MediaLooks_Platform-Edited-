using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface ISimpleColor
    {
        string BGColor { get; set; }
        int Alpha { get; set; }
    }
}
