using BookManager.CQRS.Commands.UseCases;
using BookManager.CQRS.Queries.UseCases;
using BookManager.Validators;
using ConfigurationDataProvider;
using ConfigurationDataProvider.Contract;
using DomainModels;
using FluentValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Tests
{
    [TestFixture]
    public class ValidationTests
    {
        private IServiceProvider? _serviceProvider = null;
        [SetUp]
        public void Setup()
        {
            var builder = WebApplication.CreateBuilder(new WebApplicationOptions() { });
            builder.Services.AddControllers();
            builder.Services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            builder.Services.AddMediatR(cf => cf.RegisterServicesFromAssembly(typeof(BookManager.Middleware.ExceptionHandler).Assembly));
            builder.Services.AddAutoMapper(new Assembly[] { Assembly.GetAssembly(typeof(BookManager.Middleware.ExceptionHandler)) });
            builder.Services.AddExceptionHandler<BookManager.Middleware.ExceptionHandler>();
            builder.Services.AddProblemDetails();
            BookManager.ServiceRegistration.Register(builder.Services);

            builder.Services.AddTransient<IValidator<AddBookCommand>, AddBookValidator>();
            builder.Services.AddTransient<IValidator<UpdateBookCommand>, UpdateBookValidator>();
            builder.Services.AddTransient<IValidator<DeleteBookCommand>, DeleteBookValidator>();
            builder.Services.AddTransient<IValidator<GetBookQuery>, GetBookValidator>();

            // Remove and reconfigure the data source destination for the testing context
            var configDescriptor = builder.Services.First(s => s.ServiceType == typeof(IBookDataStoreConfiguration));
            builder.Services.Remove(configDescriptor);
            builder.Services.AddSingleton<IBookDataStoreConfiguration>(sp =>
            {
                // Create temp file path for storage, hopefully these temp files will get cleaned up quickly
                var tmpFile = Path.Combine(Path.GetTempPath(), Path.GetTempFileName());
                return new BookDataStoreConfiguration(tmpFile);
            });

            _serviceProvider = builder.Services.BuildServiceProvider();

            if (_serviceProvider is null)
            {
                throw new Exception("Service provider is null");
            }
        }

        [Test]
        public async Task Test_Add_Book_Required_Fields_Invalid()
        {
            var validator = _serviceProvider.GetService<IValidator<AddBookCommand>>();
            var command = new AddBookCommand(new Book { Author = "", Isbn = null, Title = "", PublishedDate = new DateTime(1961, 5, 18) });

            var result = await validator.ValidateAsync(command);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(3));
            Assert.That(result.Errors.All(e => e.ErrorCode.Equals("400")));
        }

        [Test]
        public async Task Test_Update_Book_Not_Found_Produces_404()
        {
            var validator = _serviceProvider.GetService<IValidator<UpdateBookCommand>>();
            var id = Guid.NewGuid();
            var command = new UpdateBookCommand(id, new Book { Id = id, Author = "some author", Isbn = "some isbn", Title = "some title", PublishedDate = new DateTime(1961, 5, 18) });

            var result = await validator.ValidateAsync(command);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors.Count(e => e.ErrorCode.Equals("404")), Is.EqualTo(1));
        }

        [Test]
        public async Task Test_Update_Book_Required_Fields_Invalid()
        {
            var validator = _serviceProvider.GetService<IValidator<UpdateBookCommand>>();
            var id = Guid.NewGuid();
            var command = new UpdateBookCommand(id, new Book { Id = id, Author = "", Isbn = null, Title = "", PublishedDate = new DateTime(1961, 5, 18) });

            var result = await validator.ValidateAsync(command);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(4));
            Assert.That(result.Errors.Count(e => e.ErrorCode.Equals("400")), Is.EqualTo(3));
            Assert.That(result.Errors.Count(e => e.ErrorCode.Equals("404")), Is.EqualTo(1));
        }

        [Test]
        public async Task Test_Add_Book_PublishedDate_Invalid_400()
        {
            var validator = _serviceProvider.GetService<IValidator<AddBookCommand>>();
            var command = new AddBookCommand(new Book { Author = "some author", Isbn = "some isbn", Title = "some title", PublishedDate = new DateTime(2050, 5, 18) });

            var result = await validator.ValidateAsync(command);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors.All(e => e.ErrorCode.Equals("400")));
        }

        [Test]
        public async Task Test_Get_Invalid_Book_ID_Produces_NOt_Found_404()
        {
            var validator = _serviceProvider.GetService<IValidator<GetBookQuery>>();
            var query = new GetBookQuery(Guid.NewGuid());

            var result = await validator.ValidateAsync(query);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors.All(e => e.ErrorCode.Equals("404")));
        }

        [Test]
        public async Task Test_Delete_Invalid_Book_ID_Produces_Not_Found_404()
        {
            var validator = _serviceProvider.GetService<IValidator<DeleteBookCommand>>();
            var command = new DeleteBookCommand(Guid.NewGuid());

            var result = await validator.ValidateAsync(command);

            Assert.IsFalse(result.IsValid);
            Assert.That(result.Errors.Count, Is.EqualTo(1));
            Assert.That(result.Errors.All(e => e.ErrorCode.Equals("404")));
        }
    }
}