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
    /// Interaction logic for OpenImageEditor.xaml
    /// </summary>
    public partial class OpenImageEditor : UserControl, Xceed.Wpf.Toolkit.PropertyGrid.Editors.ITypeEditor
    {
        private bool m_bChangedByUI; //Indicates that value was changed by control UI
        private bool m_bAceptExternalUpdate; //Enables or disables external update of the control

        private IPath m_Item;
        private Editor m_Editor;

        public void SetEditor(Editor editor)
        {
            m_Editor = editor;
        }


        public OpenImageEditor()
        {
            InitializeComponent();
            m_bChangedByUI = true;
            m_bAceptExternalUpdate = true;
        }

        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(string), typeof(OpenImageEditor),
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
                if (m_Item != null && m_bChangedByUI)
                {
                    UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ((CGBaseItem)m_Item).ID));
                    m_Item.Path = value;//TextBoxText.Text;
                }
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "All Images|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF;*.PNG";
            if (openFile.ShowDialog() == true)
            {
                Value = openFile.FileName;
            }
        }

        public FrameworkElement ResolveEditor(Xceed.Wpf.Toolkit.PropertyGrid.PropertyItem propertyItem)
        {
            Binding binding = new Binding("Value");
            binding.Source = propertyItem;
            binding.Mode = propertyItem.IsReadOnly ? BindingMode.OneWay : BindingMode.TwoWay;
            BindingOperations.SetBinding(this, OpenImageEditor.ValueProperty, binding);
            return this;
        }

        public void SetControlledObject(object item)
        {
            try
            {
                if (item != null && item is IPath)
                {
                    m_Item = (IPath)item;
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
                TextBoxText.Text = m_Item.Path;
                m_bChangedByUI = true;
            }
        }

        private void ClearControls()
        {
            m_bChangedByUI = false;
            TextBoxText.Text = "";
            m_bChangedByUI = true;
        }

        void External_ItemChanged(object sender, ItemChangedArgs e)
        {
            if (m_bAceptExternalUpdate)
            {
                if (e.PropertyName == "path" || e.PropertyName == "xml")
                    FillControls();
            }
        }

        private void Button_Click2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog openFolder = new System.Windows.Forms.FolderBrowserDialog();
            if (openFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Value = openFolder.SelectedPath;
            }
        }

    }
}
