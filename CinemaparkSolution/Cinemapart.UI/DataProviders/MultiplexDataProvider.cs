using Cinemapart.UI.Db;
using Cinemapart.UI.Helpers;
using Cinemapart.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Cinemapart.UI.DataProviders
{
    class MultiplexDataProvider
    {
        public const string MultiplexUri = "http://www.cinemapark.ru/gadgets/data/multiplexes/";

        public async Task<List<Multiplex>> LoadMultiplexes()
        {
            var db = new MovieDataService();
            var appSettings = new AppSettingsHelper();
            var lastUpdated = appSettings.DateLastUpdated;
            var result = db.GetMultiplexes();

            if (result.Count == 0 || (DateTime.Now - lastUpdated) > new TimeSpan(0, 0, 10))
            {
                HttpClient client = new HttpClient();
                var response = await client.GetStringAsync(new Uri(MultiplexUri));
                var items = response.ParseMultiplexCollection();
                db.SaveMultiplexes(items);
                appSettings.DateLastUpdated = DateTime.Now;
                result = db.GetMultiplexes();
            }
            return result.OrderBy(x => x.City).ThenBy(y => y.Title).ToList();
        }

        public const string MoviesUri = "http://www.cinemapark.ru/gadgets/data/movies/{0}/";
        public const string PosterUri = "http://stasis.www.cinemapark.ru/img/film/poster_large/{0}.jpg";

        public async Task<List<Movie>> LoadMovies()
        {
            var db = new MovieDataService();
            var appSettings = new AppSettingsHelper();
            var lastUpdated = appSettings.DateLastUpdated;
            var multiplexId = appSettings.MultiplexId;
            var result = db.GetMovies(multiplexId);

            if(result.Count == 0 || (DateTime.Now - lastUpdated) > new TimeSpan(0, 0, 10))
            {
                HttpClient client = new HttpClient();
                var uri = string.Format(MoviesUri, multiplexId);
                var response = await client.GetStringAsync(new Uri(uri));
                var items = response.ParseMovieCollection(multiplexId);
                db.SaveMovies(items, multiplexId);
                appSettings.DateLastUpdated = DateTime.Now;
                result = db.GetMovies(multiplexId);
            }
            return result;
        }

        public const string ScheduleUri = "http://www.cinemapark.ru/gadgets/data/movie_schedule/{0}/{1}/";

        public async Task<List<Schedule>> LoadSchedules()
        {
            HttpClient client = new HttpClient();
            var multiplexId = 18;
            var movieId = 23482;

            var movies = await LoadMovies();

            var res = new List<Schedule>();

            foreach (var m in movies)
            {
                var uri = string.Format(ScheduleUri, multiplexId, m.Id);
                var response = await client.GetStringAsync(new Uri(uri));
                var items = response.ParseScheduleCollection(movieId);
                res.AddRange(items);
            }

            return res;
        }
    }
}
