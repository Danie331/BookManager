using AutoMapper;
using ConfigurationDataProvider.Contract;
using DomainModels;
using Newtonsoft.Json;
using Persistence.Contract;
using Persistence.Entities;

namespace Persistence
{
    public class BookDataService : IBookDataService
    {
        private readonly IBookDataStoreConfiguration _configService;
        private readonly IMapper _mapper;

        public BookDataService(IMapper mapper, IBookDataStoreConfiguration configService)
        {
            _mapper = mapper;
            _configService = configService;
        }

        public async Task AddAsync(Book book)
        {
            using var resource = new SemaphoreSlim(1, 1);
            await resource.WaitAsync();

            try
            {
                List<BookEntity>? entities = null;
                var bookEntity = _mapper.Map<BookEntity>(book);
                using (var reader = new StreamReader(_configService.DataSourceFilePath,
                                                    new FileStreamOptions { Access = FileAccess.Read, Mode = FileMode.OpenOrCreate, Share = FileShare.None }))
                {
                    var fileContents = await reader.ReadToEndAsync();
                    entities = JsonConvert.DeserializeObject<List<BookEntity>>(fileContents);
                    if (entities is null)
                    {
                        entities = new List<BookEntity>();
                    }

                    entities.Add(bookEntity);
                }

                await File.WriteAllTextAsync(_configService.DataSourceFilePath, JsonConvert.SerializeObject(entities));
            }
            finally
            {
                resource.Release();
            }
        }

        public async Task<List<Book>> AllAsync()
        {
            using var reader = new StreamReader(_configService.DataSourceFilePath, 
                                                new FileStreamOptions { Mode = FileMode.Open, Access = FileAccess.Read, Share = FileShare.None });
            var fileContents = await reader.ReadToEndAsync();
            var entities = JsonConvert.DeserializeObject<List<BookEntity>>(fileContents);

            var list = _mapper.Map<List<Book>>(entities);
            return list.OrderBy(x => x.Id).ToList(); // NB.: order important for pagination
        }

        public async Task DeleteAsync(Guid id)
        {
            using var resource = new SemaphoreSlim(1, 1);
            await resource.WaitAsync();

            try
            {
                List<BookEntity>? entities = null;
                using (var reader = new StreamReader(_configService.DataSourceFilePath,
                                                    new FileStreamOptions { Access = FileAccess.Read, Mode = FileMode.OpenOrCreate, Share = FileShare.None }))
                {
                    var fileContents = await reader.ReadToEndAsync();
                    entities = JsonConvert.DeserializeObject<List<BookEntity>>(fileContents);
                    if (entities is null)
                    {
                        entities = new List<BookEntity>();
                    }

                    var target = entities.FirstOrDefault(e => Equals(e.Id, id));
                    if (target is not null)
                    {
                        entities.Remove(target);
                    }
                }

                await File.WriteAllTextAsync(_configService.DataSourceFilePath, JsonConvert.SerializeObject(entities));
            }
            finally
            {
                resource.Release();
            }
        }

        public async Task<Book?> GetByIdAsync(Guid id) => (await AllAsync()).FirstOrDefault(b => Equals(b.Id, id));

        public async Task UpdateAsync(Guid id, Book book)
        {
            using var resource = new SemaphoreSlim(1, 1);
            await resource.WaitAsync();
            // This is not technically an update but serves the same purpose for sake of this exercise and to save some time
            try
            {
                await DeleteAsync(id);
                await AddAsync(book);
            }
            finally
            {
                resource.Release();
            }
        }
    }
}
