using System.Net;
using MentalClinic.API.Models.Domain;
using Microsoft.Azure.Cosmos;

namespace MentalClinic.API.Repositories;

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

    public async Task<List<Appointment>> GetBySpecialistId(string specialistId)
    {
        QueryDefinition query = new QueryDefinition("SELECT * FROM c WHERE c.SpecialistSelect = @specialistId").WithParameter("@specialistId", specialistId);

        return await ExecuteQuery<Appointment>(query);
    }

    public async Task<HttpStatusCode> Create(Appointment appointment)
    {
        return (await Create(appointment, new PartitionKey(appointment.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Update(Appointment appointment)
    {
        return (await Update(appointment, new PartitionKey(appointment.id))).StatusCode;
    }

    public async Task<HttpStatusCode> Delete(string id)
    {
        return (await Delete<Appointment>(id, new PartitionKey(id))).StatusCode;
    }
}
