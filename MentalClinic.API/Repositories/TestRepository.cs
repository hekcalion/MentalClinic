using System.Net;
using MentalClinic.API.Models.Domain;
using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories;

public class TestRepository : BaseRepository
{
    public TestRepository(Container container) : base(container) { }

    public async Task<IEnumerable<Test>> GetAll()
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM C");

        return await ExecuteQuery<Test>(query);
    }

    public async Task<Test> Get(string id)
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", id);

        return (await ExecuteQuery<Test>(query)).FirstOrDefault();
    }

    public async Task<HttpStatusCode> Create(Test test)
    {
        return (await Create(test, new PartitionKey(test.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Update(Test test)
    {
        return (await Update(test, new PartitionKey(test.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Delete(string id)
    {
        return (await Delete<Test>(id, new PartitionKey(id))).StatusCode;
    }
}
