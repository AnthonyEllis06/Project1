//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
//
//	Project:		Program 1 - NameList Class
//	File Name:		Program.cs
//	Description:	defines the driver for the program
//	Course:			CSCI 2210-001 - Data Structures
//	Author:			Anthony Ellis, ellisah@etsu.edu, East Tennessee State University
//	Created:		Tuesday, Febuary 018, 2020
//
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UtilityNamespace;
using System.Windows.Forms;
using System.Collections.Generic;


namespace DataStructures
{
    class Program
    {
        /// <summary>  main is where the driver starts running the program</summary>
        /// <param name="args">  areguments to be passed in from console</param>
        [STAThread]

        static void Main(string[] args)
        {
            #region setup
            String welcome = "Welcome to NameList!";
            Tools.setup("Name List", welcome); //sets up console
            Tools.PressAnyKey();
            #endregion
            #region Get User Info
            Regex emailPat = new Regex(@"([\w\W]+)(@)([\w]+)[\.](com|edu)"); // Regex pattern for emails
            Regex phonePat = new Regex(@"\(?[0-9]{3}\)?\s?[0-9]{3}\-?[0-9]{4}");//Regex pattern for phone numbers

            Console.WriteLine("What is your name?");//gets users name and checks if null
            String StringName = Console.ReadLine();
            while (StringName == null)
            {
                Console.WriteLine("What is your name?");
                StringName = Console.ReadLine();
            }
            Name UserName = new Name(StringName);
            Console.WriteLine("What is your email address?");//prompts for email
            String Email = Console.ReadLine();
            while (!emailPat.Match(Email).Success)//validates email
            {
                Console.WriteLine("Email is not valid please Enter your email again");
                Email = Console.ReadLine();
            }
            Console.WriteLine("What is your phone number?");//prompts for phone number
            String PhoneNumber = Console.ReadLine();
            while (!phonePat.Match(PhoneNumber).Success)//validates phone number
            {
                Console.WriteLine("Phone Number is not valid please Enter your Phone Number again");
                PhoneNumber = Console.ReadLine();
            }
            #endregion
            MainMenu();//starts main menu to begin list methods
            Console.WriteLine("Goodbye {0}! \n The company will not be sending any spam email to {1}.\n " +
                "We also will definitely not call you at {2}", UserName.NameToString(NameFormat.ORIGINAL),Email,PhoneNumber);//terminates and says goodbye
            Tools.PressAnyKey();

        }

        #region Menus

        /// <summary>  runs the main menu for loading a file or quitting</summary>
        public static void MainMenu()
        {
            UtilityNamespace.Menu main = new UtilityNamespace.Menu("Main Menu");//creates menu for the main menu
            main = main + "Get Name List from file" + "Quit"; //defines choices in the menu
            MainChoice choice = (MainChoice)main.GetChoice(); //displays menu and gets the users choice
            while (choice != MainChoice.QUIT)//while choice is not quit keep running
            {
                NameMenu();//calls menu for user to interact with the NameList
                choice = (MainChoice) main.GetChoice();
            }
        }

