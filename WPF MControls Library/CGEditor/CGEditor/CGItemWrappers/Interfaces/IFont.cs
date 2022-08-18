using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor.CGItemWrappers
{
    public interface IFont
    {
        string FontFamily { get; set; }
        string FontTypeface { get; set; }
        double FontSize { get; set; }
        bool Underline { get; set; }
        bool StrikeOut { get; set; }
        bool WordWrap { get; set; }
        bool RightToLeft { get; set; }
        bool Vertical { get; set; }
        bool NoTabs { get; set; }
        string Flip { get; set; }
        string Type { get; set; }
    }
}
