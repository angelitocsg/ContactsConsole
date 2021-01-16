using System;

namespace Contacts.Views
{
    public class MainView : Screen
    {
        public MainView()
        {
            InitialScreen();
        }

        public void InitialScreen()
        {
            Lines = 0;
            System.Console.Clear();
            CreateTopLine();
            CreateTextLine("CONTACT LIST");
            CreateMiddleLine();
            CreateTextLine("1) List  2) Add  3) Edit  4) Delete  5) Search");
            CreateSingleMiddleLine();

            for (int i = Lines + 3; i < SCREEN_HEIGHT; i++)
                CreateEmptyLine();

            CreateSingleMiddleLine();
            CreateEmptyLine();
            CreateBottomLine();

            SetInputPosition();
        }

        public void SetInputPosition()
        {
            Console.SetCursorPosition(70, 3);
        }
    }
}
