using System;
using System.Collections.ObjectModel;
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
    /// Interaction logic for BasicProperties.xaml
    /// </summary>
    public partial class EffectsProperties : UserControl
    {
        private CGBaseItem m_BaseItem;
        private bool bChangedByUI;

        public EffectsProperties()
        {
            InitializeComponent();
            bChangedByUI = false;
            ComboBoxBlurAlign.ItemsSource = ObservableCollections.GetAligns();
            CPShadowColor.SelectedColor = Colors.Transparent;
            bChangedByUI = true;
        }

        public void SetControlledObject(CGBaseItem baseItem)
        {
            try
            {
                m_BaseItem = baseItem;
                if (baseItem != null)
                    FillValues();
                else
                    ClearControls();
            }
            catch (System.Exception) { }
        }

        private void ClearControls()
        {
            bChangedByUI = false;
            SliderSpeedX.Value = 0;
            SliderSpeedY.Value = 0;

            CheckBoxShadow.IsChecked = false;
            NumericShadowBlur.Value = null;
            SliderShadowAlpha.Value = 0;
            NumericShadowXOffset.Value = null;
            NumericShadowYOffset.Value = null;
            CPShadowColor.SelectedColor = Colors.Transparent;

            CheckBoxBlur.IsChecked = false;
            NumericBlurXSize.Value = null;
            NumericBlurYSize.Value = null;
            ComboBoxBlurAlign.SelectedItem = null;

            CheckBoxGlow.IsChecked = false;
            NumericGlowXSize.Value = null;
            NumericGlowYSize.Value = null;
            NumericGlowValue.Value = null;

            bChangedByUI = true;
        }

        private void FillValues()
        {
            if (m_BaseItem != null)
            {
                try
                {
                    bChangedByUI = false;
                    SliderSpeedX.Value = m_BaseItem.SpeedX;
                    SliderSpeedY.Value = m_BaseItem.SpeedY;

                    CheckBoxShadow.IsChecked = m_BaseItem.ShadowEnable;
                    NumericShadowBlur.Value = m_BaseItem.ShadowBlur;
                    SliderShadowAlpha.Value = m_BaseItem.ShadowAlpha;
                    NumericShadowXOffset.Value = m_BaseItem.ShadowXOffset;
                    NumericShadowYOffset.Value = m_BaseItem.ShadowYOffset;
                    CPShadowColor.SelectedColor = Helpers.ParseCGColor(m_BaseItem.ShadowColor);

                    CheckBoxBlur.IsChecked = m_BaseItem.BlurEnable;
                    NumericBlurXSize.Value = m_BaseItem.BlurSizeX;
                    NumericBlurYSize.Value = m_BaseItem.BlurSizeY;
                    ComboBoxBlurAlign.SelectedItem = m_BaseItem.BlurAlign;

                    CheckBoxGlow.IsChecked = m_BaseItem.GlowEnable;
                    NumericGlowXSize.Value = m_BaseItem.GlowSizeX;
                    NumericGlowYSize.Value = m_BaseItem.GlowSizeY;
                    NumericGlowValue.Value = m_BaseItem.GlowValue;

                    bChangedByUI = true;
                }
                catch (Exception ex)
                {

                }
            }
        }

        private void SliderSpeedX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.SpeedX = (int)SliderSpeedX.Value;
        }

        private void SliderSpeedY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.SpeedY = (int)SliderSpeedY.Value;
        }

        private void CheckBoxShadow_Checked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI)
            {
                SliderShadowAlpha.IsEnabled = true;
                NumericShadowBlur.IsEnabled = true;
                NumericShadowXOffset.IsEnabled = true;
                NumericShadowYOffset.IsEnabled = true;
                CPShadowColor.IsEnabled = true;
                m_BaseItem.ShadowEnable = true;
            }
        }

        private void CheckBoxShadow_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI)
            {
                SliderShadowAlpha.IsEnabled = false;
                NumericShadowBlur.IsEnabled = false;
                NumericShadowXOffset.IsEnabled = false;
                NumericShadowYOffset.IsEnabled = false;
                CPShadowColor.IsEnabled = false;
                m_BaseItem.ShadowEnable = false;
            }
        }

        private void NumericShadowBlur_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxShadow.IsEnabled == true)
            {
                m_BaseItem.ShadowBlur = NumericShadowBlur.Value == null ? 0 : (double)NumericShadowBlur.Value;
            }
        }

        private void SliderShadowAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxShadow.IsEnabled == true)
            {
                m_BaseItem.ShadowAlpha = (int)SliderShadowAlpha.Value;
            }
        }

        private void NumericShadowXOffset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxShadow.IsEnabled == true)
            {
                m_BaseItem.ShadowXOffset = NumericShadowXOffset.Value == null ? 0 : (double)NumericShadowXOffset.Value;
            }
        }

        private void NumericShadowYOffset_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxShadow.IsEnabled == true)
            {
                m_BaseItem.ShadowYOffset = NumericShadowYOffset.Value == null ? 0 : (double)NumericShadowYOffset.Value;
            }
        }

        private void CPShadowColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxShadow.IsEnabled == true)
            {
                m_BaseItem.ShadowColor = CPShadowColor.SelectedColor.ToString();
            }
        }

        private void CheckBoxBlur_Checked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI)
            {
                NumericBlurXSize.IsEnabled = true;
                NumericBlurYSize.IsEnabled = true;
                ComboBoxBlurAlign.IsEnabled = true;
                m_BaseItem.BlurEnable = true;
            }
        }

        private void CheckBoxBlur_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI)
            {
                NumericBlurXSize.IsEnabled = false;
                NumericBlurYSize.IsEnabled = false;
                ComboBoxBlurAlign.IsEnabled = false;
                m_BaseItem.BlurEnable = false;
            }
        }

        private void NumericBlurXSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxBlur.IsEnabled == true)
            {
                m_BaseItem.BlurSizeX = NumericBlurXSize.Value == null ? 0 : (double)NumericBlurXSize.Value;
            }
        }

        private void NumericBlurYSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxBlur.IsEnabled == true)
            {
                m_BaseItem.BlurSizeY = NumericBlurYSize.Value == null ? 0 : (double)NumericBlurYSize.Value;
            }
        }

        private void ComboBoxBlurAlign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxBlur.IsEnabled == true)
            {
                m_BaseItem.BlurAlign = ComboBoxBlurAlign.SelectedItem.ToString();
            }
        }

        private void CheckBoxGlow_Checked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI)
            {
                NumericGlowXSize.IsEnabled = true;
                NumericGlowYSize.IsEnabled = true;
                NumericGlowValue.IsEnabled = true;
                m_BaseItem.GlowEnable= true;
            }
        }

        private void CheckBoxGlow_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI)
            {
                NumericGlowXSize.IsEnabled = false;
                NumericGlowYSize.IsEnabled = false;
                NumericGlowValue.IsEnabled = false;
                m_BaseItem.GlowEnable = false;
            }
        }

        private void NumericGlowXSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxGlow.IsEnabled == true)
            {
                m_BaseItem.GlowSizeX = NumericGlowXSize.Value == null ? 0 : (double)NumericGlowXSize.Value;
            }
        }

        private void NumericGlowYSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxGlow.IsEnabled == true)
            {
                m_BaseItem.GlowSizeY = NumericGlowYSize.Value == null ? 0 : (double)NumericGlowYSize.Value;
            }
        }

        private void NumericGlowValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI && CheckBoxGlow.IsEnabled == true)
            {
                m_BaseItem.GlowValue = NumericGlowValue.Value == null ? 0 : (double)NumericGlowValue.Value;
            }
        }

        
    }
}
