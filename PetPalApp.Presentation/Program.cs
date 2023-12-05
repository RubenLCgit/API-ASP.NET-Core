using PetPalApp.Business;
using PetPalApp.Data;
using PetPalApp.Domain;
using PetPalApp.Presentation;

IRepositoryGeneric<User> userRepository= new UserRepository();
IRepositoryGeneric<Service> serviceRepository= new ServiceRepository();
IRepositoryGeneric<Product> productRepository= new ProductRepository();
IUserService userService = new UserService(userRepository);
ISupplierService supplierService = new SupplierService(serviceRepository, userRepository);
IProductService productService = new ProductService(productRepository, userRepository);
MainMenu mainMenu = new(userService, supplierService, productService);
mainMenu.ApplicationInit();