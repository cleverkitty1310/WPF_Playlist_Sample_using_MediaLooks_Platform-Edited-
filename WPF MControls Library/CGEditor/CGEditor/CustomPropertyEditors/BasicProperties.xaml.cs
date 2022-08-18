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
    public partial class BasicProperties : UserControl
    {
        private CGBaseItem m_BaseItem;
        private bool bChangedByUI;

        public BasicProperties()
        {
            InitializeComponent();
            bChangedByUI = false;
            ComboBoxScale.ItemsSource = ObservableCollections.GetScaleTypes();
            ComboBoxAlign.ItemsSource = ObservableCollections.GetAligns();
            ComboBoxInterlace.ItemsSource = ObservableCollections.GetInterlaceTypes();
            ComboBoxPlayMode.ItemsSource = ObservableCollections.GetPlayModes();
            ComboBoxMoveType.ItemsSource = ObservableCollections.GetMoveTypes();

            CPBGColor0.SelectedColor = Colors.Transparent;
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
            CheckBoxShow.IsChecked = false;
            NumericX.Value = null;
            NumericY.Value = null;
            NumericWidth.Value = null;
            NumericHeight.Value = null;
            SliderAlpha.Value = 255;
            CPBGColor0.SelectedColor = Colors.Transparent;
            ComboBoxScale.SelectedItem = null;
            ComboBoxAlign.SelectedItem = null;
            ComboBoxInterlace.SelectedItem = null;
            ComboBoxPlayMode.SelectedItem = null;
            ComboBoxMoveType.SelectedItem = null;
            NumericEdgeSmooth.Value = null;
            NumericPixelAR.Value = null;
            CheckBoxPause.IsChecked = false;
            bChangedByUI = true;
        }

        private void FillValues()
        {
            if (m_BaseItem != null)
            {
                bChangedByUI = false;
                if (m_BaseItem.Show)
                    CheckBoxShow.IsChecked = true;
                else
                    CheckBoxShow.IsChecked = false;

                NumericX.Value = m_BaseItem.PosX;
                NumericY.Value = m_BaseItem.PosY;
                NumericWidth.Value = m_BaseItem.Width;
                NumericHeight.Value = m_BaseItem.Height;
                SliderAlpha.Value = m_BaseItem.Alpha;
                CPBGColor0.SelectedColor = Helpers.ParseCGColor(m_BaseItem.BGColor);
                ComboBoxScale.SelectedItem = m_BaseItem.Scale;
                ComboBoxAlign.SelectedItem = m_BaseItem.Align;
                ComboBoxInterlace.SelectedItem = m_BaseItem.Interlace;
                ComboBoxPlayMode.SelectedItem = m_BaseItem.PlayMode;
                ComboBoxMoveType.SelectedItem = m_BaseItem.MoveType;
                NumericEdgeSmooth.Value = m_BaseItem.EdgesSmooth;
                NumericPixelAR.Value = m_BaseItem.PixelAR;

                if (m_BaseItem.Pause.ToLower() == "true" || m_BaseItem.Pause.ToLower() == "yes" || m_BaseItem.Pause.ToLower() == "1")
                    CheckBoxPause.IsChecked = true;
                else
                    CheckBoxPause.IsChecked = false;

                bChangedByUI = true;
            }
        }

        private void SetBGColor()
        {
            string strColor = CPBGColor0.SelectedColor.ToString();
            m_BaseItem.BGColor = strColor;
        }

        private void CheckBoxShow_Checked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Show = true;
        }

        private void CheckBoxShow_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Show = false;
        }

        private void NumericX_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.PosX = NumericX.Value == null ? 0 : (double)NumericX.Value;
        }

        private void NumericY_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.PosY = NumericY.Value == null ? 0 : (double)NumericY.Value;
        }

        private void NumericWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Width = NumericWidth.Value == null ? 0 : (double)NumericWidth.Value;
        }

        private void NumericHeight_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Height = NumericHeight.Value == null ? 0 : (double)NumericHeight.Value;
        }

        private void SliderAlpha_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Alpha = (int)SliderAlpha.Value;
        }

        private void CPBGColor0_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_BaseItem != null && bChangedByUI)  SetBGColor();
        }

        private void CPBGColor1_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_BaseItem != null && bChangedByUI)  SetBGColor();
        }

        private void CPBGColor2_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_BaseItem != null && bChangedByUI) SetBGColor();
        }

        private void ComboBoxScale_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Scale = ComboBoxScale.SelectedItem.ToString();
        }

        private void ComboBoxAlign_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Align= ComboBoxAlign.SelectedItem.ToString();
        }

        private void ComboBoxInterlace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Interlace = ComboBoxInterlace.SelectedItem.ToString();
        }

        private void ComboBoxPlayMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.PlayMode = ComboBoxPlayMode.SelectedItem.ToString();
        }

        private void ComboBoxMoveType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.MoveType = ComboBoxMoveType.SelectedItem.ToString();
        }

        private void NumericEdgeSmooth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.EdgesSmooth = NumericEdgeSmooth.Value == null ? 0 : (double)NumericEdgeSmooth.Value;
        }

        private void NumericPixelAR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.PixelAR = NumericPixelAR.Value == null ? 0 : (double)NumericPixelAR.Value;
        }

        private void CheckBoxPause_Checked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Pause= "yes";
        }

        private void CheckBoxPause_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_BaseItem != null && bChangedByUI) m_BaseItem.Pause = "no";
        }
    }
}
