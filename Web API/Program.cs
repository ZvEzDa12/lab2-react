
using System.Text.Json.Serialization;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Global;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services
.AddDbContext<AppDbContext>(ServiceLifetime.Transient)
.AddScoped<IColorService,ColorService>()
.AddTransient<IColorRepository, ColorRepository>()
.AddScoped<IFuelTypeService,FuelTypeService>()
.AddTransient<IFuelTypeRepository, FuelTypeRepository>()
.AddScoped<IInsuranceService,InsuranceService>()
.AddTransient<IInsuranceRepository, InsuranceRepository>()
.AddScoped<IMaintenanceService,MaintenanceService>()
.AddTransient<IMaintenanceRepository, MaintenanceRepository>()
.AddScoped<IMaintenanceTypeService,MaintenanceTypeService>()
.AddTransient<IMaintenanceTypeRepository, MaintenanceTypeRepository>()
.AddScoped<IManufacturerService,ManufacturerService>()
.AddTransient<IManufacturerRepository, ManufacturerRepository>()
.AddScoped<IOwnershipHistoryService,OwnershipHistoryService>()
.AddTransient<IOwnershipHistoryRepository, OwnershipHistoryRepository>()
.AddScoped<IRoleService,RoleService>()
.AddTransient<IRoleRepository, RoleRepository>()
.AddScoped<IUserService,UserService>()
.AddTransient<IUserRepository, UserRepository>()
.AddScoped<IVehicleService,VehicleService>()
.AddTransient<IVehicleRepository, VehicleRepository>()
.AddScoped<IVehicleCategoryService,VehicleCategoryService>()
.AddTransient<IVehicleCategoryRepository, VehicleCategoryRepository>()
.AddScoped<IVehicleModelService,VehicleModelService>()
.AddTransient<IVehicleModelRepository, VehicleModelRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = AppConfig.ISSUER,
            ValidAudience = AppConfig.AUDIENCE,
            ValidateLifetime = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppConfig.KEY)),
            ValidateIssuerSigningKey = true
        };
});
builder.Services.AddLogging(builder =>
{
    builder.SetMinimumLevel(LogLevel.Trace);
});
builder.Services.AddAuthorization();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://127.0.0.1:5500", "http://localhost:5500", "http://localhost:8080")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});

builder.Services.ConfigureHttpJsonOptions(opts =>{
    opts.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();