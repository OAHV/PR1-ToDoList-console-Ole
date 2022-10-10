using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList_Ole.FileIO;
using ToDoList_Ole.Views;

namespace ToDoList_Ole.Models
{
    public static class TaskList
    {
        // This is the list of tasks
        public static List<Task> Tasks = new List<Task>();

        // A header for the list on screen
        public static string Header = "Task".PadRight(31) + "Project".PadRight(21) + "Status".PadRight(9) + "Deadline";
        // The footer is set dynamically (to contain the number of items)
        public static string Footer = "";
        // Conditions for sorting and filtering
        public static bool sortByDate = true;
        public static bool filterByDue = true;
        public static bool filter = false;
        // The selected ask is to be highlighted (initially none selected)
        public static int selected = -1;

        // Write all tasks to a JSON string
        public static string Serialize()
        {
            // Options for pretty/readable JSON file
            var options = new JsonSerializerOptions { WriteIndented = true };
            options.Converters.Add(new DateOnlyConverter());
            // Serialize entire asset list
            return JsonSerializer.Serialize(Tasks, options);
        }

        // Read all taskt from a JSON string into task objects
        public static void Deserialize(string JSONstring)
        {
            try {
                var options = new JsonSerializerOptions { WriteIndented = true };
                options.Converters.Add(new DateOnlyConverter());
                Tasks = JsonSerializer.Deserialize<List<Task>>(JSONstring, options);
                Footer = "Total " + Tasks.Count.ToString() + " Tasks";
            }
            catch 
            {
                return;
            }

        }

        // Display a list of taskt on screen
        // The list has a header and a footer
        // The list can be filtered and sorted
        public static void List()
        {
            // Clear the screen where the list will be displayed
            ConsoleScreen.clearLowerPart(ConsoleScreen.lowerPartOfScreen);

            // Highlight the header
            CursorControl.highLight();
            Console.WriteLine(Header);
            CursorControl.highLight(false);

            // Sort the list according to user input
            if (sortByDate)
            {
                Tasks = Tasks.OrderBy(t => t.DueDate).ToList();

            }
            else
            {
                Tasks = Tasks.OrderBy(t => t.Title).ToList();
            }

            // Display all the tasks (except for the filtered out ones)
            foreach (Task task in Tasks)
            {
                // The user selected task is yellow
                if (selected >= 0 && task == Tasks[selected]) CursorControl.highLight(true, ConsoleColor.Yellow);

                // If no filter applied by user
                if (!filter) { task.Display(); }
                // Else apply filter
                else if (task.Status == filterByDue) task.Display();

                // Turn off highlight
                CursorControl.highLight(false);
            }
            // Highlight and display the footer
            CursorControl.highLight();
            Console.WriteLine(Footer);
            CursorControl.highLight(false);
        }

        // These are functions that are pointed to from the menu items
        public static void ListAll()
        {
            // No filters applied
            filter = false;
            List();
        }

            public static void ListDue()
        {
            // Filter out accompliched tasks
            filter = true;
            filterByDue = false;
            List();
        }

        public static void ListDone()
        {
            // Filter out due tasks
            filter = true;
            filterByDue = true;
            List();
        }

        internal static void addTasks()
        {
            // Add a new task to the list from user input
            char ok = '-';      // User input
            Task newTask = new Task("New task");

            // Clear screen below for input dialog
            ConsoleScreen.clearLowerPart(ConsoleScreen.lowerPartOfScreen);

            // Where to display the new asset template as it is built
            int displayAtRow = ConsoleScreen.lowerPartOfScreen;
            newTask.Display(displayAtRow);

            // Display tempate asset as it is built
            Console.WriteLine("\n\nAdd task");

            // User input of Title, Project (from list) and deadline
            newTask.Title = ConsoleScreen.readString("Title: ", "No input. Please try again: ");
            newTask.Display(displayAtRow);     // Update asset template on screen
            newTask.Project = ConsoleScreen.readProjectFromList("Project: ", "Not a Project. Please try again: ", ProjectList.Projects);
            newTask.Display(displayAtRow);
            newTask.DueDate = ConsoleScreen.readDate("Deadline date: ", "Not a date. Please try again: ");
            newTask.Display(displayAtRow);

            // Confirm by user
            Console.Write("Add new task to list (y/n): ");
            while (ok == '-')
            {
                ok = Console.ReadKey().KeyChar;
                switch (ok)
                {
                    case 'y':
                        Tasks.Add(newTask);
                        break;
                    case 'n':
                        break;
                    default:
                        Console.CursorLeft = 0;
                        ConsoleScreen.errorDisplay("Please answer 'y' or 'n': ");
                        ok = '-';
                        break;
                }
            }
            // Calculate a new footer
            Footer = "Total " + Tasks.Count.ToString() + " Tasks";
            // Erase the input dialogue
            ConsoleScreen.clearLowerPart(ConsoleScreen.lowerPartOfScreen);
        }

        // Functions called by task editing menu items
        public static void Next()
        {
            // Select the next task
            if (selected < Tasks.Count - 1) selected++;
        }

        public static void Previous()
        {
            // Select the previous task
            if (selected > 0 ) selected--;
        }

        public static void Delete()
        {
            // Remove the selected task from the list of tasks
            Tasks.Remove(Tasks[selected]);
            // Recalculate the selection and the footer
            if(selected > Tasks.Count - 1) selected = Tasks.Count - 1;
            Footer = "Total " + Tasks.Count.ToString() + " Tasks";
        }

        public static void Done()
        {
            // Toggle the selected task status between Due and Done
            Tasks[selected].Status = !Tasks[selected].Status;
        }

        public static void NewDate()
        {
            // CHange the selected task deadline
            Tasks[selected].DueDate = ConsoleScreen.readDate("Deadline date: ", "Not a date. Please try again: ");
        }
    }
}

// By Ole Victor