using System;
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
            Console.WriteLine("What is your name?");
            Name UserStringName = new Name(Console.ReadLine());
            Console.WriteLine("What is your email address?");
            String Email = Console.ReadLine();
            Console.WriteLine("What is your phone number?");
            String PhoneNumber = Console.ReadLine();
            #endregion
            UtilityNamespace.Menu main = new UtilityNamespace.Menu("Main Menu");
            UtilityNamespace.Menu NameMenu = new UtilityNamespace.Menu("Name Menu");
            UtilityNamespace.Menu FormatMenu = new UtilityNamespace.Menu("Format Menu");
            main = main + "Get Name List from file" + "Quit";
            NameMenu = NameMenu + "Add" + "Delete" + "List" + "Name" + "Return to Main Menu";
            FormatMenu = FormatMenu+ "Original Format" + "First Name First" + "Last Name First" + "Return to Name Menu";
            

            MainChoice choice = (MainChoice)main.GetChoice();
            NameChoice nameChoice;
            NameFormat formatChoice;
            while(choice != MainChoice.QUIT)
            {
                String FileName = Tools.OpenDialog("Find Names", "text files|*.txt");
                String[] FileContents = Tools.FileToString(FileName);
                NameList Names = new NameList(FileContents);
                nameChoice = (NameChoice)NameMenu.GetChoice();
                    while(nameChoice != NameChoice.RETURN)
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


                        }
                    }
                choice = (MainChoice)main.GetChoice();
            }
            

        }
    }
}
