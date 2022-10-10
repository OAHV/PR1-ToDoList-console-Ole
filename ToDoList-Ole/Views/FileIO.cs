using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using ToDoList_Ole.Models;
using Task = ToDoList_Ole.Models.Task;

namespace ToDoList_Ole.Views
{
    // Read tasks and projects from JSON file at program start
    // and save them to file before exit program

    public static class FileIO
    {
        // Read JSON string from file
        public static string ReadJsonFromFile(string filename)
        {
            string JSONstring = "";
            try
            {
                // Read all data to buffer string
                JSONstring = File.ReadAllText(filename);
            }
            catch
            {
                // ConsoleScreen.errorDisplay("Error reading file");
            }
            return JSONstring;
        }


        // Save serialized string to JSON file
        public static void WriteJsonToFile(string JSONstring, string filename)
        {
            // Write string to JSON file
            File.WriteAllText(filename, JSONstring);
        }
    }
}

// By Ole Victor