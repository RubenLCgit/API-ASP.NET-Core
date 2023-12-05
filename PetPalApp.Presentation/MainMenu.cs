using PetPalApp.Business;

namespace PetPalApp.Presentation;

public class MainMenu
{
  public readonly IUserService userService;
  public readonly ISupplierService supplierService;
  public readonly IProductService productService;

  public MainMenu(IUserService _userService, ISupplierService _supplierService, IProductService _productService) {
    userService = _userService;
    supplierService = _supplierService;
    productService = _productService;
  }
  public void ApplicationInit()
  {
    Console.WriteLine(@"
    
                      ==================== PETPAL ====================

                    Your Digital Companion for the Well-being of your Pets

                                        ╭━━━━━━━━━━━╮
                                       ╭╯ ╭━━╮ ╭━━╮ ╰╮
                                       ┃ ┃┃╭╮┃ ┃╭╮┃┃ ┃
                                       ┃ ┃┻┻┻┛ ┗┻┻┻┃ ┃
                                       ┃ ┃╭╮┈◢▇◣┈╭╮┃
                                       ╰┳╯┃╰━━┳┳┳╯┃╰┳╯
                                        ┃ ╰━━━┫┃┣━╯ ┃
                                        ┃_____╰━╯___┃
                                          
    ");
    Thread.Sleep(2000);
    Console.Write("Loading");
    EmuleLoad();
    DisplayMainMenu();
  }

  public void DisplayMainMenu()
  {
    #if !DEBUG
    Console.Clear();
    #endif
    Console.WriteLine("\t\t1. Sign up");
    Console.WriteLine("\t\t2. Sign in");
    Console.WriteLine("\t\t3. Exit\n");
    Console.Write("\n\nPlease select an option:");
    SelectMainOption(Console.ReadLine());
  }

  public void SelectMainOption(string option)
  {
    switch (option)
    {
      case "1":
        EnterUserData();
      break;
      case "2":
        EnterDataLoginUser();
      break;
      case "3":
        Console.Write("\nClosing the application");
        EmuleLoad();
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
        DisplayMainMenu();
      break;
    }
  }

  private void EnterUserData() {
    string name, email, supplier, password;

    Console.Write("Enter your user name: ");
    name = Console.ReadLine();
    while (string.IsNullOrEmpty(name))
    {
      Console.Write("\nYou must enter a valid user name: ");
      name = Console.ReadLine();
    }
    Console.Write("\nEnter a valid email address: ");
    email = Console.ReadLine();
    while (!userService.ValidatEmail(email))
    {
      Console.Write("\nYou must enter a valid e-mail address: ");
      email = Console.ReadLine();
    }
    Console.Write("\nAre you a supplier? Y/N: ");
    supplier = Console.ReadLine();
    while (string.IsNullOrEmpty(supplier) || !string.Equals(supplier, "Y", StringComparison.OrdinalIgnoreCase) && !String.Equals(supplier, "N", StringComparison.OrdinalIgnoreCase))
    {
      Console.Write("\nYou must enter \"Y\" or \"N\": ");
      supplier = Console.ReadLine();
    }
    Console.Write("\nEnter your secret password: ");
    password = Console.ReadLine();
    while (string.IsNullOrEmpty(password) || password.Length < 7)
    {
      Console.Write("\nYour password must consist of at least 7 characters: ");
      password = Console.ReadLine();
    }
    if (userService.checkUserExist(name, email))
    {
      Console.WriteLine($"User {name} or email {email} already exists.");
    }
    else userService.RegisterUser(name, email, password, supplier);
    
    DisplayMainMenu();
  }

  private void EnterDataLoginUser()
  {
    UserMenu userMenu = new(userService, supplierService, productService);
    string name, password;
    Console.Write("\nEnter your user name: ");
    name = Console.ReadLine();
    Console.Write("\nEnter your secret password: ");
    password = Console.ReadLine();//TODO: Implement password entry method with asterisks
    if (!userService.CheckLogin(name, password))
    {
      Console.WriteLine($"Incorrect username or password.");
      DisplayMainMenu();
    }
    else 
    {
      userMenu.DisplayUserMenu(name);
    }
    
  }
  public static void EmuleLoad()
  {
    for (int i = 0; i < 4; i++)
    {
      Thread.Sleep(1000);
      Console.Write(".");
    }
  }
}