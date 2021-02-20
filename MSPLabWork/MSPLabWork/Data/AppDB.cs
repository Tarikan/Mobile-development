using System.Collections.Generic;
using SQLite;
using MSPLabWork.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Globalization;

namespace MSPLabWork.Data
{
    public class AppDB
    {
        readonly SQLiteAsyncConnection database;

        public AppDB(string path)
        {
            database = new SQLiteAsyncConnection(path,
                SQLite.SQLiteOpenFlags.ReadWrite |
                SQLite.SQLiteOpenFlags.Create |
                SQLite.SQLiteOpenFlags.SharedCache);

            database.CreateTableAsync<Movie>();
            database.CreateTableAsync<MovieInfo>();
            database.CreateTableAsync<CachedRequest>();
            //System.Console.WriteLine($"Movies: {string.Join("\n", database.Table<Movie>().ToListAsync().Result.Select(m => m.Title))}");
        }

        public async Task<List<Movie>> SearchMoviesAsync(string searchString)
        {
            return await database.Table<Movie>()
                .Where(m => m.Title.ToUpper().Contains(searchString.ToUpper()))
                //.Where(m => m.Title.Contains(searchString))
                .ToListAsync();
        }

        public async Task<MovieInfo> GetMovieInfoAsync(string ImdbId)
        {
            return await database.Table<MovieInfo>()
                .Where(mi => mi.ImdbID == ImdbId)
                .FirstOrDefaultAsync();
        }

        public async Task<CachedRequest> GetCachedRequestAsync(string reqest)
        {
            return await database.Table<CachedRequest>()
                .Where(r => r.Request == reqest)
                .FirstOrDefaultAsync();
        }

        public async Task StoreMoviesAsync(IEnumerable<Movie> movies)
        {
            if (movies.Count() == 0)
                return;
            foreach (var movie in movies)
                await database.InsertOrReplaceAsync(movie);
            //await database.InsertAllAsync(movies);
        }

        public async Task StoreMovieInfoAsync(MovieInfo info)
        {
            if (info == null)
                return;

            await database.InsertOrReplaceAsync(info);
        }

        public async Task CacheReqestAsync(CachedRequest cachedRequest)
        {
            if (cachedRequest == null ||
                string.IsNullOrEmpty(cachedRequest.Request) ||
                string.IsNullOrEmpty(cachedRequest.Request))
                return;

            await database.InsertOrReplaceAsync(cachedRequest);
        }
    }
}
