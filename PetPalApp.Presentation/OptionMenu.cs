namespace PetPalApp.Presentation;

public class OptionMenu
{
  public static void SelectOption(bool login, string option)
  {
    switch (option)
    {
      case "1":
        if (login)
        {
          //TODO:Services
          Console.WriteLine("Show Services");
        }
        else
        {
          //TODO: CreateUser
          Console.WriteLine("Create new user");
        }
        break;
      case "2":
        if (login)
        {
          //TODO:MyAccount
          Console.WriteLine("Show My Account");
        }
        else
        {
          //TODO: LoginUser
          UserMenu.DisplayUserMenu();
        }
        break;
      case "3":
        if (login)
        {
          //TODO:ShowMainMenu
          MainMenu.ShowOptions();
        }
        else
        {
          //TODO: ExitAplication
          Console.Write("\nClosing the application");
          EmuleLoad();
        }
        break;
      default:
        Console.WriteLine("\nInvalid option\n");
        ReturnOptions(login);
        break;
    }
  }

  private static void ReturnOptions(bool login)
  {
    Console.WriteLine("Press any key to continue ...");
    Console.ReadKey();
    Console.Clear();
    if (login)
    {
      UserMenu.DisplayUserMenu();
    }
    MainMenu.ShowOptions();
  }

  public static void EmuleLoad()
  {
    Thread.Sleep(1000);
    Console.Write(".");
    Thread.Sleep(1000);
    Console.Write(".");
    Thread.Sleep(1000);
    Console.Write(".");
    Thread.Sleep(1000);
    Console.Write(".");
    Thread.Sleep(1000);
    Console.Write(".");
  }

}
