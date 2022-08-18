using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using MPLATFORMLib;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MPropsControl.xaml
    /// </summary>
    public partial class MPropsControl : UserControl
    {
        public MPropsControl()
        {
            InitializeComponent();
        }
        public IMProps m_pProps;
        public class Properties
        {
            private string _inparams;
            public string property { set; get; }
            
            public string propValue
            {
                set
                {
                    _inparams = value;
                    //try
                    //{
                    //    tag.PropsSet(property, _inparams);
                    //}
                    //catch (Exception)
                    //{
                    //}
                }
                get { return _inparams; }
            }
            //public IMProps tag { set; get; }
        }
        private ObservableCollection<Properties> newItems;
        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pProps;
            try
            {
                m_pProps = (IMProps)pObject;

                //UpdateView();
                UpdateCombo("<Root>", "");
                UpdateList("", true);
            }
            catch (System.Exception) { }

            return pOld;
        }
        private string FullNodeName(string sNamePart)
        {
            string sParent = "";
            if (sParent != "" && sNamePart != "")
                return sParent + "::" + sNamePart;

            return sParent + sNamePart;
        }

        private List<string> parent;
        public event EventHandler OnPropsChanged;
        private void UpdateCombo(string sSelString, string sParentRecurse)
        {
            if (sParentRecurse == "")
            {
                parent = new List<string>();
                parent.Add("<Root>");
                //comboBoxParent.Items.Clear();
                //comboBoxParent.Items.Add("<Root>");
            }

            int nCount = 0;
            m_pProps.PropsGetCount(sParentRecurse, out nCount);
            for (int i = 0; i < nCount; i++)
            {
                string sName;
                string sValue;
                int bNode = 0;
                m_pProps.PropsGetByIndex(sParentRecurse, i, out sName, out sValue, out bNode);
                if (bNode != 0)
                {
                    string sNext = sParentRecurse.Length > 0 ? sParentRecurse + "::" + sName : sName;
                    parent.Add(sNext);
                    //comboBoxParent.Items.Add(sNext);

                    UpdateCombo(sSelString, sNext);
                }
            }

            //if (sParentRecurse == "")
            //{
            //    var nItem = -1;
            //    foreach (var cmbItem in parent)
            //    {
            //        nItem++;
            //        //MessageBox.Show(nItem.ToString());
            //        if (cmbItem == sSelString)
            //        { break; }
            //        //{ break; }
            //    }
            //    //int nItem = comboBoxBG.FindStringExact(strFile);
            //    if (nItem >= 0)
            //    {
            //        // colorbars, color, etc.
            //        comboBoxParent.SelectedIndex = nItem;
            //    }
            //    else if (sSelString == "")
            //    {
            //        // <none>
            //        comboBoxParent.SelectedIndex = 0;
            //    }
            //}
        }
        private void UpdateList(string sParent, bool _bResetList)
        {
            if (_bResetList)
            {
                newItems = new ObservableCollection<Properties>();
                //tableProps.Items.Clear();
            }
            int nCount = 0;
            m_pProps.PropsGetCount(FullNodeName(sParent), out nCount);
            for (int i = 0; i < nCount; i++)
            {
                string sName;
                string sValue;
                int bNode = 0;
                m_pProps.PropsGetByIndex(FullNodeName(sParent), i, out sName, out sValue, out bNode);

                string sRelName = sParent.Length > 0 ? sParent + "::" + sName : sName;
                if (bNode != 0)
                {
                    var nItem = -1;
                    foreach (var cmbItem in parent)
                    {
                        nItem++;
                        if (cmbItem == FullNodeName(sRelName))
                        { break; }
                    }
                    if (nItem >= parent.Count)
                    parent.Add(sRelName);

                    if (!string.IsNullOrEmpty(sValue))
                        AddProps(sRelName, sValue);

                    UpdateList(sRelName, false);
                }
                else
                {
                    AddProps(sRelName, sValue);
                }
            }

            //if (_bResetList)
            //    AddProps("<New Props>", "");
            tableProps.ItemsSource = newItems;
        }
        private void AddProps(string sName, string sValue)
        {
            // Add new item
            Properties newProps = new Properties {property = sName, propValue = sValue};

            newItems.Add(newProps);


        }
        private void comboBoxParent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UpdateList("", true);

            //string sValue = "";
            //if (comboBoxParent.SelectedIndex > 0)
            //{
            //    try
            //    {
            //        // If value not set -> the exception may occurs
            //        m_pProps.PropsGet(FullNodeName(""), out sValue);
            //    }
            //    catch (System.Exception) { }

            //    textBoxNodeValue.IsEnabled = true;
            //}
            //else
            //{
            //    textBoxNodeValue.IsEnabled = false;
            //}

            //textBoxNodeValue.Text = sValue;
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            AddProps(textBoxNewPropsName.Text, textBoxNewPropsValue.Text);
        }

        private void tableProps_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            try
            {
                Properties current = (Properties) e.EditingElement.DataContext;
                TextBox t = e.EditingElement as TextBox;
                m_pProps.PropsSet(current.property, t.Text);
            }
            catch (Exception)
            {
            }
        }
    }
}
