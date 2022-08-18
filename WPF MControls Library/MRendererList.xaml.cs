using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using MPLATFORMLib;
using System.ComponentModel;

namespace WPFMControls
{
    
    public partial class MRendererList : UserControl
    {
        public MRendererList()
        {
            InitializeComponent();
        }
       
        public class Renderer
        {
            
            public bool Start { get; set; }            
            public string renderer { set; get; }
            public ObservableCollection<string> lineIn
            { set; get; }
            public string LineInSelected             
                { set; get; }
            public ObservableCollection<string> lineOut
            { set; get; }
            public string LineOutSelected
                { set; get; }
            public ObservableCollection<string> Keying
            { set; get; }
            public string KeyingSelected            
               { set; get; }
            public MRendererClass tag { set; get; }
        }
        private Object _mPSrcObject;
        private MRendererClass _outputDevice;
        //private MPla
        private ObservableCollection<Renderer> _myRendererList;
        public Object SetSourceObject(Object pObject)
        {
            _myRendererList = new ObservableCollection<Renderer>();
            var pOld = _mPSrcObject;
            try
            {
                _mPSrcObject = pObject;
                rendererTable.Tag = _mPSrcObject;
                if (_mPSrcObject != null)
                    FillRenderers();
            }
            catch (System.Exception e)
            {}
            return pOld;
        }

        private void FillRenderers()
        {
            ClearRenderers();

            try
            {
                var pRenderEnum = new MRendererClass();
                int nDevices;
                pRenderEnum.DeviceGetCount(0, "renderer", out nDevices);
                for (int i = 0; i < nDevices; i++)
                {
                    string strName;
                    string strXML;
                    pRenderEnum.DeviceGetByIndex(0, "renderer", i, out strName, out strXML);
                    var pRenderer = new Renderer {Start = false};
                    var pDevice = new MRendererClass();
                    pDevice.DeviceSet("renderer", strName, "");
                    pRenderer.renderer = strName;
                    pRenderer.tag = pDevice;
                    int nLines = 0;
                    pDevice.DeviceGetCount(0, "renderer::line-out", out nLines);
                    var pComboLineOut = new ObservableCollection<string>();
                    for (int k = 0; k < nLines; k++)
                    {
                        pDevice.DeviceGetByIndex(0, "renderer::line-out", k, out strName, out strXML);
                        pComboLineOut.Add(strName);
                    }
                    pRenderer.lineOut = pComboLineOut;
                    if (pComboLineOut.Count > 0)
                        pRenderer.LineOutSelected = pComboLineOut[0];
                    var pComboKey = new ObservableCollection<string>();
                    pDevice.DeviceGetCount(0, "renderer::keying", out nLines);
                    for (int k = 0; k < nLines; k++)
                    {
                        pDevice.DeviceGetByIndex(0, "renderer::keying", k, out strName, out strXML);
                        pComboKey.Add(strName);
                    }
                    pRenderer.Keying = pComboKey;
                    if (pComboKey.Count > 0)
                        pRenderer.KeyingSelected = pComboKey[0];
                    var pComboLineIn = new ObservableCollection<string>();
                    pDevice.DeviceGetCount(0, "renderer::line-in", out nLines);
                    for (int k = 0; k < nLines; k++)
                    {
                        pDevice.DeviceGetByIndex(0, "renderer::line-in", k, out strName, out strXML);
                        pComboLineIn.Add(strName);
                    }
                    pRenderer.lineIn = pComboLineIn;
                    if (pComboLineIn.Count > 0)
                        pRenderer.LineInSelected = pComboLineIn[0];
                    //var test = new Renderer { Keying = pComboKey, lineIn = pComboLineIn, lineOut = pComboLineOut, renderer = pRenderer.renderer, Start = true };
                    _myRendererList.Add(pRenderer);
                }
                rendererTable.ItemsSource = _myRendererList;
            }
            catch (System.Exception) { }
        }

        void ClearRenderers()
        {
            try
            {
                foreach (Renderer t in _myRendererList)
                {
                    IMObject pObject = (IMObject) t.tag;
                    pObject.ObjectClose();
                }
                _myRendererList = new ObservableCollection<Renderer>();
            }
            catch (Exception )
            {
               
            }
        }

        private void ToggleButton_OnChecked(object sender, RoutedEventArgs e)
        {
            try
            {
                _outputDevice = new MRendererClass();
                _outputDevice.DeviceSet("renderer", _myRendererList[rendererTable.SelectedIndex].renderer, "");
                if (_myRendererList[rendererTable.SelectedIndex].LineOutSelected != null)
                _outputDevice.DeviceSet("renderer::line-out", _myRendererList[rendererTable.SelectedIndex].LineOutSelected, "");
                if (_myRendererList[rendererTable.SelectedIndex].LineInSelected != null)
                _outputDevice.DeviceSet("renderer::line-in", _myRendererList[rendererTable.SelectedIndex].LineInSelected, "");
                if (_myRendererList[rendererTable.SelectedIndex].KeyingSelected != null)
                _outputDevice.DeviceSet("renderer::keying", _myRendererList[rendererTable.SelectedIndex].KeyingSelected, "");
                _outputDevice.ObjectStart(_mPSrcObject);                
            }
            catch (Exception)
            {
                MessageBox.Show("Error to start output to " + _myRendererList[rendererTable.SelectedIndex].renderer);
            }
        }

        private void ToggleButton_OnUnchecked(object sender, RoutedEventArgs e)
        {
            _outputDevice.ObjectClose();
        }

        private void newSelection(object sender, SelectionChangedEventArgs e)
        {
            //_myRendererList[rendererTable.SelectedIndex].LineInSelected = rendererTable.SelectedCells.ToString();
        }

    }
}
