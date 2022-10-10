using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_Ole.Models
{
    internal interface Item
    {
        // Template for items like projects and tasks
        public string Title{ get; set; }

        public void Display(int row = 0);
    }
}

// By Ole Victor