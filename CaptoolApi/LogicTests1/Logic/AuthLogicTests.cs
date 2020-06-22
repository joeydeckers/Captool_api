using Logic.Logic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.InMemory;
using Microsoft.EntityFrameworkCore;
using Data;
using ModelLayer.Models;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using Logic.Repositories;
using Helper;
using Microsoft.Extensions.Options;
using Interfaces.UserInterfaces;
using ModelLayer.ViewModels;
using System.Threading.Tasks;
using System.Web.Helpers;

namespace Logic.Logic.Tests
{
    [TestClass()]
    public class AuthLogicTests
    {
        IOptions<AppSettings> appsettings;
        IUserRepos userrepos;


        [TestMethod()]
        public async Task GenerateJWTTest_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserDatabase")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_user.Add(new User { Id = 1, Email = "Jeremeymain@gmail.com", Name = "jeremey", Password = "Password" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                userrepos = new UserRepos(context);
                AppSettings inputappsetings = new AppSettings();
                inputappsetings.Secret = "thisisasecret";
                inputappsetings.Url = "url";

                appsettings = Options.Create(inputappsetings);
                AuthLogic authlogic = new AuthLogic(userrepos, appsettings);
                LoginViewModel loginView = new LoginViewModel();
                loginView.Email = "trash";
                loginView.Password = Crypto.HashPassword("trash");
                User user;

                user = await authlogic.GenerateJWT(loginView);

                Assert.IsNull(user.Email);
            }
        }

        [TestMethod()]
        public async Task GenerateJWTTest_ReturnUser()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: "UserDatabase")
            .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_user.Add(new User { Id = 1, Email = "Jeremeymain@gmail.com", Name = "jeremey", Password = "Password" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                userrepos = new UserRepos(context);
                AppSettings inputappsetings = new AppSettings();
                inputappsetings.Secret = "thisisasecret";
                inputappsetings.Url = "url";

                appsettings = Options.Create(inputappsetings);
                AuthLogic authlogic = new AuthLogic(userrepos, appsettings);
                LoginViewModel loginView = new LoginViewModel();
                User user;

                user = await authlogic.GenerateJWT(loginView);

                Assert.IsNull(user.Email);
            }
        }

        [TestMethod()]
        public void AuthenticateUserTest_returnUser()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_user.Add(new User { Id = 1, Email = "Jeremeymain@gmail.com", Name = "jeremey", Password = "Password" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {

            }
        }

        public void AuthenticateUserTest_returnNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_user.Add(new User { Id = 1, Email = "Jeremeymain@gmail.com", Name = "jeremey", Password = "Password" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {

            }
        }
    }
}