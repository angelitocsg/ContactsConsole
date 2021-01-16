using System;

namespace Contacts.Views
{
    public class Screen
    {
        public const char CORNER_TOP_LEFT = '╔';
        public const char CORNER_TOP_RIGHT = '╗';
        public const char CORNER_BOTTOM_LEFT = '╚';
        public const char CORNER_BOTTOM_RIGHT = '╝';
        public const char LINE_DOUBLE = '═';
        public const char LINE_SINGLE = '─';
        public const char DOUBLE_CORNER_LEFT = '╠';
        public const char DOUBLE_CORNER_RIGHT = '╣';
        public const char COLUMN_DOUBLE = '║';

        public const int SCREEN_WIDTH = 80;
        public const int SCREEN_HEIGHT = 25;
        public int Lines { get; protected set; } = 0;

        public void CreateTextLine(object text)
        {
            Lines++;
            Console.WriteLine($"{COLUMN_DOUBLE}{$" {text}".PadRight(SCREEN_WIDTH - 2, ' ')}{COLUMN_DOUBLE}");
        }

        public void CreateTextAtLine(int line, string content)
        {
            Console.SetCursorPosition(0, line);
            CreateTextLine(content);
        }

        public void CreateEmptyLine(bool increment = true)
        {
            if (increment) Lines++;
            Console.WriteLine($"{COLUMN_DOUBLE}{new string(' ', SCREEN_WIDTH - 2)}{COLUMN_DOUBLE}");
        }

        public void CreateTopLine()
        {
            Lines++;
            Console.WriteLine($"{CORNER_TOP_LEFT}{new string(LINE_DOUBLE, SCREEN_WIDTH - 2)}{CORNER_TOP_RIGHT}");
        }

        public void CreateMiddleLine(bool increment = true)
        {
            if (increment) Lines++;
            Console.WriteLine($"{DOUBLE_CORNER_LEFT}{new string(LINE_DOUBLE, SCREEN_WIDTH - 2)}{DOUBLE_CORNER_RIGHT}");
        }

        public void CreateSingleMiddleLine(bool increment = true)
        {
            if (increment) Lines++;
            Console.WriteLine($"{DOUBLE_CORNER_LEFT}{new string(LINE_SINGLE, SCREEN_WIDTH - 2)}{DOUBLE_CORNER_RIGHT}");
        }

        public void CreateBottomLine()
        {
            Lines++;
            Console.WriteLine($"{CORNER_BOTTOM_LEFT}{new string(LINE_DOUBLE, SCREEN_WIDTH - 2)}{CORNER_BOTTOM_RIGHT}");
        }

        public void ShowStatusBarMessage(string errorMessage)
        {
            Console.SetCursorPosition(0, SCREEN_HEIGHT - 2);
            CreateTextLine(errorMessage);
        }

        public void ShowAsList(int baseline, int currentLine, string content)
        {
            Console.SetCursorPosition(0, baseline + currentLine);
            CreateTextLine(content);
        }
    }
}
