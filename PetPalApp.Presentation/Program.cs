using PetPalApp.Business;
using PetPalApp.Data;
using PetPalApp.Domain;
using PetPalApp.Presentation;

IRepositoryGeneric<User> userRepository= new UserRepository();
IUserService userService = new UserService(userRepository);
MainMenu mainMenu = new(userService);
mainMenu.ApplicationInit();