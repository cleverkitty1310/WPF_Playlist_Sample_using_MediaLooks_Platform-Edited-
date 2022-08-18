using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MPLATFORMLib;
using UserControl = System.Windows.Controls.UserControl;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MFileName.xaml
    /// </summary>
    public partial class MFileName : UserControl
    {
        private IMFile m_pFile;

        public MFileName()
        {
            InitializeComponent();
        }
        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pFile;
            try
            {
                m_pFile = (IMFile)pObject;

                UpdateControl();
            }
            catch (System.Exception) { }

            return pOld;
        }

        public void UpdateControl()
        {
            try
            {
                // Get path
                string strPath, strProps, strInfo;
                m_pFile.FileNameGet(out strPath);

                textBoxPath.Text = strPath;


                // Get In/Out
                double dblIn, dblOut, dblLen;
                m_pFile.FileInOutGet(out dblIn, out dblOut, out dblLen);

                numericIn.Value = (decimal)dblIn;
                numericOut.Value = (decimal)dblOut;
                numericLen.Value = (decimal)dblLen;

                // Get loop
                string sLoop;
                ((IMProps)m_pFile).PropsGet("loop", out sLoop);
                checkBoxLoop.IsChecked = (sLoop != null && sLoop != "" && sLoop != "0" && sLoop != "false");
            }
            catch (System.Exception) { }
        }

        private void ChangeFileName(string sType)
        {
            if (textBoxPath.Text == "")
                BrowseForFile();

            if (textBoxPath.Text != "")
            {
                // For In/Out
                string sProps = "In=" + numericIn.Value.ToString() + " Out=" + numericOut.Value.ToString();

                if (textBoxProps.Text != "" && textBoxProps.Text[0] != '<')
                    sProps += " " + textBoxProps.Text;

                try
                {
                    if (sType != "")
                        sProps = "change_type='" + sType + "' " + sProps;
                    m_pFile.FileNameSet(textBoxPath.Text, sProps);


                    UpdateControl();
                }
                catch (System.Exception ex) { }
            }
        }

        private void BrowseForFile()
        {
            // TODO: Add filter with media extension
            OpenFileDialog openDlg = new OpenFileDialog();
            if (openDlg.ShowDialog() == DialogResult.OK)
            {
                textBoxPath.Text = openDlg.FileName;
            }
        }

        private void buttonBrowse_Click(object sender, RoutedEventArgs e)
        {
            BrowseForFile();
        }

        private void buttonSetName_Click(object sender, RoutedEventArgs e)
        {
            ChangeFileName("");
        }

        private void buttonBreak_Click(object sender, RoutedEventArgs e)
        {
            ChangeFileName("break");
        }

        private void buttonNext_Click(object sender, RoutedEventArgs e)
        {
            ChangeFileName("next");
        }

        private void buttonReplace_Click(object sender, RoutedEventArgs e)
        {
            ChangeFileName("replace");
        }

        private void buttonSetInOut_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                m_pFile.FileInOutSet((double)numericIn.Value, (double)numericOut.Value);
            }
            catch (System.Exception) { }
        }

        private void checkBoxLoop_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                ((IMProps)m_pFile).PropsSet("loop", "true");
            }
            catch (System.Exception) { }
        }

        private void checkBoxLoop_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                ((IMProps)m_pFile).PropsSet("loop", "false");
            }
            catch (System.Exception) { }
        }
    }
}
