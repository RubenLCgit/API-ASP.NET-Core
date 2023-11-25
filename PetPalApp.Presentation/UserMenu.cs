namespace PetPalApp.Presentation;

public class UserMenu
{
  public static void DisplayUserMenu()
  {
    Console.Clear();
    bool login = true;//TODO:Correct login emulation
    Console.WriteLine(@"
    1. Services

    2. My account

    3. Main Menu
    ");
    Console.Write("\n\nPlease select an option:");
    OptionMenu.SelectOption(login, Console.ReadLine());
  }


}