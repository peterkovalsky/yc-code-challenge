using YCCodeChallenge;
using YCCodeChallenge.Excel;
using YCCodeChallenge.Repository;
using YCCodeChallenge.Services;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy
    (
        builder =>
        {
            builder.WithOrigins("http://localhost:3000")
                    .SetIsOriginAllowedToAllowWildcardSubdomains()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
        }
    );
});

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
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors();

app.MapControllers();

app.Run();
