using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MLCHARGENLib;
using CGEditor.CGItemWrappers;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using MPLATFORMLib;
using eMFrameClone = MPLATFORMLib.eMFrameClone;

namespace CGEditor
{
    public class Editor
    {
        //private MPLATFORMLib.MFrames m_framesFactory;
        //private MLCHARGENLib.IMFrame m_frmBackground; //Background Frame
        //public MLCHARGENLib.IMFrame m_frmProcessedFrame; //Processed Frame 
        private MPLATFORMLib.IMEvents_Event m_objSource;

        //private long m_llFrameCounter;
        //private bool m_bPlay = false;

        public event EventHandler FrameProcessed;
        public event EventHandler ItemSelected;
        public event EventHandler ItemsListUpdated;

        private List<CopyItem> m_CopyList;
        private int m_nPasteCount;

        public string CurrentState
        {
            get
            {
                string state = string.Empty;
                if (m_objCG != null)
                    ((IMLXMLPersist)m_objCG).SaveToXMLString(out state, 0);
                return state;
            }
        }

        private string m_strBGPath;
        public string BackgroundPath
        {
            set
            {
                m_strBGPath = value;
            }
        }

        public string m_strCurrGroupID;
        public string CurrGroupID
        {
            get { return m_strCurrGroupID; }
            set 
            { 
                m_strCurrGroupID = value;
                if (m_strCurrGroupID != null && m_strCurrGroupID != string.Empty)
                {
                    SelectGroup(m_strCurrGroupID);
                }
                if (m_strCurrGroupID == string.Empty)
                {
                    IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;
                    cgedit.EditSelectionRemove("");
                    UpdateItemsList();
                }
            }
        }

        private IMSource previewSource;
        public Object SetSourceObject(Object pSource, MLCHARGENLib.CoMLCharGen pCG)
        {
            Object pOld = (Object)m_objSource;
            try
            {
                m_objSource = (MPLATFORMLib.IMEvents_Event)pSource;
                previewSource = pSource as IMSource;
                //Enable frame data on source object
                string strVal;
                //((MPLATFORMLib.IMProps)m_objSource).PropsSet("object::on_frame.sync", "true");
                //((MPLATFORMLib.IMProps)m_objSource).PropsSet("object::on_frame.data", "true");
                
                
                if (m_objSource != null && pCG != null)
                {
                    CGObject = pCG;
                    UpdateItemsList();
                    isThread = true;
                    Thread newThread = new Thread(frameThread);
                    newThread.Start();
                    //m_objSource.OnFrame -= new MPLATFORMLib.IMEvents_OnFrameEventHandler(m_objSource_OnFrame);
                    //m_objSource.OnFrame += new MPLATFORMLib.IMEvents_OnFrameEventHandler(m_objSource_OnFrame);
                }

                m_CopyList = new List<CopyItem>();
                m_nPasteCount = 0;
            }
            catch (System.Exception) { }

            return pOld;
        }


        private long cookies = 0;
        private void frameThread()
        {
            while (isThread)
            {
                MFrame sourceFrame;
                try
                {
                    previewSource.SourceFrameGetEx(ref cookies, -1, out sourceFrame, 0);
                    MFrame clonedFrame;
                    sourceFrame.FrameClone(out clonedFrame, eMFrameClone.eMFC_Video, MPLATFORMLib.eMFCC.eMFCC_ARGB32);
                    Marshal.ReleaseComObject(sourceFrame);
                    if (FrameProcessed != null)
                        FrameProcessed(clonedFrame, new EventArgs());
                    Thread.Sleep(20);

                }
                catch (Exception)
                {
                    cookies = 0;
                    continue;
                }
                

            }

        }

