using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xceed.Wpf.Toolkit.PropertyGrid.Attributes;

namespace CGEditor.CGItemWrappers
{
    public class AlignItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("center");
            col.Add("left");
            col.Add("right");
            //col.Add("!!! right-left");
            col.Add("top");
            col.Add("top-left");
            col.Add("top-right");
            //col.Add("!!! top-right-left");
            col.Add("bottom");
            col.Add("bottom-left");
            col.Add("bottom-right");
            return col;
        }
    }

    public class FontStylesItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("uppercase");
            col.Add("lowercase");
            return col;
        }
    }

    public class YesNoItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("yes");
            col.Add("no");
            return col;
        }
    }

    public class MoveTypeItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("linear");
            col.Add("accel-both");
            col.Add("accel-start");
            col.Add("accel-stop");
            return col;
        }
    }

    public class InterlaceItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("auto");
            col.Add("top");
            col.Add("bottom");
            col.Add("progressive");
            return col;
        }
    }

    public class ScaleItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("fit-ar");
            col.Add("exact-fit");
            col.Add("no-scale");
            col.Add("shrink-to-fit");
            col.Add("shrink-to-fit-ar");
            col.Add("cover-ar");
            col.Add("text-scale");
            return col;
        }
    }

    public class PlayModeItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("loop");
            col.Add("onetime");
            col.Add("onetime-hide");
            return col;
        }
    }

    public class TrueFalseItemsSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("true");
            col.Add("false");
            return col;
        }
    }

    public class ShapeTypeSource : IItemsSource
    {
        public ItemCollection GetValues()
        {
            ItemCollection col = new ItemCollection();
            col.Add("rect");
            col.Add("block-1");
            col.Add("block-2");
            col.Add("block-3");
            col.Add("block-4");
            col.Add("block-5");
            col.Add("block-6");
            col.Add("circle");
            col.Add("ellipse");
            col.Add("triangle");
            col.Add("polygon");
            col.Add("star");
            return col;
        }
    }
}
