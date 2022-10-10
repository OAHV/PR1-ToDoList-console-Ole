# PR1-ToDoList-console-Ole
A console app for ToDo list

<h1>What I have done</h1>

I have made a simple console app to try C# lists and objects.
<ul>
<li>Tasks can be added, edited, sorted and deleted
<li>Tasks are set to belong to a project
<li>Projects can be added and deleted
<li>Tasks can be 'due' or 'done'
<li>Due and Done can be toggled in the edit menu
</ul>

<ul>
<li>A user console interface has been developed
<li>Tasks and projects are read from JSON files at program start
<li>Tasks and projects are saved to JSON files on program exit
</ul>

There are classes and methods for
<ul>
<li>Cursor control on the console
<li>Position and color control for the screen
<li>Menues and menu items
<li>Tasks
<li>Projects
<li>Lists of tasks and lists of projects
</ul>

There is a clever system for menues and menu items connected to the handling of the tasks and projects.
The menues are self driven and can call each other.

There is no
<ul>
<li>Front end (React and such)
<li>Databases - only JSON files
<li>Support for longer lists (that can't fit to the screen)
</ul>

<strong>There is UML and documentation in the Docs directory.</strong>

<h1>What I have learned</h1>

I have learned
<ul>
<li>OOP
<li>C# .NET
<li>JSON file handling and (de)serializing
<li>Console control
<li>Function Action pointers
<li>Interfaces and abstract classes
<li>JSON converters for DateOnly
<li>Structuring code
<li>UML
</ul>


<h1>Assignment</h1>

<h2>Project Specification</h2>
<h3>Project Brief</h3>
Your task is to build a todo list application. The application will allow a user to create new 
tasks, assign them a title and due date, and choose a project for that task to belong to. They 
will need to use a text based user interface via the command-line. Once they are using the 
application, the user should be able to also edit, mark as done or remove tasks. They can also 
quit and save the current task list to file, and then restart the application with the former 
state restored. The interface should look similar to the mockup.

<h3>Requirements</h3>
The solution must achieve the following requirements:
<ul>
<li>Model a task with a task title, due date, status and project
<li>Display a collection of tasks that can be sorted both by date and project
<li>Support the ability to add, edit, mark as done, and remove tasks
<li>Support a text-based user interface
<li>Load and save task list to file 
</ul>
The solution may also include other creative features at your discretion in case you wish to show some flair.

<h1>Dependancies</h1>

Not much...

<ul>
<li>Visual Studio 2022
<li>Github
<li>PDF
</ul>
