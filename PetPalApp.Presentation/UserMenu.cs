using PetPalApp.Business;

namespace PetPalApp.Presentation;

public class UserMenu
{
  public void DisplayUserMenu(string name)
  {
    #if !DEBUG
    Console.Clear();
    #endif
    Console.WriteLine(@"
    1. Services

    2. My account

    3. Main Menu
    ");
    Console.Write("\n\nPlease select an option:");
    SelectUserOption(name, Console.ReadLine());
  }
  public void SelectUserOption(string name, string option)
  {
    MainMenu mainMenu = new();
    ServiceMenu serviceMenu = new();
    PersonalMenu personalMenu = new();
    switch (option)
    {
      case "1":
        serviceMenu.DisplayServiceMenu(name);
      break;
      case "2":
        personalMenu.DisplayPersonalMenu(name);
      break;
      case "3":
        mainMenu.DisplayMainMenu();
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
        DisplayUserMenu(name);
        break;
    }
  }
}