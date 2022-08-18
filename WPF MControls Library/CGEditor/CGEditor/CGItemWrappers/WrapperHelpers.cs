using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace CGEditor.CGItemWrappers
{
    public class WrapperHelpers
    {
        public static Color ParseCGColor(string cgColor)
        {
            Color res = Colors.Transparent;
            if (cgColor != null && cgColor != string.Empty)
            {
                string strColor = cgColor.Trim();
                string strAlpha = "";
                int nAlpha = 0;

                if (cgColor.Contains('('))
                {
                    string[] split = cgColor.Split('(');
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
            else if (cgColor != null && cgColor == string.Empty)
            {
                res = Colors.White;
            }
            return res;
        }

        public static Color[] parseCGradient(string cgGradient, out string gradientType)
        {
            Color[] res = null;
            gradientType = "Horizontal";
            if (cgGradient != null)
            {
                cgGradient = cgGradient.Trim();

                string[] strGradients = null;
                if (cgGradient.Contains('-'))
                {
                    strGradients = cgGradient.Split('-');
                    gradientType = "Horizontal";
                }
                else if (cgGradient.Contains('/'))
                {
                    strGradients = cgGradient.Split('/');
                    gradientType = "Vertical";
                }
                else if (cgGradient.Contains('*'))
                {
                    strGradients = cgGradient.Split('*');
                    gradientType = "Radial";
                }
                else if (cgGradient.Contains('|'))
                {
                    strGradients = cgGradient.Split('|');
                    gradientType = "Follow";
                }
                else if (cgGradient.Contains('+'))
                {
                    strGradients = cgGradient.Split('+');
                    gradientType = "Diagonal";
                }

                if (strGradients == null)
                    strGradients = new string[] { cgGradient };


                res = new Color[strGradients.Length];
                for (int i = 0; i < strGradients.Length; i++)
                {
                    res[i] = ParseCGColor(strGradients[i]);
                }
            }
            return res;
        }

        public static Color[] parseCGradient(string cgGradient, out bool isVertical)
        {
            Color[] res = null;
            isVertical = false;
            if (cgGradient != null && cgGradient != string.Empty)
            {
                if (cgGradient.Contains("/"))
                    isVertical = true;

                string[] strGradients = cgGradient.Split('-');

                if (strGradients.Length < 2)
                    strGradients = cgGradient.Split('+');

                if (strGradients.Length < 2)
                    strGradients = cgGradient.Split('*');

                if (strGradients.Length < 2)
                    strGradients = cgGradient.Split('/');

                res = new Color[strGradients.Length];
                for (int i = 0; i < strGradients.Length; i++)
                {
                    res[i] = ParseCGColor(strGradients[i]);
                }
            }
            return res;
        }

        public static string ComposeCGGradient(Color[] colors, string gradientType)
        {
            string strRes = string.Empty;
            //Check for valid gradient type
            if (gradientType != "Horizontal" && gradientType != "Vertical" && gradientType != "Radial"
                && gradientType != "Follow" && gradientType != "Diagonal")
                gradientType = null;

            if (colors.Length > 0 && gradientType != null)
            {
                for (int i = 0; i < colors.Length; i++)
                {
                    strRes += colors[i];
                    if (i < colors.Length - 1)
                    {
                        if (gradientType == "Horizontal")
                            strRes += "-";
                        if (gradientType == "Vertical")
                            strRes += "/";
                        if (gradientType == "Radial")
                            strRes += "*";
                        if (gradientType == "Follow")
                            strRes += "|";
                        if (gradientType == "Diagonal")
                            strRes += "+";
                    }
                }
            }
            return strRes;
        }

        public static bool isStringTrue(string target)
        {
            if (target.ToLower() == "true" || target.ToLower() == "yes" || target.ToLower() == "1")
                return true;
            else
                return false;
        }
    }
}
