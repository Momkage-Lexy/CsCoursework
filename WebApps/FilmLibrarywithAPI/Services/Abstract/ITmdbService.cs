using Homework4.Models;

namespace Homework4.Services
{
    public interface ITmdbService

    { 
        public Task<List<MoviesReturn>> Search(string query);

        public Task<DetailsReturn> ID(int id);
        
        public Task<CreditsReturn> Credits(int id);
    }
}