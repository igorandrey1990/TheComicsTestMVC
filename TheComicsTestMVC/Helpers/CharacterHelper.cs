using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TheComicsTestMVC.Models;

namespace TheComicsTestMVC.Helpers
{
    public class CharacterHelper
    {
        public static List<Character> ParseCharactersList(string  p_strResult)
        {
            List<Character> characterList = new List<Character>();
            dynamic charactersJSON = JsonConvert.DeserializeObject(p_strResult);

            foreach (var item in charactersJSON.data.results)
            {
                Character character = new Character();
                character.Name = item.name;
                character.ImageURL = item.thumbnail.path + "/portrait_small.jpg";
                characterList.Add(character);
            }

            return characterList;
        }
    }
}
