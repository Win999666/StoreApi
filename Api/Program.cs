
//using Api.Extension;

//var builder = WebApplication.CreateBuilder(args);
//builder.Services.AddControllers();
//builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();
//builder.Services.AddPostgreSqlDbContext(builder.Configuration);
//builder.Services.AddPostgreSqlIdentityContext();
//builder.Services.AddConfigureIdentityOptions();
//builder.Services.AddJwtTokenGenerator();
//var app = builder.Build();
//app.MapControllers();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseHttpsRedirection();
//await app.Services.InicializeRoleAsync();
//app.Run();
using Api.Extension;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddPostgreSqlDbContext(builder.Configuration);
builder.Services.AddPostgreSqlIdentityContext();
builder.Services.AddConfigureIdentityOptions();
builder.Services.AddJwtTokenGenerator();

var app = builder.Build();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

await app.Services.InicializeRoleAsync();

app.Run();

