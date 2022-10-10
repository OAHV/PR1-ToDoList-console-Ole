using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoList_Ole.Models;

namespace ToDoList_Ole.Views
{
    public static class ConsoleScreen
    {
        // This class handles input from and output to the screen
        static public int lowerPartOfScreen = 13;       // Where to list assets on screen

        // Default values for error messages
        static int errorRow = 0;        // Row
        static int errorCol = 40;       // Column
        static int errorLenth = 40;     // Length

        // Error messaging
        public static void errorDisplay(string message)
        {
            // Save cursor position
            CursorControl.PushCursor();

            // Set position and color - write message
            CursorControl.curSet(errorRow, errorCol);
            CursorControl.setAlertColor(true);
            Console.Write(message.PadRight(errorLenth));
            CursorControl.restoreCur();

            // Wait for key pressed - erase message
            while (!Console.KeyAvailable) ;
            CursorControl.curSet(errorRow, errorCol);
            CursorControl.setAlertColor(false);
            Console.Write(" ".PadRight(errorLenth));

            // Return to previous cursor position
            CursorControl.PopCursor();
        }

        // Erase rows from given row and down the screen
        public static void clearLowerPart(int fromRow = 13)
        {
            // Calculate window last row depending on window hieght
            int lastRow = Console.WindowHeight - fromRow - 1;

            // Erase from top to bottom
            Console.CursorTop = fromRow;
            Console.CursorLeft = 0;
            for (int r = 0; r < lastRow; r++) Console.WriteLine(" ".PadRight(Console.WindowWidth - Console.CursorLeft));

            // Set cursor at top of area
            Console.CursorTop = fromRow;
        }

        // User input methods with error and validity control

        // Read a string from user input
        public static string readString(string prompt, string errorMessage)
        {
            string inputBuffer = "";        // Empty string buffer
            Console.Write(prompt);          // Print input prompt
            CursorControl.PushCursor();     // Save cursor to stack
            // Read user input until valid
            while ((inputBuffer = Console.ReadLine()) == "")
            {
                errorDisplay(errorMessage);
                CursorControl.restoreCur(); // Set cursor position back
            }
            // Restor previous cursor position
            CursorControl.PopCursor();
            // Write extra linefeed corresponding to the user pressing "Enter"-key
            Console.WriteLine("");

            // Return the user input string
            return inputBuffer;
        }

        // Read a date user input
        public static DateOnly readDate(string prompt, string errorMessage)
        {
            string inputBuffer = "";
            DateOnly inputDate = new DateOnly();
            Console.Write(prompt);
            CursorControl.PushCursor();
            while (inputBuffer == "")
            {
                inputBuffer = Console.ReadLine();
                try
                {
                    // See if input is a valid date
                    inputDate = DateOnly.FromDateTime(Convert.ToDateTime(inputBuffer));
                }
                catch
                {
                    // If not a valid date - try again
                    ConsoleScreen.errorDisplay(errorMessage);
                    CursorControl.restoreCur();
                    inputBuffer = "";
                    continue;
                }
            }
            CursorControl.PopCursor();
            // Write extra linefeed corresponding to the user pressing "Enter"-key
            Console.WriteLine("");
            return inputDate;
        }

        // Read an integer number from user input
        public static int readInt(string prompt, string errorMessage)
        {
            string inputBuffer = "";
            int inputInt = 0;
            Console.Write(prompt);
            CursorControl.PushCursor();
            while (inputBuffer == "")
            {
                inputBuffer = Console.ReadLine();
                try
                {
                    inputInt = Convert.ToInt32(inputBuffer);
                }
                catch
                {
                    errorDisplay(errorMessage);
                    CursorControl.restoreCur();
                    inputBuffer = "";
                    continue;
                }
                if (inputInt < 0)
                {
                    errorDisplay(errorMessage);
                    CursorControl.restoreCur();
                    inputBuffer = "";
                }
            }
            CursorControl.PopCursor();
            // Write extra linefeed corresponding to the user pressing "Enter"-key
            Console.WriteLine("");
            return inputInt;
        }

        // User choise from a list of valid choises (not used in this application)
        public static string readStringFromList(string prompt, string errorMessage, List<string> validList)
        {
            string found = "";
            int matches = 0;
            string inputBuffer = "";

            // Promt for input (and save cursor position)
            Console.Write(prompt);
            CursorControl.PushCursor();

            // Erase lower part of screen for showing choises
            clearLowerPart(19);
            foreach (string str in validList) Console.WriteLine(str);

            // Until exactly one match from list of valid choises
            while (matches != 1)
            {
                matches = 0;

                // Restore cursor to input position
                CursorControl.restoreCur();

                // Read a user input character and add it to the unput buffer
                inputBuffer += Console.ReadKey().KeyChar.ToString();

                // Erase lower part of screen for showing now possible choises
                clearLowerPart(19);
                foreach (string s in validList)
                {
                    // If a valid choise contains the now input buffer
                    if (s.ToLower().Contains(inputBuffer.ToLower()))
                    {
                        // Count it to matches
                        matches++;
                        // Save it as the current choise
                        found = s;
                        // Print it as a still valid choise
                        Console.WriteLine(s + " : " + inputBuffer);
                    }
                }

                // If no matches after looping through the list
                if (matches == 0)
                {
                    ConsoleScreen.errorDisplay(errorMessage);
                    // Start again from scratch
                    inputBuffer = "";
                }
            }
            // Now we have found exactly one match
            CursorControl.PopCursor();
            // Write extra linefeed corresponding to the user pressing "Enter"-key
            Console.WriteLine("");

            // Return the found list item (string)
            return found;
        }

        // User choise of project from a list of projects
        public static Project readProjectFromList(string prompt, string errorMessage, List<Project> validList)
        {
            Project found = new Project("No project");
            int matches = 0;
            string inputBuffer = "";

            // Promt for input (and save cursor position)
            Console.Write(prompt);
            CursorControl.PushCursor();

            // Erase lower part of screen for showing choises
            clearLowerPart(19);
            foreach (Project pr in validList) pr.Display();

            // Until exactly one match from list of valid choises
            while (matches != 1)
            {
                matches = 0;

                // Restore cursor to input position
                CursorControl.restoreCur();

                // Read a user input character and add it to the unput buffer
                inputBuffer += Console.ReadKey().KeyChar.ToString();

                // Erase lower part of screen for showing now possible choises
                clearLowerPart(19);
                foreach (Project pr in validList)
                {
                    // If a valid choise contains the now input buffer
                    if (pr.Title.ToLower().Contains(inputBuffer.ToLower()))
                    {
                        // Count it to matches
                        matches++;
                        // Save it as the current choise
                        found = pr;
                        // Print it as a still valid choise
                        Console.WriteLine(pr.Title + " : " + inputBuffer);
                    }
                }

                // If no matches after looping through the list
                if (matches == 0)
                {
                    ConsoleScreen.errorDisplay(errorMessage);
                    // Start again from scratch
                    inputBuffer = "";
                }
            }
            // Now we have found exactly one match
            CursorControl.PopCursor();
            // Write extra linefeed corresponding to the user pressing "Enter"-key
            Console.WriteLine("");

            // Return the found list item (string)
            return found;
        }
    }

}

// By Ole Victor