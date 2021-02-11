using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MSPLabWork.Models;
using Newtonsoft.Json.Linq;

namespace MSPLabWork.Services
{
    public static class MovieReadService
    {
        public static IEnumerable<Movie> ExtractMovies()
        {
            var movies = new List<Movie>();
            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Resources.Resources)).Assembly;

            using (var stream = assembly.GetManifestResourceStream("MSPLabWork.Resources.MoviesList.json"))
            {
                using (var jsonReader = new StreamReader(stream))
                {
                    var j = jsonReader.ReadToEnd();
                    JObject json = JObject.Parse(j);
                    movies = json["Search"].Select(t => new Movie()
                    {
                        Title = (string)t["Title"],
                        Type = (string)t["Type"],
                        Year = (string)t["Year"],
                        Poster = (string)t["Poster"],
                        ImdbID = (string)t["imdbID"]
                    }).ToList();
                }
            }
            return movies;
        }
    }
}
