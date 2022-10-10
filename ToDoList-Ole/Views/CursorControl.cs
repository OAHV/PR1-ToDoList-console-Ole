using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList_Ole.Views
{
    public static class CursorControl
    {
        // Cursor and color control

        // Implements a stack for cursor positions that can
        // be pushed (saved to stack), restored (read from stack)
        // and poped (read and then removed from stack).

        // Class that keeps track of cursor positions
        public class CursorPos
        {
            // Constructor
            public CursorPos(int row, int col)
            {
                Row = row;
                Col = col;
            }

            private int Row { get; set; }
            private int Col { get; set; }

            // Set the screen cursor position to this object's values
            public void Set()
            {
                Console.CursorTop = Row;
                Console.CursorLeft = Col;
            }
        }

        // A stack of cursor positions to push, pop and read from
        static Stack<CursorPos> cursorStack = new Stack<CursorPos>();

        // Save the current screen cursor position to the stack
        public static void PushCursor()
        {
            cursorStack.Push(new CursorPos(Console.CursorTop, Console.CursorLeft));
        }

        // Set the screen cursor position to the top stack values and pop it
        public static CursorPos PopCursor()
        {
            CursorPos cursorPos = cursorStack.Pop();
            cursorPos.Set();
            return cursorPos;
        }

        // Set the screen cursor position (no stack involvement)
        public static void curSet(int row = 0, int col = 0)
        {
            Console.CursorTop = row;
            Console.CursorLeft = col;
        }

        // Set screen cursor position to stack top values (no pop)
        public static void restoreCur()
        {
            cursorStack.Peek().Set();
        }

        // Set text colors to alert (warning) for error messages etc
        public static void setAlertColor(bool alert = false)
        {
            if (alert)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.BackgroundColor = ConsoleColor.Red;
            }
            else Console.ResetColor();
        }

        // Set text colors to highligh for headings etc
        public static void highLight(bool h = true, ConsoleColor color = ConsoleColor.White)
        {
            if (h)
            {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = color;
            }
            else Console.ResetColor();
        }

    }

}

// By Ole Victor