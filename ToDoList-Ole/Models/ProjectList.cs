using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList_Ole.Views;

namespace ToDoList_Ole.Models
{
    public static class ProjectList
    {
        // This implements a list of projects that tasks belong to
        // Each project can have manu taskt
        // Each task belongs to just one project

        // The list of valid projects
        public static List<Project> Projects = new List<Project>();

        // A header and a footer for the display of project list
        public static string Header = "Projects";
        public static string Footer = "Total " + Projects.Count.ToString();

        // The selected project (for editing - i.e.  deletion)
        public static int selected = -1;

        // Write all the projects to a JSON string
        public static string Serialize()
        {
            // Options for pretty/readable JSON file
            var options = new JsonSerializerOptions { WriteIndented = true };
            // Serialize entire asset list
            return JsonSerializer.Serialize(Projects, options);
        }

        // Read all projects from a JSON string into project objects
        public static void Deserialize(string JSONstring)
        {
            try
            {
                Projects = JsonSerializer.Deserialize<List<Project>>(JSONstring);

                // Sort the objects and calculate the footer string
                Projects = Projects.OrderBy(p => p.Title).ToList();
                Footer = "Total " + Projects.Count.ToString() + " projects";
            }
            catch
            {
                return;
            }
        }

        // Display the list of projects on screen
        public static void List()
        {
            ConsoleScreen.clearLowerPart();
            CursorControl.highLight();
            Console.WriteLine(Header);
            CursorControl.highLight(false);
            foreach (var pr in Projects)
            {
                // Yellow background if selected
                if (selected >= 0 && pr == Projects[selected]) CursorControl.highLight(true, ConsoleColor.Yellow);

                // Display the project
                pr.Display();
                CursorControl.highLight(false);
            }
            // Display the footer
            CursorControl.highLight();
            Console.WriteLine(Footer);
            CursorControl.highLight(false);
        }

        // Add a new project to the list
        public static void addProjects()
        {
            char ok = '-';      // User input
            Project newProject = new Project("New project");

            // Clear screen below for input dialog
            ConsoleScreen.clearLowerPart(ConsoleScreen.lowerPartOfScreen);

            // Where to display the new asset template as it is built
            int displayAtRow = ConsoleScreen.lowerPartOfScreen;
            newProject.Display(displayAtRow);


            // Display tempate asset as it is built
            Console.WriteLine("\n\nAdd project");

            // User input of project title
            newProject.Title = ConsoleScreen.readString("Title: ", "No input. Please try again: ");
            newProject.Display(displayAtRow);     // Update asset template on screen

            // Confirm by user
            Console.Write("Add new project to list (y/n): ");
            while (ok == '-')
            {
                ok = Console.ReadKey().KeyChar;
                switch (ok)
                {
                    case 'y':
                        Projects.Add(newProject);
                        break;
                    case 'n':
                        break;
                    default:
                        Console.CursorLeft = 0;
                        ConsoleScreen.errorDisplay("Pleas answer 'y' or 'n': ");
                        ok = '-';
                        break;
                }
            }
            // Sort list and update footer
            Projects = Projects.OrderBy(p => p.Title).ToList();
            Footer = "Total " + Projects.Count.ToString() + " Projects";
        }

        public static void Next()
        {
            // Select the next item in the list
            if (selected < Projects.Count - 1) selected++;
        }

        public static void Previous()
        {
            // Select the previous item in the list
            if (selected > 0) selected--;
        }

        public static void Delete()
        {
            // Delete the selected item
            if (Projects[selected].IsUsed())
            {
                // Dont delete a project with tasks in it
                ConsoleScreen.errorDisplay("Project used in some task");
            }
            else
            {
                // Remove project from list
                Projects.Remove(Projects[selected]);
            }
            // Update selection and footer
            if (selected > Projects.Count - 1) selected = Projects.Count - 1;
            Footer = "Total " + Projects.Count.ToString() + " Projects";
        }
    }
}

// By Ole Victor