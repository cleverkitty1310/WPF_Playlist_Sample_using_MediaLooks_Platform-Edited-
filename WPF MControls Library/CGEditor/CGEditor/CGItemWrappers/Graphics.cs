using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Windows.Media;
using System.ComponentModel;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.Diagnostics;
using System.Globalization;
using CGEditor.Misc;
using MLCHARGENLib;


namespace CGEditor.CGItemWrappers
{
    public class CGGraphicsItem : CGBaseItem, IShape, IGradient, IOutline
    {
        public CGGraphicsItem(string itemId) : base(itemId) { }

        public string Type
        {
            get
            {
                return GetStringProperty("graphics::type");
            }
            set
            {
                SetProperty("graphics::type", value);
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
                SetProperty("graphics::outline", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("outline", value.ToString(CultureInfo.InvariantCulture)));
            }
        }
        
        public double RoundCorners
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("graphics::round-corners") == "" ? "0" : GetStringProperty("graphics::round-corners"));
            }
            set
            {
                SetProperty("graphics::round-corners", value.ToString(CultureInfo.InvariantCulture));
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
                InvokeItemChanged(this, new ItemChangedArgs("fixsizeheight", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public bool Gamma
        {
            get
            {
                string strGamma = GetStringProperty("graphics::gamma");
                if (Helpers.isStringTrue(strGamma))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                    SetProperty("graphics::gamma", "yes");
                else
                    SetProperty("graphics::gamma", "no");

                InvokeItemChanged(this, new ItemChangedArgs("gammacorrection", value.ToString(CultureInfo.InvariantCulture)));
            }
        }
    }
}
