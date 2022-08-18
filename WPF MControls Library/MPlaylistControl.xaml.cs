using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MControls;
using MPLATFORMLib;
using Cursor = System.Windows.Input.Cursor;
using Cursors = System.Windows.Input.Cursors;
using DataGrid = System.Windows.Controls.DataGrid;
using DataGridCell = System.Windows.Controls.DataGridCell;
using MessageBox = System.Windows.MessageBox;
using UserControl = System.Windows.Controls.UserControl;

namespace WPFMControls
{
    /// <summary>
    /// Interaction logic for MPlaylistControl.xaml
    /// </summary>
    public partial class MPlaylistControl : UserControl
    {
        public MPlaylistControl()
        {
            InitializeComponent();
        }
        IMPlaylist m_pPlaylist;
        public class NewItem
        {
            public string pos { set; get; }
            public string filecommand { set; get; }
            private string _inparams;
            public string inparams
            {
                set 
                { 
                    _inparams = value;
                    try
                    {
                        double dblIn = MHelpers.ParsePos(inparams);
                        double dblOut = MHelpers.ParsePos(_outpoint);

                        // Set new in-out
                        tag.FileInOutSet(dblIn, dblOut);
                        //tag.FileInOutSet();
                    }
                    catch (Exception)
                    {


                    }
                }
                get { return _inparams; }
            }
            private string _outpoint;
            public string outpoint 
            {
                get { return _outpoint; }
                set 
                { 
                    _outpoint = value;
                    try
                    {
                        double dblIn = MHelpers.ParsePos(inparams);
                        double dblOut = MHelpers.ParsePos(_outpoint);

                        // Set new in-out
                        tag.FileInOutSet(dblIn, dblOut);
                        //tag.FileInOutSet();
                    }
                    catch (Exception)
                    {
                        
                    }
                    
                }

            }
            public MItem tag { set; get; }
        }

        private ObservableCollection<NewItem> newItems;
        public Object SetControlledObject(Object pObject)
        {
            Object pOld = m_pPlaylist;
            try
            {
                m_pPlaylist = (IMPlaylist)pObject;
                UpdateList(false);

                ((MPlaylistClass)m_pPlaylist).OnEvent += new IMEvents_OnEventEventHandler(MPlaylistControl_OnEvent);
            }
            catch (System.Exception) { }

            return pOld;
        }
        // Called if user change playlist selection
        public event EventHandler OnPlaylistSelChanged;

        // Called if playlist changed (files,added, removed, rearanged, etc.) 
        public event EventHandler OnPlaylistChanged;
        void MPlaylistControl_OnEvent(string bsChannelID, string bsEventName, string bsEventParam, object pEventObject)
        {
            //UpdateListState();
        }
        public void SelectFile(IMFile pFile)
        {
            for (int i = 0; i < PlaylistTable.Items.Count; i++)
            {
                if (pFile.Equals(((NewItem)PlaylistTable.Items[i]).tag))
                {
                    PlaylistTable.SelectedIndex = i;
                    //listViewFiles.EnsureVisible(i);
                }
                else
                {
                    PlaylistTable.SelectedIndex = -1;
                }
            }
        }
        void ClearList()
        {
            int Count;
            double totalDuration;
            m_pPlaylist.PlaylistGetCount(out Count, out totalDuration);
            for (int i = 0; i < Count; i++)
            {
                MItem pItem;
                double pFileOffset;
                string pPath;
                m_pPlaylist.PlaylistGetByIndex(i, out pFileOffset, out pPath, out pItem);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(pItem);
                }
            
            try
            {
                newItems.Clear();
            }
            catch (Exception)
            {
            }
            //PlaylistTable.Items.Clear();
            GC.Collect();
        }

