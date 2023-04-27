using CloudCustomer.API.Config;
using CloudCustomer.API.Models;
using Microsoft.Extensions.Options;

namespace CloudCustomer.API.Services
{
    public interface IUsersService
    {
        public Task<List<User>> GetAllUsers();
    }
    public class UsersService : IUsersService
    {
        private readonly HttpClient _httpClient;
        private readonly UsersApiOptions _apiConfig;

        public UsersService(HttpClient httpClient,IOptions<UsersApiOptions> apiConfig)
        {
            _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
            _apiConfig = apiConfig.Value ?? throw new ArgumentNullException(nameof(apiConfig));
        }

        public async Task<List<User>> GetAllUsers()
        {
            var usersResponse = await _httpClient.GetAsync(_apiConfig.Endpoint);
            if(usersResponse.StatusCode == System.Net.HttpStatusCode.NotFound)
                return new List<User>();

            var responseContent = usersResponse.Content;
            var allUsers = await responseContent.ReadFromJsonAsync<List<User>>();
            return allUsers.ToList();
        }
    }
}
