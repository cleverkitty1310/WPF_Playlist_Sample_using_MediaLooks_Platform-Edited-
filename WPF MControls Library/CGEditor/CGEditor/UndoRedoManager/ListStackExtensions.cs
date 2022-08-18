using System.Collections.Generic;

namespace CGEditor
{
    /// <summary>
    /// Extension methods which allow a List to be used as a stack. This was created as we need to be able to manipulate the stack size dynamically
    /// which is not allowed by the Stack class
    /// </summary>
    public static class ListStackExtensions
    {
        public static void Push(this List<IUndoRedoRecord> list, IUndoRedoRecord item)
        {
            list.Insert(0, item);
        }

        public static IUndoRedoRecord Pop(this  List<IUndoRedoRecord> list)
        {
            IUndoRedoRecord ret = list[0];
            list.RemoveAt(0);
            return ret;
        }
    }
}