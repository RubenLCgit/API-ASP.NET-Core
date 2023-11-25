namespace PetPalApp.Presentation;

public class MainMenu
{

  public static void DisplayMainMenu()
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
    OptionMenu.EmuleLoad();
    ShowOptions();
  }

  public static void ShowOptions()
  {
    Console.Clear();
    bool login = false;
    Console.WriteLine("\t\t1. Sign up");
    Console.WriteLine("\t\t2. Sign in");
    Console.WriteLine("\t\t3. Exit\n");
    Console.Write("\n\nPlease select an option:");
    OptionMenu.SelectOption(login, Console.ReadLine());
  }

}