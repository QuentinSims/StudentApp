using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Radzen;
using StudentApp;
using StudentApp.Services.AccountManagementService;
using StudentApp.Services.AuthenicationManager;
using StudentApp.Services.CourseService;
using StudentApp.Services.JwtService;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddAuthorizationCore();
builder.Services.AddSingleton<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<CustomAuthenticationStateProvider>();
builder.Services.AddSingleton<AuthenticationStateProvider>(provider => provider.GetRequiredService<CustomAuthenticationStateProvider>());
builder.Services.AddSingleton<IAccountService, AccountService>();
builder.Services.AddSingleton<IAuthService, AuthService>();
builder.Services.AddSingleton<ICourseManagementService, CourseManagementService>();

builder.Services.AddSingleton<JwtAuthorizationMessageHandler>();
builder.Services.AddSingleton(sp =>
{
    var handler = sp.GetRequiredService<JwtAuthorizationMessageHandler>();
    handler.InnerHandler = new HttpClientHandler();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? builder.HostEnvironment.BaseAddress)
    };
});

builder.Services.AddRadzenComponents();


await builder.Build().RunAsync();
