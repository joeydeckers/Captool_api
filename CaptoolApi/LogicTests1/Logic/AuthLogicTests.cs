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

namespace Logic.Logic.Tests
{
    [TestClass()]
    public class AuthLogicTests
    {
        IOptions<AppSettings> appsettings;
        IUserRepos userrepos; 


        [TestMethod()]
        public void GenerateJWTTest_ReturnUser()
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
                AuthLogic authlogic = new AuthLogic(userrepos, appsettings);
                
            }
        }

        public void GenerateJWTTest_ReturnNull()
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
                /*
                Configuration config
                AuthLogic authlogic = new AuthLogic()
                */
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