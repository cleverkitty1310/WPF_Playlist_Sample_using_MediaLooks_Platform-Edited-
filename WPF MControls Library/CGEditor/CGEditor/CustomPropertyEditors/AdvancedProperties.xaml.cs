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
    /// <summary>
    /// Interaction logic for AdvancedProperties.xaml
    /// </summary>
    public partial class AdvancedProperties : UserControl
    {
        public AdvancedProperties()
        {
            InitializeComponent();
        }
        public void SetControlledObject(CGBaseItem item)
        {
            try
            {
                HideAllEditors();
                if (item == null)
                {
                    ResetAllEditors();
                }
                else
                {
                    if (item is CGTextItem)
                    {
                        TextEditor.SetControlledObject((CGTextItem)item);
                        TextEditor.Visibility = System.Windows.Visibility.Visible;
                    }
                    if (item is CGGraphicsItem)
                    {
                        GraphicsEditor.SetControlledObject((CGGraphicsItem)item);
                        GraphicsEditor.Visibility = System.Windows.Visibility.Visible;
                    }
                    if (item is CGTickerItem)
                    {
                        TickerEditor.SetControlledObject((CGTickerItem)item);
                        TickerEditor.Visibility = System.Windows.Visibility.Visible;
                    }

                    if (item is CGImageItem)
                    {
                        ImageEditor.SetControlledObject((CGImageItem)item);
                        ImageEditor.Visibility = System.Windows.Visibility.Visible;
                    }

                    if (item is CGFlashItem)
                    {
                        FlashEditor.SetControlledObject((CGFlashItem)item);
                        FlashEditor.Visibility = System.Windows.Visibility.Visible;
                    }
                }
            }
            catch (System.Exception) { }
        }

        private void HideAllEditors()
        {
            TextEditor.Visibility = System.Windows.Visibility.Hidden;
            GraphicsEditor.Visibility = System.Windows.Visibility.Hidden;
            TickerEditor.Visibility = System.Windows.Visibility.Hidden;
            ImageEditor.Visibility = System.Windows.Visibility.Hidden;
            FlashEditor.Visibility = System.Windows.Visibility.Hidden;
        }

        private void ResetAllEditors()
        {
            TextEditor.SetControlledObject(null);
            GraphicsEditor.SetControlledObject(null);
            TickerEditor.SetControlledObject(null);
            ImageEditor.SetControlledObject(null);
            FlashEditor.SetControlledObject(null);
        }
    }
}
