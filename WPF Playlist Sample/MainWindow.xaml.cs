using System;
using System.Windows;
using MPLATFORMLib;
using Common;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace WPFPlaylistSample
{
    public partial class MainWindow : Window
    {
        private string _current_play_list = "";
        private string _current_char_gen = "";

        private string _sub_play_list = "";
        private string _sub_char_gen = "";

        private bool _is_sub = false;

        private double _current_play_pos = 0;

        private Settings mSettings = new Settings(@"MainWindow");
        public Common.Settings Settings
        {
            get
            {
                return mSettings;
            }
        }
        public MPlaylistClass MyPlaylist;
        public MainWindow()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MyPlaylist = new MPlaylistClass();
            }
            catch (Exception exception)
            {
                MessageBox.Show("Can't create a MPlatform's object: " + exception.ToString(), "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
                Close();
                return;
            }
            
            MyPlaylist.ObjectStart(null);
            string objectName;
            MyPlaylist.ObjectNameGet(out objectName);
            MPreviewControl1.SetControlledObject(MyPlaylist);
            MFileState1.SetControlledObject(MyPlaylist);
            MPersistControl1.SetControlledObject(MyPlaylist);
            MPersistControl1.Filter = "MPlatform Config Files (*.mpl, *.xml)|*.mpl;*.xml;*.mlp|All Files|*.*";
            MPersistControl1.DefaultExt = ".xml";
            MFormatsControl1.SetControlledObject(MyPlaylist);
            MFileRateControl1.SetControlledObject(MyPlaylist);
            MFileSeeking1.SetControlledObject(MyPlaylist);
            PlaylistBackground1.SetControlledObject(MyPlaylist);
            MPlaylistControl1.SetControlledObject(MyPlaylist);
            MRendererList1.SetSourceObject(MyPlaylist);

            MPersistControl1.OnLoad += new EventHandler(mPersistControl1_OnLoad);
            MyPlaylist.OnFrame += new IMEvents_OnFrameEventHandler(myPlaylist_OnFrame);
            MyPlaylist.OnEvent += new IMEvents_OnEventEventHandler(MyPlaylist_OnEvent);

            MObjCharGen = new MLCHARGENLib.CoMLCharGenClass();
            MyPlaylist.PluginsAdd(MObjCharGen, 0);
        }

        void MyPlaylist_OnEvent(string bsChannelID, string bsEventName, string bsEventParam, object pEventObject)
        {
            UpdateSeekControl();
        }

        private void mPersistControl1_OnLoad(object sender, EventArgs e)
        {
            MPlaylistControl1.UpdateList(true);
        }

        private void myPlaylist_OnFrame(string bschannelid, object pmframe)
        {
            MFileSeeking1.UpdatePos();
        }

        private void checkBoxVSource_Checked(object sender, RoutedEventArgs e)
        {
            MyPlaylist.ObjectVirtualSourceCreate( 1 , "", "");
        }

        private void checkBoxVSource_Unchecked(object sender, RoutedEventArgs e)
        {
            MyPlaylist.ObjectVirtualSourceCreate(0, "", "");
        }

        string _mStrItemId;
        public MLCHARGENLib.CoMLCharGenClass MObjCharGen;
        private void checkBoxCG_Checked(object sender, RoutedEventArgs e)
        {
            MObjCharGen = new MLCHARGENLib.CoMLCharGenClass();
            MyPlaylist.PluginsAdd(MObjCharGen, 0);

            // MObjCharGen.AddNewItem("MediaLooks", 0.1, 0.1, 1, 1, ref _mStrItemId);
            // MObjCharGen.SetItemProperties(_mStrItemId, "movement::speed-x", "1", "", 0);
            // MObjCharGen.ShowItem(_mStrItemId, 1, 1000);
            buttonCG_Props.IsEnabled = true;
        }

        private void checkBoxCG_Unchecked(object sender, RoutedEventArgs e)
        {
            MyPlaylist.PluginsRemove(MObjCharGen);
            System.Runtime.InteropServices.Marshal.ReleaseComObject(MObjCharGen);
            MObjCharGen = null;
            _mStrItemId = "";
            buttonCG_Props.IsEnabled = false;
        }

        private void buttonCG_Props_Click(object sender, RoutedEventArgs e)
        {
            if (MObjCharGen != null)
            {
                CGEditor.MainWindow CGEdWindow = new CGEditor.MainWindow();
                CGEdWindow.SetSourceObject(MyPlaylist, MObjCharGen);
                CGEdWindow.Show();
            }
                //MObjCharGen.ShowPropertiesPage(0);
        }

        void UpdateSeekControl()
        {
            // Update seek control
            if (checkBoxListSeek.IsChecked==false)
             {
                double dblPos;
                MItem pCurrent;
                string strPath;
                MyPlaylist.PlaylistGetByIndex(-1, out dblPos, out strPath, out pCurrent);
                MFileSeeking1.SetControlledObject(pCurrent);
            }
            else
            {
                //mSeekControl1.Enabled = true;
                MFileSeeking1.SetControlledObject(MyPlaylist);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (MyPlaylist!=null) MyPlaylist.ObjectClose();

        }

        private void CheckBoxListSeek_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                UpdateSeekControl();
            }
            catch (Exception)
            {
                MessageBox.Show("Please choose a file.");
                checkBoxListSeek.IsChecked = true;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog fileDialog = new SaveFileDialog();

            fileDialog.Filter = "Timeline Schedule Files (*.timeline)|*.timeline|All Files|*.*";
            fileDialog.DefaultExt = ".timeline";
            if (fileDialog.ShowDialog() == true)
            {
                MyPlaylist.PersistSaveToString("", out string my_play_list_string, "");
                MObjCharGen.SaveToXMLString(out string my_obj_char_gen_string, 1);

                int my_play_list_length = my_play_list_string.Length;
                int my_obj_char_gen_length = my_obj_char_gen_string.Length;

                List<byte> src_bytes = new List<byte>();
                src_bytes.AddRange(BitConverter.GetBytes(my_play_list_length));
                src_bytes.AddRange(BitConverter.GetBytes(my_obj_char_gen_length));
                src_bytes.AddRange(Encoding.UTF8.GetBytes(my_play_list_string));
                src_bytes.AddRange(Encoding.UTF8.GetBytes(my_obj_char_gen_string));

                File.WriteAllBytes(fileDialog.FileName, DataCompressionClass.Compress(src_bytes.ToArray()));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Timeline Schedule Files (*.timeline)|*.timeline|All Files|*.*";
            fileDialog.DefaultExt = ".timeline";
            if (fileDialog.ShowDialog() == true)
            {
                byte[] src_bytes = DataCompressionClass.Decompress(File.ReadAllBytes(fileDialog.FileName));

                int my_play_list_length = BitConverter.ToInt32(src_bytes, 0);
                int my_obj_char_gen_length = BitConverter.ToInt32(src_bytes, 4);

                byte[] my_play_list_bytes = new byte[my_play_list_length];
                byte[] my_obj_char_gen_bytes = new byte[my_obj_char_gen_length];

                Array.Copy(src_bytes, 8, my_play_list_bytes, 0, my_play_list_length);
                Array.Copy(src_bytes, 8 + my_play_list_length, my_obj_char_gen_bytes, 0, my_obj_char_gen_length);

                MyPlaylist.PersistLoad("", Encoding.UTF8.GetString(my_play_list_bytes), "");
                MObjCharGen.LoadFromXML(Encoding.UTF8.GetString(my_obj_char_gen_bytes), -1);
                MPlaylistControl1.UpdateList(true);
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();

            fileDialog.Filter = "Timeline Schedule Files (*.timeline)|*.timeline|All Files|*.*";
            fileDialog.DefaultExt = ".timeline";
            if (fileDialog.ShowDialog() == true)
            {
                byte[] src_bytes = DataCompressionClass.Decompress(File.ReadAllBytes(fileDialog.FileName));

                int my_play_list_length = BitConverter.ToInt32(src_bytes, 0);
                int my_obj_char_gen_length = BitConverter.ToInt32(src_bytes, 4);

                byte[] my_play_list_bytes = new byte[my_play_list_length];
                byte[] my_obj_char_gen_bytes = new byte[my_obj_char_gen_length];

                Array.Copy(src_bytes, 8, my_play_list_bytes, 0, my_play_list_length);
                Array.Copy(src_bytes, 8 + my_play_list_length, my_obj_char_gen_bytes, 0, my_obj_char_gen_length);

                _sub_play_list = Encoding.UTF8.GetString(my_play_list_bytes);
                _sub_char_gen = Encoding.UTF8.GetString(my_obj_char_gen_bytes);
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            // Play sub
            if(_is_sub == false)
            {
                _is_sub = true;
                MyPlaylist.PersistSaveToString("", out string my_play_list_string, "");
                MObjCharGen.SaveToXMLString(out string my_obj_char_gen_string, 1);

                _current_play_list = my_play_list_string;
                _current_char_gen = my_obj_char_gen_string;

                MyPlaylist.FilePosGet(out double _filepos);
                _current_play_pos = _filepos;

                MyPlaylist.PersistLoad("", _sub_play_list, "");
                MObjCharGen.LoadFromXML(_sub_char_gen, -1);
                MPlaylistControl1.UpdateList(true);

                MyPlaylist.FilePlayStart();
            }
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            // Stop sub
            if(_is_sub == true)
            {
                _is_sub = false;
                MyPlaylist.PersistLoad("", _current_play_list, "");
                MObjCharGen.LoadFromXML(_current_char_gen, -1);
                MPlaylistControl1.UpdateList(true);

                MyPlaylist.FilePosSet(_current_play_pos, 1.0);

                MyPlaylist.FilePlayStart();
            }
        }
    }
}
