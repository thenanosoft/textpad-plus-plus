using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using TextEditor.Functionality;

namespace TextEditor
{
    public class EditOperation
    {

        private UndoRedoClass data;

        public EditOperation()
        {
            data = new UndoRedoClass();
        }

        public bool TxtAreaTextChangeRequired { get; set; } = true;


        public string UndoClicked()
        {
            TxtAreaTextChangeRequired = false;
            return data.Undo();
        }

        public string RedoClicked()
        {
            TxtAreaTextChangeRequired = false;
            return data.Redo();
        }

        public void Add_UndoRedo(string item)
        {
            data.AddItem(item);
        }

        public bool CanUndo()
        {
            return data.canUndo();
        }

        public bool CanRedo()
        {
            return data.canRedo();
        }


        /// Search Result
        public FindNextResult findNext(FindNextSearch search)
        {
            FindNextResult result = new FindNextResult();
            int position = -1;
            StringComparison s = search.MatchCase ? StringComparison.CurrentCulture :
                StringComparison.CurrentCultureIgnoreCase;
            if (search.Direction == "UP")
            {
                position = search.Content.Substring(0, search.Position)
                    .LastIndexOf(search.SearchString, s);
                search.Success = position >= 0 ? true : false;
                result.SearchStatus = search.Success;
            }
            else
            {
                int start = search.Success ? search.Position + search.SearchString.Length :
                    search.Position;
                position = start + search.Content
                    .Substring(start, search.Content.Length - start)
                    .IndexOf(search.SearchString, s);
                search.Success = position - start >= 0 ? true : false;
                result.SearchStatus = search.Success;
            }
            result.SelectionStart = result.SearchStatus ? position : -1;
            return result;
        }
        public string DateTime_Now()
        {
            return DateTime.Now.ToString();
        }
    }
}
