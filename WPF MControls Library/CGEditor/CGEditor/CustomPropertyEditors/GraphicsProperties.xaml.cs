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
using Xceed.Wpf.Toolkit;
using CGEditor.CGItemWrappers;
using CGEditor.Misc;

namespace CGEditor.CustomPropertyEditors
{
    /// <summary>
    /// Interaction logic for GraphicsProperties.xaml
    /// </summary>
    public partial class GraphicsProperties : UserControl
    {
        private CGGraphicsItem m_GraphicsItem;
        private bool bChangedByUI;

        public GraphicsProperties()
        {
            InitializeComponent();
            bChangedByUI = false;
            ComboBoxShapeType.ItemsSource = ObservableCollections.GetShapeTypes();
            CPColor0.SelectedColor = Colors.Transparent;
            CPColor1.SelectedColor = Colors.Transparent;
            CPColor2.SelectedColor = Colors.Transparent;
            bChangedByUI = true;
        }

        public void SetControlledObject(CGGraphicsItem graphicsItem)
        {
            try
            {
                m_GraphicsItem = graphicsItem;
                if (m_GraphicsItem != null)
                    FillValues();
                else
                    ClearControls();
            }
            catch (System.Exception) { }
        }

        private void ClearControls()
        {
            bChangedByUI = false;
            ComboBoxShapeType.SelectedItem = null;
            NumericSides.Value = null;
            CPColor0.SelectedColor = Colors.Transparent;
            CheckBoxGradient1.IsChecked = false;
            CPColor1.SelectedColor = Colors.Transparent;
            CheckBoxGradient2.IsChecked = false;
            CPColor2.SelectedColor = Colors.Transparent;
            NumericColorAngle.Value = null;
            CPOutlineColor.SelectedColor = Colors.Transparent;
            NumericOutlineWidth.Value = null;
            NumericRoundCorners.Value = null;
            NumericRotate.Value = null;
            NumericStarInset.Value = null;
            NumericWidth.Value = null;
            NumericHeight.Value = null;
            CheckBoxGamma.IsChecked = false;
            bChangedByUI = true;
        }

        private void FillValues()
        {
            bChangedByUI = false;
            if (m_GraphicsItem != null)
            {
                bChangedByUI = false;
                ComboBoxShapeType.SelectedItem = m_GraphicsItem.Type;
                NumericSides.Value = (decimal)m_GraphicsItem.Sides;

                bool bVerticalGradient;
                Color[] gradientColors = Helpers.parseCGradient(m_GraphicsItem.Color, out bVerticalGradient);
                if (gradientColors.Length > 0)
                    CPColor0.SelectedColor = gradientColors[0];

                if (gradientColors.Length > 1)
                {
                    CPColor1.SelectedColor = gradientColors[1];
                    CheckBoxGradient1.IsChecked = true;
                }
                if (gradientColors.Length > 2)
                {
                    CPColor2.SelectedColor = gradientColors[2];
                    CheckBoxGradient2.IsChecked = true;
                }
                NumericColorAngle.Value = m_GraphicsItem.ColorAngle;
                CPOutlineColor.SelectedColor = Helpers.ParseCGColor(m_GraphicsItem.OutlineColor);
                NumericOutlineWidth.Value = m_GraphicsItem.OutlineWidth;
                NumericRoundCorners.Value = m_GraphicsItem.RoundCorners * 100;
                NumericRotate.Value = m_GraphicsItem.Rotate;
                NumericStarInset.Value = m_GraphicsItem.StarInset;
                NumericWidth.Value = m_GraphicsItem.Width;
                NumericHeight.Value = m_GraphicsItem.Height;
                CheckBoxGamma.IsChecked = m_GraphicsItem.Gamma;
                bChangedByUI = true;
            }
            bChangedByUI = true;
        }

        private void SetCGGradient()
        {
            string CGColor = CPColor0.SelectedColor.ToString();
            if ((bool)CheckBoxGradient1.IsChecked && CPColor1.IsEnabled)
            {
                CGColor += "-" + CPColor1.SelectedColor.ToString();
            }
            if ((bool)CheckBoxGradient2.IsChecked && CPColor2.IsEnabled)
            {
                CGColor += "-" + CPColor2.SelectedColor.ToString();
            }

            m_GraphicsItem.Color = CGColor;
        }

        private void ComboBoxShapeType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Type = ComboBoxShapeType.SelectedItem.ToString();
        }

        private void NumericSides_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Sides = NumericSides.Value == null ? 3 : (int)NumericSides.Value;
        }

        private void CPColor0_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) SetCGGradient();
        }

        private void CheckBoxGradient1_Checked(object sender, RoutedEventArgs e)
        {
            CPColor1.IsEnabled = true;
        }

        private void CheckBoxGradient1_Unchecked(object sender, RoutedEventArgs e)
        {
            CPColor2.IsEnabled = false;
        }

        private void CPColor1_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) SetCGGradient();
        }

        private void CheckBoxGradient2_Checked(object sender, RoutedEventArgs e)
        {
            CPColor2.IsEnabled = true;
        }

        private void CheckBoxGradient2_Unloaded(object sender, RoutedEventArgs e)
        {
            CPColor2.IsEnabled = false;
        }

        private void CPColor2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) SetCGGradient();
        }

        private void NumericColorAngle_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.ColorAngle = NumericColorAngle.Value == null ? 0 : (double)NumericColorAngle.Value;
        }

        private void CPOutlineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.OutlineColor = CPOutlineColor.SelectedColor.ToString();
        }

        private void NumericOutlineWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.OutlineWidth = NumericOutlineWidth.Value == null ? 0 : (double)NumericOutlineWidth.Value;
        }

        private void NumericRoundCorners_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.RoundCorners = NumericRoundCorners.Value == null ? 0 : ((double)NumericRoundCorners.Value) / 100;
        }

        private void NumericRotate_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Rotate = NumericRotate.Value == null ? 0 : (double)NumericRotate.Value;
        }

        private void NumericStarInset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.StarInset = NumericStarInset.Value == null ? 0 : (double)NumericStarInset.Value;
        }

        private void NumericWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Width = NumericWidth.Value == null ? 0 : (double)NumericWidth.Value;
        }

        private void NumericHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Height = NumericHeight.Value == null ? 0 : (double)NumericHeight.Value;
        }

        private void CheckBoxGamma_Checked(object sender, RoutedEventArgs e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Gamma = true;
        }

        private void CheckBoxGamma_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_GraphicsItem != null && bChangedByUI) m_GraphicsItem.Gamma = false;
        }

    }
}
