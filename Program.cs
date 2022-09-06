using RiftekTemplateUpgrade.Service;
using RiftekTemplateUpgrade.FanucSocket;
using RiftekTemplateUpgrade.Worker;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSingleton<JsonWorker>();
builder.Services.AddSingleton<ScannerService>();
builder.Services.AddSingleton<TemplateService>();
builder.Services.AddSingleton<SocketTcpServer>();
builder.Services.AddCors();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseCors(config => config.AllowAnyOrigin());

app.Map("/", (routes) =>
    {
        routes.Response.Redirect("/templates.html");
        return Task.FromResult(0);
    });
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


app.Run();


