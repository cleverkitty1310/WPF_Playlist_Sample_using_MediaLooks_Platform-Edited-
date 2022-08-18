using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using CGEditor.Misc;
using MLCHARGENLib;

namespace CGEditor.CustomPropertyEditors
{
    /// <summary>
    /// Interaction logic for RibbonControl.xaml
    /// </summary>
    public partial class RibbonControl : UserControl
    {
        private CGBaseItem m_BaseItem;

        public RibbonControl()
        {
            InitializeComponent();
            HideTabs();
        }

        Editor m_Editor;
        public Editor Editor
        {
            get { return m_Editor; }
            set
            {
                m_Editor = value;
            }
        }

        public void SetControlledObject(CGBaseItem cgItem)
        {
            try
            {
                m_BaseItem = cgItem;
                if (m_BaseItem != null)
                {
                    BasicVisibilityEditor.SetControlledObject(cgItem);
                    BasicVisibilityEditor.SetEditor(m_Editor);

                    BasicPositionEditor.SetControlledObject(cgItem);
                    BasicPositionEditor.SetEditor(m_Editor);

                    BasicColorEditor.SetControlledObject(cgItem);
                    BasicColorEditor.SetEditor(m_Editor);

                    BasicMiscEditor.SetControlledObject(cgItem);
                    BasicMiscEditor.SetEditor(m_Editor);

                    EffectsSpeedEditor.SetControlledObject(cgItem);
                    EffectsSpeedEditor.SetEditor(m_Editor);

                    EffectsShadowEditor.SetControlledObject(cgItem);
                    EffectsShadowEditor.SetEditor(m_Editor);

                    EffectsBlurEditor.SetControlledObject(cgItem);
                    EffectsBlurEditor.SetEditor(m_Editor);

                    EffectsGlowEditor.SetControlledObject(cgItem);
                    EffectsGlowEditor.SetEditor(m_Editor);

                    if (m_BaseItem is CGTextItem || m_BaseItem is CGTickerLine)
                    {
                        TextContEditor.SetControlledObject(cgItem);
                        TextContEditor.SetEditor(m_Editor);

                        TextColorEditor.SetControlledObject(cgItem);
                        TextColorEditor.SetEditor(m_Editor);

                        TextOutlineEditor.SetControlledObject(cgItem);
                        TextOutlineEditor.SetEditor(m_Editor);

                        TextFontEditor.SetControlledObject(cgItem);
                        TextFontEditor.SetEditor(m_Editor);

                        ShowTab(this.TabText);
                    }
                    if (m_BaseItem is CGGraphicsItem)
                    {
                        GraphicsShapeEditor.SetControlledObject(cgItem);
                        GraphicsShapeEditor.SetEditor(m_Editor);

                        GraphicsColorEditor.SetControlledObject(cgItem);
                        GraphicsColorEditor.SetEditor(m_Editor);

                        GraphicsOutlineEditor.SetControlledObject(cgItem);
                        GraphicsOutlineEditor.SetEditor(m_Editor);

                        ShowTab(this.TabGraphics);
                    }
                    if (m_BaseItem is CGTickerItem)
                    {
                        TickerNewContentEditor.SetControlledObject(cgItem);
                        TickerNewContentEditor.SetEditor(m_Editor);

                        TickerPropsEditor.SetControlledObject(cgItem);
                        TickerPropsEditor.SetEditor(m_Editor);

                        TickerShapeEditor.SetControlledObject(cgItem);
                        TickerShapeEditor.SetEditor(m_Editor);

                        TickerColorEditor.SetControlledObject(cgItem);
                        TickerColorEditor.SetEditor(m_Editor);

                        TickerOutlineEditor.SetControlledObject(cgItem);
                        TickerOutlineEditor.SetEditor(m_Editor);

                        TickerTextColorEditor.SetControlledObject(cgItem);
                        TickerTextColorEditor.SetEditor(m_Editor);

                        TickerTextOutlineEditor.SetControlledObject(cgItem);
                        TickerTextOutlineEditor.SetEditor(m_Editor);

                        TickerTextFontEditor.SetControlledObject(cgItem);
                        TickerTextFontEditor.SetEditor(m_Editor);
                        
                        ShowTab(this.TabTicker);
                    }
                    if (m_BaseItem is CGImageItem)
                    {
                        ImageEditor.SetControlledObject(cgItem);
                        ImageEditor.SetEditor(m_Editor);
                        ShowTab(this.TabImage);
                    }
                    if (m_BaseItem is CGFlashItem)
                    {
                        FlashEditor.SetControlledObject(cgItem);
                        FlashEditor.SetEditor(m_Editor);
                        ShowTab(this.TabFlash);
                    }
                }
                else
                {
                    ClearControls();
                    HideTabs();
                }
            }
            catch (System.Exception) { }
        }

        private void ShowTab(Fluent.RibbonTabItem ribbonTabItem)
        {
            HideTabs();
            ribbonTabItem.Visibility = Visibility.Visible;
            ribbonTabItem.IsSelected = true;
        }

