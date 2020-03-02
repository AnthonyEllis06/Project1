//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Program 1 - NameList Class
//	File Name:		Tools.cs
//	Description:	Defines Static methods that are used as tools
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Anthony Ellis, ellisah@etsu.edu, East Tennessee State University
//	Created:		Tuesday, Febuary 018, 2020
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace DataStructures
{


    /// <summary>
    /// The static Methods are used to split strings up and display them.
    /// </summary>
    class Tools
    {
        #region Tokenize
        /// <summary>Tokenizes the specified line.</summary>
        /// <param name="line">A multichar String that is  going to be tokenized</param>
        /// <param name="delims"> The String containing the delimiters to be used to tokenize the line</param>
        /// <returns>Returns the tokenized version of line as an array of strings</returns>
        public static String[] Tokenize(string line, string delims)
        {   
            List<String> Tokens= new List<String>();// Tokens is a collection that uses a list and has helper methods for adding a string token
            String Token; //Holds the currnet string token to be added
            char[] DelimChars = delims.ToCharArray(); //Array of chars made of the delims string
            int CurrDelimPosition = line.IndexOfAny(DelimChars); //the current position of the delimiter in the string
            while (CurrDelimPosition != -1)//used to find all the delimiters untill none are found
            {
                if (CurrDelimPosition == 0)//In case delimiter is the next token
                    CurrDelimPosition = 1;
                Token = line.Substring(0, CurrDelimPosition);
                if(!String.IsNullOrWhiteSpace(Token))//checks if token is a space
                    Tokens.Add(Token);
                line = line.Substring(CurrDelimPosition);//shortens line to rest of string
                CurrDelimPosition = line.IndexOfAny(DelimChars);
            }
            if (!String.IsNullOrWhiteSpace(line))
                Tokens.Add(line);//adds the final token
            return Tokens.ToArray();//Calls the tokens class to get an String array of the all the tokens
        }
        #endregion
        #region File Methods

        /// <summary>Opens the dialog.</summary>
        /// <param name="Title">the OpenFileDialog title</param>
        /// <param name="Filter">The Prefered Filter for the OpenFileDialog, Default: all files|*.*</param>
        /// <returns>a string file name chosen. null if no file is chosen.</returns>
        public static String OpenDialog(String Title,String Filter = "all files|*.*")
        {
            OpenFileDialog dlg = new OpenFileDialog();//create new dialog
            dlg.InitialDirectory = @"..\..\DataFiles";//set initial directory
            dlg.Title = Title;  //set title of dialog
            dlg.Filter = Filter; //set file filter for dialog
            if (dlg.ShowDialog() == DialogResult.Cancel) //if dialog ges canceled then it returns null
                return null;
            return dlg.FileName; //returns the name of file chosen
        }

        /// <summary>  Saves a String Array to a file using a file dialong</summary>
        /// <param name="Title">  title of file dialog</param>
        /// <param name="Contents">  string array to write to file</param>
        /// <param name="Filter">  file filter</param>
        /// <returns>returns boolean on if FileDialog worked or not</returns>
        public static bool SaveFileDialog(String Title, String[] Contents, String Filter = "all files|*.*")
        {
            SaveFileDialog dlg = new SaveFileDialog(); //creates new save file dialog
            dlg.InitialDirectory = @"..\..\DataFiles";
            dlg.Title = Title;
            dlg.Filter = Filter;
            if (dlg.ShowDialog() == DialogResult.Cancel)
                return false;
            StreamWriter writer = null; //creates a streamWriter to allow writing to a file
            try
            {
                writer = new StreamWriter(new FileStream(dlg.FileName, FileMode.Create, FileAccess.Write)); //defines the stream writer
                for (int line = 0; line < Contents.Length; line++) //for each element in contents
                {
                    String n = Contents[line]; 
                    writer.WriteLine(n); //write value of array at index to file
                }
                return true; //returns true because it completed
            }
            catch (Exception e)
            {
                Console.WriteLine("File failed to process." + e.Message); //throws exceptions from the stream writer
                return false;
            }
            finally
            {
                if (writer != null) //close the file
                    writer.Close();
            }
        }
        /// <summary>converts the file into a string array of each line</summary>
        /// <param name="FileName">hte name of the file.</param>
        /// <returns>Returns a string array of each line in the file</returns>
        public static String[] FileToString(String FileName)
        {
            StreamReader FileStream = null; //null filestream
            List<String> Contents = new List<String>(); //empty string array
            try
            {
                FileStream = new StreamReader(FileName); 
                while(FileStream.Peek()!=-1) //while the stream reader keeps reading more lines
                {
                    Contents.Add(FileStream.ReadLine()); //adds line to string array
                }
                return Contents.ToArray();//returns array of strings
            }
            catch(Exception e)
            {
                Console.WriteLine("File failed to process." + e.Message); //exception thrown primarily by the stream reader
            }
            finally
            {
                if (FileStream != null) //close file
                    FileStream.Close();
                
            }
            return Contents.ToArray();
        }
        
        #endregion
        #region Display Methods

        /// <summary>
        /// Displays the list.
        /// </summary>
        /// <param name="DisplayList">The list to be displayed.</param>
        public static void DisplayList(List<String> DisplayList)
        {
            foreach (String ListItem in DisplayList)
            {
                Console.WriteLine(ListItem); //writes line from list to console
            }
        }
        /// <summary>Setups the specified console</summary>
        /// <param name="Title">  The title for the Console Window</param>
        /// <param name="Message">A welcome message, default: null;</param>
        public static void setup(String Title,String Message=null)
        {
            Console.BackgroundColor = ConsoleColor.White; //sets console back ground color
            Console.Clear();
            Console.Title = Title; //sets title

            if (Message != null)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue; //sets font color
                Console.WriteLine(Message);
            }
            
        }

        /// <summary>
        /// Displays the specified tokens.
        /// </summary>
        /// <param name="tokens">A String Array Containing the String tokens to be displayed</param>
        public static void displayTokenArray(String[] tokens)
        {
            int LineNumber = 0;
            int Lines = Console.WindowHeight-5;//Number of Console Lines 
            DisplayTop();
            for (int i = 0; i<tokens.Length && tokens[i] != null; i++)//Iterates through Tokens to display
            {
                Console.WriteLine(" {0}.    {1}", i + 1, tokens[i]);//Sets format for each line
                if (LineNumber == Lines)//If Window line limit is reached
                {
                    DisplayBottom();
                    Console.Clear();
                    DisplayTop();
                    LineNumber = 0;//resets the current line in the window to 0
                }
                LineNumber++;
            }
            DisplayBottom();//Displays Bottom line for the last time
        }

        /// <summary>
        /// Sets Colors and Displays the Top then resets colors.
        /// </summary>
        private static void DisplayTop()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" No. Token");
            Console.WriteLine("---- ----");
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        /// <summary>
        /// Sets Colors and Displays the bottom then resets colors.
        /// </summary>
        private static void DisplayBottom()
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();//waits for any key to be pressed to continue
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
        }

        /// <summary>
        /// Display a Press Any Key to ... message at the bottom of the screen
        /// </summary>
        /// <param name="strVerb">term in the Press Any Key to ... message; default: "continue . . ."</param>
        public static void PressAnyKey(string strVerb = "continue ...")
        {
            Console.ForegroundColor = ConsoleColor.Red;
            if (Console.CursorTop < Console.WindowHeight - 1)
                Console.SetCursorPosition(0, Console.WindowHeight - 1);
            else
                Console.SetCursorPosition(0, Console.CursorTop + 2);

            Console.Write($"Press any key to {strVerb}");
            Console.ReadKey();
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
        } // End PressAnyKey 

        /// <summary>
        /// Skip n lines in the console window
        /// </summary>
        /// <param name="n">the number of lines to skip - defaults to 1</param>
        public static void Skip(int n = 1)
        {
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine();
            }
        }
        #endregion

    }
}
