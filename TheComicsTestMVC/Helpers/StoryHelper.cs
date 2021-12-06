using Newtonsoft.Json;
using TheComicsTestMVC.Models;

namespace TheComicsTestMVC.Helpers
{
    public class StoryHelper
    {

        public static Story ParseStoryObject(string p_strResult)
        {
            dynamic storyJSON = JsonConvert.DeserializeObject(p_strResult);

            Story story = new Story();
            story.Id = storyJSON.data.results[0].id;
            story.Title = storyJSON.data.results[0].title;
            story.Description = storyJSON.data.results[0].description;

            return story;
        }
    }
}
