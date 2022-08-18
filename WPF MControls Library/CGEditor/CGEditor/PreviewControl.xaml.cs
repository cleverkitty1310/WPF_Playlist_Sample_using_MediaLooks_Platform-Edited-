using System;
using System.Diagnostics;
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
using MLCHARGENLib;
using CGEditor.CGItemWrappers;

namespace CGEditor
{
    public enum eMode
    {
        eM_Default = 0,
        eM_Add = 1,
        eM_Drag = 2
    }

    public partial class PreviewControl : UserControl
    {
        private Point m_ptTransformOrigin;
        private Point m_ptImgStartPoint;
        private Point m_ptRectSelStartPoint;
        private string m_strCurrentGroupForEdit;

        private bool isResizingFromTop;
        private bool isResizingFromBottom;
        private bool isResizingFromLeft;
        private bool isResizingFromRight;
        private bool isMooving;
        private bool isPanning;
        private bool isRectSelecting;
        private bool isDoubleClick;

        private eMode m_eMode;
        public eMode eMode
        {
            get { return m_eMode; }
            set { m_eMode = value; }
        }
        
        
        private double m_dblResizeAreaWidth = 0.08;

        public event EventHandler PreviewItemDoubleClick;
        public event EventHandler PreviewItemDragged;
        public event EventHandler PreviewLeftButtonDown;
        public event EventHandler PreviewDoubleClick;

        public double ResizeAreaRatio
        {
            get { return m_dblResizeAreaWidth; }
            set { m_dblResizeAreaWidth = value; }
        }

        Editor m_Editor;
        public Editor Editor
        {
            get { return m_Editor; }
            set 
            { 
                m_Editor = value;
            }
        }

        public PreviewControl()
        {
            InitializeComponent();
            m_eMode = eMode.eM_Default;
        }

        public PreviewControl(Editor editor)
        {
            InitializeComponent();
            Editor = editor;
        }

        public void SetSource(BitmapSource bmpSource)
        {
            if (bmpSource != null)
            {
                this.imageCtrl.Source = bmpSource;
            }
        }

        public void SetEditGroup(string groupId)
        {
            if (groupId == null || groupId == string.Empty)
            {
                m_strCurrentGroupForEdit = string.Empty;
            }
            else
            {
                m_strCurrentGroupForEdit = groupId;
                ((IMLCharGenEdit)m_Editor.CGObject).EditSelectionAdd(m_strCurrentGroupForEdit, "lime", 1);
            }
        }

