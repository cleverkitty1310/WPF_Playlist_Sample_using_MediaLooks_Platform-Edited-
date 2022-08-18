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
using CGEditor.CGItemWrappers;

namespace CGEditor.CustomPropertyEditors
{
    public partial class FontEditor : UserControl
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IFont m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }

        public FontEditor()
        {
            InitializeComponent();
            m_bChangedByUI = false;
            FillFontFamilies();

            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;

            this.IsEnabled = false;
            //Set collections
            ComboFlip.ItemsSource = ObservableCollections.GetFlipModes();
            ComboType.ItemsSource = ObservableCollections.GetTextTypes(); 
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IGradient)
                {
                    m_Item = (IFont)item;
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
                ComboFontFamily.SelectedItem = m_Item.FontFamily;

                FillFontTypeFaces();
                ComboFontTypeface.SelectedItem = m_Item.FontTypeface;

                SpinnerFontSize.Value = m_Item.FontSize;

                ToggleUnderline.IsChecked = m_Item.Underline;
                ToggleStrikeout.IsChecked = m_Item.StrikeOut;
                ToggleWordWrap.IsChecked = m_Item.WordWrap;
                ToggleVertical.IsChecked = m_Item.Vertical;
                ToggleRightToLeft.IsChecked = m_Item.RightToLeft;
                ToggleNoTabs.IsChecked = m_Item.NoTabs;

                ComboFlip.SelectedItem = m_Item.Flip;
                ComboType.SelectedItem = m_Item.Type;

                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            ComboFontFamily.SelectedItem = null;
            ComboFontTypeface.SelectedItem = null;
            SpinnerFontSize.Value = 0;
            ToggleUnderline.IsChecked = false;
            ToggleStrikeout.IsChecked = false;
            ToggleWordWrap.IsChecked = false;
            ToggleVertical.IsChecked = false;
            ToggleRightToLeft.IsChecked = false;
            ToggleNoTabs.IsChecked = false;
            ComboFlip.SelectedItem = null;
            ComboType.SelectedItem = null;

            m_bChangedByUI = true;
        }

        private void FillFontFamilies()
        {
            ObservableCollection<string> fontFamilies = new ObservableCollection<string>();
            foreach (FontFamily ff in Fonts.SystemFontFamilies)
            {
                fontFamilies.Add(ff.ToString());
            }
            fontFamilies = new ObservableCollection<string>(fontFamilies.OrderBy(i => i));
            ComboFontFamily.ItemsSource = fontFamilies;
        }

        private void FillFontTypeFaces()
        {
            if (ComboFontFamily.SelectedItem != null && ComboFontFamily.SelectedItem.ToString() != string.Empty)
            {
                ComboFontTypeface.SelectedItem = null;
                FontFamily ff = new FontFamily(ComboFontFamily.SelectedItem.ToString());
                ObservableCollection<string> fontTypefaces = new ObservableCollection<string>();
                fontTypefaces.Add("");
                foreach (FamilyTypeface ft in ff.FamilyTypefaces)
                {
                    System.Windows.Markup.XmlLanguage lang = System.Windows.Markup.XmlLanguage.GetLanguage("en-us");
                    if (ft.AdjustedFaceNames.ContainsKey(lang))
                    {
                        if (!ft.AdjustedFaceNames[lang].ToLower().Contains("regular") &&
                            !ft.AdjustedFaceNames[lang].ToLower().Contains("oblique"))
                        {
                            fontTypefaces.Add(ft.AdjustedFaceNames[lang]);
                        }
                    }
                }
                if (fontTypefaces.Count > 0)
                {
                    ComboFontTypeface.ItemsSource = fontTypefaces;
                }
            }
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if ( m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "font" || e.PropertyName == "font-size" || e.PropertyName == "word-break"
                    || e.PropertyName == "textformat" || e.PropertyName == "textflip" || e.PropertyName == "texttype" || e.PropertyName == "xml")
                {
                    FillControls();
                }
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

        private void ComboFontFamily_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboFontFamily.SelectedItem != null)
            {
                SetProperty(delegate
                {
                    m_Item.FontFamily = ComboFontFamily.SelectedItem.ToString(); 
                    FillFontTypeFaces();
                });
                
            }
        }

        private void SpinnerFontSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            SetProperty(delegate
            {
                m_Item.FontSize = SpinnerFontSize.Value;
            });
        }

        private void ComboFontTypeface_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboFontTypeface.SelectedItem != null)
            {
                SetProperty(delegate
                {
                    m_Item.FontTypeface = ComboFontTypeface.SelectedItem.ToString();
                });
            }
        }

        private void ToggleUnderline_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate 
            {
                m_Item.Underline = true;
            });
        }

        private void ToggleUnderline_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Underline = false;
            });
        }

        private void ToggleStrikeout_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.StrikeOut = true;
            });
        }

        private void ToggleStrikeout_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.StrikeOut = false;
            });
        }

        private void ToggleWordWrap_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.WordWrap = true;
            });
        }

        private void ToggleWordWrap_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.WordWrap = false;
            });
        }

        private void ToggleVertical_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Vertical = true;
            });
        }

        private void ToggleVertical_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.Vertical = false;
            });
        }

        private void ToggleRightToLeft_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.RightToLeft = true;
            });
        }

        private void ToggleRightToLeft_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.RightToLeft = false;
            });
        }

        private void ToggleNoTabs_Checked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.NoTabs = true;
            });
        }

        private void ToggleNoTabs_Unchecked(object sender, RoutedEventArgs e)
        {
            SetProperty(delegate
            {
                m_Item.NoTabs = false;
            });
        }

        private void ComboFlip_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboFlip.SelectedItem != null)
            {
                SetProperty(delegate
                {
                    m_Item.Flip = ComboFlip.SelectedItem.ToString();
                });
            }
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ComboType.SelectedItem != null)
            {
                SetProperty(delegate
                {
                    m_Item.Type = ComboType.SelectedItem.ToString();
                });
            }
        }
    }
}
