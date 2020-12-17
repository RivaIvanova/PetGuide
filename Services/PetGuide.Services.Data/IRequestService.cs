namespace PetGuide.Services.Data
{
    using System.Threading.Tasks;

    public interface IRequestService
    {
        Task AddAsync(string id, string volunteerId);

        bool IsRequestSent(string id, string volunteerId);
    }
}
