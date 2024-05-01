using System.Net;
using MentalClinic.API.Models.Domain;
using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories;

public class EmployeeRepository : BaseRepository
{
    public EmployeeRepository(Container container) : base(container) { }


    public async Task<IEnumerable<Employee>> GetAll()
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM C");

        return await ExecuteQuery<Employee>(query);
    }

    public async Task<Employee> Get(string id)
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", id);

        return (await ExecuteQuery<Employee>(query)).FirstOrDefault();
    }

    public async Task<HttpStatusCode> Create(Employee employee)
    {
        return (await Create(employee, new PartitionKey(employee.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Update(Employee employee)
    {
        return (await Update(employee, new PartitionKey(employee.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Delete(string id)
    {
        return (await Delete<Employee>(id, new PartitionKey(id))).StatusCode;
    }
}
