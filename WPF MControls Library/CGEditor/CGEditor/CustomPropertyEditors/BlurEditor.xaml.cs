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
    public partial class BlurEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IBlur m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }

        public BlurEditor()
        {
            InitializeComponent();
            m_bChangedByUI = false;
            ComboBlurAlign.ItemsSource = ObservableCollections.GetAligns();
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
            this.IsEnabled = false;
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IBlur)
                {
                    m_Item = (IBlur)item;
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
                ToggleBlur.IsChecked = m_Item.BlurEnable;
                if ((bool)ToggleBlur.IsChecked)
                    EnableControls();
                else
                    DisableControls();

                ComboBlurAlign.SelectedItem = m_Item.BlurAlign;
                SpinnerBlurSizeX.Value = m_Item.BlurSizeX;
                SpinnerBlurSizeY.Value = m_Item.BlurSizeY;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            ComboBlurAlign.SelectedItem = null;
            SpinnerBlurSizeX.Value = 0;
            SpinnerBlurSizeY.Value = 0;
            m_bChangedByUI = true;
        }

        private void DisableControls()
        {
            ComboBlurAlign.IsEnabled = false;
            SpinnerBlurSizeX.IsEnabled = false;
            SpinnerBlurSizeY.IsEnabled = false;
        }
        private void EnableControls()
        {
            ComboBlurAlign.IsEnabled = true;
            SpinnerBlurSizeX.IsEnabled = true;
            SpinnerBlurSizeY.IsEnabled = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "blur-enabled" || e.PropertyName == "blur-size-x" || e.PropertyName == "blur-size-y" ||
                    e.PropertyName == "blur-align" || e.PropertyName == "xml")
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
        private void ToggleBlur_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.BlurEnable = true;
                EnableControls();
            });
        }

        private void ToggleBlur_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.BlurEnable = false;
                DisableControls();
            });
        }

        private void ComboBlurAlign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.BlurAlign = ComboBlurAlign.SelectedItem.ToString();
            });
        }

        private void SpinnerBlurSizeX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.BlurSizeX = SpinnerBlurSizeX.Value;
            });
        }

        private void SpinnerBlurSizeY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.BlurSizeY = SpinnerBlurSizeY.Value;
            });
        }
    }
}
