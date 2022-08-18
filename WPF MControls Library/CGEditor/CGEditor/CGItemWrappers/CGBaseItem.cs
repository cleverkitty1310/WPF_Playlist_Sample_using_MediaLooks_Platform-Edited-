using System;
using System.Globalization;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Xceed.Wpf.Toolkit.PropertyGrid.Editors;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;
using System.ComponentModel;
using MLCHARGENLib;
using CGEditor.Misc;
using System.Windows;



namespace CGEditor.CGItemWrappers
{
    public class CGBaseItem : IChangeNotify, IVisible, IPosition, ISimpleColor, IMisc, ISpeed, IShadow, IBlur, IGlow
    {
        //Constructors 
        public CGBaseItem()
        {
            xmlDoc = new XmlDocument();
        }

        //Events
        public event ItemChangedHandler ItemChanged;
        protected virtual void InvokeItemChanged(object sender, ItemChangedArgs e)
        {
            if (ItemChanged != null)
            {
                ItemChanged(sender, e);
            }
        }

        public CGBaseItem(string itemId)
        {
            m_strID = itemId;
            xmlDoc = new XmlDocument();
            GetXMLDescription();
        }

        #region ================================Properties and Fields================================
        public static CoMLCharGen m_objCG;
        protected XmlDocument xmlDoc;
        protected bool m_bDirty;
        protected int m_dblChangeTime;

        protected string m_strID;
        public string ID
        {
            get { return m_strID; }
        }

        protected bool m_bSelected;
        public bool isSelected
        {
            get { return m_bSelected; }
            set { m_bSelected = value; }
        }

        public string XML
        {
            get 
            {
                return GetXMLDescription();
            }
            set
            {
                SetProperty("", value.ToString());
                if (ItemChanged != null)
                    ItemChanged(this, new ItemChangedArgs("xml", XML));

            }
        }

        public string Content
        {
            get
            {
                string strRes = GetStringPropertyExplicit("content");
                strRes = strRes.Replace("\r\n", "");
                strRes = strRes.Replace("\t", "");
                return strRes;
            }
            set
            {
                SetProperty("content", value.ToString());
                if (ItemChanged != null)
                    ItemChanged(this, new ItemChangedArgs("content", value.ToString()));
            }
        }

	    public Rect AbsRect
        {
            get
            {
                Rect res = new Rect();
                IMLCharGenEdit cgedit = (IMLCharGenEdit)m_objCG;
                tagRECT rect;
                cgedit.EditItemAbsoluteRectGet(ID, out rect);
                res.X = rect.left;
                res.Y = rect.top;
                res.Width = rect.right - rect.left;
                res.Height = rect.bottom - rect.top;
                return res;
            }
            set
            {
                tagRECT tagRect = new tagRECT();
                tagRect.left = (int)value.Left;
                tagRect.right = (int)value.Right;
                tagRect.top = (int)value.Top;
                tagRect.bottom = (int)value.Bottom;
                IMLCharGenEdit cgedit = (IMLCharGenEdit)m_objCG;
                cgedit.EditItemAbsoluteRectSet(ID, ref tagRect);
            }
	    }

        public double PosX
        {
            get 
            {
                //return XmlConvert.ToDouble(GetStringProperty("cg-props::pos-x"));
                return AbsRect.X;
            }
            set
            {
                Rect rect = new Rect((int)value, AbsRect.Y, AbsRect.Width, AbsRect.Height);
                AbsRect = rect;
                InvokeItemChanged(this, new ItemChangedArgs("posx", ""));
                //SetProperty("cg-props::pos-x", value.ToString());
            }
        }

        public double PosY
        {
            get 
            {
                //return XmlConvert.ToDouble(GetStringProperty("cg-props::pos-y"));
                return AbsRect.Y;
            }
            set
            {
                Rect rect = new Rect(AbsRect.X, (int)value, AbsRect.Width, AbsRect.Height);
                AbsRect = rect;
                InvokeItemChanged(this, new ItemChangedArgs("posy", ""));
                //SetProperty("cg-props::pos-y", value.ToString());
            }
        }

        public double Width
        {
            get 
            {
                //return XmlConvert.ToDouble(GetStringProperty("cg-props::width"));
                return AbsRect.Width;
            }
            set
            {
                Rect rect = new Rect(AbsRect.X, AbsRect.Y, value > 0 ? (int)value : 0, AbsRect.Height);
                AbsRect = rect;
                InvokeItemChanged(this, new ItemChangedArgs("width", ""));
                //SetProperty("cg-props::width", value.ToString());
            }
        }

