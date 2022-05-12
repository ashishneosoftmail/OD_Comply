using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using OD_Comply.Core.Entities;
using OD_Comply.Infrastructure;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSingleton<JwtSettings>(
    new JwtSettings
    {
        Key = builder.Configuration["JwtSettings:Key"],
        Issuer = builder.Configuration["JwtSettings:Issuer"],
        Audience = builder.Configuration["JwtSettings:Audience"],
        DurationInMinutes = double.Parse(builder.Configuration["JwtSettings:DurationInMinutes"])
    });

builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("V1", new OpenApiInfo { Title = "OD Comply API", Version = "V1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.AddInfrastructure();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
              .AddJwtBearer(o =>
              {
                  o.RequireHttpsMetadata = false;
                  o.SaveToken = false;
                  o.TokenValidationParameters = new TokenValidationParameters
                  {
                      ValidateIssuerSigningKey = true,
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ClockSkew = TimeSpan.Zero,
                      ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
                      ValidAudience = builder.Configuration["JwtSettings:Audience"],
                      IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"]))
                  };

                  o.Events = new JwtBearerEvents()
                  {
                      OnAuthenticationFailed = context =>
                      {
                          if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                          {
                              context.Response.Headers.Add("Token-Expired", "true");
                          }
                          return Task.CompletedTask;
                      },
                    
                      OnForbidden = context =>
                      {
                          context.Response.StatusCode = 403;
                          context.Response.ContentType = "application/json";
                          var result = JsonConvert.SerializeObject("403 Not authorized");
                          return context.Response.WriteAsync(result);
                      },
                  };
              });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("V1/swagger.json", "OD Comply API V1");
    });
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseStaticFiles();

// global cors policy
app.UseCors(x => x
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();

app.Run();
