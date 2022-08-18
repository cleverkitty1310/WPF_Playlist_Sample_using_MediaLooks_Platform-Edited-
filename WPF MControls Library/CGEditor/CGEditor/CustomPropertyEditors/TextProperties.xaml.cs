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
    public partial class TextProperties : UserControl
    {
        private CGTextItem m_TextItem;
        private bool bChangedByUI;

        public TextProperties()
        {
            InitializeComponent();
            bChangedByUI = false;
            FillFontFamilies();
            ComboBoxFontSize.ItemsSource = ObservableCollections.GetDefaultFontSizes();
            ComboBoxFlip.ItemsSource = ObservableCollections.GetFlipModes();
            CPTextColor.SelectedColor = Colors.Transparent;
            CPTextOutlineColor.SelectedColor = Colors.Transparent;
            bChangedByUI = true;
        }

        private void FillFontFamilies()
        {
            ObservableCollection<string> fontFamilies = new ObservableCollection<string>();
            foreach (FontFamily ff in Fonts.SystemFontFamilies)
            {
                fontFamilies.Add(ff.ToString());
            }
            fontFamilies = new ObservableCollection<string>(fontFamilies.OrderBy(i => i));
            ComboBoxFontFamily.ItemsSource = fontFamilies;
        }

        private void FillFontStyles()
        {
            FontFamily ff = new FontFamily(m_TextItem.FontFamily);

            ObservableCollection<string> fontTypefaces = new ObservableCollection<string>();
            foreach (FamilyTypeface ft in ff.FamilyTypefaces)
            {
                System.Windows.Markup.XmlLanguage lang = System.Windows.Markup.XmlLanguage.GetLanguage("en-us");
                if (ft.AdjustedFaceNames.ContainsKey(lang))
                {
                    fontTypefaces.Add(ft.AdjustedFaceNames[lang]);
                }
            }
            if (fontTypefaces.Count > 0)
            {
                bChangedByUI = false;
                ComboBoxFontTypeFace.SelectedItem = null;
                ComboBoxFontTypeFace.ItemsSource = fontTypefaces;
                bChangedByUI = true;
            }
        }

        public void SetControlledObject(CGTextItem textItem)
        {
            //CGTextItem pOld = m_TextItem;
            try
            {
                m_TextItem = textItem;
                if (m_TextItem != null)
                    FillValues();
                else
                    ClearControls();
            }
            catch (System.Exception) { }
            //return pOld;
        }

        private void ClearControls()
        {
            bChangedByUI = false;
            TextBoxText.Text = "";
            CPTextColor.SelectedColor = Colors.Transparent;
            CPTextOutlineColor.SelectedColor = Colors.Transparent;
            NumericOutlineWidth.Value = null;
            ComboBoxFontFamily.SelectedItem = null;
            ComboBoxFontTypeFace.SelectedItem = null;
            ComboBoxFontSize.SelectedItem = null;
            ComboBoxFlip.SelectedItem = null;
            CheckBoxUnderline.IsChecked = false;
            CheckBoxStrikeout.IsChecked = false;
            CheckBoxWordWrap.IsChecked = false;
            CheckBoxVertical.IsChecked = false;
            CheckBoxRTL.IsChecked = false;
            CheckBoxNoTabs.IsChecked = false;
            bChangedByUI = true;
        }

        private void FillValues()
        {
            bChangedByUI = false;
            if (m_TextItem != null)
            {
                TextBoxText.Text = m_TextItem.Text;
                CPTextColor.SelectedColor = Helpers.ParseCGColor(m_TextItem.Color);
                CPTextOutlineColor.SelectedColor = Helpers.ParseCGColor(m_TextItem.OutlineColor);
                NumericOutlineWidth.Value = m_TextItem.OutlineWidth;
                ComboBoxFontFamily.SelectedItem = m_TextItem.FontFamily;
                ComboBoxFontTypeFace.SelectedItem = m_TextItem.FontTypeface;
                ComboBoxFontSize.Text = m_TextItem.FontSize.ToString();
                ComboBoxFlip.SelectedItem = m_TextItem.Flip;
                CheckBoxUnderline.IsChecked = m_TextItem.Underline;
                CheckBoxStrikeout.IsChecked = m_TextItem.StrikeOut;
                CheckBoxWordWrap.IsChecked = m_TextItem.WordWrap;
                CheckBoxVertical.IsChecked = m_TextItem.Vertical;
                CheckBoxRTL.IsChecked = m_TextItem.RightToLeft;
                CheckBoxNoTabs.IsChecked = m_TextItem.NoTabs;
            }
            bChangedByUI = true;
        }

        private void TextBoxText_TextChanged(object sender, TextChangedEventArgs e)
        {
            string strNew = TextBoxText.Text;
            strNew = strNew.Replace("\r\n", "<br/>");
            if (m_TextItem != null && bChangedByUI) m_TextItem.Text = strNew;
        }

        private void CPTextColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.Color = CPTextColor.SelectedColor.ToString();
        }

        private void CPTextOutlineColor_SelectedColorChanged(object sender, RoutedPropertyChangedEventArgs<Color> e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.OutlineColor = CPTextOutlineColor.SelectedColor.ToString();
        }

        private void NumericOutlineWidth_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.OutlineWidth = NumericOutlineWidth.Value == null ? 0 : (double)NumericOutlineWidth.Value;
        }

        private void ComboBoxFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI)
            {
                m_TextItem.FontTypeface = "";
                m_TextItem.FontFamily = ComboBoxFontFamily.SelectedItem.ToString();
                FillFontStyles();
            }
            
        }

        private void ComboBoxFontTypeFace_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.FontTypeface = ComboBoxFontTypeFace.SelectedItem.ToString();
        }

        private void ComboBoxFontSize_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            double dblRes = 0;
            bool bSuccess = Double.TryParse(ComboBoxFontSize.Text, out dblRes);
            if (m_TextItem != null && bChangedByUI && bSuccess)
            {
                    m_TextItem.FontSize = dblRes;
            }
        }

        private void ComboBoxFlip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.Flip = ComboBoxFlip.SelectedItem.ToString();
        }

        private void CheckBoxUnderline_Checked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.Underline = true;
        }

        private void CheckBoxUnderline_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.Underline = false;
        }

        private void CheckBoxStrikeout_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.StrikeOut = false;
        }

        private void CheckBoxStrikeout_Checked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.StrikeOut = true;
        }

        private void CheckBoxWordWrap_Checked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.WordWrap = true;
        }

        private void CheckBoxWordWrap_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.WordWrap = false;
        }

        private void CheckBoxVertical_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.Vertical = false;
        }

        private void CheckBoxVertical_Checked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.Vertical = true;
        }

        private void CheckBoxRTL_Checked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.RightToLeft = true;
        }

        private void CheckBoxRTL_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.RightToLeft = false;
        }

        private void CheckBoxNoTabs_Unchecked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.NoTabs = false;
        }

        private void CheckBoxNoTabs_Checked(object sender, RoutedEventArgs e)
        {
            if (m_TextItem != null && bChangedByUI) m_TextItem.NoTabs = true;
        }
    }
}
