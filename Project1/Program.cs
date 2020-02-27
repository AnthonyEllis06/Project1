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
        [STAThread]
        static void Main(string[] args)
        {
            #region setup
            String welcome = "Welcome to NameList!";
            Tools.setup("Name List", welcome);
            Tools.PressAnyKey();
            #endregion
            #region Get User Info
            
            #endregion

            MainMenu();

        }

        static void other()
        {
            Regex emailPat = new Regex(@"([\w\W]+)(@)([\w]+)[\.](com|edu)");
            Regex phonePat = new Regex(@"\(?[0-9]{3}\)?\s?[0-9]{3}\-?[0-9]{4}");

            Console.WriteLine("What is your name?");
            Name UserStringName = new Name(Console.ReadLine());
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
        }

       #region Menus
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

        public static void NameMenu()
        {
            String FileName = Tools.OpenDialog("Find Names", "text files|*.txt");
            String[] FileContents = Tools.FileToString(FileName);
            NameList Names = new NameList(FileContents);
            for(int i = 0;i<Names.Count;i++)
            {
                Name n = Names[i];
                Console.WriteLine(n.NameToString(NameFormat.FIRST));
                Console.WriteLine(n.NameToString(NameFormat.ORIGINAL));
            }

            Tools.PressAnyKey();
            UtilityNamespace.Menu NameMenu = new UtilityNamespace.Menu("Name Menu");
            NameMenu = NameMenu + "Add Name" + "Delete Name" + "List Names" + "Find Names" + "Return to Main Menu";
            NameChoice nameChoice = (NameChoice) NameMenu.GetChoice();
                while (nameChoice != NameChoice.RETURN)
                {
                    switch (nameChoice)
                    {
                        case NameChoice.ADD:
                            Console.WriteLine("Enter name to add");
                            Name newName = new Name(Console.ReadLine());
                            Names = Names + newName;
                            break;
                        case NameChoice.DELETE:
                            Console.WriteLine("Which name would you like to remove?");
                            Name RemoveName = Names[Console.ReadLine()];
                            Names = Names - RemoveName;
                            break;
                        case NameChoice.NAME:
                            Console.WriteLine("Which Name would you like to find?");
                            String nameToFind = Console.ReadLine();
                            Name FoundName= Names[nameToFind];
                            if(FoundName!=null)
                            {
                                Console.WriteLine("Found the name {0}\nWhat would you like to do with it?",FoundName.NameToString(NameFormat.ORIGINAL));
                                UtilityNamespace.Menu NameAction = new UtilityNamespace.Menu("Name Actions");
                                NameAction = NameAction+"Delete The Name"+"Nothing";
                                int choice = NameAction.GetChoice();
                                if(choice==1)
                                    Names=Names-FoundName;
                            }
                            else
                            {
                                Console.WriteLine("Name was not found");
                                Tools.PressAnyKey();
                            }
                            break;
                        case NameChoice.LIST:
                            Console.WriteLine("Which format would you like the name(s) to be in?");
                            FormatMenu(Names);
                            break;
                        case NameChoice.RETURN:
                            break;
                    }
                    nameChoice = (NameChoice)NameMenu.GetChoice();
            }
        }

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
                case NameFormat.RETURN:
                    Console.WriteLine("Returning to Name menu");
                    return;
            }
            Tools.PressAnyKey();
            return;

        }
        #endregion
    }
    
}
