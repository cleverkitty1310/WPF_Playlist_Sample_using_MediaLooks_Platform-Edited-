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
using System.Windows.Threading;
using System.IO;
using System.Threading;
using Microsoft.Win32;
using System.ComponentModel;

using MLCHARGENLib;
using System.ComponentModel;
using CGEditor.CGItemWrappers;
using CGEditor.CustomPropertyEditors;
using System.Runtime.InteropServices;

namespace CGEditor
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        Editor m_objEditor;
        //BackgroundWorker m_bgWorker;

        //MPLATFORMLib.MPlaylistClass m_tempPlaylist;

        string m_strImagePath;
        string m_strFlashPath;

        private int m_nFrameCounter;
        private Point m_ptStartPos;


        private static RoutedCommand MyCommand = new RoutedCommand();

        //public class VideoFormat
        //{
        //    private string m_strDisplayName;
        //    public string DisplayName
        //    {
        //        get { return m_strDisplayName; }
        //    }
        //    public eMVideoFormat m_eVFormat;
        //    public MLCHARGENLib.eMVideoFormat eVFormat
        //    {
        //        get { return m_eVFormat; }
        //    }
        //    public VideoFormat(string displayName, eMVideoFormat eMVFormat)
        //    {
        //        m_strDisplayName = displayName;
        //        m_eVFormat = eMVFormat;
        //    }
        //}



        public MainWindow()
        {
            InitializeComponent();

            m_objEditor = new Editor();
            m_objEditor.ItemSelected -= (m_objEditor_ItemSelected);
            m_objEditor.ItemSelected += new EventHandler(m_objEditor_ItemSelected);

            m_objEditor.ItemsListUpdated -= new EventHandler(m_objEditor_ItemsListUpdated);
            m_objEditor.ItemsListUpdated += new EventHandler(m_objEditor_ItemsListUpdated);

            m_objEditor.FrameProcessed -= new EventHandler(m_objEditor_FrameProcessed);
            m_objEditor.FrameProcessed += new EventHandler(m_objEditor_FrameProcessed);


            ctrlPreview.PreviewItemDoubleClick -= new EventHandler(ctrlPreview_ItemDoubleClick);
            ctrlPreview.PreviewItemDoubleClick += new EventHandler(ctrlPreview_ItemDoubleClick);


            ctrlPreview.PreviewItemDragged -= new EventHandler(ctrlPreview_ItemDragged);
            ctrlPreview.PreviewItemDragged += new EventHandler(ctrlPreview_ItemDragged);

            ctrlPreview.PreviewDoubleClick -= new EventHandler(ctrlPreview_DoubleClick);
            ctrlPreview.PreviewDoubleClick += new EventHandler(ctrlPreview_DoubleClick);

            ctrlPreview.PreviewLeftButtonDown -= new EventHandler(ctrlLeftButtonDown_Click);
            ctrlPreview.PreviewLeftButtonDown += new EventHandler(ctrlLeftButtonDown_Click);

            ctrlPreview.Editor = m_objEditor;
            RibbonCtrl.Editor = m_objEditor;

            m_nFrameCounter = 0;

            RoutedCommand copyCommand = new RoutedCommand();
            copyCommand.InputGestures.Add(new KeyGesture(Key.C, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(copyCommand, CopyEventHandler));

            RoutedCommand pasteCommand = new RoutedCommand();
            pasteCommand.InputGestures.Add(new KeyGesture(Key.V, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(pasteCommand, PasteEventHandler));

            RoutedCommand UndoCommand = new RoutedCommand();
            UndoCommand.InputGestures.Add(new KeyGesture(Key.Z, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(UndoCommand, UndoEventHandler));

            RoutedCommand RedoCommand = new RoutedCommand();
            RedoCommand.InputGestures.Add(new KeyGesture(Key.Y, ModifierKeys.Control));
            CommandBindings.Add(new CommandBinding(RedoCommand, RedoEventHandler));

            //RoutedCommand TestCommand = new RoutedCommand();
            //TestCommand.InputGestures.Add(new KeyGesture(Key.T, ModifierKeys.Control));
            //CommandBindings.Add(new CommandBinding(TestCommand, TestEventHandler));
        }

        private void CopyEventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //handler code goes here.
            //m_objEditor.Copy();
            //MessageBox.Show("CTRL+C key pressed");
        }
        private void PasteEventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            //m_objEditor.Paste();
            //handler code goes here.
            //MessageBox.Show("CTRL+V key pressed");
        }

        private void UndoEventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (m_objEditor != null)
                m_objEditor.Undo();
        }

        private void RedoEventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (m_objEditor != null)
                m_objEditor.Redo();
        }

        private void TestEventHandler(object sender, ExecutedRoutedEventArgs e)
        {
            if (m_objEditor != null)
            {
                string id = "ticker-000";
                //m_objEditor.CGObject.
                m_objEditor.UpdateItemsList();
            }
        }

        private void ctrlLeftButtonDown_Click(object sender, EventArgs e)
        {
            if (ListBoxTools.SelectedItem != null && ctrlPreview.eMode == eMode.eM_Add)
            {
                try
                {
                    PreviewClickArgs args = (PreviewClickArgs)e;
                    ListBoxItem lbi = (ListBoxItem)ListBoxTools.SelectedItem;
                    TextBlock tbCurrent = FindVisualChild<TextBlock>(lbi);
                    string strType = tbCurrent.Text;
                    AddDefaultItem(strType, args.CGPoint);

                    ctrlPreview.eMode = eMode.eM_Default;
                    ctrlPreview.SetCurcsor(Cursors.Arrow);
                }
                catch
                {
                    //throw;
                }
            }
        }

        public Object MainSourceObject;
        public void SetSourceObject(Object pSource, MLCHARGENLib.CoMLCharGen pCG)
        {
            try
            {
                MainSourceObject = pSource;
                m_objEditor.SetSourceObject(pSource, pCG);
                m_objEditor.UpdateItemsList();                
                List<CGBaseItem> items = m_objEditor.CGItems;
                this.listBoxItems.ItemsSource = items;
            }
            catch (System.Exception) { }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }

        void m_objEditor_FrameProcessed(object sender, EventArgs e)
        {
            try
            {
                MPLATFORMLib.MFrame pFrame = (MPLATFORMLib.MFrame)sender;
                MPLATFORMLib.M_AV_PROPS avProps;
                pFrame.FrameAVPropsGet(out avProps);
                m_objEditor.AVProps = avProps;
                int cbPicture;
                long pbPicture;
                pFrame.FrameVideoGetBytes(out cbPicture, out pbPicture);
                int nRowBytes = avProps.vidProps.nRowBytes;
                IntPtr ptr = new IntPtr(pbPicture);

                BitmapSource bSource = BitmapSource.Create(avProps.vidProps.nWidth, Math.Abs(avProps.vidProps.nHeight), 96, 96, PixelFormats.Bgr32, null, ptr, cbPicture, nRowBytes);
                bSource.Freeze();
                Dispatcher.Invoke(new Action(delegate { ctrlPreview.SetSource(bSource); }));
                //ctrlPreview.SetSource(bSource);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(pFrame);

                m_nFrameCounter++;
                if (m_nFrameCounter == 10)
                {
                    GC.Collect();
                    m_nFrameCounter = 0;
                }
            }
            catch { }
        }

        void ctrlPreview_DoubleClick(object sender, EventArgs e)
        {
            m_objEditor.CurrGroupID = "";
        }

        void ctrlPreview_ItemDragged(object sender, EventArgs e)
        {
            ctrlPreview.eMode = eMode.eM_Default;
            ctrlPreview.SetCurcsor(Cursors.Arrow);
            try
            {
                ItemDragEventArgs args = (ItemDragEventArgs)e;
                AddDefaultItem(args.ItemType, new Point(args.Location.X, args.Location.Y));
            }
            catch
            {
                throw;
            }
        }

        void ctrlPreview_ItemDoubleClick(object sender, EventArgs e)
        {
            if (sender is CGTextItem)
            {
                //RibbonCtrl.se
            }
            else if (sender is CGTickerItem)
            {
                m_objEditor.CurrGroupID = ((CGTickerItem)sender).ID;
                m_objEditor.UpdateItemsList();
            }
            else if (sender is CGImageItem)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Multiselect = false;
                openFile.Title = "Select image";
                openFile.Filter = "All Images|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF;*.PNG";
                if (openFile.ShowDialog() == true)
                {
                    ((CGImageItem)sender).Path = openFile.FileName;
                }
            }
            else if (sender is CGGroupItem)
            {
                m_objEditor.CurrGroupID = ((CGGroupItem)sender).ID;
                m_objEditor.UpdateItemsList();
            }
            else if (sender is CGFlashItem)
            {
                OpenFileDialog openFile = new OpenFileDialog();
                openFile.Multiselect = false;
                openFile.Title = "Select flash file";
                openFile.Filter = "Flash Files (*.swf)|*.swf";
                if (openFile.ShowDialog() == true)
                {
                    ((CGFlashItem)sender).Path = openFile.FileName;
                }
            }
        }

        void m_objEditor_ItemsListUpdated(object sender, EventArgs e)
        {
            this.listBoxItems.SelectionChanged -= new SelectionChangedEventHandler(listBoxItems_SelectionChanged);
            this.listBoxItems.ItemsSource=null;
            this.listBoxItems.ItemsSource = m_objEditor.CGItems;
            for (int i = 0; i < m_objEditor.CGItems.Count; i++)
            {
                if (m_objEditor.CGItems[i].isSelected)
                {
                    listBoxItems.SelectedItems.Add(m_objEditor.CGItems[i]);
                }
            }

            if (m_objEditor.SelectedtItems.Count == 1)
            {
                RibbonCtrl.SetControlledObject(m_objEditor.SelectedtItems[0]);
                XMLEditor.SetControlledObject(m_objEditor.SelectedtItems[0]);

            }

            this.listBoxItems.SelectionChanged += new SelectionChangedEventHandler(listBoxItems_SelectionChanged);
        }

        void m_objEditor_ItemSelected(object sender, EventArgs e)
        {
            
            RibbonCtrl.SetControlledObject(null);
            XMLEditor.SetControlledObject(null);

            listBoxItems.SelectionChanged -= new SelectionChangedEventHandler(listBoxItems_SelectionChanged);
            listBoxItems.UnselectAll();
            foreach (CGBaseItem item in m_objEditor.SelectedtItems)
            {
                if (item.isSelected)
                {
                    listBoxItems.SelectedItems.Add(item);
                }
            }
            listBoxItems.SelectionChanged += new SelectionChangedEventHandler(listBoxItems_SelectionChanged);

            if (m_objEditor.SelectedtItems.Count == 1)
            {
                RibbonCtrl.SetControlledObject(m_objEditor.SelectedtItems[0]);
                XMLEditor.SetControlledObject(m_objEditor.SelectedtItems[0]);

            }
        }


        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        public static BitmapSource CreateBitmapSourceFromBitmap(System.Drawing.Bitmap bitmap)
        {
            if (bitmap == null)
                throw new ArgumentNullException("bitmap");

            lock (bitmap)
            {
                IntPtr hBitmap = new IntPtr();

                try
                {
                    hBitmap = bitmap.GetHbitmap();

                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                        hBitmap,
                        IntPtr.Zero,
                        Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
                catch( Exception ex )
                {
                    MessageBox.Show(ex.ToString());
                }

                finally
                {
                    if( hBitmap != null )
                        DeleteObject(hBitmap);

                    GC.Collect();
                }
            }

            return null;
        }

        private void listBoxItems_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string[] strIds = new string[listBoxItems.SelectedItems.Count];
            for (int i = 0; i < listBoxItems.SelectedItems.Count; i++)
            {
                strIds[i] = ((CGBaseItem)listBoxItems.SelectedItems[i]).ID;
            }
            m_objEditor.SelectItems(strIds, true);
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //LayoutProps.Show();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            LayoutItems.Show();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            LayoutXML.Show();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            LayoutTools.Show();
        }

        private void listBoxItems_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                m_objEditor.SelectItem("", true);
            }
            if (e.Key == Key.Delete)
            {
                UndoRedoManager.Instance().Push<HistoryItem>(m_objEditor.UndoAction,  new HistoryItem(m_objEditor.CurrentState,""));
                for (int i = 0; i < listBoxItems.SelectedItems.Count; i++)
                {
                    string strId = ((CGBaseItem)listBoxItems.SelectedItems[i]).ID;
                    m_objEditor.CGObject.RemoveItemWithDelay(strId, 0, 0);
                    
                }
                m_objEditor.UpdateItemsList();
            }
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog saveDlg = new Microsoft.Win32.SaveFileDialog();
            saveDlg.DefaultExt = ".ml-cgc"; // Default file extension
            saveDlg.Filter = "CG config files (.ml-cgc)|*.ml-cgc"; // Filter files by extension
            if (saveDlg.ShowDialog() == true)
            {
                IMLXMLPersist pXMLPersist = (IMLXMLPersist)m_objEditor.CGObject;
                pXMLPersist.SaveToXMLFile(saveDlg.FileName, 1);
            }
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            openFile.Multiselect = false;
            openFile.Filter = "CG config files (.ml-cgc)|*.ml-cgc"; // Filter files by extension
            if (openFile.ShowDialog() == true)
            {
                IMLXMLPersist pXMLPersist = (IMLXMLPersist)m_objEditor.CGObject;
                pXMLPersist.LoadFromXML(openFile.FileName, -1);

                m_objEditor.UpdateItemsList();
                List<CGBaseItem> items = m_objEditor.CGItems;

                this.listBoxItems.ItemsSource = items;
            }
        }

        private void ComoBoxOutputFormat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //try
            //{
            //    VideoFormat selectedFormat = (VideoFormat)ComoBoxOutputFormat.SelectedItem;
            //    M_AV_PROPS props = new M_AV_PROPS();
            //    props.vidProps.eVideoFormat = selectedFormat.eVFormat;
            //    m_objEditor.AVProps = props;
            //}
            //catch (System.Exception ex)
            //{
            //    throw ex;
            //}
        }

        private void ButtonBG_Click(object sender, RoutedEventArgs e)
        {
            //Microsoft.Win32.OpenFileDialog openFile = new Microsoft.Win32.OpenFileDialog();
            //openFile.Filter = "All Images|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF;*.PNG";
            //if (openFile.ShowDialog() == true)
            //{
            //    m_objEditor.SetBackGround(openFile.FileName);
            //}

        }

        private void ButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            //m_objEditor.SetPlayMode();
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            //m_objEditor.SetPauseMode();
        }

        private void MetroWindow_Closing(object sender, CancelEventArgs e)
        {
            if (m_objEditor != null)
            {
                m_objEditor.SelectItem("", true);

                m_objEditor.AbortThread();
            }
        }

        private void ListBoxTools_MouseMove(object sender, MouseEventArgs e)
        {
            // Get the current mouse position
            Point ptCurrentPos = e.GetPosition(null);
            Vector diff = m_ptStartPos - ptCurrentPos;

            if (e.LeftButton == MouseButtonState.Pressed &&
                (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
                Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
 
                // Get the dragged ListViewItem
                ListView listView = sender as ListView;

                string itemName = string.Empty;

                ListBoxItem currentItem = FindVisualParent<ListBoxItem>((DependencyObject)e.OriginalSource);
                if (currentItem != null)
                {
                    TextBlock item = FindVisualChild<TextBlock>(currentItem);
                    itemName = item.Text;
                    ListBox parent = (ListBox)sender;
                    DragDrop.DoDragDrop(parent, itemName, DragDropEffects.Move);
                }
            }
        }

        private void ListBoxTools_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            ListBoxItem currentItem = FindVisualParent<ListBoxItem>((DependencyObject)e.OriginalSource);
            if (currentItem != null)
            {
                m_ptStartPos = e.GetPosition(null);
                ctrlPreview.eMode = eMode.eM_Add;
                ctrlPreview.SetCurcsor(Cursors.Cross);
            }
        }

        private static T FindVisualParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject dep = child;
            while ((dep != null) && !(dep is T))
            {
                dep = VisualTreeHelper.GetParent(dep);
            }
            if (dep == null)
                return null;
            return dep as T;
        }

        private static T FindVisualChild<T>(DependencyObject parent) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                if (child != null && child is T)
                    return (T)child;
                else
                {
                    T childOfChild = FindVisualChild<T>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void AddDefaultItem(string type, Point cgCoords)
        {
            if (type != null && type != string.Empty)
            {
                try
                {
                    UndoRedoManager.Instance().Push<HistoryItem>(m_objEditor.UndoAction, new HistoryItem(m_objEditor.CurrentState, ""));
                    string strID = "";
                    if (type == "Text")
                    {
                        m_objEditor.CGObject.AddNewItem("Sample Text", cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                        
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Date")
                    {
                        m_objEditor.CGObject.AddNewItem(@"<text font='Arial' type='date-time'>yyyy/MM/dd</text>", cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Time")
                    {
                        m_objEditor.CGObject.AddNewItem(@"<text font='Arial' type='date-time'>HH:mm:ss</text>", cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Timecode")
                    {
                        m_objEditor.CGObject.AddNewItem(@"<text font='Arial' type='timecode' time-offset='00:00:00:00'>HH:MM:SS:FF</text>", cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Graphics")
                    {
                        m_objEditor.CGObject.AddNewItem(@"<graphics type='rect' round-corners='0.30' outline='3.0' color='ML(180)' outline-color='white'/>", cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Image")
                    {
                        if (m_strImagePath == null || m_strImagePath == string.Empty)
                        {

                            OpenFileDialog openFile = new OpenFileDialog();
                            openFile.Multiselect = false;
                            openFile.Title = "Select image";
                            openFile.Filter = "All Images|*.BMP;*.JPG;*.JPEG;*.GIF;*.TIF;*.TIFF;*.PNG";
                            if (openFile.ShowDialog() == true)
                            {
                                m_objEditor.CGObject.AddNewItem(openFile.FileName, cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                                m_objEditor.UpdateItemsList();
                                m_strImagePath = openFile.FileName;
                            }
                        }
                        else
                        {
                            m_objEditor.CGObject.AddNewItem(m_strImagePath, cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                            m_objEditor.UpdateItemsList();
                        }
                    }
                    if (type == "Image seq.")
                    {
                        System.Windows.Forms.FolderBrowserDialog openFolder = new System.Windows.Forms.FolderBrowserDialog();
                        //System.Windows.Forms.DialogResult reopenFoldersult = openFolder.ShowDialog();
                        if (openFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                        {
                            m_objEditor.CGObject.AddNewItem(openFolder.SelectedPath, cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                            m_objEditor.UpdateItemsList();
                        }
                    }
                    if (type == "Flash")
                    {
                        if (m_strFlashPath == null || m_strFlashPath == string.Empty)
                        {
                            //string strassembly = System.Reflection.Assembly.GetExecutingAssembly().Location.ToString();
                            //string imgpath = strassembly.Substring(0, strassembly.LastIndexOf(@"CGEditor\") + 9) + "Images\\";
                            OpenFileDialog openFile = new OpenFileDialog();
                            openFile.Multiselect = false;
                            openFile.Title = "Select flash file";
                            openFile.Filter = "JPEG Files (*.swf)|*.swf";
                            if (openFile.ShowDialog() == true)
                            {
                                m_objEditor.CGObject.AddNewItem(openFile.FileName, cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                                m_objEditor.UpdateItemsList();
                                m_strFlashPath = openFile.FileName;
                            }
                        }
                        else
                        {
                            m_objEditor.CGObject.AddNewItem(m_strFlashPath, cgCoords.X, cgCoords.Y, 0, 1, ref strID);
                            m_objEditor.UpdateItemsList();
                        }
                    }
                    if (type == "Ticker")
                    {
                        string strTickerDescr = @"<ticker type='ticker'>
<background color='ML(180)-White(180)'/>
<default-text bold=true size='20' style='uppercase'/>
</ticker>";
                        m_objEditor.CGObject.TickerAddNew(strTickerDescr, cgCoords.X, cgCoords.Y, 500, 100, 0, 1, ref strID);
                        m_objEditor.CGObject.TickerAddContent(strID, "Ticker content1\r\nTickercontent2\r\nTicker content3", "remove-all");
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Crawling Ticker")
                    {
                        string strTickerDescr = @"<ticker type='crawl'>
<background color='ML(180)-White(180)'/>
<default-text bold=true size='20' style='uppercase'/>
</ticker>";
                        m_objEditor.CGObject.TickerAddNew(strTickerDescr, cgCoords.X, cgCoords.Y, 700, 50, 0, 1, ref strID);
                        m_objEditor.CGObject.TickerAddContent(strID, "Scrolling ticker content", "multiline, remove-all");
                        m_objEditor.UpdateItemsList();
                    }
                    if (type == "Rolling Ticker")
                    {
                        string strTickerDescr = @"<ticker type='roll'>
<background color='ML(180)*White(180)'/>
<default-text bold=true size='20' style='uppercase'/>
</ticker>";
                        m_objEditor.CGObject.TickerAddNew(strTickerDescr, cgCoords.X, cgCoords.Y, 500, 300, 0, 1, ref strID);
                        m_objEditor.CGObject.TickerAddContent(strID, "Rolling ticker content", "multiline, remove-all");
                        m_objEditor.UpdateItemsList();
                    }

                    //if (strID != string.Empty);
                    //m_objEditor.SelectItem(strID, true);
                }
                catch
                {
                   //throw;
                }
            }
        }

    }
}