        void m_objSource_OnFrame(string bsChannelID, object pMFrame)
        {
            try
            {
                //Get processed frame and prepare it for preview
                MFrame pConvertedFrame;
                M_AV_PROPS avPropsBase;
                M_AV_PROPS avPropsFinal;
                ((IMFrame)pMFrame).FrameAVPropsGet(out avPropsBase);

                M_AV_PROPS avPropsOverride = new M_AV_PROPS();

                if (avPropsBase.vidProps.nHeight > 0)
                    avPropsOverride.vidProps.nHeight = avPropsBase.vidProps.nHeight * -1;
                else
                    avPropsOverride.vidProps.nHeight = avPropsBase.vidProps.nHeight;

                avPropsOverride.vidProps.nWidth = avPropsBase.vidProps.nWidth;

                avPropsOverride.vidProps.fccType = eMFCC.eMFCC_ARGB32;

                ((IMFrame)pMFrame).FrameConvert(ref avPropsOverride.vidProps, out pConvertedFrame, "");

                pConvertedFrame.FrameAVPropsGet(out avPropsFinal);
                if (!isSameVideoProps(m_avProps.vidProps, avPropsFinal.vidProps))
                    m_avProps = avPropsFinal;

                if (FrameProcessed != null) 
                    FrameProcessed(pConvertedFrame, new EventArgs());

                System.Runtime.InteropServices.Marshal.ReleaseComObject(pMFrame);
            }
            catch { }

        }

        //public MLCHARGENLib.IMFrame BackgroundFrame
        //{
        //    get
        //    {
        //        if (m_strBGPath != null)
        //        {
        //            // Create 
        //            MPLATFORMLib.MFrame pFrame;
        //            m_framesFactory.FramesCreateFromFile(m_strBGPath, out pFrame, "");
        //            m_strBGPath = null;
        //            m_frmBackground = (MLCHARGENLib.IMFrame)pFrame;
        //            //m_frmBackground.FrameAVPropsGet(out m_avProps);
        //        }

        //        if (m_frmBackground == null)
        //            throw new Exception("Background frame isn't initialized");
        //        return m_frmBackground;
        //    }
        //}

        private M_AV_PROPS m_avProps;
        public M_AV_PROPS AVProps
        {
            get
            {
                    return m_avProps;
            }
            set 
            {
                m_avProps = value;

                //MLCHARGENLib.M_AV_PROPS tempProps = value;
                //MPLATFORMLib.MFrame mplFrame;
                //if (m_framesFactory == null)
                //    m_framesFactory = new MPLATFORMLib.MFrames();
                //m_framesFactory.FramesCreate(10000,"", out mplFrame);

                //MLCHARGENLib.IMFrame tempframe1 = (MLCHARGENLib.IMFrame)mplFrame;
                //MLCHARGENLib.IMFrame tempframe2;


                //tempProps.vidProps.fccType = eMFCC.eMFCC_ARGB32;

                //tempframe1.FrameConvert(ref tempProps.vidProps, out tempframe2, "");
                //tempframe2.FrameAVPropsGet(out tempProps);

                
                //if (tempProps.vidProps.nHeight > 0)
                //    tempProps.vidProps.nHeight = tempProps.vidProps.nHeight * -1;

                //if (!m_bPlay)
                //    tempProps.vidProps.dblRate = 0;

                //m_avProps = tempProps;
            }
        }

        
        private CoMLCharGen m_objCG;  //Medialooks Character Generator object
        public MLCHARGENLib.CoMLCharGen CGObject
        {
            get
            {
                //if (m_objCG == null)
                    //throw new Exception("Character Generator object isn't initialized");
                return m_objCG;
            }
            set
            {
                m_objCG = value;
                CGBaseItem.m_objCG = m_objCG; //Initialize CGItemWrappers
                m_objCG.OnCGEvent += new IMLCharGenEvents_OnCGEventEventHandler(m_objCG_OnCGEvent);
            }
        }

        void m_objCG_OnCGEvent(string bsItemOrCompositionID, string bsEventType, string bsEventParam)
        {

        }

        private List<CGBaseItem> m_CGItems;
        public List<CGBaseItem> CGItems
        {
            get
            {
                if (m_CGItems == null)
                    m_CGItems = new List<CGBaseItem>();
                return m_CGItems;
            }
            set { m_CGItems = value; }
        }

