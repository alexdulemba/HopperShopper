using HopperShopper.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

builder.Services.AddLogging();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
  options.Cookie.Name = ".HopperShopper.LoginInformation";
  options.IdleTimeout = TimeSpan.FromMinutes(5);
  options.Cookie.IsEssential = true;
  options.Cookie.HttpOnly = true;
  options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
});

builder.Services.AddDbContext<HopperShopperContext>(options =>
{
  var folder = Environment.SpecialFolder.DesktopDirectory;
  var folderPath = Environment.GetFolderPath(folder);
  var path = Path.Join(folderPath, "hoppershopper.db");

  options.UseSqlite($"Data Source={path}");

  Debug.WriteLine($"Creating SQLite db at: {path}");
});

builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;
  var logger = services.GetRequiredService<ILogger<Program>>();
  try 
  {
    var dbContext = services.GetRequiredService<HopperShopperContext>();
    await dbContext.Database.EnsureCreatedAsync();
    await BogusDatabaseInitializer.InitializeAsync(dbContext);
    logger.Log(LogLevel.Information, $"Created database with {dbContext.Products.Count()} products");
  }
  catch (Exception ex)
  {
    logger.LogError(ex, "An error occurred when initializing the database");
  }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
  app.UseExceptionHandler("/Home/Error");
  // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
  app.UseHsts();
}
else
{
  app.UseDeveloperExceptionPage();
  app.UseMigrationsEndPoint();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
