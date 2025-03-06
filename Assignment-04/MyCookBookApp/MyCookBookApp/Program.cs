using MyCookBookApp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpContextAccessor();
builder.Services.AddControllersWithViews();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder => builder.WithOrigins("http://localhost:5082")
                          .AllowAnyHeader()
                          .AllowAnyMethod());
});

builder.Services.AddHttpClient<IRecipeService, RecipeService>();
builder.Services.AddScoped<IRecipeService, RecipeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors("AllowSpecificOrigin");
app.UseAuthorization();

app.MapControllerRoute(
    name: "recipes",
    pattern: "Recipes",
    defaults: new { controller = "Home", action = "Recipes" });

app.MapControllerRoute(
    name: "addRecipe",
    pattern: "addRecipe",
    defaults: new { controller = "Home", action = "addRecipe" });

app.MapControllerRoute(
    name: "about",
    pattern: "About",
    defaults: new { controller = "Home", action = "About" });

app.MapControllerRoute(
    name: "recipeList",
    pattern: "recipeList",
    defaults: new { controller = "Recipe", action = "RecipeList" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();