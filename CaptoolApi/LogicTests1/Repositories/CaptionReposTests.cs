using Microsoft.VisualStudio.TestTools.UnitTesting;
using Logic.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Data;
using ModelLayer.Models;

namespace Logic.Repositories.Tests
{
    [TestClass()]
    public class CaptionReposTests
    {
        [TestMethod()]
        public void getCaptionsAsyncTest_ReturnCaption()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "dsfdsfdsfdsfds" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {

            }
        }

        public void getCaptionsAsyncTest_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "dsfdsfdsfdsfds" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {

            }
        }

        [TestMethod()]
        public void addCaptionAsyncTest_ReturnCaption()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "dsfdsfdsfdsfds" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {

            }
        }

        [TestMethod()]
        public void addCaptionAsyncTest_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                 .UseInMemoryDatabase(databaseName: "UserDatabase")
                 .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "dsfdsfdsfdsfds" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {

            }
        }
    }
}