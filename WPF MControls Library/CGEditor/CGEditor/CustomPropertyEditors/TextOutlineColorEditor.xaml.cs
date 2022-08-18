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
    public partial class TextOutlineColorEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private ITextOutline m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public int nMaxGradients = 4; //Max number of gradients in control

        public TextOutlineColorEditor()
        {
            InitializeComponent();
            m_bChangedByUI = false;
            CPColor0.SelectedColor = Colors.Transparent;
            m_bChangedByUI = true;

            m_bAceptExternalUpdate = true;

            this.IsEnabled = false;
            ComboGradientType.ItemsSource = ObservableCollections.GetGradientTypes();
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IOutline)
                {
                    m_Item = (ITextOutline)item;
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
                GetColor();
                GetOutlineWidth();
                m_bChangedByUI = true;
            }
        }

        private void AddColorPicker(Color startColor)
        {
            Xceed.Wpf.Toolkit.ColorPicker cPicker = new Xceed.Wpf.Toolkit.ColorPicker();
            cPicker.Width = 38;
            cPicker.Margin = new Thickness(2);
            cPicker.SelectedColor = startColor;
            cPicker.ColorPickerDisabled += new EventHandler(cp_ColorPickerDisabled);
            cPicker.SelectedColorChanged += new RoutedPropertyChangedEventHandler<Color>(SelectedColorChanged);
            StackColors.Children.Add(cPicker);
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            ComboGradientType.SelectedItem = null;
            SpinnerOutlineWidth.Value = 0;
            CPColor0.SelectedColor = Colors.Transparent;
            //Remove ColorPickers
            for (int i = StackColors.Children.Count - 1; i >= 0; i-- )
            {
                UIElement element = StackColors.Children[i];
                if (element is Xceed.Wpf.Toolkit.ColorPicker && element != CPColor0)
                {
                    StackColors.Children.Remove(element);
                }
            }
            m_bChangedByUI = true;
        }

        private void GetColor()
        {
            //Get gradient colors
            string strGradientType;
            string strColor = m_Item.TextOutlineColor;

            Color[] gradientColors = WrapperHelpers.parseCGradient(strColor, out strGradientType);
            ComboGradientType.SelectedItem = strGradientType;

            if (gradientColors != null && gradientColors.Length > 0)
            {
                for (int i = 0; i < gradientColors.Length; i++)
                {
                    if (i == 0)
                        CPColor0.SelectedColor = gradientColors[0];
                    else
                    {
                        AddColorPicker(gradientColors[i]);
                    }
                }
            }
        }

        private void GetOutlineWidth()
        {
            SpinnerOutlineWidth.Value = m_Item.TextOutlineWidth;
        }

        private void SetColor()
        {
            if (m_Item != null && m_bChangedByUI)
            {
                Color[] colors = new Color[StackColors.Children.Count];
                for (int i = 0; i < StackColors.Children.Count; i++)
                {
                    if (StackColors.Children[i] is Xceed.Wpf.Toolkit.ColorPicker)
                    {
                        Xceed.Wpf.Toolkit.ColorPicker cp = (Xceed.Wpf.Toolkit.ColorPicker)StackColors.Children[i];
                        colors[i] = cp.SelectedColor;
                    }
                }
                string strGrType = ComboGradientType.SelectedItem.ToString();
                string strCGGradient = WrapperHelpers.ComposeCGGradient(colors, strGrType);

                UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ((CGBaseItem)m_Item).ID));

                m_bAceptExternalUpdate = false;
                m_Item.TextOutlineColor = strCGGradient;
                m_bAceptExternalUpdate = true;
            }
        }

        private void SetOutlineWidth()
        {
            if (m_Item != null && m_bChangedByUI)
            {
                UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ((CGBaseItem)m_Item).ID));
                double dblWidth = SpinnerOutlineWidth.Value;
                m_bAceptExternalUpdate = false;
                m_Item.TextOutlineWidth = dblWidth;
                m_bAceptExternalUpdate = true;
            }
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "textoutline-color")
                    GetColor();
                else if (e.PropertyName == "textoutline")
                    GetOutlineWidth();
                else if (e.PropertyName == "xml")
                    FillControls();
            }
        }

        //=========================Control handlers====================================
        private void buttonAddGradient_Click(object sender, RoutedEventArgs e)
        {
            if (StackColors.Children.Count < nMaxGradients)
            {
                m_bAceptExternalUpdate = false;
                AddColorPicker(Colors.Transparent);
                SetColor();
                m_bAceptExternalUpdate = true;
            }
        }

        void cp_ColorPickerDisabled(object sender, EventArgs e)
        {
            Xceed.Wpf.Toolkit.ColorPicker cp = (Xceed.Wpf.Toolkit.ColorPicker)sender;
            StackColors.Children.Remove(cp);
            SetColor();
        }

        private void SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            SetColor();
        }

        private void ComboGradientType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SetColor();
        }

        private void SpinnerGradientAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetOutlineWidth();
        }
    }
}
