using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Functionality
{
    public class UndoRedoClass
    {
        private Stack<string> UndoStack;
        private Stack<string> RedoStack;

        public UndoRedoClass()
        {
            UndoStack = new Stack<string>();
            RedoStack = new Stack<string>();
        }

        public void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
        }

        public void AddItem(string item)
        {
            UndoStack.Push(item);
        }

        public string Undo()
        {
            if (!canUndo())
            {
                return "";
            }
            else
            {

                string item = UndoStack.Pop();
                RedoStack.Push(item);
                return UndoStack.First();
            }
        }

        public string Redo()
        {
            if(RedoStack.Count == 0)
            {
                return UndoStack.First();
            }
            string item = RedoStack.Pop();
            UndoStack.Push(item);
            return UndoStack.First();
        }

        public bool canUndo()
        {
            return UndoStack.Count > 1;
        }

        public bool canRedo()
        {
            return RedoStack.Count > 0;
        }

        public List<string> UndoItems()
        {
            return UndoStack.ToList();
        }

        public List<string> RedoItems()
        {
            return RedoStack.ToList();
        }
    }
}
