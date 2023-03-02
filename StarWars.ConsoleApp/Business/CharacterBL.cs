using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Newtonsoft.Json;

using StarWars.ConsoleApp.Models;

namespace StarWars.ConsoleApp.Business
{
    /// <inheritdoc cref="ICharacterBL"/>
    public class CharacterBL : ICharacterBL
    {
        private readonly HttpClient _apiClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="CharacterBL">CharacterBL</see> using
        /// the specified <paramref name="apiClient">API client</paramref>.
        /// </summary>
        /// <param name="apiClient">
        /// The <see cref="HttpClient"> to use for requests.
        /// </param>
        public CharacterBL(HttpClient apiClient)
        {
            _apiClient = apiClient;
        }

        /// <summary>
        /// Gets or sets the base <see cref="Uri">URI</see> used to call the API.
        /// </summary>
        public Uri BaseUri { get; set; } = new Uri("http://localhost:8002/api/");

        /// <summary>
        /// Gets or sets the <see cref="JsonSerializerSettings">settings</see> used to
        /// deserialize JSON responses.
        /// </summary>
        public JsonSerializerSettings SerializerSettings { get; set; } =
            new JsonSerializerSettings
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
            };

        /// <inheritdoc/>
        public async Task<IEnumerable<CharacterModel>> GetAllAsync()
        {
            Uri uri = new Uri(BaseUri, "Characters");
            string json;
            using (HttpResponseMessage response = await _apiClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }
                json = await response.Content.ReadAsStringAsync();
            }
            IEnumerable<CharacterModel> characters = JsonConvert.DeserializeObject<
                IEnumerable<CharacterModel>
            >(json, SerializerSettings);
            return characters;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CharacterModel>> GetAllByAllegianceAsync(
            Allegiance allegiance
        )
        {
            Uri uri = new Uri(BaseUri, $"Characters/AllByAllegiance/{allegiance}");
            string json;
            using (HttpResponseMessage response = await _apiClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }
                json = await response.Content.ReadAsStringAsync();
            }
            IEnumerable<CharacterModel> characters = JsonConvert.DeserializeObject<
                IEnumerable<CharacterModel>
            >(json, SerializerSettings);
            return characters;
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<CharacterModel>> GetAllByTrilogyAsync(Trilogy trilogy)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public async Task<CharacterModel> GetOneByNameAsync(string name)
        {
            Uri uri = new Uri(BaseUri, $"Characters/ByName/{name}");
            string json;
            using (HttpResponseMessage response = await _apiClient.GetAsync(uri))
            {
                if (!response.IsSuccessStatusCode)
                {
                    throw new HttpRequestException(
                        "Response status code does not indicate success: "
                            + $"{(int)response.StatusCode} ({response.ReasonPhrase})."
                    );
                }
                json = await response.Content.ReadAsStringAsync();
            }
            CharacterModel character = JsonConvert.DeserializeObject<CharacterModel>(
                json,
                SerializerSettings
            );
            return character;
        }
    }
}
