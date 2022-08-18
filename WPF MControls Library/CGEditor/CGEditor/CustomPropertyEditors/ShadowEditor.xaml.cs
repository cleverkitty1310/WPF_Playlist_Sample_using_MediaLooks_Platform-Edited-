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
    public partial class ShadowEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IShadow m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public ShadowEditor()
        {
            InitializeComponent();
            m_bChangedByUI = false;
            CPShadowColor.SelectedColor = Colors.Transparent;
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
            this.IsEnabled = false;
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IShadow)
                {
                    m_Item = (IShadow)item;
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
                ToggleShadow.IsChecked = m_Item.ShadowEnable;
                if ((bool)ToggleShadow.IsChecked)
                    EnableControls();
                else
                    DisableControls();
                SliderShadowAlpha.Value = m_Item.ShadowAlpha;
                SpinnerShadowBlur.Value = m_Item.ShadowBlur;
                SpinnerOffsetX.Value = m_Item.ShadowXOffset;
                SpinnerOffsetY.Value = m_Item.ShadowYOffset;
                CPShadowColor.SelectedColor = WrapperHelpers.ParseCGColor(m_Item.ShadowColor);
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            ToggleShadow.IsChecked = false;
            SliderShadowAlpha.Value = 255;
            SpinnerShadowBlur.Value = 0;
            SpinnerOffsetX.Value = 0;
            SpinnerOffsetY.Value = 0;
            CPShadowColor.SelectedColor = Colors.Transparent;
            m_bChangedByUI = true;
        }

        private void DisableControls()
        {
            SpinnerShadowBlur.IsEnabled = false;
            SpinnerOffsetX.IsEnabled = false;
            SpinnerOffsetY.IsEnabled = false;
            SliderShadowAlpha.IsEnabled = false;
            CPShadowColor.IsEnabled = false;
        }
        private void EnableControls()
        {
            SpinnerShadowBlur.IsEnabled = true;
            SpinnerOffsetX.IsEnabled = true;
            SpinnerOffsetY.IsEnabled = true;
            SliderShadowAlpha.IsEnabled = true;
            CPShadowColor.IsEnabled = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "shadow-enabled" || e.PropertyName == "shadow-blur" || e.PropertyName == "shadow-alpha" ||
                    e.PropertyName == "shadow-offset-x" || e.PropertyName == "shadow-offset-y" || e.PropertyName == "shadow-color" || e.PropertyName == "xml")
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
        private void ToggleShadow_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowEnable = true;
                EnableControls();
            });
        }

        private void ToggleShadow_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowEnable = false;
                DisableControls();
            });
        }

        private void SpinnerShadowBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowBlur = SpinnerShadowBlur.Value;
            });
        }

        private void CPShadowColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowColor = CPShadowColor.SelectedColor.ToString();
            });
        }

        private void SpinnerOffsetX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowXOffset = SpinnerOffsetX.Value;
            });
        }

        private void SpinnerOffsetY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowYOffset = SpinnerOffsetY.Value;
            });
        }

        private void SliderShadowAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.ShadowAlpha = (int)SliderShadowAlpha.Value;
            });
        }

    }
}
