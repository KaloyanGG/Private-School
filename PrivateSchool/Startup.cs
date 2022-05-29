using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using PrivateSchool.Data;
using PrivateSchool.Entities;
using PrivateSchool.Middlewares;
using PrivateSchool.Repositories;
using PrivateSchool.Repositories.Interfaces;
using PrivateSchool.Services;
using PrivateSchool.Services.Interfaces;
using System.Text;

namespace PrivateSchool
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrivateSchoolDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("SchoolDB"));

            });

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.SignIn.RequireConfirmedEmail = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequiredLength = 3;
                options.SignIn.RequireConfirmedEmail = false;
            }).AddEntityFrameworkStores<PrivateSchoolDBContext>();

            // Configure JWT settings object
            IConfigurationSection JWTSection = Configuration.GetSection("JWT");
            services.Configure<JWTSettings>(JWTSection);

            // Configure JWT Authentication
            JWTSettings jwtSettings = JWTSection.Get<JWTSettings>();
            byte[] key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddControllers();

            services.AddAutoMapper(typeof(MappingConfiguration));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PrivateSchool", Version = "v1" });
            });

            services.AddScoped<ISubjectRepository,SubjectRepository>();

            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ISubjectService, SubjectService>();

            


            /*
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", policy =>
                {
                    policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials();
                });
            });
            */
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PrivateSchool v1"));
            }

            app.UseAuthentication();
            app.UseRoleMiddleWare();
            app.UseCors("AllowAll");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
