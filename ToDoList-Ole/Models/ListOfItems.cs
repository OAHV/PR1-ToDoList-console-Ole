using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_Ole.Models
{
    public abstract class ListOfItems
    {
        // This is a template for any list of items
        protected ListOfItems(string header = "List Header", string footer = "List Footer")
        {
            Header = header;
            Footer = footer;
        }

        public string Header { get; set; }
        public string Footer { get; set; }

        public abstract void Print();
    }
}

// By Ole Victor