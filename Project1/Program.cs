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
            Tools.setup("Name List", welcome);
            Tools.PressAnyKey();
            #endregion
            #region Get User Info
            Regex emailPat = new Regex(@"([\w\W]+)(@)([\w]+)[\.](com|edu)");
            Regex phonePat = new Regex(@"\(?[0-9]{3}\)?\s?[0-9]{3}\-?[0-9]{4}");

            Console.WriteLine("What is your name?");
            String StringName = Console.ReadLine();
            while (StringName == null)
            {
                Console.WriteLine("What is your name?");
                StringName = Console.ReadLine();
            }
            Name UserName = new Name(StringName);
            Console.WriteLine("What is your email address?");
            String Email = Console.ReadLine();
            while (!emailPat.Match(Email).Success)
            {
                Console.WriteLine("Email is not valid please Enter your email again");
                Email = Console.ReadLine();
            }
            Console.WriteLine("What is your phone number?");
            String PhoneNumber = Console.ReadLine();
            while (!phonePat.Match(PhoneNumber).Success)
            {
                Console.WriteLine("Phone Number is not valid please Enter your Phone Number again");
                PhoneNumber = Console.ReadLine();
            }
            #endregion
            MainMenu();
            Console.WriteLine("Goodbye {0}! \n The company will not be sending any spam email to {1}.\n " +
                "We also will definitely not call you at {2}", UserName.NameToString(NameFormat.ORIGINAL),Email,PhoneNumber);
            Tools.PressAnyKey();

        }

        #region Menus

        /// <summary>  runs the main menu for loading a file or quitting</summary>
        public static void MainMenu()
        {
            UtilityNamespace.Menu main = new UtilityNamespace.Menu("Main Menu");
            main = main + "Get Name List from file" + "Quit";
            MainChoice choice = (MainChoice)main.GetChoice();
            while (choice != MainChoice.QUIT)
            {
                NameMenu();
                choice = (MainChoice) main.GetChoice();
            }
        }

        /// <summary>runs Name menu which controls and allows the user to change and edit the name list</summary>
        public static void NameMenu()
        {
            String FileName = Tools.OpenDialog("Find Names", "text files|*.txt");
            if (FileName == null)
                return;
            bool ListChanged = false;
            String[] FileContents = Tools.FileToString(FileName);
            NameList Names = new NameList(FileContents);
            for(int i = 0;i<Names.Count;i++)
            {
                Console.WriteLine(Names[i].NameToString(NameFormat.ORIGINAL));
            }

            Tools.PressAnyKey();
            UtilityNamespace.Menu NameMenu = new UtilityNamespace.Menu("Name Menu");
            NameMenu = NameMenu + "Add Name" + "Delete Name" + "List Names" + "Find Name" + "Return to Main Menu";
            NameChoice nameChoice = (NameChoice) NameMenu.GetChoice();
                while (nameChoice != NameChoice.QUIT)
                {
                    switch (nameChoice)
                    {
                        case NameChoice.ADD:
                            Console.WriteLine("Enter name to add");
                            Name newName = new Name(Console.ReadLine());
                            Names = Names + newName;
                            Names.ListChange();
                            ListChanged = true;
                        break;
                        case NameChoice.DELETE:
                            Console.WriteLine("Which name would you like to remove?");
                            Name RemoveName = Names[Console.ReadLine()];
                            Names = Names - RemoveName;
                            ListChanged = true;
                        break;
                        case NameChoice.LIST:
                            Console.WriteLine("Which format would you like the name(s) to be in?");
                            FormatMenu(Names);
                        break;
                        case NameChoice.NAME:
                            Console.WriteLine("Which Name would you like to find?");
                            String nameToFind = Console.ReadLine();
                            NameList FoundNames = Names.FindNames(nameToFind);
                            Name FoundName;
                            if (FoundNames.Count!=0)
                            {
                            
                                for ( int i = 1; i <= FoundNames.Count; i++)
                                    {
                                    Console.WriteLine("{0}. {1}",i, FoundNames[i-1].NameToString(NameFormat.FIRST));
                                    }
                                Console.WriteLine("These names were found, please select one. \n0 is if you did not see the name you were looking for");
                            String s = Console.ReadLine();
                            try
                            {
                                int input = Convert.ToInt32(Console.ReadLine());


                                if (input != 0)
                                {
                                    Console.Clear();
                                    FoundName = FoundNames[input];
                                    Console.WriteLine("Found the name {0}\nWhat would you like to do with it?", FoundName.NameToString(NameFormat.ORIGINAL));
                                    UtilityNamespace.Menu NameAction = new UtilityNamespace.Menu("Name Actions");
                                    NameAction = NameAction + "Delete The Name" + "Nothing";
                                    int choice = NameAction.GetChoice();
                                    if (choice == 1)
                                        Names = Names - FoundName;
                                }
                            }
                            catch(Exception e)
                            {
                                Console.WriteLine("Invalid choice");
                            }

                            }
                            else
                            {
                                Console.WriteLine("Name was not found");
                                Tools.PressAnyKey();
                            }
                        break;
                       
                    }
                    nameChoice = (NameChoice)NameMenu.GetChoice();
                }
                if (Names.ListChanged)
                {
                    UtilityNamespace.Menu SaveMenu = new UtilityNamespace.Menu("NameList has been changed, Would you like to save it to a file?");
                    SaveMenu = SaveMenu + "Yes" + "No";
                    int SaveChoice = SaveMenu.GetChoice();
                    if (SaveChoice == 1)
                    {
                        Tools.SaveFileDialog("Save File", Names.ToArray(), "text files|*.txt");
                    }
                }
                
           }
 
        /// <summary>  used to let user choose what format they want their names to be shown in.</summary>
        /// <param name="listOFNames">  takes the list of names to be displayed</param>
        public static void FormatMenu(NameList listOFNames)
        {
            UtilityNamespace.Menu FormatMenu = new UtilityNamespace.Menu("Format Menu");
            FormatMenu = FormatMenu + "Original Format" + "First Name First" + "Last Name First" + "Return to Name Menu";
            NameFormat formatChoice = (NameFormat)FormatMenu.GetChoice();
            switch (formatChoice)
            {
                case NameFormat.ORIGINAL:
                    listOFNames.Format = NameFormat.ORIGINAL;
                    listOFNames.Display();
                    break;
                case NameFormat.FIRST:
                    Tools.DisplayList(listOFNames.SortFirst());
                    break;
                case NameFormat.LAST:
                    Tools.DisplayList(listOFNames.SortLast());
                    break;
                case NameFormat.QUIT:
                    Console.WriteLine("Returning to Name menu");
                    return;
            }
            Tools.PressAnyKey();
            return;

        }
        #endregion
    }
    
}
