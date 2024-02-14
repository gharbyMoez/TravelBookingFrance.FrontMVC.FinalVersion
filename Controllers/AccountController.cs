using GraphQL.Client.Abstractions;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TravelBookingFrance.FrontMVC.Models;

namespace TravelBookingFrance.FrontMVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly HttpClient _client;

        public AccountController()
        {
            _client = new HttpClient();
            _client.BaseAddress = new Uri("http://localhost:5277/");
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        // Méthode pour récupérer les informations de profil de l'utilisateur
        /*   private async Task<UserInfo> GetUserProfile(int UserId)
           {
               var _client1 = new GraphQLHttpClient("http://localhost:5016/graphql", new NewtonsoftJsonSerializer());
               var query2 = @"
   query ($UserId: Int!) {
       userInformationById(userId: 1) {
           username,
           email,
           busPhone,
           prov,
           postal,
           country,
           city
       }
   }";

               var variables = new { UserId = 1 };
               var response = await _client1.SendQueryAsync<dynamic>(query2, variables);

               var userProfileData = response.Data.userInformationById;

               var userProfile = new UserInfo
               {
                   Username = userProfileData.username,
                   Email = userProfileData.email,
                   BusPhone = userProfileData.busPhone,
                   Prov = userProfileData.prov,
                   Postal = userProfileData.postal,
                   Country = userProfileData.country,
                   City = userProfileData.city
               };

               return userProfile;
           }*/
        public async Task<List<TravelInfo>> GetTravelListByUserId(int UserId)
        {

            var response = await _client.GetAsync($"api/Travel/GetAllTravelByCustomerId?CustomerId={UserId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse<List<TravelInfo>>>(content);

                if (result != null && result.Data != null)
                {
                    // Utilisez la liste de voyages ici selon vos besoins
                    var voyages = result.Data;
                    return voyages;
                }
                else
                {
                    // Gérer le cas où la liste de voyages est null ou vide
                    // Rediriger vers une page d'erreur ou afficher un message approprié
                    return null;
                }
            }
            else
            {
                // Gérer les erreurs de requête HTTP
                throw new HttpRequestException($"Erreur lors de la requête : {response.StatusCode}");
            }
        }
        public async Task<UserInfo> GetUserProfile(int UserId)
        {

            var response = await _client.GetAsync($"api/v1/User/getuser?userId={UserId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var userInfo = JsonConvert.DeserializeObject<UserInfo>(content);



                return userInfo;
            }
            else
            {
                throw new HttpRequestException($"Erreur lors de la requête : {response.StatusCode}");
            }
        }

        public async Task<UserProfileViewModel> UserWithListTravel(int id)
        {
            var userProfileTask = GetUserProfile(id);
            var travelListTask = GetTravelListByUserId(id);

            await Task.WhenAll(userProfileTask, travelListTask);

            if (userProfileTask.Result != null && travelListTask.Result != null)
            {
                var userProfile = userProfileTask.Result;
                var travelList = travelListTask.Result;

                var viewModel = new UserProfileViewModel
                {
                    UserProfile = userProfile,
                    TravelList = travelList,
                    UserId = id
                };

                return viewModel;
            }
            else
            {
                // Gérer les cas où l'utilisateur ou la liste de voyages est null
                return null;
            }
        }
        // Méthode Login qui appelle GetUserProfile avec le UserId
        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var _client2 = new GraphQLHttpClient("http://localhost:5016/graphql", new NewtonsoftJsonSerializer());

            var query = @"
    query ($username: String!, $password: String!) {
        login(username: $username, password: $password) {
            surname,
            userId,
            isAuthenticated
        }
    }";

            var variables = new { username, password };
            var response = await _client2.SendQueryAsync<dynamic>(query, variables);

            var userInfo = new LoginResponse
            {
                UserId = response.Data.login.userId,
                Username = username,
                Surname = response.Data.login.surname,
                IsAuthenticated = response.Data.login.isAuthenticated
            };

            if (userInfo.IsAuthenticated)
            {
                int id = userInfo.UserId;
                var userProfileViewModel = await UserWithListTravel(id);
                return View("Profile", userProfileViewModel);
            }
            else
            {
                // Gérer le cas où l'utilisateur n'est pas authentifié
                return View("LoginError");
            }
        }
        public IActionResult Logout()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest(); // Ou redirigez vers une page d'erreur appropriée
            }

            var travel = await GetTravelById(id.Value);

            if (travel == null)
            {
                return NotFound(); // Ou redirigez vers une page d'erreur appropriée
            }

            return View(travel);
        }

        // Action pour traiter les données soumises par le formulaire
        [HttpPost]
        public async Task<IActionResult> Details(int TravelId)
        {
            var travelList = await GetTravelById(TravelId);

            return View(travelList);
        }

        public async Task<TravelInfo> GetTravelById(int TravelId)
        {

            var response = await _client.GetAsync($"api/Travel/GetTravelById?TravelId={TravelId}");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<ApiResponse<TravelInfo>>(content);

                if (result != null && result.Data != null)
                {
                    // Utilisez la liste de voyages ici selon vos besoins
                    var voyages = result.Data;
                    return voyages;
                }
                else
                {
                    // Gérer le cas où la liste de voyages est null ou vide
                    // Rediriger vers une page d'erreur ou afficher un message approprié
                    return null;
                }
            }
            else
            {
                // Gérer les erreurs de requête HTTP
                throw new HttpRequestException($"Erreur lors de la requête : {response.StatusCode}");
            }
        }
    }
}
