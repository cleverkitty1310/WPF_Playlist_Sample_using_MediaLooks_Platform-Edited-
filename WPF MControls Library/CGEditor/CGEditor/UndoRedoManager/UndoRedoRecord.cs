using System.Collections.Generic;
using System.Diagnostics;

namespace CGEditor
{
    public delegate void UndoRedoOperation<T>(T undoData);

    /// <summary>
    /// Contains information about an undo or redo record
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class UndoRedoRecord<T> : IUndoRedoRecord
    {
       
        private UndoRedoOperation<T> _operation;
        private T _undoData;    
        private string _description;

        public UndoRedoRecord()
        {
            
        }

     
        public UndoRedoRecord(UndoRedoOperation<T> operation, T undoData, string description="")
        {
            SetInfo(operation, undoData, description);
        }

        public void SetInfo(UndoRedoOperation<T> operation, T undoData, string description="")
        {
            _operation = operation;
            _undoData = undoData;
            _description = description;
        }
       
        
        public void Execute()
        {
            Trace.TraceInformation("Undo/redo operation {0} with data {1} - {2}", _operation, _undoData, _description);
            _operation(_undoData);
        }

        public string Name
        {
            get { return _description; }
        }
    }
}