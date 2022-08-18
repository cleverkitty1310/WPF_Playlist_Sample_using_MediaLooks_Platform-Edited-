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


namespace CGEditor.CustomPropertyEditors
{
    /// <summary>
    /// Interaction logic for OpenFileEditor.xaml
    /// </summary>
    public partial class OpenFileEditor : UserControl, Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        public OpenFileEditor()
        {
            InitializeComponent();
        }

        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(OpenFileEditor),
                                                                                               new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));
        public string Value
        {
            get
            { 
                return (string)GetValue(ValueProperty); 
            }
            set 
            { 
                SetValue(ValueProperty, value);
            }
        }

        private string m_strFilter;
        public string Filter
        {
            get
            {
                return m_strFilter;
            }
            set
            {
                m_strFilter = Value;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            if (openFile.ShowDialog() == true)
            {
                Value = openFile.FileName;
            }
            //Value = string.Empty;
        }

        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Binding binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, OpenFileEditor.ValueProperty, binding);
            return this;
        }
    }
}
