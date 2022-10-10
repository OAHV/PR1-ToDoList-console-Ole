using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_Ole.Views;

namespace ToDoList_Ole.Models
{
    public class Task : Item
    {
        // Definition of a task
        public Task(string title)
        {
            Title = title;
            Status = false;
            DueDate = DateOnly.FromDateTime(DateTime.Now.AddDays(30));
            Project = new Project("\"No project\"");
        }

        // The title (name) of the task - what to do
        public string Title { get; set; }
        // Task status - Due or Done
        public bool Status { get; set; }
        // The due date
        public DateOnly DueDate { get; set; }
        // The project it belongs to
        public Project Project { get; set; }

        public void Display(int row = 0)
        {
            // Display the task on screen

            // If a position is give - display at that position
            if (row != 0)
            {
                // Save cursor position on the stack
                CursorControl.PushCursor();
                // Set the requested position
                CursorControl.curSet(row);
            }

            // Write title and project
            Console.Write(Title.PadRight(30) + " " + Project.Title.PadRight(20) + " ");

            if (Status)
            {
                // If done - display Done in green
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Done".PadRight(9));
            }
            else
            {
                // If not done - dis´play Due in red
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Due".PadRight(9));
            }
            
            if (!Status && (DueDate < DateOnly.FromDateTime(DateTime.Now.AddDays(14))))
            {
                // If less than two weeks from deadlin - display in red + message
                Console.Write(DueDate);
                Console.WriteLine("   <---- Less than two weeks!");
                Console.ResetColor();
            }
            else
            {
                // No color
                Console.ResetColor();
                Console.WriteLine(DueDate);
            }
            // Restore cursor position from the stack
            if(row != 0) CursorControl.PopCursor();
        }
    }
}

// By Ole Victor