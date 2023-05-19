using YCCodeChallenge;
using YCCodeChallenge.Excel;
using YCCodeChallenge.Repository;
using YCCodeChallenge.Services;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDataRepository, DataRepository>();
builder.Services.AddScoped<ICalculationService, CalculationService>();
builder.Services.AddSingleton<IExcelReader>(new ExcelReader("Data/Sample Super Data.xlsx"));

builder.Services.Configure<CalculationOptions>(
    builder.Configuration.GetSection(CalculationOptions.Calculation));

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
