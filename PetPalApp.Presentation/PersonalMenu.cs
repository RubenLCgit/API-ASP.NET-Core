using PetPalApp.Business;

namespace PetPalApp.Presentation;

public class PersonalMenu
{
  public readonly IUserService userService;
  public readonly ISupplierService supplierService;

  public PersonalMenu(IUserService _userService, ISupplierService _supplierService) {
    userService = _userService;
    supplierService = _supplierService;
  }

    public void DisplayPersonalMenu(string name)
  {
    #if !DEBUG
    Console.Clear();
    #endif
    Console.WriteLine(@"
    1. Account information

    2. Offering service

    3. Sell product

    4. My products and services

    5. Remove product or service

    6. Back
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
        Console.WriteLine(userService.ShowAccount(name));
        Console.WriteLine("Press any key to return...");
        Console.ReadLine();
        DisplayPersonalMenu(name);
      break;
      case "2":
        EnterServiceData(name);
      break;
      case "3":
        Console.WriteLine("Sell product");
      break;
      case "4":
        Console.WriteLine("My products and services");
      break;
      case "5":
        Console.WriteLine("Remove product or service");
      break;
      case "6":
        userMenu.DisplayUserMenu(name);
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
      break;
    }
  }

  private void EnterServiceData(string name) {
    int idUser;
    string type, nameService, description, onlineStr;
    decimal price;
    bool online, isNumber;

    idUser = userService.GetIdUser(name);

    Console.Write("Enter the service name: ");
    nameService = Console.ReadLine();
    while (string.IsNullOrEmpty(nameService))
    {
      Console.Write("\nYou must enter a valid service name: ");
      nameService = Console.ReadLine();
    }
    Console.Write("\nEnter the type of service: ");
    type = Console.ReadLine();
    while (string.IsNullOrEmpty(type))
    {
      Console.Write("\nYou must enter a valid type: ");
      type = Console.ReadLine();
    }
    Console.Write("\nEnter a service description: ");
    description = Console.ReadLine();
    while (string.IsNullOrEmpty(description))
    {
      Console.Write("\nYou must enter a description: ");
      description = Console.ReadLine();
    }
    do
    {
      Console.Write("\nEnter the service price: ");
      string priceStr = Console.ReadLine();
      isNumber = decimal.TryParse(priceStr, out price);
      if (!isNumber)
      {
        Console.Write("\nYou must enter a valid number\n");
      }
      else price = decimal.Parse(priceStr);
    }
    while (!isNumber);

    Console.Write("\nDo you offer shipping services? Y/N: ");
    onlineStr = Console.ReadLine();
    while (string.IsNullOrEmpty(onlineStr) || !string.Equals(onlineStr, "Y", StringComparison.OrdinalIgnoreCase) && !String.Equals(onlineStr, "N", StringComparison.OrdinalIgnoreCase))
    {
      Console.Write("\nYou must enter \"Y\" or \"N\": ");
      onlineStr = Console.ReadLine();
    }
    if (String.Equals(onlineStr, "Y", StringComparison.OrdinalIgnoreCase))
    {
      online = true;
    }
    else
    {
      online = false;
    }
    supplierService.RegisterService(idUser, type, nameService, description, price, online);
    
    DisplayPersonalMenu(name);
  }
}