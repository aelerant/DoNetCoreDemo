using WebAPIDemo;

#if false
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ×¢²áÓÊ¼þ·þÎñ
builder.Services.AddTransient<EmailService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
#endif

#if true
public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args)
    {
        //var host = new ConfigurationBuilder().SetBasePath(Environment.CurrentDirectory)
        //    .AddJsonFile("host.json")
        //    .Build();

        //var db = new ConfigurationBuilder().SetBasePath(Path.Join(Environment.CurrentDirectory, "config"))
        //    .AddJsonFile("db.json")
        //    .Build();

        return Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                //webBuilder.UseConfiguration(db);
                //webBuilder.UseConfiguration(host);
                webBuilder.UseStartup<Startup>();
            });
    }
}
#endif
