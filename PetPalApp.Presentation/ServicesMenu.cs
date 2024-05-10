namespace PetPalApp.Presentation;

public class ServiceMenu
{
  public void DisplayServiceMenu(string name)
  {
    #if !DEBUG
    Console.Clear();
    #endif
    Console.WriteLine(@"
    1. See all Services

    2. Search Service

    3. Back
    ");
    Console.Write("\n\nPlease select an option:");
    SelectServiceOption(name, Console.ReadLine());
  }
  public void SelectServiceOption(string name, string option)
  {
    UserMenu userMenu = new(userService, supplierService, productService);
    switch (option)
    {
      case "1":
        Console.WriteLine("Pintar todos los servicios del usuario");
        PressToContinue();
        DisplayServiceMenu(name);
      break;
      case "2":
        Console.Write("What service are you looking for?: ");
        String typeService = Console.ReadLine();
        while (string.IsNullOrEmpty(typeService))
        {
          Console.Write("\nYou must enter a valid service type: ");
          typeService = Console.ReadLine();
        }
        Console.WriteLine("Pintar los servicios que coincidan con el tipo de servicio");
        PressToContinue();
        DisplayServiceMenu(name);
      break;
      case "3":
        userMenu.DisplayUserMenu(name);
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
        PressToContinue();
        DisplayServiceMenu(name);
        break;
    }
  }

  public static void PressToContinue()
  {
    Console.WriteLine("Press any key to continue...");
    #if !DEBUG
    Console.ReadKey();
    #endif
  }
}