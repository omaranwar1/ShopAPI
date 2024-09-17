 
public class DependencyContainer
{
  public static void RegisterServices(IServiceCollection services)
  
  {
    services.AddScoped<ICartService, CartService>();
    services.AddScoped<IProductService, ProductService>();
    services.AddScoped<ICartRepository, CartRepository>();
    services.AddScoped<IProductRepository, ProductRepository>();

  }

}