using System.Net;
using MentalClinic.API.Models.Domain;
using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories
{
    public class AppointmentRepository : BaseRepository
    {
        public AppointmentRepository(Container container) : base(container) { }

        public async Task<IEnumerable<Appointment>> GetAll()
        {
            QueryDefinition query = new QueryDefinition("SELECT * FROM C");

            return await ExecuteQuery<Appointment>(query);
        }

        public async Task<Appointment> Get(string id)
        {
            QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.id = @id").WithParameter("@id", id);

            return (await ExecuteQuery<Appointment>(query)).FirstOrDefault();
        }

        public async Task<HttpStatusCode> Create(Appointment employee)
        {
            return (await Create(employee, new PartitionKey(employee.id))).StatusCode;
        }

        public async Task<HttpStatusCode> Update(Appointment employee)
        {
            return (await Update(employee, new PartitionKey(employee.id))).StatusCode;
        }

        public async Task<HttpStatusCode> Delete(string id)
        {
            return (await Delete<Appointment>(id, new PartitionKey(id))).StatusCode;
        }
    }
}
