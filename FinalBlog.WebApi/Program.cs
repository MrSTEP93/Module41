using AutoMapper;
using FinalBlog.Data;
using FinalBlog.Data.Models;
using FinalBlog.Data.Repositories;
using FinalBlog.Data.Repositories.Interfaces;
using FinalBlog.Services.Interfaces;
using FinalBlog.Services;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinalBlog.WebApi
{
    /// <summary>
    /// ���������� Web Api
    /// </summary>
    public class Program
    {
        /// <summary>
        /// ����� �����
        /// </summary>
        /// <param name="args"></param>
        /// <exception cref="Exception"></exception>
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();
            //builder.Services.AddControllersWithViews();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(opt =>
            {
                var xmlFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFileName));
                opt.SupportNonNullableReferenceTypes();
            });
            /*
                var xml = $"{Assembly.GetAssembly(typeof(UserApiModel)).GetName().Name}.xml";
                                                        // MyBlog.Services.ApiModels.Users.Response.UserApiModel
                opt.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xml));
            */

            builder.Configuration
                .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "bin\\Debug\\net8.0", "appsettings.json"))
                .AddJsonFile(Path.Combine(Environment.CurrentDirectory, "bin\\Debug\\net8.0", "appsettings.Development.json"));
            string connection = builder.Configuration.GetConnectionString("DefaultConnection") ?? string.Empty;

            if (string.IsNullOrEmpty(connection))
                throw new Exception("Can't get connection string from the file \"appsettings.Development.json\"");
            
            builder.Services
                .AddDbContext<AppDbContext>(opt => opt.UseSqlServer(connection))
                .AddIdentity<BlogUser, Role>(opts => {
                    opts.Password.RequiredLength = 3;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = false;
                    opts.Password.RequireUppercase = false;
                    opts.Password.RequireDigit = false;
                })
                .AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();

            var mapperConfig = new MapperConfiguration((c) =>
            {
                c.AddProfile(new MappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            builder.Services
                .AddSingleton(mapper)
                .AddTransient<IUserService, UserService>()
                .AddTransient<IRoleService, RoleService>()
                .AddTransient<IArticleService, ArticleService>()
                .AddTransient<ICommentService, CommentService>()
                .AddTransient<ITagService, TagService>();

            builder.Services
                .AddUnitOfWork()
                .AddCustomRepository<Comment, CommentRepository>()
                .AddCustomRepository<Role, RoleRepository>()
                .AddCustomRepository<BlogUser, UserRepository>();

            builder.Services
               .AddTransient<IArticleRepository, ArticleRepository>()
               .AddTransient<ITagRepository, TagRepository>();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
