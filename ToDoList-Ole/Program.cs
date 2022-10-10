// Project 1 - ToDo-list, Simple console app
using ToDoList_Ole;
using ToDoList_Ole.Models;
using ToDoList_Ole.Views;

// Initiate projects and tasks lists (read from JSON files)
Environment.CurrentDirectory = "../../..";      // Set directory to depository
ProjectList.Deserialize(FileIO.ReadJsonFromFile("projects.json"));
TaskList.Deserialize(FileIO.ReadJsonFromFile("tasks.json"));

// Display list and main menu - take user choises
Menues.mainMenu.Perform();

// Save assets to file before program exit
FileIO.WriteJsonToFile(ProjectList.Serialize(), "projects.json");
FileIO.WriteJsonToFile(TaskList.Serialize(), "tasks.json");

// By Ole Victor