using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CGEditor
{
    /// <summary>
    /// This acts as a container for multiple undo/redo records.
    /// </summary>
    public class UndoTransaction : IDisposable, IUndoRedoRecord
    {
        private string _name;
        private List<IUndoRedoRecord> _undoRedoOperations = new List<IUndoRedoRecord>();
        public UndoTransaction(string name="")
        {
            _name = name;
            UndoRedoManager.Instance().StartTransaction(this);
        }


        public string Name
        {
            get { return _name; }
        }

        public void Dispose()
        {
            UndoRedoManager.Instance().EndTransaction(this);
        }

         public void AddUndoRedoOperation(IUndoRedoRecord operation)
         {
             _undoRedoOperations.Insert(0, operation);
         }

        public int OperationsCount
        {
            get { return _undoRedoOperations.Count; }
        }

        #region Implementation of IUndoRedoRecord

        public void Execute()
        {
            _undoRedoOperations.ForEach((a)=>a.Execute());
        }



        #endregion
    }
}