        public void UpdateList(bool _bKeepSelection)
        {
            
            //try
            //{
                IMFile pSelFile = null;
                if (PlaylistTable.SelectedItems.Count > 0)
                {
                    pSelFile = ((NewItem)PlaylistTable.SelectedItems[0]).tag;
                }
                ClearList();
                newItems = new ObservableCollection<NewItem>();
                int nFiles = 0;
                double dblDuration = 0;
                m_pPlaylist.PlaylistGetCount(out nFiles, out dblDuration);
                for (int i = 0; i < nFiles; i++)
                {
                    double dblPos;
                    string strPathOrCommand;
                    MItem pItem;
                    m_pPlaylist.PlaylistGetByIndex(i, out dblPos, out strPathOrCommand, out pItem);
                    eMItemType eItemType;
                    pItem.ItemTypeGet(out eItemType);
                    M_DATETIME mTimeStart;
                    M_DATETIME mTimeStop;
                    eMStartType eStartType;
                    pItem.ItemTimesGet(out mTimeStart, out mTimeStop, out eStartType);
                    if (eItemType != eMItemType.eMPIT_Command)
                    {
                        string strFile = strPathOrCommand.Substring(strPathOrCommand.LastIndexOf('\\') + 1);
                        int nIdxFake;
                        M_VID_PROPS vidProps;
                        M_AUD_PROPS audProps;
                        string strFormat;
                        ((IMFormat)pItem).FormatVideoGet(eMFormatType.eMFT_Input, out vidProps, out nIdxFake, out strFormat);
                        ((IMFormat)pItem).FormatAudioGet(eMFormatType.eMFT_Input, out audProps, out nIdxFake, out strFormat);
                        double dblIn = 0, dblOut = 0, dblFileDuration = 0;
                        pItem.FileInOutGet(out dblIn, out dblOut, out dblFileDuration);
                        eMState eState;
                        double dblTime = 0;
                        pItem.FileStateGet(out eState, out dblTime);
                        NewItem pNewItem = new NewItem();
                        pNewItem.pos = MHelpers.PosToString(dblPos, 0);
                        pNewItem.filecommand = strFile;
                        pNewItem.inparams = MHelpers.PosToString(dblIn);
                        pNewItem.outpoint = MHelpers.PosToString(dblOut);
                        pNewItem.tag = pItem;
                        newItems.Add(pNewItem);
                        PlaylistTable.ItemsSource = newItems;
                    }
                    else
                    {
                        string strParam;
                        Object pTarget;
                        pItem.ItemCommandGet(out strPathOrCommand, out strParam, out pTarget);
                        NewItem pNewItem = new NewItem();
                        pNewItem.pos = MHelpers.PosToString(dblPos);
                        pNewItem.filecommand = strPathOrCommand;
                        pNewItem.inparams = strParam;
                        pNewItem.tag = pItem;
                        newItems.Add(pNewItem);
                        PlaylistTable.ItemsSource = newItems;
                    }
                }
                if (pSelFile != null)
                    SelectFile(pSelFile);
            //}
            //catch (System.Exception ex) { }
        }


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                MItem pFile = null;
                Cursor prev = this.Cursor;
                this.Cursor = Cursors.Wait;
                for (int i = 0; i < fileDialog.FileNames.Length; i++)
                {
                    int nIndex = -1;
                    m_pPlaylist.PlaylistAdd(null, fileDialog.FileNames[i], "", ref nIndex, out pFile);
                }
                this.Cursor = prev;
                UpdateList(true);
                if (this.OnPlaylistChanged != null)
                    this.OnPlaylistChanged(fileDialog.FileNames.Length > 1 ? null : pFile, e);
            }
        }

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;
            if (fileDialog.ShowDialog() == DialogResult.OK && fileDialog.FileNames.Length > 0)
            {
                Cursor prev = this.Cursor;
                this.Cursor = Cursors.Wait;

                // Add file (as playlist)
                int nIndex = -1;
                MItem pPlaylist;
                m_pPlaylist.PlaylistAdd(null, fileDialog.FileNames[0], "playlist", ref nIndex, out pPlaylist);

                // Add other files to this playlist
                for (int i = 1; i < fileDialog.FileNames.Length; i++)
                {
                    nIndex = -1;
                    MItem pFile;
                    ((IMPlaylist)pPlaylist).PlaylistAdd(null, fileDialog.FileNames[i], "", ref nIndex, out pFile);
                }

                this.Cursor = prev;
                UpdateList(true);

                // Notify about playlist changing
                if (this.OnPlaylistChanged != null)
                    this.OnPlaylistChanged(pPlaylist, e);
            }
        }

        private void buttonAddLive_Click(object sender, RoutedEventArgs e)
        {
            int nIndex = -1;
            MItem pLive;

            m_pPlaylist.PlaylistAdd(null, "my_live", "live", ref nIndex, out pLive);
            UpdateList(true);

            // Notify about playlist changing
            if (this.OnPlaylistChanged != null)
                this.OnPlaylistChanged(pLive, e);
        }

        private void buttonAddRef_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTable.SelectedItems.Count > 0)
            {
                int nIndex = -1;
                IMFile pFile = (IMFile)((NewItem)PlaylistTable.SelectedItems[0]).tag;

                string strPath, strProps, strInfo;
                pFile.FileNameGet(out strPath);

                // TODO: Unique ID
                MItem pNewItem;
                m_pPlaylist.PlaylistAdd(pFile, strPath + ":ref", "", ref nIndex, out pNewItem);

                UpdateList(true);

                // Notify about playlist changing
                if (this.OnPlaylistChanged != null)
                    this.OnPlaylistChanged(pNewItem, e);
            }
        }

        private void buttonRemove_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTable.SelectedItems.Count <= 0) return;
            if (((NewItem) PlaylistTable.SelectedItems[0]).tag != null)
            {
                MItem pItem = ((NewItem) PlaylistTable.SelectedItems[0]).tag;

                m_pPlaylist.PlaylistRemove(pItem);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pItem);
                UpdateList(false);
                if (this.OnPlaylistChanged != null)
                    this.OnPlaylistChanged(null, e);
            }
            else
            {
                PlaylistTable.Items.Remove(PlaylistTable.SelectedItems[0]);
            }
            GC.Collect();
        }
        private int FindRowIndex(DataGridRow row)
        {
            DataGrid dataGrid =
                ItemsControl.ItemsControlFromItemContainer(row)
                as DataGrid;

            int index = dataGrid.ItemContainerGenerator.
                IndexFromContainer(row);

            return index;
        }
        private void PlaylistTable_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DependencyObject dep = (DependencyObject)e.OriginalSource;

            // iteratively traverse the visual tree
            while ((dep != null) &&
                    !(dep is DataGridCell) &&
                    !(dep is DataGridColumnHeader))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }

            if (dep == null)
                return;

            if (dep is DataGridColumnHeader)
            {
                DataGridColumnHeader columnHeader = dep as DataGridColumnHeader;
                // do something
            }

            if (dep is DataGridCell)
            {
                DataGridCell cell = dep as DataGridCell;
                while ((dep != null) && !(dep is DataGridRow))
                {
                    dep = VisualTreeHelper.GetParent(dep);
                }
 
                DataGridRow row = dep as DataGridRow;
                if (cell.Column.DisplayIndex==1)
                {
                    //int pIndex = FindRowIndex(row);
                    IMItem pItem = ((NewItem)PlaylistTable.SelectedItems[0]).tag; ;
                    eMItemType eType;
                    pItem.ItemTypeGet(out eType);
                    // Check for Live
                    if (eType == eMItemType.eMPIT_File)
                    {
                        // File
                        FormFileName formFile = new FormFileName();
                        formFile.m_pFile = (IMFile)((NewItem)PlaylistTable.SelectedItems[0]).tag;
                        formFile.ShowDialog();
                        //MessageBox.Show("File");
                    }
                    else if (eType == eMItemType.eMPIT_Live)
                    {
                        try
                        {
                            //// TODO: Make non-modal
                            FormLive formLive = new FormLive();
                            formLive.m_pDevice = (IMDevice)((NewItem)PlaylistTable.SelectedItems[0]).tag;
                            formLive.ShowDialog();
                            //MessageBox.Show("Live");
                        }
                        catch (System.Exception) { }
                    }
                    else if (eType == eMItemType.eMPIT_Playlist)
                    {
                        try
                        {
                            //// TODO: Make non-modal
                            FormPlaylist formPlaylist = new FormPlaylist();
                            formPlaylist.m_pPlaylist = (IMPlaylist)((NewItem)PlaylistTable.SelectedItems[0]).tag;
                            formPlaylist.ShowDialog();
                        }
                        catch (System.Exception) { }
                    }
                    else if (eType == eMItemType.eMPIT_Command)
                    {
                        try
                        {
                            //// TODO: Make non-modal
                            FormCommand formCommand = new FormCommand();
                            formCommand.m_pPlaylistItem = pItem;
                            formCommand.ShowDialog();
                            //MessageBox.Show("Command");
                        }
                        catch (System.Exception) { }
                    }
                }
                if (cell.Column.DisplayIndex == 0)
                {
                    int pIndex = FindRowIndex(row);
                    m_pPlaylist.PlaylistPosSet(pIndex, 0, 0);
                }
                UpdateList(true);
            }
        }

        private void buttonAddCommand_Click(object sender, RoutedEventArgs e)
        {
            int nIndex = -1;
            MItem pItem;
            m_pPlaylist.PlaylistCommandAdd(@"<new command>", "", null, ref nIndex, out pItem);
            UpdateList(true);
            // Notify about playlist changing
            if (this.OnPlaylistChanged != null)
                this.OnPlaylistChanged(pItem, e);
        }

        private void buttonUp_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTable.SelectedItems.Count > 0)
            {
                try
                {
                    m_pPlaylist.PlaylistReorder(PlaylistTable.SelectedIndex, -1);
                    UpdateList(true);

                    // Notify about playlist changing
                    if (this.OnPlaylistChanged != null)
                        this.OnPlaylistChanged(null, e);
                }
                catch (System.Exception) { }
            }
        }

        private void buttonDown_Click(object sender, RoutedEventArgs e)
        {
            if (PlaylistTable.SelectedItems.Count > 0)
            {
                try
                {
                    m_pPlaylist.PlaylistReorder(PlaylistTable.SelectedIndex, 1);
                    UpdateList(true);

                    // Notify about playlist changing
                    if (this.OnPlaylistChanged != null)
                        this.OnPlaylistChanged(null, e);
                }
                catch (System.Exception) { }
            }
        }

        private void PlaylistTable_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PlaylistTable.SelectedItems.Count > 0 && ((NewItem)PlaylistTable.SelectedItems[0]).tag != null)
            {
                MItem pItem = ((NewItem)PlaylistTable.SelectedItems[0]).tag;
                eMItemType eType;
                pItem.ItemTypeGet(out eType);

            }
            else
            {
            }

            buttonRemove.IsEnabled = PlaylistTable.SelectedItems.Count > 0;

            // For handler
            if (this.OnPlaylistSelChanged != null)
                this.OnPlaylistSelChanged(null, e);
        }
    }
}
