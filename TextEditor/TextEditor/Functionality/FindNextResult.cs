using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Functionality
{
    public class FindNextResult
    {
        bool searchStatus;
        int selectionStart;

        public bool SearchStatus { get => searchStatus; set => searchStatus = value; }
        public int SelectionStart { get => selectionStart; set => selectionStart = value; }
    }
}