        public double Height
        {
            get 
            { 
                //return XmlConvert.ToDouble(GetStringProperty("cg-props::height"));
                return AbsRect.Height;
            }
            set
            {
                Rect rect = new Rect(AbsRect.X, AbsRect.Y, AbsRect.Width, value > 0 ? (int)value : 0);
                AbsRect = rect;
                InvokeItemChanged(this, new ItemChangedArgs("height", ""));
                //SetProperty("cg-props::height", value.ToString());
            }
        }

        public string Align
        {
            get 
            { 
                return GetStringProperty("cg-props::align");
            }
            set
            {
                SetProperty("cg-props::align", value);
                InvokeItemChanged(this, new ItemChangedArgs("align", value));
            }
        }

        public int Alpha
        {
            get
            {
                return Int32.Parse(GetStringProperty("cg-props::alpha"));
            }
            set
            {
                SetProperty("cg-props::alpha", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("alpha", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public bool Show
        {
            get
            {
                string strShow = GetStringProperty("cg-props::show");
                if (WrapperHelpers.isStringTrue(strShow))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    SetProperty("cg-props::show", "true");
                }
                else
                {
                    SetProperty("cg-props::show", "false");
                }
                InvokeItemChanged(this, new ItemChangedArgs("show", value.ToString()));
            }
        }

        public string Interlace
        {
            get 
            {
                return GetStringProperty("cg-props::interlace");
            }
            set
            {
                SetProperty("cg-props::interlace", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("interlace", value));
            }
        }

        public string MoveType
        {
            get
            {
                return GetStringProperty("cg-props::move-type");
            }
            set
            {
                SetProperty("cg-props::move-type", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("move-type", value));
            }
        }

        public string BGColor
        {
            get 
            { 
                return GetStringProperty("cg-props::bg-color");
            }
            set
            {
                SetProperty("cg-props::bg-color", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("bgcolor", value));
            }
        }

        public double PixelAR
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::pixel-ar"));
            }
            set
            {
                SetProperty("cg-props::pixel-ar", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("pixel-ar", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string Scale
        {
            get 
            { 

                return GetStringProperty("cg-props::scale");
            }
            set
            {
                SetProperty("cg-props::scale", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("scale", value));
            }
        }

        public double EdgesSmooth
        {
            get 
            {
                return XmlConvert.ToDouble(GetStringProperty("cg-props::edges-smooth"));
            }
            set
            {
                SetProperty("cg-props::edges-smooth", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("edges-smooth", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string PlayMode
        {
            get
            { 
                return GetStringProperty("cg-props::play-mode");
            }
            set
            {
                SetProperty("cg-props::play-mode", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("play-mode", value));
            }
        }

        public string Pause
        {
            get
            {
                return GetStringProperty("cg-props::pause");
            }
            set
            {
                SetProperty("cg-props::pause", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("pause", value));
            }
        }

        //Indents
        public double IndentLeft
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::indent::left"));
            }
            set
            {
                SetProperty("cg-props::indent::left", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("indent", ""));
            }
        }

        public double IndentRight
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::indent::right"));
            }
            set
            {
                SetProperty("cg-props::indent::right", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("indent", ""));
            }
        }

        public double IndentTop
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::indent::top"));
            }
            set
            {
                SetProperty("cg-props::indent::top", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("indent", ""));
            }
        }

        public double IndentBottom
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::indent::bottom"));
            }
            set
            {
                SetProperty("cg-props::indent::bottom", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("indent", ""));
            }
        }

        public double GroupIndentLeft
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::group-indent::left"));
            }
            set
            {
                SetProperty("cg-props::group-indent::left", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("group-indent", ""));
            }
        }

        public double GroupIndentRight
        {
            get 
            {
                return XmlConvert.ToDouble(GetStringProperty("cg-props::group-indent::right"));
            }
            set
            {
                SetProperty("cg-props::group-indent::right", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("group-indent", ""));
            }
        }

