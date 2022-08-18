using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CGEditor.CGItemWrappers;

namespace CGEditor.CustomPropertyEditors
{
    public partial class MiscEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IMisc m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public MiscEditor()
        {
            InitializeComponent();
            m_bChangedByUI = false;
            ComboScale.ItemsSource = ObservableCollections.GetScaleTypes();
            ComboAlign.ItemsSource = ObservableCollections.GetAligns();
            ComboInterlace.ItemsSource = ObservableCollections.GetInterlaceTypes();
            ComboPlayMode.ItemsSource = ObservableCollections.GetPlayModes();
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
            this.IsEnabled = false;
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IMisc)
                {
                    m_Item = (IMisc)item;
                    FillControls();
                    if (item is IChangeNotify)
                    {
                        ((IChangeNotify)item).ItemChanged += External_ItemChanged;
                    }
                    this.IsEnabled = true;
                }
                else
                {
                    ClearControls();
                    this.IsEnabled = false;
                }
            }
            catch (System.Exception) { }
        }

        private void FillControls()
        {
            if (m_Item != null)
            {
                ClearControls();
                m_bChangedByUI = false;
                SpinnerEdgeSmooth.Value = m_Item.EdgesSmooth;
                SpinnerPixelAR.Value = m_Item.PixelAR;
                ComboScale.SelectedItem = m_Item.Scale;
                ComboAlign.SelectedItem = m_Item.Align;
                ComboInterlace.SelectedItem = m_Item.Interlace;
                ComboPlayMode.SelectedItem = m_Item.PlayMode;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
                SpinnerEdgeSmooth.Value = 0;
                SpinnerPixelAR.Value = 0;
                ComboScale.SelectedItem = null;
                ComboAlign.SelectedItem = null;
                ComboInterlace.SelectedItem = null;
                ComboPlayMode.SelectedItem = null;
            m_bChangedByUI = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "align" || e.PropertyName == "interlace" || e.PropertyName == "pixel-ar" ||
                    e.PropertyName == "scale" || e.PropertyName == "edges-smooth" || e.PropertyName == "play-mode" || e.PropertyName == "xml")
                    FillControls();
            }
        }

        delegate void Setter();
        private void SetProperty(Setter s)
        {
            if (m_Item != null && m_bChangedByUI)
            {
                UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ((CGBaseItem)m_Item).ID));
                m_bAceptExternalUpdate = false;
                s();
                m_bAceptExternalUpdate = true;
            }
        }

        //=========================Control handlers====================================

        private void SpinnerEdgeSmooth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.EdgesSmooth = SpinnerEdgeSmooth.Value;
            });
        }

        private void SpinnerPixelAR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.PixelAR = SpinnerPixelAR.Value;
            });
        }

        private void ComboScale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Scale= ComboScale.SelectedItem.ToString();
            });
        }

        private void ComboAlign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Align = ComboAlign.SelectedItem.ToString();
            });
        }

        private void ComboInterlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Interlace = ComboInterlace.SelectedItem.ToString();
            });
        }

        private void ComboPlayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.PlayMode = ComboPlayMode.SelectedItem.ToString();
            });
        }


    }
}
