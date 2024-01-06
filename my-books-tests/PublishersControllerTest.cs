using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using my_books;
using my_books.Controller;
using my_books.Data.Models;
using NuGet.ContentModel;

namespace my_books_tests;

public class PublishersControllerTest
{
    private static DbContextOptions<AppDbContext> dbContextOptions= new DbContextOptionsBuilder<AppDbContext>()
    .UseInMemoryDatabase(databaseName : "BookDbControllerTest")
    .Options;

    AppDbContext context;

    PublishersService publishersService;
    PublishersController publishersController;

    [OneTimeSetUp]
    public void Setup()
    {
        context = new AppDbContext(dbContextOptions);
        context.Database.EnsureCreated();

        seedDatabase();

        publishersService = new PublishersService(context);
        publishersController = new PublishersController(publishersService,new NullLogger<PublishersController>());

    }

    [Test,Order(1)]
    public void HTTPGETGetAllPublishers_ResultOk_Test()
    {
        IActionResult actionResult = publishersController.GetAllPublishers("name_desc", 1);
        Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
        var actionResultData = (actionResult as OkObjectResult).Value as List<Publisher>;
        Assert.That(actionResultData.First().Name, Is.EqualTo("Publisher 6"));
    }

    // [Test,Order(2)]
    // public void HTTPGETPublishersByID_ResultOk_Test()
    // {
    //     int pubid=1;
    //     IActionResult actionResult = publishersController.GetPublisherById(pubid);
    //     Assert.That(actionResult, Is.TypeOf<OkObjectResult>());
    //     var actionResultData = (actionResult as OkObjectResult).Value as Publisher;
    //     Assert.That(actionResultData.Name, Is.EqualTo("Publisher 1"));
    // }

    [Test, Order(2)]
        public void HTTPGET_GetPublisherById_ReturnsOk_Test()
        {
            int publisherId = 1;

            IActionResult actionResult = publishersController.GetPublisherById(publisherId);

            Assert.That(actionResult, Is.TypeOf<OkObjectResult>());

            var publisherData = (actionResult as OkObjectResult).Value as Publisher;
            Assert.That(publisherData.Id, Is.EqualTo(1));
            Assert.That(publisherData.Name, Is.EqualTo("publisher 1").IgnoreCase);
        }

        [Test, Order(3)]
        public void HTTPGET_GetPublisherById_ReturnsNotFound_Test()
        {
            int publisherId = 99;

            IActionResult actionResult = publishersController.GetPublisherById(publisherId);

            Assert.That(actionResult, Is.TypeOf<NotFoundResult>());

        }

        [Test, Order(4)]
        public void HTTPPOST_AddPublisher_ReturnsCreated_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "New Publisher"
            };

            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<CreatedResult>());
        }

        [Test, Order(5)]
        public void HTTPPOST_AddPublisher_ReturnsBadRequest_Test()
        {
            var newPublisherVM = new PublisherVM()
            {
                Name = "123 New Publisher"
            };

            IActionResult actionResult = publishersController.AddPublisher(newPublisherVM);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }

        [Test, Order(6)]
        public void HTTPDELETE_DeletePublisherById_ReturnsOk_Test()
        {
            int publisherId = 6;

            IActionResult actionResult = publishersController.DeletePublisherById(publisherId);

            Assert.That(actionResult, Is.TypeOf<OkResult>());

        }

        [Test, Order(7)]
        public void HTTPDELETE_DeletePublisherById_ReturnsBadRequest_Test()
        {
            int publisherId = 6;

            IActionResult actionResult = publishersController.DeletePublisherById(publisherId);

            Assert.That(actionResult, Is.TypeOf<BadRequestObjectResult>());
        }


    [OneTimeTearDown]
    public void CleanUp(){
        context.Database.EnsureDeleted();
    }

    public void seedDatabase(){
        var publishers = new List<Publisher>
            {
                    new Publisher() {
                        Id = 1,
                        Name = "Publisher 1"
                    },
                    new Publisher() {
                        Id = 2,
                        Name = "Publisher 2"
                    },
                    new Publisher() {
                        Id = 3,
                        Name = "Publisher 3"
                    },
                    new Publisher() {
                        Id = 4,
                        Name = "Publisher 4"
                    },
                    new Publisher() {
                        Id = 5,
                        Name = "Publisher 5"
                    },
                    new Publisher() {
                        Id = 6,
                        Name = "Publisher 6"
                    },
            };
            context.Publishers.AddRange(publishers);

            context.SaveChanges();
        }
     
}
