using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly Container _container;

        protected BaseRepository(Container container)
        {
            _container = container;
        }

        protected async Task<ItemResponse<T>> Create<T>(T document, PartitionKey partitionKey)
        {
            return await _container.CreateItemAsync(document, partitionKey);
        }

        protected async Task<ItemResponse<T>> Update<T> (T document, PartitionKey partitionKey)
        {
            return await _container.UpsertItemAsync(document, partitionKey);
        }

        protected async Task<ItemResponse<T>> Delete<T>(string id, PartitionKey partitionKey)
        {
            return await _container.DeleteItemAsync<T>(id, partitionKey);
        }

        protected async Task<List<T>> ExecuteQuery<T>(QueryDefinition query)
        {
            using FeedIterator<T> feedIterator = _container.GetItemQueryIterator<T>(query);

            List<T> documents = new List<T>();

            while (feedIterator.HasMoreResults)
            {
                FeedResponse<T> response = await feedIterator.ReadNextAsync();

                foreach(T document in response)
                {
                    documents.Add(document);
                }
            }

            return documents;
        }
    }
}
