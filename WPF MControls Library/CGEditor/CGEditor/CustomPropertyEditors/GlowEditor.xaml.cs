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
    public partial class GlowEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IGlow m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public GlowEditor()
        {
            InitializeComponent();
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
            this.IsEnabled = false;
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IGlow)
                {
                    m_Item = (IGlow)item;
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
                ToggleGlow.IsChecked = m_Item.GlowEnable;
                if ((bool)ToggleGlow.IsChecked)
                    EnableControls();
                else
                    DisableControls();

                SpinnerGlowVlue.Value = m_Item.GlowValue;
                SpinnerGlowSizeX.Value = m_Item.GlowSizeX;
                SpinnerGlowSizeY.Value = m_Item.GlowSizeY;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            SpinnerGlowVlue.Value = 0;
            SpinnerGlowSizeX.Value = 0;
            SpinnerGlowSizeY.Value = 0;
            m_bChangedByUI = true;
        }

        private void DisableControls()
        {
            SpinnerGlowVlue.IsEnabled = false;
            SpinnerGlowSizeX.IsEnabled = false;
            SpinnerGlowSizeY.IsEnabled = false;
        }
        private void EnableControls()
        {
            SpinnerGlowVlue.IsEnabled = true;
            SpinnerGlowSizeX.IsEnabled = true;
            SpinnerGlowSizeY.IsEnabled = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "glow-enabled" || e.PropertyName == "glow-size-x" || e.PropertyName == "glow-size-y" ||
                    e.PropertyName == "glow-value" || e.PropertyName == "xml")
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
        private void ToggleGlow_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.GlowEnable = true;
                EnableControls();
            });
        }

        private void ToggleGlow_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.GlowEnable = false;
                DisableControls();
            });
        }

        private void SpinnerGlowSizeX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.GlowSizeX = SpinnerGlowSizeX.Value;
            });
        }

        private void SpinnerGlowSizeY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.GlowSizeY = SpinnerGlowSizeY.Value;
            });
        }

        private void SpinnerGlowVlue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.GlowValue = SpinnerGlowVlue.Value;
            });
        }
    }
}
