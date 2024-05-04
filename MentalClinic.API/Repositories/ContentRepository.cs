using System.Net;
using MentalClinic.API.Models.Domain;
using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories;

public class ContentRepository : BaseRepository
{
    public ContentRepository(Container container) : base(container) { }

    public async Task<IEnumerable<Content>> GetAll()
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM C");

        return await ExecuteQuery<Content>(query);
    }

    public async Task<Content> Get(string id)
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", id);

        return (await ExecuteQuery<Content>(query)).FirstOrDefault();
    }

    public async Task<HttpStatusCode> Create(Content content)
    {
        return (await Create(content, new PartitionKey(content.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Update(Content content)
    {
        return (await Update(content, new PartitionKey(content.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Delete(string id)
    {
        return (await Delete<Content>(id, new PartitionKey(id))).StatusCode;
    }
}
