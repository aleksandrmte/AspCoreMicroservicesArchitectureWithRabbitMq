using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Web.Interfaces;
using Web.Models.Dto;

namespace Web.Services
{
    public class UserService: IUserService
    {
        private readonly HttpClient _apiClient;
        public UserService(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task Create(UserDto userDto)
        {
            var uri = "https://localhost:5001/api/Management";
            var content = new StringContent(
                JsonConvert.SerializeObject(userDto),
                System.Text.Encoding.UTF8,
                "application/json"
            );
            var response = await _apiClient.PostAsync(uri, content);
            response.EnsureSuccessStatusCode();
        }
    }
}
