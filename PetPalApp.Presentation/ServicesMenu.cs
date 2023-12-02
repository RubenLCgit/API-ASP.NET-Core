using PetPalApp.Business;

namespace PetPalApp.Presentation;

public class ServiceMenu
{
  public readonly IUserService userService;
  public readonly ISupplierService supplierService;

  public ServiceMenu(IUserService _userService, ISupplierService _supplierService) {
    userService = _userService;
    supplierService = _supplierService;
  }

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
    UserMenu userMenu = new(userService, supplierService);
    switch (option)
    {
      case "1":
        Console.WriteLine("Show All Services");
      break;
      case "2":
        Console.WriteLine("Search Service");
      break;
      case "3":
        userMenu.DisplayUserMenu(name);
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
        
        break;
    }
  }
}