        private void imageCtrl_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (PreviewLeftButtonDown != null)
            {
                Point previewPt = e.GetPosition(imageCtrl);
                Point cgPt = GetCGPoint(previewPt);
                PreviewLeftButtonDown(this, new PreviewClickArgs(cgPt));
            }
            if (m_eMode == eMode.eM_Default)
            {
                if (imageCtrl.IsMouseCaptured) return;
                imageCtrl.CaptureMouse();

                isMooving = false;
                isRectSelecting = false;
                isResizingFromTop = false;
                isResizingFromBottom = false;
                isResizingFromLeft = false;
                isResizingFromRight = false;
                isDoubleClick = false;

                m_ptImgStartPoint = e.GetPosition(imageCtrl);
                double previewScaleFactor = GetPreviewScaleFactor();
                double transformScaleFactor = GetTransformScaleFactor();

                double selStartX = e.GetPosition(imageCtrl).X;
                double selStartY = e.GetPosition(imageCtrl).Y;
                m_ptRectSelStartPoint = new Point(selStartX, selStartY);
                Point pt = new Point(selStartX, selStartY);

                //for (int i = m_Editor.CGItems.Count ; i > 0; i-- )
                for (int i = 0; i < m_Editor.CGItems.Count; i++)
                {
                    //CGBaseItem item = m_Editor.CGItems[i-1];
                    CGBaseItem item = m_Editor.CGItems[i];

                    double recalcX = item.PosX * previewScaleFactor;
                    double recalcY = item.PosY * previewScaleFactor;

                    double recalcWidth = item.Width * previewScaleFactor;
                    double recalcHeight = item.Height * previewScaleFactor;

                    double resizeWidth = recalcWidth * m_dblResizeAreaWidth;
                    double resizeHeight = recalcHeight * m_dblResizeAreaWidth;

                    Rect rectInner = new Rect();
                    rectInner.X = recalcX + resizeWidth;
                    rectInner.Y = recalcY + resizeHeight;
                    rectInner.Width = recalcWidth - 2 * resizeWidth;
                    rectInner.Height = recalcHeight - 2 * resizeHeight;

                    //Rect rectOuterTop = new Rect();
                    //rectOuterTop.X = recalcX + resizeWidth;
                    //rectOuterTop.Y = recalcY - resizeHeight;
                    //rectOuterTop.Width = recalcWidth - 2 * resizeWidth;
                    //rectOuterTop.Height = 2 * resizeHeight;

                    //Rect rectOuterBottom = new Rect();
                    //rectOuterBottom.X = recalcX + resizeWidth;
                    //rectOuterBottom.Y = recalcY + recalcHeight - resizeHeight;
                    //rectOuterBottom.Width = recalcWidth - 2 * resizeWidth;
                    //rectOuterBottom.Height = 2 * resizeHeight;

                    //Rect rectOuterLeft = new Rect();
                    //rectOuterLeft.X = recalcX - resizeWidth;
                    //rectOuterLeft.Y = recalcY + resizeHeight;
                    //rectOuterLeft.Width = 2 * resizeWidth;
                    //rectOuterLeft.Height = recalcHeight - 2 * resizeHeight;

                    //Rect rectOuterRight = new Rect();
                    //rectOuterRight.X = recalcX + recalcWidth - resizeWidth;
                    //rectOuterRight.Y = recalcY + resizeHeight;
                    //rectOuterRight.Width = 2 * resizeWidth;
                    //rectOuterRight.Height = recalcHeight - 2 * resizeHeight;

                    item.StartLocation = new Point(item.PosX, item.PosY);
                    item.StartWidth = item.Width;
                    item.StartHeight = item.Height;

                    if (rectInner.Contains(pt))
                    {
                        if (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl))
                            m_Editor.SelectItem(item.ID, false);
                        else if (item.isSelected == false)
                            m_Editor.SelectItem(item.ID, true);


                        if (PreviewItemDoubleClick != null && e.ClickCount == 2)
                        {
                            PreviewItemDoubleClick(item, new EventArgs());
                            isDoubleClick = true;
                            return;
                        }

                        isMooving = true;

                    }

                    //if (rectOuterTop.Contains(pt))
                    //{
                    //    isResizingFromTop = true;
                    //    if (item.isSelected == false)
                    //        m_Editor.SelectItem(item.ID, true);
                    //    break;
                    //}
                    //if (rectOuterBottom.Contains(pt))
                    //{
                    //    isResizingFromBottom = true;
                    //    if (item.isSelected == false)
                    //        m_Editor.SelectItem(item.ID, true);
                    //    break;
                    //}
                    //if (rectOuterLeft.Contains(pt))
                    //{
                    //    isResizingFromLeft = true;
                    //    if (item.isSelected == false)
                    //        m_Editor.SelectItem(item.ID, true);
                    //    break;
                    //}
                    //if (rectOuterRight.Contains(pt))
                    //{
                    //    isResizingFromRight = true;
                    //    if (item.isSelected == false)
                    //        m_Editor.SelectItem(item.ID, true);
                    //    break;
                    //}
                }


