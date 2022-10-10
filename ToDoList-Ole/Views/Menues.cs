using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_Ole.Models;

namespace ToDoList_Ole.Views
{
    public static class Menues
    {
        // These are the menu objects.
        // They actually do most of the controlling
        // by responding to user input.
        // They can also call each other

        // See Menu.cs and MenuItem.cs for definitions

        // Used to exit menu loops
        public static bool exit = false;

        // Create a project menu
        private static int j = 1;
        public static Menu projectMenu = new Menu("Project Menu", "Your Choise: ",
            new List<MenuItem> {
                new MenuItem("List projects", j++, "Li", ProjectList.List),
                new MenuItem("Add project", j++, "Aa", ProjectList.addProjects),
                new MenuItem("Next (press 'n' to select the next project to handle)", j++, "Nn", ProjectList.Next),
                new MenuItem("Previous", j++, "Pp", ProjectList.Previous),
                new MenuItem("Delete the selected project", j++, "Dd", ProjectList.Delete),
                new MenuItem("Exit to Main menu", j++, "QqeE", (()=>Menues.exit=true))
            }, ProjectList.List);

        // Create a sorting menu
        private static int k = 1;
        public static Menu sortMenu = new Menu("Sorting Menu", "Your Choise: ",
            new List<MenuItem> {
                new MenuItem("Sort alphabetically (by title)", k++, "Aa", (() => TaskList.sortByDate = false)),
                new MenuItem("Sort by date (deadline)", k++, "Dd", (() => TaskList.sortByDate = true)),
                new MenuItem("Exit to Main menu", k++, "QqeE", (()=>Menues.exit=true))
            }, TaskList.List);

        // Create a editing menu
        private static int m = 1;
        public static Menu editMenu = new Menu("Edit Menu", "Your Choise: ",
            new List<MenuItem> {
                new MenuItem("Next (press 'n' to select the next task to handle)", m++, "Nn", TaskList.Next),
                new MenuItem("Previous", m++, "Pp", TaskList.Previous),
                new MenuItem("Add a new task", m++, "Aa+", TaskList.addTasks),
                new MenuItem("Delete the selected task", m++, "Dd-", TaskList.Delete),
                new MenuItem("Toggle due & accompliched", m++, "Xx ", TaskList.Done),
                new MenuItem("Set new deadline", m++, "Ww", TaskList.NewDate),
                new MenuItem("Exit to Main menu", m++, "QqeE", (()=>Menues.exit=true))
            }, TaskList.ListAll);


        // Create a main menu
        private static int i = 1;
        public static Menu mainMenu = new Menu("Main Menu", "Your Choise: ",
            new List<MenuItem> {
                new MenuItem("List all tasks", i++, "Ll", TaskList.ListAll),
                new MenuItem("List due tasks", i++, "Uu", TaskList.ListDue),
                new MenuItem("List accomplished tasks", i++, "Cc", TaskList.ListDone),
                new MenuItem("Edit tasks (Add, edit, delete, set as Done)", i++, "Ee", editMenu.Perform),
                new MenuItem("Work with projects (Add, delete)", i++, "Pp", projectMenu.Perform),
                new MenuItem("Sort tasks (Alphabetically or by deadline date)", i++, "Ss", sortMenu.Perform),
                new MenuItem("Quit and Save (to file)", i++, "Qq", (()=>Menues.exit=true))
            }, TaskList.List);

    }
}

// By Ole Victor