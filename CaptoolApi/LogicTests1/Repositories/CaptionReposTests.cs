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
    public class CaptionReposTests
    {
        [TestMethod()]
        public async Task getCaptionsAsyncTest_ReturnCaption()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "String" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                CaptionRepos captionrepos = new CaptionRepos(context);
                CaptionFile capget;
                capget = await captionrepos.getCaptionsAsync("1");
                Assert.AreEqual(capget.Data, "String");
                
            }
        }
        [TestMethod()]
        public async Task getCaptionsAsyncTest_ReturnNull()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "String" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                CaptionRepos captionrepos = new CaptionRepos(context);
                CaptionFile capget;
                capget = await captionrepos.getCaptionsAsync("2");
                Assert.IsNull(capget);
            }
        }

        [TestMethod()]
        public async Task addCaptionAsyncTest_ReturnCaption()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "UserDatabase")
                .Options;

            using (var context = new AppDbContext(options))
            {
                context.ct_captions.Add(new CaptionFile { VideoID = "1", Data = "String" });
                context.SaveChanges();
            }

            using (var context = new AppDbContext(options))
            {
                CaptionRepos captionrepos = new CaptionRepos(context);
                CaptionFile captionfile = new CaptionFile();
                captionfile.VideoID = "2";
                captionfile.Data = "String2";
                await captionrepos.addCaptionAsync(captionfile);
                CaptionFile capget;
                capget = await captionrepos.getCaptionsAsync("2");
                Assert.AreEqual(captionfile.VideoID, capget.VideoID);
            }
        }
    }
}