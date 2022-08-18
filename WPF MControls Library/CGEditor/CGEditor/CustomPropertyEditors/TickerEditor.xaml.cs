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
    public partial class TickerEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private ITicker m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public TickerEditor()
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
                if (item != null && item is ITicker)
                {
                    m_Item = (ITicker)item;
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
                SpinnerSpeed.Value = m_Item.Speed;
                SpinnerLineHeight.Value = m_Item.LineHeight;
                SpinnerDistance.Value = m_Item.Distance;
                ToggleGapless.IsChecked = m_Item.Gapless;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            SpinnerSpeed.Value = 0;
            SpinnerLineHeight.Value = 0;
            SpinnerDistance.Value = 0;
            ToggleGapless.IsChecked = false;
            m_bChangedByUI = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "ticker-speed" || e.PropertyName == "gaplesscrawl" || e.PropertyName == "ticker-line-height" ||
                    e.PropertyName == "ticker-distance" || e.PropertyName == "xml")
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
        private void SpinnerSpeed_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.Speed = SpinnerSpeed.Value;
            });
        }

        private void SpinnerLineHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.LineHeight = SpinnerLineHeight.Value;
            });
        }

        private void SpinnerDistance_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.Distance = SpinnerDistance.Value;
            });
        }

        private void ToggleGapless_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Gapless = true;
            });
        }

        private void ToggleGapless_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Gapless = false;
            });
        }

        private void ToggleRewind_Click(object sender, RoutedEventArgs e)
        {
            MLCHARGENLib.CoMLCharGen cg = CGBaseItem.m_objCG;
            CGBaseItem baseItem = (CGBaseItem)m_Item;
            cg.RewindItem(baseItem.ID);
        }
    }
}
