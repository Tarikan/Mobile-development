using MSPLabWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSPLabWork.Services
{
    public class MovieStore : IDataStore<Movie>
    {
        private List<Movie> data;
        public MovieStore()
        {
            data = MovieReadService.ExtractMovies();
        }
        public async Task<bool> AddItemAsync(Movie item)
        {
            data.Add(item);
            return await Task.FromResult(true);
        }

        

        public async Task<bool> DeleteItemAsync(Movie item)
        {
            try
            {
                data.Remove(item);
            }
            catch
            {
                return await Task.FromResult(false);
            }
            return await Task.FromResult(true);
        }

        public async Task<IEnumerable<Movie>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(data);
        }
    }
}
