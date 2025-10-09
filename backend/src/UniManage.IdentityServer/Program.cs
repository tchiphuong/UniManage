using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using Duende.IdentityServer.Test;
using Microsoft.Extensions.DependencyInjection;
using UniManage.IdentityServer;

// Khởi tạo builder cho ứng dụng web
var builder = WebApplication.CreateBuilder(args);

// Thêm dịch vụ IdentityServer vào DI container
builder.Services.AddIdentityServer()
    .AddInMemoryIdentityResources(Config.IdentityResources)
    .AddInMemoryApiScopes(Config.ApiScopes)
    .AddInMemoryClients(Config.Clients)
    .AddTestUsers(Config.TestUsers); // Đăng ký TestUsers ở đây

// Thêm dịch vụ static files và routing (nếu dùng UI)
builder.Services.AddRouting();
builder.Services.AddControllersWithViews(); // Thêm dòng này để hỗ trợ MVC
builder.Services.AddRazorPages(); // Thêm dòng này để hỗ trợ Razor Pages

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

// Sử dụng middleware IdentityServer
app.UseIdentityServer();

app.UseAuthorization();

// Map UI endpoints (IdentityServer UI)
app.MapDefaultControllerRoute();
app.MapRazorPages();

// Định nghĩa endpoint GET "/" trả về thông báo trạng thái
app.MapGet("/", () => "IdentityServer is running!");

// Chạy ứng dụng
app.Run();