        public double GroupIndentTop
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("cg-props::group-indent::top"));
            }
            set
            {
                SetProperty("cg-props::group-indent::top", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("group-indent", ""));
            }
        }

        public double GroupIndentBottom
        {
            get 
            {
                return XmlConvert.ToDouble(GetStringProperty("cg-props::group-indent::bottom"));
            }
            set
            {
                SetProperty("cg-props::group-indent::bottom", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("group-indent", ""));
            }
        }

        //Movement

        public double SpeedX
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("movement::speed-x") == "" ? "0" : GetStringProperty("movement::speed-x"));
            }
            set
            {
                SetProperty("movement::speed-x", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("speed-x", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double SpeedY
        {
            get 
            { 
                return XmlConvert.ToDouble(GetStringProperty("movement::speed-y") == "" ? "0" : GetStringProperty("movement::speed-y"));
            }
            set
            {
                SetProperty("movement::speed-y", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("speed-y", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public bool ShadowEnable
        {
            get
            {
                string strShadowEnable = GetStringProperty("effects::shadow::enabled");
                if (WrapperHelpers.isStringTrue(strShadowEnable))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    SetProperty("effects::shadow::enabled", "yes");
                }
                else
                {
                    SetProperty("effects::shadow::enabled", "no");
                }
                InvokeItemChanged(this, new ItemChangedArgs("shadow-enabled", value.ToString()));
            }
        }

        public double ShadowBlur
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::shadow::blur") == "" ? "0" : GetStringProperty("effects::shadow::blur"));
            }
            set
            {
                SetProperty("effects::shadow::blur", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("shadow-blur", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double ShadowAlpha
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::shadow::alpha") == "" ? "0" : GetStringProperty("effects::shadow::alpha"));
            }
            set
            {
                SetProperty("effects::shadow::alpha", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("shadow-alpha", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double ShadowXOffset
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::shadow::offset-x") == "" ? "0" : GetStringProperty("effects::shadow::offset-x"));
            }
            set
            {
                SetProperty("effects::shadow::offset-x", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("shadow-offset-x", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double ShadowYOffset
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::shadow::offset-y") == "" ? "0" : GetStringProperty("effects::shadow::offset-y"));
            }
            set
            {
                SetProperty("effects::shadow::offset-y", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("shadow-offset-y", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string ShadowColor
        {
            get
            {
                return GetStringProperty("effects::shadow::color");
            }
            set
            {
                SetProperty("effects::shadow::color", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("shadow-color", value));
            }
        }

        public bool BlurEnable
        {
            get
            {
                string strEnable = GetStringProperty("effects::blur::enabled");
                if (Helpers.isStringTrue(strEnable))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    SetProperty("effects::blur::enabled", "yes");
                }
                else
                {
                    SetProperty("effects::blur::enabled", "no");
                }
                InvokeItemChanged(this, new ItemChangedArgs("blur-enabled", value.ToString()));
            }
        }

        public double BlurSizeX
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::blur::size-x") == "" ? "0" : GetStringProperty("effects::blur::size-x"));
            }
            set
            {
                SetProperty("effects::blur::size-x", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("blur-size-x", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double BlurSizeY
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::blur::size-y") == "" ? "0" : GetStringProperty("effects::blur::size-y"));
            }
            set
            {
                SetProperty("effects::blur::size-y", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("blur-size-y", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string BlurAlign
        {
            get
            {
                return GetStringProperty("effects::blur::align");
            }
            set
            {
                SetProperty("effects::blur::align", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("blur-align", value));
            }
        }

        public bool GlowEnable
        {
            get
            {
                string strEnable = GetStringProperty("effects::glow::enabled");
                if (Helpers.isStringTrue(strEnable))
                    return true;
                else
                    return false;
            }
            set
            {
                if (value == true)
                {
                    SetProperty("effects::glow::enabled", "yes");
                }
                else
                {
                    SetProperty("effects::glow::enabled", "no");
                }
                InvokeItemChanged(this, new ItemChangedArgs("glow-enabled", value.ToString()));
            }
        }

        public double GlowSizeX
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::glow::size-x") == "" ? "0" : GetStringProperty("effects::glow::size-x"));
            }
            set
            {
                SetProperty("effects::glow::size-x", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("glow-size-x", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double GlowSizeY
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::glow::size-y") == "" ? "0" : GetStringProperty("effects::glow::size-y"));
            }
            set
            {
                SetProperty("effects::glow::size-y", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("glow-size-y", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double GlowValue
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::glow::value") == "" ? "0" : GetStringProperty("effects::glow::value"));
            }
            set
            {
                SetProperty("effects::glow::value", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("glow-value", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public double GlowBlend
        {
            get
            {
                return XmlConvert.ToDouble(GetStringProperty("effects::glow::blend") == "" ? "0" : GetStringProperty("effects::glow::blend"));
            }
            set
            {
                SetProperty("effects::glow::blend", value.ToString(CultureInfo.InvariantCulture));
                InvokeItemChanged(this, new ItemChangedArgs("glow-blend", value.ToString(CultureInfo.InvariantCulture)));
            }
        }

        public string MotionblurEnable
        {
            get
            {
                return GetStringProperty("effects::motion-blur::enabled");
            }
            set
            {
                SetProperty("effects::glow::blend", value.ToString());
                InvokeItemChanged(this, new ItemChangedArgs("motion-blur-enabled", value));
            }
        }
       
        //Additional fields for interaction purposes
        protected Point m_ptStartLocation;
        public Point StartLocation
        {
            get { return m_ptStartLocation; }
            set { m_ptStartLocation = value; }
        }

        protected Rect m_ptStartRectangle;
        public Rect StartRectangle
        {
            get 
            {
                return m_ptStartRectangle; 
            }
            set 
            {
                m_ptStartRectangle = value; 
                
            }
        }

        protected double m_dblStartWidth;
        public double StartWidth
        {
            get { return m_dblStartWidth; }
            set { m_dblStartWidth = value; }
        }

        protected double m_dblStartHeight;
        public double StartHeight
        {
            get { return m_dblStartHeight; }
            set { m_dblStartHeight = value; }
        }

        #endregion

        #region ================================METHODS================================
        protected string GetXMLDescription()
        {
            string result;
            m_objCG.GetItemProperties(m_strID, "", out result);
            xmlDoc.LoadXml(result);
            return result;
        }

        protected virtual string GetNodeXML(string path)
        {
            string result = "";
            if (m_bDirty)
                GetXMLDescription();

            if (xmlDoc != null)
            {
                try
                {
                    XmlElement elem = xmlDoc.DocumentElement;
                    string[] strSplit = path.Split(new string[] { "::" }, StringSplitOptions.None);
                    string xPath = "/";
                    for (int i = 0; i < strSplit.Length; i++)
                    {
                        if (i == strSplit.Length - 1)
                            xPath += strSplit[i];
                        else
                            xPath += strSplit[i] + "/";
                    }
                    xPath = "/" + elem.Name + xPath;

                    XmlNode node = elem.SelectSingleNode(xPath);

                    if (node != null && node.InnerXml != null)
                        result = node.InnerXml;
                }
                catch (System.Exception ex)
                {

                }

            }
            return result;
        }

        public void ForceUpdateNotification()
        {
            if (ItemChanged != null)
                ItemChanged(this, new ItemChangedArgs("xml", XML));
        }

        protected virtual string GetStringProperty(string path)
        {
            string result = "";
            if (m_bDirty)
                GetXMLDescription();

            if (xmlDoc != null)
            {
                try
                {
                    XmlElement elem = xmlDoc.DocumentElement;
                    string[] strSplit = path.Split(new string[] { "::" }, StringSplitOptions.None);
                    string xPath = "/";
                    for (int i = 0; i < strSplit.Length; i++)
                    {
                        if (i == strSplit.Length - 1)
                            xPath += "@" + strSplit[i];
                        else
                            xPath += strSplit[i] + "/";
                    }
                    xPath = "/" + elem.Name + xPath;
                    XmlNode node = elem.SelectSingleNode(xPath);
                    if (node != null && node.Value != null)
                    {
                        result = node.Value;
                        if (result.EndsWith("."))
                            result += "0";
                    }
                }
                catch (System.Exception ex)
                {

                }

            }
            return result;
        }

        protected virtual string GetStringPropertyExplicit(string path)
        {
            string result = "";
                try
                {
                    if (m_objCG != null && m_strID != null && m_strID != string.Empty)
                    {
                        m_objCG.GetItemProperties(m_strID, path, out result);
                        m_bDirty = true;
                    }
                }
                catch (System.Exception ex)
                {

                }
            return result;
        }

        protected virtual void SetProperty(string strPropName, string strPropValue)
        {
            if (m_objCG != null && m_strID != null && m_strID != string.Empty)
            {
                m_objCG.SetItemProperties(m_strID, strPropName, strPropValue, "", m_dblChangeTime);
                m_bDirty = true;
            }
        }



        protected Rect GetAbsoluteRect()
        {
            Rect res = new Rect();
            IMLCharGenEdit cgedit = (IMLCharGenEdit)m_objCG;
            tagRECT rect;
            cgedit.EditItemAbsoluteRectGet(ID, out rect);
            res.X = rect.left;
            res.Y = rect.top;
            res.Width = rect.left + rect.right;
            res.Height = rect.top + rect.bottom;
            return res;
        }
        #endregion
    }
}