                if (isMooving)
                {
                    UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ""));
                    return;
                }


                if (m_Editor.SelectedtItems.Count == 1)
                {
                    CGBaseItem item = m_Editor.SelectedtItems[0];
                    double recalcX = item.PosX * previewScaleFactor;// *transformScaleFactor;
                    double recalcY = item.PosY * previewScaleFactor;// *transformScaleFactor;

                    double recalcWidth = item.Width * previewScaleFactor;// *transformScaleFactor;
                    double recalcHeight = item.Height * previewScaleFactor;// *transformScaleFactor;

                    double resizeWidth = recalcWidth * m_dblResizeAreaWidth;
                    double resizeHeight = recalcHeight * m_dblResizeAreaWidth;

                    Rect rectOuterTop = new Rect();
                    rectOuterTop.X = recalcX + resizeWidth;
                    rectOuterTop.Y = recalcY - resizeHeight;
                    rectOuterTop.Width = recalcWidth - 2 * resizeWidth;
                    rectOuterTop.Height = 2 * resizeHeight;

                    //double cgX = rectOuterTop.X / previewScaleFactor / transformScaleFactor;
                    //double cgY = rectOuterTop.Y / previewScaleFactor / transformScaleFactor;

                    //double cgWidth = rectOuterTop.Width / transformScaleFactor / previewScaleFactor;
                    //double cgHeight = rectOuterTop.Height / transformScaleFactor / previewScaleFactor;


                    //string strId = "";
                    //m_Editor.CGObject.AddNewItem(@"<graphics type='rect' color='Red(120)-Green(180)-Blue(220)' outline-color='White/Orange/White' outline='3.0'/>", cgX, cgY, 0, 1, ref strId);
                    //m_Editor.CGObject.SetItemProperties(strId, "cg-props::height", cgHeight.ToString(), "", 0);
                    //m_Editor.CGObject.SetItemProperties(strId, "cg-props::width", cgWidth.ToString(), "", 0);

                    Rect rectOuterBottom = new Rect();
                    rectOuterBottom.X = recalcX + resizeWidth;
                    rectOuterBottom.Y = recalcY + recalcHeight - resizeHeight;
                    rectOuterBottom.Width = recalcWidth - 2 * resizeWidth;
                    rectOuterBottom.Height = 2 * resizeHeight;

                    Rect rectOuterLeft = new Rect();
                    rectOuterLeft.X = recalcX - resizeWidth;
                    rectOuterLeft.Y = recalcY + resizeHeight;
                    rectOuterLeft.Width = 2 * resizeWidth;
                    rectOuterLeft.Height = recalcHeight - 2 * resizeHeight;

                    Rect rectOuterRight = new Rect();
                    rectOuterRight.X = recalcX + recalcWidth - resizeWidth;
                    rectOuterRight.Y = recalcY + resizeHeight;
                    rectOuterRight.Width = 2 * resizeWidth;
                    rectOuterRight.Height = recalcHeight - 2 * resizeHeight;


                    item.StartLocation = new Point(item.PosX, item.PosY);
                    item.StartWidth = item.Width;
                    item.StartHeight = item.Height;

                    if (rectOuterTop.Contains(pt))
                    {
                        isResizingFromTop = true;
                        UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ""));
                        return;
                    }
                    if (rectOuterBottom.Contains(pt))
                    {
                        isResizingFromBottom = true;
                        UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ""));
                        return;
                    }
                    if (rectOuterLeft.Contains(pt))
                    {
                        isResizingFromLeft = true;
                        UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ""));
                        return;
                    }
                    if (rectOuterRight.Contains(pt))
                    {
                        isResizingFromRight = true;
                        UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ""));
                        return;
                    }
                }

                if (PreviewDoubleClick != null && e.ClickCount == 2)
                {
                    PreviewDoubleClick(this, new EventArgs());
                    return;
                }

                //if (!isMooving && !isDoubleClick)
                isRectSelecting = true;
            }
        }

        private void imageCtrl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (m_eMode == eMode.eM_Default)
            {
                rectangleSelect.Visibility = Visibility.Hidden;
                if (isRectSelecting)
                {

                    double transformScaleFactor = GetTransformScaleFactor();
                    double previewScaleFactor = GetPreviewScaleFactor();

                    double x = e.GetPosition(imageCtrl).X;
                    double y = e.GetPosition(imageCtrl).Y;

                    double xx = Math.Min(x, m_ptRectSelStartPoint.X);
                    double yy = Math.Min(y, m_ptRectSelStartPoint.Y);

                    double cgX = xx / previewScaleFactor;
                    double cgY = yy / previewScaleFactor;

                    double width = rectangleSelect.ActualWidth / transformScaleFactor / previewScaleFactor;
                    double height = rectangleSelect.ActualHeight / transformScaleFactor / previewScaleFactor;

                    int left = (int)cgX;
                    int top = (int)cgY;
                    int right = (int)(cgX + width);
                    int bottom = (int)(cgY + height);

                    if (width <= 10 || height <= 10)
                    {
                        m_Editor.SelectByPoint(left, top);
                    }
                    else
                        m_Editor.SelectByRectangle(left, top, right, bottom);

                }





                imageCtrl.ReleaseMouseCapture();

                isMooving = false;
                isRectSelecting = false;
                isResizingFromTop = false;
                isResizingFromBottom = false;
                isResizingFromLeft = false;
                isResizingFromRight = false;
                isDoubleClick = false;

                rectangleSelect.Margin = new Thickness(0, 0, 0, 0);
                rectangleSelect.Width = 0;
                rectangleSelect.Height = 0;

                m_ptRectSelStartPoint = new Point(0, 0);

                this.Cursor = Cursors.Arrow;
            }

            eMode = eMode.eM_Default;
            SetCurcsor(Cursors.Arrow);
        }

        private void imageCtrl_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (imageCtrl.IsMouseCaptured) return;
            double width = this.imageCtrl.Width;
            double heigth = this.imageCtrl.Height;
            Point pt = e.GetPosition(imageCtrl);
            if (imageCtrl.IsMouseCaptured) return;
            imageCtrl.CaptureMouse();
            m_ptImgStartPoint = e.GetPosition(gridPreview);
            m_ptTransformOrigin.X = imageCtrl.RenderTransform.Value.OffsetX;
            m_ptTransformOrigin.Y = imageCtrl.RenderTransform.Value.OffsetY;
            isPanning = true;
        }

        private void imageCtrl_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            imageCtrl.ReleaseMouseCapture();
            isPanning = false;
            this.Cursor = Cursors.Arrow;
        }

        private void imageCtrl_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_eMode == eMode.eM_Default)
            {
                double transformScaleFactor = GetTransformScaleFactor();
                double previewScaleFactor = GetPreviewScaleFactor();


                //=========================================================================
                if (!isMooving && !isRectSelecting && !isPanning && !isResizingFromTop
                    && !isResizingFromBottom && !isResizingFromLeft && !isResizingFromRight && m_Editor.SelectedtItems.Count == 1)
                {
                    double selStartX = e.GetPosition(imageCtrl).X;// *transformScaleFactor;
                    double selStartY = e.GetPosition(imageCtrl).Y;// *transformScaleFactor;
                    Point pt = new Point(selStartX, selStartY);
                    foreach (CGBaseItem item in m_Editor.SelectedtItems)
                    {
                        //CGBaseItem item = m_Editor.SelectedtItems[0];
                        double recalcX = item.PosX * previewScaleFactor;// *transformScaleFactor;
                        double recalcY = item.PosY * previewScaleFactor;// *transformScaleFactor;

                        double recalcWidth = item.Width * previewScaleFactor;// *transformScaleFactor;
                        double recalcHeight = item.Height * previewScaleFactor;// *transformScaleFactor;

                        double resizeWidth = recalcWidth * m_dblResizeAreaWidth;
                        double resizeHeight = recalcHeight * m_dblResizeAreaWidth;

                        Rect rectOuterTop = new Rect();
                        rectOuterTop.X = recalcX + resizeWidth;
                        rectOuterTop.Y = recalcY - resizeHeight;
                        rectOuterTop.Width = recalcWidth - 2 * resizeWidth;
                        rectOuterTop.Height = 2 * resizeHeight;

                        //double cgX = rectOuterTop.X / previewScaleFactor / transformScaleFactor;
                        //double cgY = rectOuterTop.Y / previewScaleFactor / transformScaleFactor;
                        //double cgWidth = rectOuterTop.Width / transformScaleFactor / previewScaleFactor;
                        //double cgHeight = rectOuterTop.Height / transformScaleFactor / previewScaleFactor;
                        //string strId = "";
                        //m_Editor.CGObject.AddNewItem(@"<graphics type='rect' color='Red(120)-Green(180)-Blue(220)' outline-color='White/Orange/White' outline='3.0'/>", cgX, cgY, 0, 1, ref strId);
                        //m_Editor.CGObject.SetItemProperties(strId, "cg-props::height", cgHeight.ToString(), "", 0);
                        //m_Editor.CGObject.SetItemProperties(strId, "cg-props::width", cgWidth.ToString(), "", 0);

                        Rect rectOuterBottom = new Rect();
                        rectOuterBottom.X = recalcX + resizeWidth;
                        rectOuterBottom.Y = recalcY + recalcHeight - resizeHeight;
                        rectOuterBottom.Width = recalcWidth - 2 * resizeWidth;
                        rectOuterBottom.Height = 2 * resizeHeight;

                        Rect rectOuterLeft = new Rect();
                        rectOuterLeft.X = recalcX - resizeWidth;
                        rectOuterLeft.Y = recalcY + resizeHeight;
                        rectOuterLeft.Width = 2 * resizeWidth;
                        rectOuterLeft.Height = recalcHeight - 2 * resizeHeight;

                        Rect rectOuterRight = new Rect();
                        rectOuterRight.X = recalcX + recalcWidth - resizeWidth;
                        rectOuterRight.Y = recalcY + resizeHeight;
                        rectOuterRight.Width = 2 * resizeWidth;
                        rectOuterRight.Height = recalcHeight - 2 * resizeHeight;


                        item.StartLocation = new Point(item.PosX, item.PosY);
                        item.StartWidth = item.Width;
                        item.StartHeight = item.Height;

                        if (rectOuterTop.Contains(pt))
                        {
                            this.Cursor = Cursors.SizeNS;
                            break;
                        }
                        else if (rectOuterBottom.Contains(pt))
                        {
                            this.Cursor = Cursors.SizeNS;
                            break;
                        }
                        else if (rectOuterLeft.Contains(pt))
                        {
                            this.Cursor = Cursors.SizeWE;
                            break;
                        }
                        else if (rectOuterRight.Contains(pt))
                        {
                            this.Cursor = Cursors.SizeWE;
                            break;
                        }
                        else this.Cursor = Cursors.Arrow;
                    }
                }
                //=========================================================================


                if (isPanning)
                {
                    this.Cursor = Cursors.Hand;
                    Point p = e.MouseDevice.GetPosition(gridPreview);
                    Matrix m = imageCtrl.RenderTransform.Value;
                    m.OffsetX = m_ptTransformOrigin.X + (p.X - m_ptImgStartPoint.X);
                    m.OffsetY = m_ptTransformOrigin.Y + (p.Y - m_ptImgStartPoint.Y);
                    imageCtrl.RenderTransform = new MatrixTransform(m);
                    labelStatus.Content = String.Format("X Offset = {0:0.00} , Y Offset = {1:0.00}", m.OffsetX, m.OffsetY);
                }
                else if (isMooving)
                {
                    this.Cursor = Cursors.SizeAll;
                    Point p = e.MouseDevice.GetPosition(imageCtrl);
                    foreach (CGBaseItem item in m_Editor.SelectedtItems)
                    {
                        double xPos = item.StartLocation.X + (p.X - m_ptImgStartPoint.X) / previewScaleFactor;
                        double yPos = item.StartLocation.Y + (p.Y - m_ptImgStartPoint.Y) / previewScaleFactor;
                        item.PosX = xPos;
                        item.PosY = yPos;
                    }
                    //labelStatus.Content = String.Format("X Pos = {0:0.00} , Y Offset = {1:0.00}", xPos, yPos);
                }
                else if (isResizingFromRight)
                {
                    this.Cursor = Cursors.SizeWE;
                    Point p = e.MouseDevice.GetPosition(imageCtrl);
                    foreach (CGBaseItem item in m_Editor.SelectedtItems)
                    {
                        item.Width = item.StartWidth + (p.X - m_ptImgStartPoint.X) / previewScaleFactor;
                    }
                }
                else if (isResizingFromLeft)
                {
                    this.Cursor = Cursors.SizeWE;
                    Point p = e.MouseDevice.GetPosition(imageCtrl);
                    foreach (CGBaseItem item in m_Editor.SelectedtItems)
                    {
                        item.Width = item.StartWidth - (p.X - m_ptImgStartPoint.X) / previewScaleFactor;
                        item.PosX = p.X / previewScaleFactor;
                    }
                }
                else if (isResizingFromTop)
                {
                    this.Cursor = Cursors.SizeNS;
                    Point p = e.MouseDevice.GetPosition(imageCtrl);
                    foreach (CGBaseItem item in m_Editor.SelectedtItems)
                    {
                        item.Height = item.StartHeight - (p.Y - m_ptImgStartPoint.Y) / previewScaleFactor;
                        item.PosY = p.Y / previewScaleFactor;
                    }
                }

                else if (isResizingFromBottom)
                {
                    this.Cursor = Cursors.SizeNS;
                    Point p = e.MouseDevice.GetPosition(imageCtrl);
                    foreach (CGBaseItem item in m_Editor.SelectedtItems)
                    {
                        item.Height = item.StartHeight + (p.Y - m_ptImgStartPoint.Y) / previewScaleFactor;
                    }
                }

                else if (isRectSelecting)
                {

                    double x = e.GetPosition(imageCtrl).X * transformScaleFactor;
                    double y = e.GetPosition(imageCtrl).Y * transformScaleFactor;

                    double xStart = m_ptRectSelStartPoint.X * transformScaleFactor;
                    double yStart = m_ptRectSelStartPoint.Y * transformScaleFactor;

                    UIElement container = VisualTreeHelper.GetParent(imageCtrl) as UIElement;
                    Point relativeLocation = imageCtrl.TranslatePoint(new Point(0, 0), container);

                    rectangleSelect.Margin = new System.Windows.Thickness(
                        Math.Min(x + relativeLocation.X, xStart + relativeLocation.X),
                        Math.Min(y + relativeLocation.Y, yStart + relativeLocation.Y),
                        0, 0);
                    rectangleSelect.Visibility = Visibility.Visible;
                    rectangleSelect.Width = Math.Abs(x - xStart);
                    rectangleSelect.Height = Math.Abs(y - yStart);
                }
            }
        }

        private void imageCtrl_MouseWheel(object sender, MouseWheelEventArgs e)
        {

            Point p = e.MouseDevice.GetPosition(imageCtrl);

            Matrix m = imageCtrl.RenderTransform.Value;
            if (e.Delta > 0)
                m.ScaleAtPrepend(1.1, 1.1, p.X, p.Y);
            else
                m.ScaleAtPrepend(1 / 1.1, 1 / 1.1, p.X, p.Y);
            imageCtrl.RenderTransform = new MatrixTransform(m);

            labelStatus.Content = "Zoom factor = " + String.Format("{0:0.00}", imageCtrl.RenderTransform.Value.M11); 
        }


        private double GetPreviewScaleFactor()
        {
            double scaleRatio = 0;
            if (m_Editor != null)
            {
                if (m_Editor.AVProps.vidProps.nWidth >= Math.Abs(m_Editor.AVProps.vidProps.nHeight))
                    scaleRatio = imageCtrl.ActualHeight / Math.Abs((double)m_Editor.AVProps.vidProps.nHeight);
                else
                    scaleRatio = imageCtrl.ActualWidth / (double)m_Editor.AVProps.vidProps.nWidth;
            }
            return scaleRatio;
        }

        private double GetTransformScaleFactor()
        {
            return imageCtrl.RenderTransform.Value.M11;
        }

        private void imageCtrl_Drop(object sender, DragEventArgs e)
        {
            if (PreviewItemDragged != null)
            {
                Point point = new Point(e.GetPosition(imageCtrl).X, e.GetPosition(imageCtrl).Y);
                point = GetCGPoint(point);
                object data = e.Data.GetData(typeof(String));
                PreviewItemDragged(this, new ItemDragEventArgs(data.ToString(), point));
            }
        }

        private Point GetCGPoint(Point previewPoint)
        {
            Point res = new Point();
            if (previewPoint != null)
            {
                double previewScaleFactor = GetPreviewScaleFactor();
                res.X = previewPoint.X / previewScaleFactor ;
                res.Y = previewPoint.Y / previewScaleFactor ;
            }
            return res;
        }

        public void SetCurcsor(Cursor cursor)
        {
            this.Cursor = cursor;
        }

        private void UserControl_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete && m_Editor != null)
            {
                UndoRedoManager.Instance().Push<HistoryItem>(m_Editor.UndoAction, new HistoryItem(m_Editor.CurrentState, ""));
                for (int i = 0; i < m_Editor.SelectedtItems.Count; i++)
                {
                    string strId = ((CGBaseItem)m_Editor.SelectedtItems[i]).ID;
                    m_Editor.CGObject.RemoveItemWithDelay(strId, 0, 0);
                }
                m_Editor.UpdateItemsList();
            }
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Focus();
        }
    }


    public class PreviewClickArgs : EventArgs
    {
        private Point m_cgPoint;
        public Point CGPoint
        {
            get { return m_cgPoint; }
            set { m_cgPoint = value; }
        }

        public PreviewClickArgs(Point cgPoint)
        {
            m_cgPoint = cgPoint;
        }
    }

    public class ItemDragEventArgs : EventArgs
    {
        public ItemDragEventArgs(string type, Point location)
        {
            m_strItemType = type;
            m_ptLocation = location;

        }
        private string m_strItemType;
        public string ItemType
        {
            get { return m_strItemType; }
            set { m_strItemType = value; }
        }
        private Point m_ptLocation;
        public System.Windows.Point Location
        {
            get { return m_ptLocation; }
            set { m_ptLocation = value; }
        }
    }
}
