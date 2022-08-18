using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows;
using System.Windows.Media;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Diagnostics;
using CGEditor.Misc;
using MLCHARGENLib;


namespace CGEditor.CGItemWrappers
{
    public class CGTickerItem : CGBaseItem, IShape, IGradient, ITextGradient, IOutline, ITextOutline, ITicker, ITickerNewContent, IFont
    {
        // ITextGradient, ITextOutline, IShapeOutline,
        public CGTickerItem(string itemId) : base(itemId) { }

        public string NewContent
        {
            get
            {
                return GetStringProperty("ticker::content");
            }
            set
            {
                m_objCG.TickerAddContent(ID, value, "multiline, remove-all");
                InvokeItemChanged(this, new ItemChangedArgs("tickercontent", value));
            }
        }

        public string TickerContent
        {
            get
            {
                string strRes = GetStringPropertyExplicit("ticker::content");
                strRes = strRes.Replace("\r\n", "");
                strRes = strRes.Replace("\t", "");
                return strRes;
            }
            set
            {
                SetProperty("ticker::content", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("ticker::content", value));
            }
        }

        public double Speed
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("ticker::speed") == "" ? "0" : GetStringProperty("ticker::speed"));
            }
            set
            {
                SetProperty("ticker::speed", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("ticker-speed", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public bool Gapless
        {
            get
            {
                string strGapless = GetStringProperty("ticker::gapless-crawl");
                if (WrapperHelpers.isStringTrue(strGapless))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    SetProperty("ticker::gapless-crawl", "yes");
                }
                else
                {
                    SetProperty("ticker::gapless-crawl", "no");
                }
                InvokeItemChanged(this, new ItemChangedArgs("gaplesscrawl", value.ToString()));
            }
        }

        public double LineHeight
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("ticker::line-height") == "" ? "0" : GetStringProperty("ticker::line-height"));
            }
            set
            {
                SetProperty("ticker::line-height", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("ticker-line-height", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double Distance
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("ticker::distance") == "" ? "0" : GetStringProperty("ticker::distance"));
            }
            set
            {
                SetProperty("ticker::distance", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("ticker-distance", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string Type
        {
            get
            {
                return GetStringProperty("graphics::type");
            }
            set
            {
                SetProperty("graphics::type", value);
                SetProperty("ticker::background::type", value);
                InvokeItemChanged(this, new ItemChangedArgs("shapetype", value));
            }
        }


        public int Sides
        {
            get
            {
                int nSides = XmlConvert.ToInt32(GetStringProperty("graphics::sides") == "" ? "0" : GetStringProperty("graphics::sides"));
                if (nSides < 3) nSides = 3;
                return nSides;
            }
            set
            {
                SetProperty("graphics::sides", value.ToString());
                SetProperty("ticker::background::sides", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("nsides", value.ToString(CultureInfo.InvariantCulture)));
            }
        }


        public string Color
        {
            get
            {
                return GetStringProperty("graphics::color");
            }
            set
            {
                SetProperty("graphics::color", value);
                SetProperty("ticker::background::color", value);
                InvokeItemChanged(this, new ItemChangedArgs("color", value));
            }
        }

        public double ColorAngle
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::color-angle") == "" ? "0" : GetStringProperty("graphics::color-angle"));
            }
            set
            {
                SetProperty("graphics::color-angle", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::color-angle", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("color-angle", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string OutlineColor
        {
            get
            {
                return GetStringProperty("graphics::outline-color");
            }
            set
            {
                SetProperty("graphics::outline-color", value);
                SetProperty("ticker::background::outline-color", value);
                InvokeItemChanged(this, new ItemChangedArgs("outline-color", value));
            }
        }

        public double OutlineWidth
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::outline") == "" ? "0" : GetStringProperty("graphics::outline"));
            }
            set
            {
                SetProperty("graphics::outline", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::outline", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("outline", value.ToString(CultureInfo.InvariantCulture)));
            }
        }


        //-----------------------------------------------------------------------------------------
        public string TextColor
        {
            get
            {
                return GetStringProperty("ticker::default-text::color");
            }
            set
            {
                SetProperty("ticker::default-text::color", value);
                InvokeItemChanged(this, new ItemChangedArgs("textcolor", value));
            }
        }

        public double TextColorAngle
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("ticker::default-text::color-angle") == "" ? "0" : GetStringProperty("ticker::default-text::color-angle"));
            }
            set
            {
                SetProperty("ticker::default-text::color-angle", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("textcolor-angle", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string TextOutlineColor
        {
            get
            {
                return GetStringProperty("ticker::default-text::outline-color");
            }
            set
            {
                SetProperty("ticker::default-text::outline-color", value);
                InvokeItemChanged(this, new ItemChangedArgs("textoutline-color", value));
            }
        }

        public double TextOutlineWidth
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("ticker::default-text::outline") == "" ? "0" : GetStringProperty("ticker::default-text::outline"));
            }
            set
            {
                SetProperty("ticker::default-text::outline", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("textoutline", value.ToString(CultureInfo.InvariantCulture)));
            }
        }


        //-----------------------------------------------------------------------------------------
        public double RoundCorners
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::round-corners") == "" ? "0" : GetStringProperty("graphics::round-corners"));
            }
            set
            {
                SetProperty("graphics::round-corners", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::round-corners", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("round-corners", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double Rotate
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::rotate") == "" ? "0" : GetStringProperty("graphics::rotate"));
            }
            set
            {
                SetProperty("graphics::rotate", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::rotate", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("rotate", value.ToString(CultureInfo.InvariantCulture)));
            }
        }



        public double StarInset
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::star-inset") == "" ? "0" : GetStringProperty("graphics::star-inset"));
            }
            set
            {
                SetProperty("graphics::star-inset", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::star-inset", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("star-inset", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double FixSizeWidth
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::width") == "" ? "0" : GetStringProperty("graphics::width"));
            }
            set
            {
                SetProperty("graphics::width", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::width", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("fixsizewidth", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double FixSizeHeight
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::height") == "" ? "0" : GetStringProperty("graphics::height"));
            }
            set
            {
                SetProperty("graphics::height", value.ToString(CultureInfo.InvariantCulture));
                SetProperty("ticker::background::height", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("fixsizeheight", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public bool Gamma
        {
            get
            {
                string strGamma = GetStringProperty("graphics::gamma");
                if (WrapperHelpers.isStringTrue(strGamma))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    SetProperty("graphics::gamma", "yes");
                    SetProperty("ticker::background::gamma", "yes");
                }
                else
                {
                    SetProperty("graphics::gamma", "no");
                    SetProperty("ticker::background::gamma", "no");
                }
                InvokeItemChanged(this, new ItemChangedArgs("gammacorrection", value.ToString(CultureInfo.InvariantCulture)));
            }
        }
        

        private string m_strFontFamily;
        public string FontFamily
        {
            get
            {
                string strCGFont = GetStringProperty("ticker::default-text::font");
                foreach (FontFamily ff in Fonts.SystemFontFamilies)
                {
                    if (strCGFont.Contains(ff.ToString()))
                        return ff.ToString();
                }
                return "";
            }
            set
            {
                m_strFontFamily = value;
                m_strFontFamily = m_strFontFamily.Trim();
                SetProperty("ticker::default-text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }
        }

        private string m_strFontTypeface;
        public string FontTypeface
        {
            get
            {
                string strCGFont = GetStringProperty("ticker::default-text::font");
                string strFontFamily = FontFamily;
                if (strFontFamily != string.Empty)
                {
                    FontFamily ff = new FontFamily(strFontFamily);
                    System.Windows.Markup.XmlLanguage lang = System.Windows.Markup.XmlLanguage.GetLanguage("en-us");
                    foreach (FamilyTypeface ft in ff.FamilyTypefaces)
                    {
                        if (ft.AdjustedFaceNames.ContainsKey(lang))
                        {
                            string strFontTypefaceName = ft.AdjustedFaceNames[lang];
                            if (strCGFont.Contains(strFontTypefaceName))
                                return strFontTypefaceName;
                        }
                    }
                }
                return "";
            }
            set
            {
                m_strFontTypeface = value;
                SetProperty("ticker::default-text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }
        }

        public double FontSize
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("ticker::default-text::font-size") == "" ? "0" : GetStringProperty("ticker::default-text::font-size"));
            }
            set
            {
                SetProperty("ticker::default-text::font-size", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("font-size", value.ToString(CultureInfo.InvariantCulture)));
            }
        }


        private bool m_bUnderline;
        public bool Underline
        {
            get
            {
                string strCGFont = GetStringProperty("ticker::default-text::font");
                if (strCGFont.ToLower().Contains("underline"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bUnderline = value;
                SetProperty("ticker::default-text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }

        }

        private bool m_bStrikeOut;
        public bool StrikeOut
        {
            get
            {
                string strCGFont = GetStringProperty("ticker::default-text::font");
                if (strCGFont.ToLower().Contains("strikeout"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bStrikeOut = value;
                SetProperty("ticker::default-text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }
        }

        public bool WordWrap
        {
            get
            {
                string strWpap = GetStringProperty("ticker::default-text::word-break");
                if (Helpers.isStringTrue(strWpap))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    SetProperty("ticker::default-text::word-break", "yes");
                else
                    SetProperty("ticker::default-text::word-break", "no");

                InvokeItemChanged(this, new ItemChangedArgs("word-break", value.ToString()));
            }
        }

        private bool m_bRTL;
        public bool RightToLeft
        {
            get
            {
                string strFormat = (GetStringProperty("ticker::default-text::format")).ToUpper();
                if (strFormat != null && strFormat.Contains("RTL"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bRTL = value;
                SetProperty("ticker::default-text::format", getTextFormat());
                InvokeItemChanged(this, new ItemChangedArgs("textformat", getTextFormat()));
            }
        }

        private bool m_bVertical;
        public bool Vertical
        {
            get
            {
                string strFormat = (GetStringProperty("ticker::default-text::format")).ToUpper();
                if (strFormat != null && strFormat.Contains("VERTICAL"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bVertical = value;
                SetProperty("ticker::default-text::format", getTextFormat());
                InvokeItemChanged(this, new ItemChangedArgs("textformat", getTextFormat()));
            }
        }

        private bool m_bNoTabs;
        public bool NoTabs
        {
            get
            {
                string strFormat = (GetStringProperty("ticker::default-text::format")).ToUpper();
                if (strFormat != null && strFormat.Contains("NO-TAB"))
                    return true;
                else
                    return false;

            }
            set
            {
                m_bNoTabs = value;
                SetProperty("ticker::default-text::format", getTextFormat());
                InvokeItemChanged(this, new ItemChangedArgs("textformat", getTextFormat()));
            }
        }

        public string Flip
        {
            get
            {
                return GetStringProperty("ticker::default-text::flip");
            }
            set
            {
                SetProperty("ticker::default-text::flip", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("textflip", value));
            }
        }

        private string getTextFormat()
        {
            string res = "";
            if (m_bRTL)
                res += "RTL";

            if (m_bVertical)
            {
                if (res == "")
                    res += "VERTICAL";
                else
                    res += " VERTICAL";
            }

            if (m_bNoTabs)
            {
                if (res == "")
                    res += "NO-TAB";
                else
                    res += " NO-TAB";
            }
            return res;
        }

        public string GetCGFontString()
        {
            string strRes = "";
            if (m_strFontFamily != null && m_strFontFamily != "")
            {
                strRes += m_strFontFamily;
                if (m_strFontTypeface != null && m_strFontTypeface != "")
                    strRes += " " + m_strFontTypeface;

                if (m_bUnderline)
                    strRes += " Underline";
                if (m_bStrikeOut)
                    strRes += " StrikeOut";
            }
            return strRes;
        }

    }
}
