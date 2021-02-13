using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using MSPLabWork.Models;
using MSPLabWork.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace MSPLabWork.Services
{
    public static class MovieReadService
    {
        public static List<Movie> ExtractMovies()
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

        public static MovieInfo ExtractMovieInfo(string imdbId)
        {
            MovieInfo info;

            var assembly = IntrospectionExtensions.GetTypeInfo(typeof(Resources.Resources)).Assembly;

            using (var stream = assembly.GetManifestResourceStream($"MSPLabWork.Resources.MovieInfo.{imdbId}.txt"))
            {
                if (stream == null)
                {
                    System.Console.WriteLine("File " + $"MSPLabWork.Resources.MovieInfo.{imdbId}.txt" + " not found");
                    return null;
                }
                using (var jsonReader = new StreamReader(stream))
                {
                    var j = jsonReader.ReadToEnd();
                    
                    info = JsonConvert.DeserializeObject<MovieInfo>(j);
                }
            }

            return info;
        }
    }
}
