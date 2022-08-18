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
    public partial class PositionEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IPosition m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public PositionEditor()
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
                if (item != null && item is IPosition)
                {
                    m_Item = (IPosition)item;
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
                    m_Item = null;
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
                SpinnerXPos.Value = m_Item.PosX;
                SpinnerYPos.Value = m_Item.PosY;
                SpinnerWidth.Value = m_Item.Width;
                SpinnerHeight.Value = m_Item.Height;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            SpinnerXPos.Value = 0;
            SpinnerYPos.Value = 0;
            SpinnerWidth.Value = 0;
            SpinnerHeight.Value = 0;
            m_bChangedByUI = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "posx" || e.PropertyName == "posx" ||
                    e.PropertyName == "width" || e.PropertyName == "height" || e.PropertyName == "xml")
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
        private void SpinnerXPos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.PosX = SpinnerXPos.Value;
            });
        }

        private void SpinnerYPos_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.PosY = SpinnerYPos.Value;
            });
        }

        private void SpinnerWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.Width = SpinnerWidth.Value;
            });
        }

        private void SpinnerHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.Height = SpinnerHeight.Value;
            });
        }
    }
}
