using System.Net;
using MentalClinic.API.Models.Domain;
using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories;

public class ServiceRepository : BaseRepository
{
    public ServiceRepository(Container container) : base(container) { }


    public async Task<IEnumerable<Service>> GetAll()
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM C");

        return await ExecuteQuery<Service>(query);
    }

    public async Task<Service> Get(string id)
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", id);

        return (await ExecuteQuery<Service>(query)).FirstOrDefault();
    }

    public async Task<HttpStatusCode> Create(Service service)
    {
        return (await Create(service, new PartitionKey(service.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Update(Service service)
    {
        return (await Update(service, new PartitionKey(service.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Delete(string id)
    {
        return (await Delete<Service>(id, new PartitionKey(id))).StatusCode;
    }
}