        private void HideTabs()
        {
            this.TabText.Visibility = Visibility.Collapsed;
            this.TabImage.Visibility = Visibility.Collapsed;
            this.TabFlash.Visibility = Visibility.Collapsed;
            this.TabGraphics.Visibility = Visibility.Collapsed;
            this.TabTicker.Visibility = Visibility.Collapsed;
        }

        private void ClearControls()
        {
            ////================Clear basic props======================
            BasicVisibilityEditor.SetControlledObject(null);
            BasicPositionEditor.SetControlledObject(null);
            BasicColorEditor.SetControlledObject(null);
            BasicMiscEditor.SetControlledObject(null);
            ////================Clear effects======================
            EffectsSpeedEditor.SetControlledObject(null);
            EffectsShadowEditor.SetControlledObject(null);
            EffectsBlurEditor.SetControlledObject(null);
            EffectsGlowEditor.SetControlledObject(null);
            //================Clear text======================
            TextContEditor.SetControlledObject(null);
            TextColorEditor.SetControlledObject(null);
            TextOutlineEditor.SetControlledObject(null);
            TextFontEditor.SetControlledObject(null);
            //================Clear graphics======================
            GraphicsShapeEditor.SetControlledObject(null);
            GraphicsColorEditor.SetControlledObject(null);
            GraphicsOutlineEditor.SetControlledObject(null);
           // //================Clear ticker======================
            TickerNewContentEditor.SetControlledObject(null);
            TickerShapeEditor.SetControlledObject(null);
            TickerColorEditor.SetControlledObject(null);
            TickerOutlineEditor.SetControlledObject(null);
            TickerPropsEditor.SetControlledObject(null);
            TickerTextColorEditor.SetControlledObject(null);
            TickerTextOutlineEditor.SetControlledObject(null);
            TickerTextFontEditor.SetControlledObject(null);
            ////================Clear Image======================
            ImageEditor.SetControlledObject(null);
            ////================Clear Falsh======================
            FlashEditor.SetControlledObject(null);
            ////=================================================
        }


        //================================ACTIONS HANDLERS====================================

        private void BtnGroup_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null)
            {
                m_Editor.GroupSelected();
            }
        }

        private void BtnUnGroup_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null)
            {
                m_Editor.UnGroupSelected();
            }
        }

        private void BtnUp_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                for (int i = 0; i < m_Editor.SelectedtItems.Count; i++)
                {
                    m_Editor.CGObject.ChangeItemZOrder(m_Editor.SelectedtItems[i].ID, 1);
                    m_Editor.UpdateItemsList();
                }
            }
        }

        private void BtnDown_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                for (int i = 0; i < m_Editor.SelectedtItems.Count; i++)
                {
                    m_Editor.CGObject.ChangeItemZOrder(m_Editor.SelectedtItems[i].ID, -1);
                    m_Editor.UpdateItemsList();

                }
            }
        }

        private void BtnToFront_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                for (int i = 0; i < m_Editor.SelectedtItems.Count; i++)
                {
                    m_Editor.CGObject.ChangeItemZOrder(m_Editor.SelectedtItems[i].ID, 10000);
                    m_Editor.UpdateItemsList();
                }
            }
        }

        private void BtnToBack_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                for (int i = 0; i < m_Editor.SelectedtItems.Count; i++)
                {
                    m_Editor.CGObject.ChangeItemZOrder(m_Editor.SelectedtItems[i].ID, -10000);
                    m_Editor.UpdateItemsList();
                }
            }
        }

        private void AlignLefts_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                m_Editor.AlignLefts();
            }
        }

        private void AlignCenters_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                m_Editor.AlignCenters();
            }
        }

        private void AlignRights_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                m_Editor.AlignRights();
            }
        }

        private void AlignTops_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                m_Editor.AlignTops();
            }
        }

        private void AlignMiddles_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                m_Editor.AlignMiddles();
            }
        }

        private void AlignBottoms_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null && m_Editor.SelectedtItems.Count > 0)
            {
                m_Editor.AlignBottoms();
            }
        }

        private void LoadConfig_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "CG config files (.ml-cgc)|*.ml-cgc"; // Filter files by extension
            if (openFile.ShowDialog() == true)
            {
                IMLXMLPersist pXMLPersist = (IMLXMLPersist)m_Editor.CGObject;
                pXMLPersist.LoadFromXML(openFile.FileName, -1);

                m_Editor.UpdateItemsList();
                List<CGBaseItem> items = m_Editor.CGItems;
                m_Editor.UpdateItemsList();
            }

        }

        private void SaveConfig_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveDlg = new Microsoft.Win32.SaveFileDialog();
            saveDlg.DefaultExt = ".ml-cgc"; // Default file extension
            saveDlg.Filter = "CG config files (.ml-cgc)|*.ml-cgc"; // Filter files by extension
            if (saveDlg.ShowDialog() == true)
            {
                IMLXMLPersist pXMLPersist = (IMLXMLPersist)m_Editor.CGObject;
                pXMLPersist.SaveToXMLFile(saveDlg.FileName, 1);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnUndo_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null)
                m_Editor.Undo();
        }

        private void BtnRedo_Click(object sender, RoutedEventArgs e)
        {
            if (m_Editor != null)
                m_Editor.Redo();
        }

    }
}
