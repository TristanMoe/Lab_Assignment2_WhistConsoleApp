using System;

namespace Lab_Assignment2_WhistConsoleApp.ConsoleViews
{
    public class InputException : Exception
    {
        public InputException(string msg)
            :base(msg)
        {
            
        }
    }
}