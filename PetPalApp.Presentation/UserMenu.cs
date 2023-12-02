using PetPalApp.Business;

namespace PetPalApp.Presentation;

public class UserMenu
{
  public readonly IUserService userService;

  public UserMenu(IUserService _userService) {
    userService = _userService;
  }

    public void DisplayUserMenu()
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
    SelectUserOption(Console.ReadLine());
  }
  public void SelectUserOption(string option)
  {
    MainMenu mainMenu = new(userService);
    switch (option)
    {
      case "1":
        Console.WriteLine("Show Services");
      break;
      case "2":
        Console.WriteLine("Show My Account");
      break;
      case "3":
        mainMenu.DisplayMainMenu();
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
        DisplayUserMenu();
        break;
    }
  }
}