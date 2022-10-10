using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_Ole.Models
{
    public class MenuItem : Item
    {
        // This defines a menu item (a row in the menu)
        // Item interface ---------------------------------------------------
         public string Title { get; set; }
 
        // Menu item ---------------------------------------------------------
        public int Index { get; set; }
        public string Choises { get;set; }

        public Action MenuAction;

        // Constructor
        public MenuItem(string title, int index, string choises, Action menuAction)
        {
            // Each menu choise (row) has a title, an index, a string of valid
            // choises and a pointer to an action to take upon choise
            Title = title;
            Index = index;
            Choises = index + choises;
            MenuAction = menuAction;
        }

        // Perform this menu action
        public void Perform()
        {
            MenuAction();
        }


        // Item interface ------------------------------------------------------
        public void Display(int row = 0)
        {
            // Display the menu item (row)
            Console.WriteLine($"{Index}. {Title} (\"{Choises}\")".PadRight(50));
        }
    }
}

// By Ole Victor