using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CGEditor.Misc
{
    static class Helpers
    {
        public static Color ParseCGColor(string strCGColor)
        {
            Color res = Colors.Transparent;
            if (strCGColor != null && strCGColor != string.Empty)
            {
                string strColor = strCGColor.Trim();
                string strAlpha = "";
                int nAlpha = 0;

                if (strCGColor.Contains('('))
                {
                    string[] split = strCGColor.Split('(');
                    strColor = split[0];
                    strAlpha = split[1].Replace(")", "");
                }

                if (strColor.ToLower() == "ml")
                {
                    res = new Color();
                    res.A = 0;
                    res.R = 0;
                    res.G = 88;
                    res.B = 143;
                }
                else
                    res = (Color)System.Windows.Media.ColorConverter.ConvertFromString(strColor);
                if (strAlpha != String.Empty)
                {
                    bool bSucceded = Int32.TryParse(strAlpha, out nAlpha);
                    if (bSucceded)
                        res.A = (Byte)nAlpha;
                }
            }
            return res;
        }

        public static Color[] parseCGradient(string strCGGradient, out bool isVertical)
        {
            Color[] res = null;
            isVertical = false;
            if (strCGGradient != null && strCGGradient != string.Empty)
            {
                if (strCGGradient.Contains("/"))
                    isVertical = true;

                string[] strGradients = strCGGradient.Split('-');

                if (strGradients.Length < 2)
                    strGradients = strCGGradient.Split('+');

                if (strGradients.Length < 2)
                    strGradients = strCGGradient.Split('*');

                if (strGradients.Length < 2)
                    strGradients = strCGGradient.Split('/');

                res = new Color[strGradients.Length];
                for (int i = 0; i < strGradients.Length; i++)
                {
                    res[i] = ParseCGColor(strGradients[i]);
                }
            }
            return res;
        }

        public static bool isStringTrue(string strTarget)
        {
            if (strTarget.ToLower() == "true" || strTarget.ToLower() == "yes" || strTarget.ToLower() == "1")
                return true;
            else
                return false;
        }
    }
}