        /// <summary>runs Name menu which controls and allows the user to change and edit the name list</summary>
        public static void NameMenu()
        {
            String FileName = Tools.OpenDialog("Find Names", "text files|*.txt"); //opens file dialog to get chosen filename
            if (FileName == null) //if file name is null return to previous menu
                return;
            bool ListChanged = false;//other redundant listchanged boolean
            String[] FileContents = Tools.FileToString(FileName);//transfers file contents to a string array
            NameList Names = new NameList(FileContents);//creates the name list
            for(int i = 0;i<Names.Count;i++)//displays contents of file
            {
                Console.WriteLine(Names[i].NameToString(NameFormat.ORIGINAL));
            }

            Tools.PressAnyKey();
            UtilityNamespace.Menu NameMenu = new UtilityNamespace.Menu("Name Menu");//creats name menu to  help user interact with name list
            NameMenu = NameMenu + "Add Name" + "Delete Name" + "List Names" + "Find Name" + "Return to Main Menu";//defines choices for name menu
            NameChoice nameChoice = (NameChoice) NameMenu.GetChoice();//displays name menu and gets choice
                while (nameChoice != NameChoice.QUIT)//if name choice is quit it returns to main menu
                {
                    switch (nameChoice)//switch statement for dealing with user choice
                    {
                        case NameChoice.ADD://choice to add name to NameList
                            Console.WriteLine("Enter name to add");
                            Name newName = new Name(Console.ReadLine());//gets name to add
                            Names = Names + newName;//adds name to namelist
                            Names.ListChange();
                            ListChanged = true;//sets list as changed
                        break;
                        case NameChoice.DELETE: //choice to delete a name
                            Console.WriteLine("Which name would you like to remove?");
                            Name RemoveName = Names[Console.ReadLine()];//gets name to remove
                            Names = Names - RemoveName;
                            ListChanged = true;//sets list as changed
                        break;
                        case NameChoice.LIST://choice to list the names
                            Console.WriteLine("Which format would you like the name(s) to be in?");//prompts for user prefered name format
                            FormatMenu(Names);//calls to display format menu
                        break;
                        case NameChoice.NAME://choice to find a name
                            Console.WriteLine("Which Name would you like to find?");
                            String nameToFind = Console.ReadLine();
                            NameList FoundNames = Names.FindNames(nameToFind);//gets names similar to name given
                            Name FoundName;
                            if (FoundNames.Count!=0)//if no names found
                            {
                            
                                for ( int i = 1; i <= FoundNames.Count; i++)//displays each name found
                                    {
                                        Console.WriteLine("{0}. {1}",i, FoundNames[i-1].NameToString(NameFormat.FIRST));
                                    }
                                Console.WriteLine("These names were found, please select one. \n0 is if you did not see the name you were looking for");//asks user to pick name from names found
                            String s = Console.ReadLine();
                            try
                            {
                                int input = Convert.ToInt32(Console.ReadLine());//converts user input to int


                                if (input != 0 && input<= FoundNames.Count)//validates name chosen
                                {
                                    Console.Clear(); //clears console
                                    FoundName = FoundNames[input]; //gets name chosen from found names
                                    Console.WriteLine("Found the name {0}\nWhat would you like to do with it?", FoundName.NameToString(NameFormat.ORIGINAL));
                                    UtilityNamespace.Menu NameAction = new UtilityNamespace.Menu("Name Actions"); //options to remove name
                                    NameAction = NameAction + "Delete The Name" + "Nothing"; //menu for single found name
                                    int choice = NameAction.GetChoice();
                                    if (choice == 1)
                                        Names = Names - FoundName;//removes name from NameList
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Invalid choice");//throws exception if user choice is invalid
                            }

                            }
                            else
                            {
                                Console.WriteLine("Name was not found"); //if name is not found
                                Tools.PressAnyKey();
                            }
                        break;
                       
                    }
                    nameChoice = (NameChoice)NameMenu.GetChoice(); //prompts for name choice again
                }
                if (Names.ListChanged) //if nameList is change ask to save
                {
                    UtilityNamespace.Menu SaveMenu = new UtilityNamespace.Menu("NameList has been changed, Would you like to save it to a file?");//save file menu
                    SaveMenu = SaveMenu + "Yes" + "No";
                    int SaveChoice = SaveMenu.GetChoice();
                    if (SaveChoice == 1)
                    {
                        Tools.SaveFileDialog("Save File", Names.ToArray(), "text files|*.txt");// saves file
                    }
                }
                
           }
 
        /// <summary>  used to let user choose what format they want their names to be shown in.</summary>
        /// <param name="listOFNames">  takes the list of names to be displayed</param>
        public static void FormatMenu(NameList listOFNames)
        {
            UtilityNamespace.Menu FormatMenu = new UtilityNamespace.Menu("Format Menu");//menu for name format
            FormatMenu = FormatMenu + "Original Format" + "First Name First" + "Last Name First" + "Return to Name Menu";
            NameFormat formatChoice = (NameFormat)FormatMenu.GetChoice();
            switch (formatChoice)//switch case for user choice
            {
                case NameFormat.ORIGINAL: //list names in original given format
                    listOFNames.Format = NameFormat.ORIGINAL;
                    listOFNames.Display();
                    break;
                case NameFormat.FIRST: //list names in First Name First format
                    Tools.DisplayList(listOFNames.SortFirst());
                    break;
                case NameFormat.LAST: //list names in last name first format
                    Tools.DisplayList(listOFNames.SortLast());
                    break;
                case NameFormat.QUIT: //returns to previous menu
                    Console.WriteLine("Returning to Name menu");
                    return;
            }
            Tools.PressAnyKey();
            return;

        }
        #endregion
    }
    
}
