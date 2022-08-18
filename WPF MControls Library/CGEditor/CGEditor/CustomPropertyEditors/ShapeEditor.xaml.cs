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
    public partial class ShapeEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IShape m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public ShapeEditor()
        {
            InitializeComponent();
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
            this.IsEnabled = false;
            ComboShapeType.ItemsSource = ObservableCollections.GetShapeTypes();
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IShape)
                {
                    m_Item = (IShape)item;
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
                ComboShapeType.SelectedItem = m_Item.Type;
                if (m_Item.Type == "polygon")
                {
                    SpinnerNSides.IsEnabled = true;
                    SpinnerNSides.Value = (double)m_Item.Sides;
                }
                else
                {
                    SpinnerNSides.IsEnabled = false;
                    SpinnerNSides.Value = 0;
                }
   
                SpinnerRoundCorners.Value = m_Item.RoundCorners * 100;
                SpinnerRotate.Value = m_Item.Rotate;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            ComboShapeType.SelectedItem = null;
            SpinnerNSides.Value = 0;
            SpinnerRoundCorners.Value = 0;
            SpinnerRotate.Value = 0;
            m_bChangedByUI = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "shapetype" || e.PropertyName == "nsides" || e.PropertyName == "round-corners" ||
                    e.PropertyName == "rotate" || e.PropertyName == "xml")
                    FillControls();
            }
        }

        delegate void Setter();
        private void SetProperty(Setter s)
        {
            UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ((CGBaseItem)m_Item).ID));
            if (m_Item != null && m_bChangedByUI)
            {
                m_bAceptExternalUpdate = false;
                s();
                m_bAceptExternalUpdate = true;
            }
        }

        //=========================Control handlers====================================

        private void ComboShapeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Type = ComboShapeType.SelectedItem.ToString();
                if (m_Item.Type == "polygon")
                {
                    SpinnerNSides.IsEnabled = true;
                    SpinnerNSides.Value = (double)m_Item.Sides;
                }
                else
                {
                    SpinnerNSides.IsEnabled = false;
                    SpinnerNSides.Value = 0;
                }
            });
        }

        private void SpinnerNSides_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.Sides = (int)SpinnerNSides.Value;
            });
        }

        private void SpinnerRoundCorners_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.RoundCorners = SpinnerRoundCorners.Value/100;
            });
        }

        private void SpinnerRotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.Rotate = SpinnerRotate.Value;
            });
        }
    }
}
