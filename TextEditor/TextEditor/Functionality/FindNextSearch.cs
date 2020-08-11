using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextEditor.Functionality
{
    public class FindNextSearch
    {
        string searchString;
        string direction;
        string content;
        int position;
        bool matchCase;
        bool success;

        public string SearchString { get => searchString; set => searchString = value; }
        public string Direction { get => direction; set => direction = value; }
        public string Content { get => content; set => content = value; }
        public int Position { get => position; set => position = value; }
        public bool MatchCase { get => matchCase; set => matchCase = value; }
        public bool Success { get => success; set => success = value; }
    }
}
