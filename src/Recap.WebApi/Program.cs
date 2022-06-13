using Autofac;
using Autofac.Extensions.DependencyInjection; 
using Microsoft.AspNetCore.Authentication.JwtBearer; 
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Recap.Business;
using Recap.Core;
using Recap.Core.Aspects.Secured.Encryption;
using Recap.Core.Aspects.Secured.Jwt;
using Recap.Core.Entities.Concrete;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



#region Core Option
var configurationErrorLoggerDbConString = builder.Configuration.GetSection("ConfigurationErrorLoggerDbConString").Get<string>();
var configurationEmailModelJson = builder.Configuration.GetSection("ConfigurationEmailModel").Get<EmailModel>();


builder.Services.AddDependencyResolversWithEmailErrorLogger(new IServiceRegisterationMicrosoftDEPI[] { new ServiceRegisterationMicrosoftDEPI() }, JsonConvert.SerializeObject(configurationEmailModelJson), configurationErrorLoggerDbConString);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{ builder.RegisterModule(new ServiceRegisterationCore()); });

var tokenOptions = builder.Configuration.GetSection("TokenOptionsModel").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };
});
#endregion




#region Business Options
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory()).ConfigureContainer<ContainerBuilder>(builder =>
{ builder.RegisterModule(new ServiceRegisterationErpBusiness()); });
#endregion


 



var app = builder.Build();



app.UseCustomExceptionExtension(); //Core Option




if (app.Environment.IsDevelopment())
{
app.UseSwagger();
app.UseSwaggerUI();
app.UseDeveloperExceptionPage(); 
app.UseRouting();
}

app.UseAuthentication();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();