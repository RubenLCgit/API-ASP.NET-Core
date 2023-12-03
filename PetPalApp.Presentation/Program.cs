using PetPalApp.Business;
using PetPalApp.Data;
using PetPalApp.Domain;
using PetPalApp.Presentation;

IRepositoryGeneric<User> userRepository= new UserRepository();
IRepositoryGeneric<Service> serviceRepository= new ServiceRepository();
IUserService userService = new UserService(userRepository);
ISupplierService supplierService = new SupplierService(serviceRepository, userRepository);
MainMenu mainMenu = new(userService, supplierService);
mainMenu.ApplicationInit();