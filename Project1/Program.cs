using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using UtilityNamespace;
using System.Windows.Forms;


namespace DataStructures
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            #region setup
            String welcome = "Welcome to NameList!";
            Tools.setup("Name List",welcome);
            Tools.PressAnyKey();
            #endregion
            #region Get User Info
            Regex emailPat = new Regex(@"([\w\W]+)(@)([\w]+)[\.](com|edu)");
            Regex phonePat = new Regex(@"\(?[0-9]{3}\)?\s?[0-9]{3}\-?[0-9]{4}");
            Console.WriteLine("What is your name?");
            Name UserStringName = new Name(Console.ReadLine());
            Console.WriteLine("What is your email address?");
            String Email = Console.ReadLine();
            Console.WriteLine("What is your phone number?");
            String PhoneNumber = Console.ReadLine();
            #endregion
            NameChoice nameChoice;
 
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
                            Console.WriteLine("Which format would you like the names to be in?");

                            //Tools.DisplayList(Names.FindNames(nameToFind));
                            break;
                        case NameChoice.LIST:
                            FormatMenu();
                            break;
                        case NameChoice.RETURN:
                            break;
                    }
                    nameChoice = (NameChoice)NameMenu.GetChoice();
            }
        }

        public static void FormatMenu()
        {
            UtilityNamespace.Menu FormatMenu = new UtilityNamespace.Menu("Format Menu");
            FormatMenu = FormatMenu + "Original Format" + "First Name First" + "Last Name First" + "Return to Name Menu";
            NameFormat formatChoice = (NameFormat)FormatMenu.GetChoice();
            switch (formatChoice)
            {
                case NameFormat.ORIGINAL:
                    break;
                case NameFormat.FIRST:
                    break;
                case NameFormat.LAST:
                    break;
                case NameFormat.RETURN:
                    Console.WriteLine("Returning to Name menu");
                    Tools.PressAnyKey();
                    return;
            }

            return;

        }
        #endregion
    }
    
}
