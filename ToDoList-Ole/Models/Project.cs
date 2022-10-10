using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_Ole.Views;
using ToDoList_Ole.Models;

namespace ToDoList_Ole.Models
{
    public class Project : Item
    {
        // The list of valid projects that a task can belong to
        public Project(string title)
        {
            Title = title;
        }

        public string Title { get; set; }
        public List<Task> Tasks = new List<Task>();

        // Determine if the project is in use (before deleting it)
        public bool IsUsed()
        {
            foreach (Task t in TaskList.Tasks)
            {
                // See if this project is in any task
                if (t.Project.Title.Equals(Title))
                {
                    // Match - it is in use: don't delete it
                    return true;
                }
            }
            // No match - clear to delete
            return false;
        }

        public void Display(int row = 0)
        {
            // Display a project on screen

            // If a row is given - display at that row
            if (row != 0)
            {
                // Save the cursor position on the stack
                CursorControl.PushCursor();
                // Set the cursor position
                CursorControl.curSet(row);
            }

            // Display the project (which in this case is just a title)
            Console.WriteLine(Title);
            // Restore the cursor from the stack
            if (row != 0) CursorControl.PopCursor();
        }
    }
}

// By Ole Victor