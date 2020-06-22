using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data;
using ModelLayer.Models;
using System.Threading.Tasks;

namespace Logic.Repositories.Tests
{
    [TestClass()]
    public class UserReposTests
    {
        [TestMethod()]
        public async Task AddTest_Users_AreEqual()
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
                UserRepos userrepos = new UserRepos(context);
                User user = new User();
                User getuser;
                user.Id = 2;
                user.Email = "Jeremeymain2@gmail.com";
                user.Name = "jeremey2";
                user.Password = "password2";
                await userrepos.Add(user);
                getuser = await userrepos.GetAsync(2);

                Assert.AreEqual(user, getuser);
            }
        }

        [TestMethod()]
        public async Task AddTestUsers_AreNotEqual()
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
                UserRepos userrepos = new UserRepos(context);
                User user = new User();
                User getuser;
                user.Id = 2;
                user.Email = "Jeremeymain2@gmail.com";
                user.Name = "jeremey2";
                user.Password = "password2";
                await userrepos.Add(user);
                getuser = await userrepos.GetAsync(1);

                Assert.AreNotEqual(user, getuser);
            }
        }

        [TestMethod()]
        public async Task DeleteTest_Users_ReturnNull()
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
                UserRepos userrepos = new UserRepos(context);
                await userrepos.Delete(1);
                User getuser;
                getuser = await userrepos.GetAsync(1);
                Assert.IsNull(getuser);
            }
        }

        [TestMethod()]
        public async Task UpdateAsyncTest_Users_ReturnEqual()
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
                UserRepos userrepos = new UserRepos(context);
                User user = new User();
                User updateuser;
                user.Id = 1;
                user.Email = "Jeremeymain2@gmail.com";
                user.Name = "jeremey2";
                user.Password = "password2";
                await userrepos.UpdateAsync(user);
                updateuser = await userrepos.GetAsync(1);
                Assert.AreEqual(user.Email, updateuser.Email);

            }
        }
    }
}