        public List<CGBaseItem> SelectedtItems
        {
            get
            {
                List<CGBaseItem> selected = new List<CGBaseItem>();
                IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;
                int nCount;
                cgedit.EditSelectionGetCount(out nCount);
                string strId, strColor;
                for (int i = 0; i < nCount; i++)
                {
                    cgedit.EditSelectionGetByIndex(i, out strId, out strColor);
                    foreach (CGBaseItem item in m_CGItems)
                    {
                        if (item.ID == strId)
                        {
                            item.isSelected = true;
                            selected.Add(item);
                            break;
                        }

                    }

                }
                return selected;
            }
        }

        public Editor()
        {
            try
            {
                //CGObject = new CoMLCharGen();
                //m_framesFactory = new MPLATFORMLib.MFrames();
                //MPLATFORMLib.MFrame tempFrame;
                //string path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();

                //Bitmap bmp = (Bitmap)(Properties.Resources._1080_2);
                //path = Path.Combine(path, "BG.jpg");
                //bmp.Save(path);
                //BackgroundPath = path;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        public void SelectItem(string id, bool clearSelection)
        {
            if (CGObject != null)
            {
                IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;
                if (clearSelection)
                {
                    cgedit.EditSelectionRemove("");
                    foreach (CGBaseItem item in CGItems)
                        item.isSelected = false;
                }

                if (CurrGroupID != null && CurrGroupID != string.Empty)
                {
                    cgedit.EditSelectionAdd(CurrGroupID, "lime", 0);
                }

                cgedit.EditSelectionAdd(id, "red", 0);
                foreach (CGBaseItem item in CGItems)
                {
                    if (item.ID == id)
                    {
                        item.isSelected = true;
                        break;
                    }
                }
                if (ItemSelected != null) ItemSelected(this, new EventArgs());
            }
        }

        public void SelectGroup(string groupId)
        {
            IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;

            cgedit.EditSelectionAdd(groupId, "lime", 0);
            foreach (CGBaseItem item in CGItems)
            {
                if (item.ID == groupId)
                {
                    item.isSelected = true;
                    break;
                }
            }
            if (ItemSelected != null) ItemSelected(this, new EventArgs());
        }

        public void Copy()
        {
            if (SelectedtItems != null && SelectedtItems.Count > 0)
            {
                m_CopyList.Clear();
                m_nPasteCount = 0;
                foreach (CGBaseItem item in SelectedtItems)
                {
                    if (m_CopyList != null)
                    {
                        if (item is CGTickerItem)
                            m_CopyList.Add(new CopyItem(item.ID, item.XML, item.PosX, item.PosY, item.Width, item.Height, ((CGTickerItem)item).TickerContent));
                        else
                            m_CopyList.Add(new CopyItem(item.ID, item.XML, item.PosX, item.PosY, item.Width, item.Height, item.Content));
                    }
                }
            }
        }

        public void Paste()
        {
            if (m_CopyList != null && m_CopyList.Count > 0)
            {
                foreach (CopyItem item in m_CopyList)
                {
                    string strNewId = string.Empty;
                    if (item.XML.Contains("<ticker"))
                    {
                        m_objCG.TickerAddNew(item.XML, item.PosX + 50 * (1 + m_nPasteCount), item.PosY + 50 * (1 + m_nPasteCount), item.Width, item.Height, 0, 1, ref strNewId);
                        //m_objCG.TickerAddContent(strNewId, "test", "remove-all");
                        //m_objCG.SetItemProperties(strNewId, "content", item.Content, "", 0);
                        m_objCG.TickerAddContent(strNewId, @"<content-item>Rolling ticker content</content-item>", "remove-all");
                    }
                    else
                        m_objCG.AddNewItem(item.XML, item.PosX + 50 * (1 + m_nPasteCount), item.PosY + 50 * (1 + m_nPasteCount), 0, 1, ref strNewId);
                }
                UpdateItemsList();
                m_nPasteCount++;
            }
        }

        public void Undo()
        {
            if (UndoRedoManager.Instance().HasUndoOperations)
            {
                UndoRedoManager.Instance().Undo();
            }
        }

        public void Redo()
        {
            if (UndoRedoManager.Instance().HasRedoOperations)
            {
                UndoRedoManager.Instance().Redo();
            }
        }

        public void UndoAction(HistoryItem state)
        {
            string currState = string.Empty;
            ((IMLXMLPersist)m_objCG).SaveToXMLString(out currState, 0);
            UndoRedoManager.Instance().Push<HistoryItem>(UndoAction, new HistoryItem(currState, state.ItemID));

            ((IMLXMLPersist)m_objCG).LoadFromXML(state.XMLState, -1);

            UpdateItemsList();
            foreach (CGBaseItem item in CGItems)
            {
                if (state.ItemID == string.Empty)
                    item.ForceUpdateNotification();
                else if (state.ItemID == item.ID)
                    item.ForceUpdateNotification();
            }
            
        }

        public void SelectItems(string[] ids, bool clearSelection)
        {
            IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;
            if (clearSelection)
            {
                cgedit.EditSelectionRemove("");
                foreach (CGBaseItem item in CGItems)
                    item.isSelected = false;
            }

            if (CurrGroupID != null && CurrGroupID != string.Empty)
            {
                cgedit.EditSelectionAdd(CurrGroupID, "lime", 0);
            }

            foreach (string id in ids)
            {
                cgedit.EditSelectionAdd(id, "red", 0);
                foreach (CGBaseItem item in CGItems)
                {
                    if (item.ID == id)
                    {
                        item.isSelected = true;
                    }
                }
            }
            if (ItemSelected != null) ItemSelected(this, new EventArgs());
        }

        //private double AvgTimePerFrame()
        //{
        //    if (!m_bPlay)
        //        return 0;
        //    if (m_avProps.vidProps.dblRate > 0)
        //        return 10000000.0 / m_avProps.vidProps.dblRate;

        //    return 40000.0;
        //}

        //public MLCHARGENLib.IMFrame ProcessFrame(MLCHARGENLib.IMFrame sourceFrame)
        //{
        //    MLCHARGENLib.IMFrame processedFrame = null;
        //    M_TIME mTime = new M_TIME();
        //    if (m_llFrameCounter == 0)
        //        mTime.eFFlags = eMFrameFlags.eMFF_Break;

        //    mTime.rtStartTime = (long)(AvgTimePerFrame() * m_llFrameCounter);
        //    mTime.rtEndTime = (long)(AvgTimePerFrame() * (m_llFrameCounter + 1));
        //    if (m_bPlay)
        //        m_llFrameCounter++;

        //    sourceFrame.FrameTimeSet(ref mTime);


        //    M_AV_PROPS avProps;
        //    sourceFrame.FrameAVPropsGet(out avProps);
        //    if (!m_bPlay)
        //    {
        //        avProps.vidProps.dblRate = 0;
        //        sourceFrame.FrameAVPropsSet(ref avProps);
        //        sourceFrame.FrameAVPropsGet(out avProps);
        //    }

        //    try
        //    {
        //        MLCHARGENLib.IMPlugin pPlugin = (MLCHARGENLib.IMPlugin)CGObject;
        //        pPlugin.OnMediaReceive(0, null, (MLCHARGENLib.IMFrame)sourceFrame, out processedFrame);
        //        processedFrame.FrameAVPropsGet(out avProps);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return processedFrame;  //This have to be released after being used
        //}

        //public void ProcessFrameInBG(System.ComponentModel.BackgroundWorker worker,
        //System.ComponentModel.DoWorkEventArgs e)
        //{
        //    while (true)
        //    {
        //        //MLCHARGENLib.IMFrame tempFrame;
        //        //BackgroundFrame.FrameConvert(ref m_avProps.vidProps, out tempFrame, "");

        //        //M_AV_PROPS avProps;
        //        //tempFrame.FrameAVPropsGet(out avProps);
        //        //if (avProps.vidProps.nHeight > 0)
        //        //    avProps.vidProps.nHeight = avProps.vidProps.nHeight * -1;

        //        //BackgroundFrame.FrameConvert(ref avProps.vidProps, out tempFrame, "");

        //        //m_frmProcessedFrame = ProcessFrame(tempFrame);

                
        //        MLCHARGENLib.IMFrame tempFrame;
        //        BackgroundFrame.FrameConvert(ref m_avProps.vidProps, out tempFrame, "");
        //        m_frmProcessedFrame = ProcessFrame(tempFrame);

        //        GC.Collect();
        //        worker.ReportProgress(0, null);
        //        Thread.Sleep(30);
        //    }
        //}

        //public void SetBackGround(string path)
        //{
        //    try
        //    {
        //        Bitmap bmp = new Bitmap(path);
        //        path = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
        //        bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);
        //        path = Path.Combine(path, "BG.jpg");
        //        bmp.Save(path);
        //        BackgroundPath = path;
        //    }
        //    catch (System.Exception ex)
        //    {
        //        MessageBox.Show("Cant create background frame from this image", "Error", MessageBoxButton.OK);
        //    }
        //}

        public void SelectByPoint(int left, int top)
        {
            IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;
            string strItemID;
            cgedit.EditSelectionRemove("");
            foreach (CGBaseItem item in CGItems)
                item.isSelected = false;

            if (CurrGroupID != null && CurrGroupID != string.Empty)
            {
                cgedit.EditSelectionAdd(CurrGroupID, "lime", 0);
            }

            cgedit.EditItemGetByPoint(m_strCurrGroupID, left, top, out strItemID);
            cgedit.EditSelectionAdd(strItemID, "red", 0);
            foreach (CGBaseItem item in CGItems)
            {
                if (item.ID == strItemID)
                {
                    item.isSelected = true;
                }
            }
            if (ItemSelected != null) ItemSelected(this, new EventArgs());
        }

        public void SelectByRectangle(int left, int top, int right, int bottom)
        {
            IMLCharGenEdit cgedit = (IMLCharGenEdit)CGObject;
            string strItemIDs;
            cgedit.EditSelectionRemove("");
            foreach (CGBaseItem item in CGItems)
                item.isSelected = false;

            if (CurrGroupID != null && CurrGroupID != string.Empty)
            {
                cgedit.EditSelectionAdd(CurrGroupID, "lime", 0);
            }

            cgedit.EditItemsGetByRect(CurrGroupID, left, top, right, bottom, out strItemIDs);

            if (strItemIDs != null && strItemIDs != string.Empty)
            {
                string[] strSplit = strItemIDs.Split(',');

                foreach (string s in strSplit)
                {
                    string strTrimmed = s.Trim();
                    cgedit.EditSelectionAdd(strTrimmed, "red", 0);
                    foreach (CGBaseItem item in CGItems)
                    {
                        if (item.ID == strTrimmed)
                        {
                            item.isSelected = true;
                            //break;
                        }
                    }
                }
            }
            if (ItemSelected != null) ItemSelected(this, new EventArgs());
        }

        public void AlignLefts()
        {
            if (SelectedtItems.Count > 1)
            {
                double left = SelectedtItems[0].PosX;
                for (int i = 1; i < SelectedtItems.Count; i++)
                {
                    SelectedtItems[i].PosX = left;
                }
            }
        }
        
        public void AlignCenters()
        {
            if (SelectedtItems.Count > 1)
            {
                double left = SelectedtItems[0].PosX;
                double width = SelectedtItems[0].Width;
                for (int i = 1; i < SelectedtItems.Count; i++)
                {
                    SelectedtItems[i].PosX = left + width / 2 - SelectedtItems[i].Width / 2;
                }
            }
        }

        public void AlignRights()
        {
            if (SelectedtItems.Count > 1)
            {
                double left = SelectedtItems[0].PosX;
                double width = SelectedtItems[0].Width;
                for (int i = 1; i < SelectedtItems.Count; i++)
                {
                    SelectedtItems[i].PosX = left + width - SelectedtItems[i].Width;
                }
            }
        }

        public void AlignTops()
        {
            if (SelectedtItems.Count > 1)
            {
                double top = SelectedtItems[0].PosY;
                for (int i = 1; i < SelectedtItems.Count; i++)
                {
                    SelectedtItems[i].PosY = top;
                }
            }
        }

        public void AlignMiddles()
        {
            if (SelectedtItems.Count > 1)
            {
                double top = SelectedtItems[0].PosY;
                double height = SelectedtItems[0].Height;
                for (int i = 1; i < SelectedtItems.Count; i++)
                {
                    SelectedtItems[i].PosY = top + height / 2 - SelectedtItems[i].Height / 2;
                }
            }
        }

        public void AlignBottoms()
        {
            if (SelectedtItems.Count > 1)
            {
                double top = SelectedtItems[0].PosY;
                double height = SelectedtItems[0].Height;
                for (int i = 1; i < SelectedtItems.Count; i++)
                {
                    SelectedtItems[i].PosY = top + height - SelectedtItems[i].Height;
                }
            }
        }
        

        public void UpdateItemsList()
        {
            CGItems.Clear();
            int nItems = 0;

            if (CurrGroupID != null && CurrGroupID != string.Empty)
                CGObject.GroupItemsCount(CurrGroupID, out nItems);
            else
                CGObject.GetItemsCount(out nItems);

            for (int i = 0; i < nItems; i++)
            {
                string strId = "";
                if (CurrGroupID != null && CurrGroupID != string.Empty)
                    CGObject.GroupGetItem(CurrGroupID, i, out strId);
                else
                    CGObject.GetItem(i, out strId);


                string strNameDesc;
                CG_ITEM_PROPS itemProps;
                CGObject.GetItemBaseProps(strId, out strNameDesc, out itemProps);
                string strXMLDesc;
                CGObject.GetItemProperties(strId, "", out strXMLDesc);

                CGBaseItem itemToAdd = null;
                if (itemProps.eType == eCG_ItemType.eCGIT_Text)
                {
                    CGTextItem text = new CGTextItem(strId);
                    if (text.FontFamily == "")
                        text.FontFamily = "Arial";
                    itemToAdd = text;
                    CGItems.Add(text);
                }
                if (itemProps.eType == eCG_ItemType.eCGIT_Flash)
                {
                    CGFlashItem flash = new CGFlashItem(strId);
                    itemToAdd = flash;
                    CGItems.Add(flash);
                }
                if (itemProps.eType == eCG_ItemType.eCGIT_Image)
                {
                    CGImageItem img = new CGImageItem(strId);
                    itemToAdd = img;
                    CGItems.Add(img);
                }
                if (itemProps.eType == eCG_ItemType.eCGIT_Graphics)
                {
                    CGGraphicsItem gr = new CGGraphicsItem(strId);
                    itemToAdd = gr;
                    CGItems.Add(gr);
                }
                if (strXMLDesc.Contains("<ticker"))
                {
                    CGTickerItem tkr = new CGTickerItem(strId);
                    itemToAdd = tkr;
                    CGItems.Add(tkr);
                }
                if ((itemProps.eType & eCG_ItemType.eCGIT_Group) != 0 && strXMLDesc.Contains(@"group-type='line'") && strXMLDesc.Contains(@"<text"))
                {
                    CGTickerLine line = new CGTickerLine(strId);
                    itemToAdd = line;
                    CGItems.Add(line);
                }
                if ((itemProps.eType & eCG_ItemType.eCGIT_Group) != 0 && !strXMLDesc.Contains("<ticker") && !strXMLDesc.Contains(@"group-type='line'"))
                {
                    CGGroupItem group = new CGGroupItem(strId);
                    itemToAdd = group;
                    CGItems.Add(group);
                }

                if (itemToAdd != null)
                {
                    IMLCharGenEdit cgEdit = (IMLCharGenEdit)CGObject;
                    int nCount;
                    cgEdit.EditSelectionGetCount(out nCount);
                    for (int j = 0; j < nCount; j++)
                    {
                        string strID, strColor;
                        cgEdit.EditSelectionGetByIndex(j, out strID, out strColor);
                        {
                            if (strID == itemToAdd.ID)
                                itemToAdd.isSelected = true;
                        }
                    }
                }


            }
            if (ItemsListUpdated != null) ItemsListUpdated(this, new EventArgs());
        }

        //public void SetPlayMode()
        //{
        //    m_bPlay = true;
        //}

        //public void SetPauseMode()
        //{
        //    m_bPlay = false;
        //}
        public void GroupSelected()
        {
            string strGroupID = string.Empty;
            for (int i = 0; i < SelectedtItems.Count; i++)
            {
                m_objCG.GroupAddItem(SelectedtItems[i].ID, 0, null, ref strGroupID);
            }
            SelectItem(strGroupID, true);
            UpdateItemsList();
        }
        public void UnGroupSelected()
        {
            string strGroupID = string.Empty;
            for (int i = 0; i < SelectedtItems.Count; i++)
            {
                string strNameDesc;
                CG_ITEM_PROPS baseProps;
                m_objCG.GetItemBaseProps(SelectedtItems[i].ID, out strNameDesc, out baseProps);

                if ((baseProps.eType & eCG_ItemType.eCGIT_Group) != 0)
                {
                    m_objCG.GroupRemoveAll(SelectedtItems[i].ID, eCG_GroupItemsRemoveType.eCGRT_Ungroup);
                    m_objCG.RemoveItem(SelectedtItems[i].ID, 0);
                }
            }
            SelectItem("", true);
            UpdateItemsList();
        }

        private bool isSameVideoProps(M_VID_PROPS vProps1, M_VID_PROPS vProps2)
        {
            if (vProps1.fccType == vProps2.fccType &&
                vProps1.nWidth == vProps2.nWidth &&
                vProps1.nHeight == vProps2.nHeight)
                return true;
            else return false;
        }

        private bool isThread = false;
        public void AbortThread()
        {
            isThread = false;
        }
    }

    public class CopyItem
    {
        public CopyItem(string id, string xml, double posX, double posY, double width, double height, string content)
        {
            m_strID = id;
            m_strXML = xml;
            m_posX = posX;
            m_posY = posY;
            m_width = width;
            m_height = height;
            m_strContent = content;
        }

        private string m_strContent;
        public string Content
        {
            get { return m_strContent; }
            set { m_strContent = value; }
        }

        private string m_strID;

        public string ID
        {
            get { return m_strID; }
            set { m_strID = value; }
        }

        private string m_strXML;
        public string XML
        {
            get { return m_strXML; }
            set { m_strXML = value; }
        }

        private double m_posX;
        public double PosX
        {
            get { return m_posX; }
            set { m_posX = value; }
        }

        private double m_posY;
        public double PosY
        {
            get { return m_posY; }
            set { m_posY = value; }
        }

        private double m_width;
        public double Width
        {
            get { return m_width; }
            set { m_width = value; }
        }

        private double m_height;
        public double Height
        {
            get { return m_height; }
            set { m_height = value; }
        }
    }

    public class HistoryItem
    {
        public HistoryItem(string state, string itemId)
        {
            m_strXMLState = state;
            m_strItemID = itemId;
        }

        private string m_strXMLState;
        public string XMLState
        {
            get { return m_strXMLState; }
            set { m_strXMLState = value; }
        }

        private string m_strItemID;
        public string ItemID
        {
            get { return m_strItemID; }
            set { m_strItemID = value; }
        }
    }
}
