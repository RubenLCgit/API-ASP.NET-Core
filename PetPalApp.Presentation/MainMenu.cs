namespace PetPalApp.Presentation;

public class MainMenu
{
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
    Console.WriteLine("\t\t3. Products\n");
    Console.WriteLine("\t\t4. Services\n");
    Console.WriteLine("\t\t5. Exit\n");
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
      case "4":
        Conlose.Write("\nServicios disponibles");
        ServiceMenu.PressToContinue();
        DisplayMainMenu();
      break;
      case "5":
        Console.Write("\nProductos disponibles");
        ServiceMenu.PressToContinue();
        DisplayMainMenu();
      break;
      default:
        Console.WriteLine("\nInvalid option\n");
        ServiceMenu.PressToContinue();
        DisplayMainMenu();
      break;
    }
  }

  private void EnterUserData() {
    string name, email, supplier, password;
    Console.Write("\nEnter your user name: ");
    name = Console.ReadLine();
    while (string.IsNullOrEmpty(name))
    {
    Console.Write("\nYou must enter a valid user name: ");
      name = Console.ReadLine();
    }
    Console.Write("\nEnter a valid email address: ");
    email = Console.ReadLine();
    while (true) //Metodo que compruebe si el email es valido
    {
    Console.Write("\nYou must enter a valid e-mail address: ");
    email = Console.ReadLine();
    }
    if (false) //Metodo que comprueve si el email de usuario ya existe
    {
      Console.Write($"\nUser email {email} already exists.");
      ServiceMenu.PressToContinue();
      DisplayMainMenu();
    }
    else
    {
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
      //userService.RegisterUser(name, email, password, supplier); Metodo que registra el usuario
      Console.WriteLine("Registered user.");
      ServiceMenu.PressToContinue();
      DisplayMainMenu();
    }
  }

  private void EnterDataLoginUser()
  {
    string name, password;
    bool incorrectLogin = true;
    Console.Write("\nEnter your user name: ");
    name = Console.ReadLine();
    Console.Write("\nEnter your secret password: ");
    password = Console.ReadLine();
    if (false) //Metodo que compruebe si el usuario y la contraseña son correctos
    {
      Console.WriteLine($"Incorrect username or password.");
      ServiceMenu.PressToContinue();
    }
    else 
    {
        incorrectLogin = false;
    }
    if (incorrectLogin) DisplayMainMenu();
    else userMenu.DisplayUserMenu(name);
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