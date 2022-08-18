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
using MLCHARGENLib;
using CGEditor.Misc;

namespace CGEditor.CGItemWrappers
{
    [DisplayName("Text Item Properties")]
    public class CGTextItem : CGBaseItem, IGradient, IOutline, IFont, IText
    {
        //Constructor
        public CGTextItem(string itemId): base(itemId)
        {
            //Initialize internal variables
            m_strFontFamily = FontFamily;
            m_strFontTypeface = FontTypeface;
            m_bUnderline = Underline;
            m_bStrikeOut = StrikeOut;
            m_bVertical = Vertical;
            m_bRTL = RightToLeft;
            m_bNoTabs = NoTabs;
        }

        public string Text
        {
            get 
            {
                return GetNodeXML("text");
            }
            set
            {
                SetProperty("text", value);
                InvokeItemChanged(this, new ItemChangedArgs("textcontent", value));
            }
        }

        public string Color
        {
            get 
            {
                return GetStringProperty("text::color");
            }
            set
            {
                SetProperty("text::color", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("color", value));
            }
        }

        public double ColorAngle
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("text::color-angle") == "" ? "0" : GetStringProperty("text::color-angle"));
            }
            set
            {
                SetProperty("text::color-angle", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("color-angle", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string OutlineColor
        {
            get
            {
                return GetStringProperty("text::outline-color");
            }
            set
            {
                SetProperty("text::outline-color", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("outline-color", value));
            }
        }

        public double OutlineWidth
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("text::outline") == "" ? "0" : GetStringProperty("text::outline"));
            }
            set
            {
                SetProperty("text::outline", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("outline", value.ToString(CultureInfo.InvariantCulture)));
            }
        }


        private string m_strFontFamily;
        public string FontFamily
        {
            get
            {
                string strCGFont = GetStringProperty("text::font");
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
                SetProperty("text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }
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

        private string m_strFontTypeface;
        public string FontTypeface
        {
            get
            {
                string strCGFont = GetStringProperty("text::font");
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
                SetProperty("text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }
        }

        public double FontSize
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("text::font-size") == "" ? "0" : GetStringProperty("text::font-size"));
            }
            set
            {
                SetProperty("text::font-size", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("font-size", value.ToString(CultureInfo.InvariantCulture)));
            }
        }


        private bool m_bUnderline;
        public bool Underline
        {
            get 
            {
                string strCGFont = GetStringProperty("text::font");
                if (strCGFont.ToLower().Contains("underline"))
                    return true;
                else
                    return false;
            }
            set 
            {
                m_bUnderline = value;
                SetProperty("text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }

        }

        private bool m_bStrikeOut;
        public bool StrikeOut
        {
            get
            {
                string strCGFont = GetStringProperty("text::font");
                if (strCGFont.ToLower().Contains("strikeout"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bStrikeOut = value;
                SetProperty("text::font", GetCGFontString());
                InvokeItemChanged(this, new ItemChangedArgs("font", GetCGFontString()));
            }
        }

        public bool WordWrap
        {
            get
            {
                string strWpap = GetStringProperty("text::word-break");
                if (Helpers.isStringTrue(strWpap))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    SetProperty("text::word-break", "yes");
                else
                    SetProperty("text::word-break", "no");

                InvokeItemChanged(this, new ItemChangedArgs("word-break", value.ToString()));
            }
        }

        private bool m_bRTL;
        public bool RightToLeft
        {
            get
            {
                string strFormat = (GetStringProperty("text::format")).ToUpper();
                if (strFormat != null && strFormat.Contains("RTL"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bRTL = value;
                SetProperty("text::format", getTextFormat());
                InvokeItemChanged(this, new ItemChangedArgs("textformat", getTextFormat()));
            }
        }

        private bool m_bVertical;
        public bool Vertical
        {
            get
            {
                string strFormat = (GetStringProperty("text::format")).ToUpper();
                if (strFormat != null && strFormat.Contains("VERTICAL"))
                    return true;
                else
                    return false;
            }
            set
            {
                m_bVertical = value;
                SetProperty("text::format", getTextFormat());
                InvokeItemChanged(this, new ItemChangedArgs("textformat", getTextFormat()));
            }
        }

        private bool m_bNoTabs;
        public bool NoTabs
        {
            get
            {
                string strFormat = (GetStringProperty("text::format")).ToUpper();
                if (strFormat != null && strFormat.Contains("NO-TAB"))
                    return true;
                else
                    return false;

            }
            set
            {
                m_bNoTabs = value;
                SetProperty("text::format", getTextFormat());
                InvokeItemChanged(this, new ItemChangedArgs("textformat", getTextFormat()));
            }
        }

        public string Flip
        {
            get
            {
                return GetStringProperty("text::flip");
            }
            set
            {
                SetProperty("text::flip", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("textflip", value));
            }
        }

        public string Type
        {
            get
            {
                return GetStringProperty("text::type");
            }
            set
            {
                SetProperty("text::type", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("texttype", value));
            }
        }


        public string getTextFormat()
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
    }
}
