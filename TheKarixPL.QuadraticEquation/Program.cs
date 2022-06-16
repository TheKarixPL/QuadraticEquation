using TheKarixPL.QuadraticEquation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMvcCore()
    .AddRazorViewEngine();
builder.Services.AddControllers();
builder.Services.AddSingleton<GraphService>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
app.UseStaticFiles();
app.UseRouting();
app.UseEndpoints(endpoint =>
{
    endpoint.MapControllerRoute("Home", "{Controller=Home}/{Action=Index}");
});

app.Run();