using TheComicsTestMVC.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using System.Text;
using TheComicsTestMVC.Helpers;

namespace TheComicsTestMVC.Helpers
{
    public class MarvelAPIHelper
    {
        public Story LoadStory([FromServices] IConfiguration config, int p_StoryID)
        {
            Story story = new Story();

            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string ts = DateTime.Now.Ticks.ToString();
                string publicKey = config.GetSection("MarvelComicsAPI:PublicKey").Value;
                string hash = GenerateHash(ts, publicKey, config.GetSection("MarvelComicsAPI:PrivateKey").Value);

                HttpResponseMessage storyResponse = client.GetAsync(
                    config.GetSection("MarvelComicsAPI:BaseURL").Value +
                    $"stories/{p_StoryID}?limit=100&ts={ts}&apikey={publicKey}&hash={hash}").Result;

                storyResponse.EnsureSuccessStatusCode();

                story = StoryHelper.ParseStoryObject(storyResponse.Content.ReadAsStringAsync().Result);
                story.Characters = LoadCharacters(config, story.Id);

                return story;
            }
        }

        public List<Character> LoadCharacters([FromServices] IConfiguration config, int p_StoryID)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string ts = DateTime.Now.Ticks.ToString();
                string publicKey = config.GetSection("MarvelComicsAPI:PublicKey").Value;
                string hash = GenerateHash(ts, publicKey, config.GetSection("MarvelComicsAPI:PrivateKey").Value);

                HttpResponseMessage charactersResponse = client.GetAsync(
                    config.GetSection("MarvelComicsAPI:BaseURL").Value +
                    $"stories/{p_StoryID}/characters?limit=100&ts={ts}&apikey={publicKey}&hash={hash}").Result;

                charactersResponse.EnsureSuccessStatusCode();

                return CharacterHelper.ParseCharactersList(charactersResponse.Content.ReadAsStringAsync().Result);
            }
        }

        private string GenerateHash(string ts, string publicKey, string privateKey)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(ts + privateKey + publicKey);
            var gerador = MD5.Create();
            byte[] bytesHash = gerador.ComputeHash(bytes);
            return BitConverter.ToString(bytesHash).ToLower().Replace("-", String.Empty);
        }
    }
}
