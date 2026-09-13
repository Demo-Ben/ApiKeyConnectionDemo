using BlazorClientDemo.Components;
using BlazorClientDemo.Services;

var builder = WebApplication.CreateBuilder(args);


builder.WebHost.ConfigureKestrel(options =>
{
    options.Configure(builder.Configuration.GetSection("Kestrel"));
});

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


var apiUrl = builder.Configuration["ApiSettings:BaseUrl"];
builder.Services.AddScoped<ApiClient>(sp =>
{
    var client = new HttpClient
    {
        BaseAddress = new Uri(apiUrl)
    };
    return new ApiClient(client);
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseCors("AllowBlazor");   // <--- IMPORTANT

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
