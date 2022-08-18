using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using MPLATFORMLib;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for PlaylistBackground.xaml
    /// </summary>
    public partial class PlaylistBackground : UserControl
    {
        public PlaylistBackground()
        {
            InitializeComponent();
        }
        IMPlaylist m_pPlaylist;

        public Object SetControlledObject(Object pObject)
        {
            Object pOld = (Object)m_pPlaylist;
            //try
            //{
                m_pPlaylist = (IMPlaylist)pObject;

                UpdateCombo();
            //}
            //catch (System.Exception) { }

            return pOld;
        }

        // Called if background changed
        public event EventHandler OnBackgroundChanged;

        public void UpdateCombo()
        {
            comboBoxBG.Items.Clear();
            comboBoxBG.Items.Add("<None>");
            comboBoxBG.Items.Add("colorbars-hd");
            comboBoxBG.Items.Add("colorbars-ntsc");
            comboBoxBG.Items.Add("colorbars-pal");
            //comboBoxBG.Items.Add("colrobars_hd1080");
            comboBoxBG.Items.Add("colorbars-75");
            comboBoxBG.Items.Add("colorbars-100");
            comboBoxBG.Items.Add("blue");
            comboBoxBG.Items.Add("black");
            comboBoxBG.Items.Add("white");
            comboBoxBG.Items.Add("transparent");
            comboBoxBG.Items.Add("<Media File>");
            comboBoxBG.Items.Add("<Playlist>");
            comboBoxBG.Items.Add("<Live>");


            string strFile;
            MItem pBGItem;
            m_pPlaylist.PlaylistBackgroundGet(out strFile, out pBGItem);

            Focus(); // For remove focus from combo
            int nItem = -1;
            foreach (var cmbItem in comboBoxBG.Items)
            {
                nItem++;
                //MessageBox.Show(nItem.ToString());
                if (cmbItem.ToString() == strFile) 
                { break; }
                //{ break; }
            } 
            //int nItem = comboBoxBG.FindStringExact(strFile);
            if (nItem >= 0)
            {
                // colorbars, color, etc.
                comboBoxBG.SelectedIndex = nItem;
            }
            else if (strFile == "")
            {
                // <none>
                comboBoxBG.SelectedIndex = 0;
            }
            else
            {
                // Check item type
                eMItemType eType;
                pBGItem.ItemTypeGet(out eType);
                if (eType == eMItemType.eMPIT_File)
                {
                    comboBoxBG.SelectedIndex = comboBoxBG.Items.Count - 3;
                }
                else if (eType == eMItemType.eMPIT_Playlist)
                {
                    comboBoxBG.SelectedIndex = comboBoxBG.Items.Count - 3;
                }
                else if (eType == eMItemType.eMPIT_Live)
                {
                    comboBoxBG.SelectedIndex = comboBoxBG.Items.Count - 3;
                }
            }
        }

        private void buttonConfig_Click(object sender, RoutedEventArgs e)
        {
            string strFile;
            MItem pItem;
            m_pPlaylist.PlaylistBackgroundGet(out strFile, out pItem);

            eMItemType eType;
            pItem.ItemTypeGet(out eType);
            // Check for Live
            if (eType == eMItemType.eMPIT_File)
            {
                // File
                FormFileName formFile = new FormFileName();
                formFile.m_pFile = (IMFile)pItem;
                formFile.ShowDialog();
            }
            else if (eType == eMItemType.eMPIT_Live)
            {
                try
                {
                    //ODO: Make non-modal
                    var formLive = new FormLive {m_pDevice = (IMDevice) pItem};
                    formLive.ShowDialog();
                }
                catch (System.Exception) { }
            }
            else if (eType == eMItemType.eMPIT_Playlist)
            {
                try
                {
                    //ODO: Make non-modal
                    var formPlaylist = new FormPlaylist {m_pPlaylist = (IMPlaylist) pItem};
                    formPlaylist.ShowDialog();
                }
                catch (System.Exception) { }
            }
        }

        private void comboBoxBG_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
                MItem pNewItem;
                if (comboBoxBG.SelectedIndex == 0)
                {
                    m_pPlaylist.PlaylistBackgroundSet(null, "", "", out pNewItem);
                    buttonConfig.IsEnabled = false;
                }
                else if (comboBoxBG.SelectedIndex < comboBoxBG.Items.Count - 3)
                {
                    m_pPlaylist.PlaylistBackgroundSet(null, comboBoxBG.SelectedValue.ToString(), "", out pNewItem);
                    buttonConfig.IsEnabled = true;
                }
                else if (comboBoxBG.SelectedIndex == comboBoxBG.Items.Count - 3)
                {
                    // File
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    if (fileDialog.ShowDialog() == DialogResult.OK)
                    {
                        m_pPlaylist.PlaylistBackgroundSet(null, fileDialog.FileName, "", out pNewItem);

                        buttonConfig.IsEnabled = true;
                    }
                    else
                    {
                        UpdateCombo();
                    }
                }
                else if (comboBoxBG.SelectedIndex == comboBoxBG.Items.Count - 2)
                {
                    // Playlist
                    OpenFileDialog fileDialog = new OpenFileDialog();
                    fileDialog.Multiselect = true;
                    if (fileDialog.ShowDialog() == DialogResult.OK && fileDialog.FileNames.Length > 0)
                    {
                        // Add file (as playlist)
                        MItem pPlaylist;
                        m_pPlaylist.PlaylistBackgroundSet(null, fileDialog.FileNames[0], "playlist", out pPlaylist);

                        // Add other files to this playlist
                        for (int i = 1; i < fileDialog.FileNames.Length; i++)
                        {
                            MItem pFile;
                            int nIndex = -1;
                            ((IMPlaylist)pPlaylist).PlaylistAdd(null, fileDialog.FileNames[i], "", ref nIndex, out pFile);
                        }

                        buttonConfig.IsEnabled = true;
                    }
                    else
                    {
                        UpdateCombo();
                    }
                }
                else if (comboBoxBG.SelectedIndex == comboBoxBG.Items.Count - 1)
                {
                    // Show live config
                    MLiveClass objLive = new MLiveClass();
                    FormLive formLive = new FormLive();
                    formLive.m_pDevice = objLive;
                    formLive.ShowDialog();

                    eMState eState;
                    objLive.ObjectStateGet(out eState);
                    if (eState != eMState.eMS_Running)
                    {
                        UpdateCombo();
                        return;
                    }

                    // Add file (as playlist)
                    MItem pLive;
                    m_pPlaylist.PlaylistBackgroundSet(objLive, "my_live_bg", "live", out pLive);

                    buttonConfig.IsEnabled = true;
                }
            }

        
        

    }
}
