using ConfigurationDataProvider.Contract;

namespace ConfigurationDataProvider
{
    public class BookDataStoreConfiguration : IBookDataStoreConfiguration
    {
        private readonly string _dataSourceFilePath;
        public BookDataStoreConfiguration(string dataSourceFilePath)
        {
            _dataSourceFilePath = dataSourceFilePath;
        }

        public string DataSourceFilePath => _dataSourceFilePath;
    }
}