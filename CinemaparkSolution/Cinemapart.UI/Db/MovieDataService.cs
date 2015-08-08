using Cinemapart.UI.Models;
using System.Collections.Generic;
using System.Linq;

namespace Cinemapart.UI.Db
{
    public class MovieDataService
    {
        public List<Multiplex> GetMultiplexes()
        {
            using (var db = new MovieDataContext())
            {
                return db.Multiplexes.ToList();
            }
        }

        public void SaveMultiplexes(List<Multiplex> items)
        {
            using (var db = new MovieDataContext())
            {
                db.Multiplexes.DeleteAllOnSubmit(db.Multiplexes);
                db.SubmitChanges();
                db.Multiplexes.InsertAllOnSubmit(items);
                db.SubmitChanges();
            }
        }


        public List<Movie> GetMovies(int multiplexId)
        {
            using (var db = new MovieDataContext())
            {
                return db.Movies.Where(x => x.MultiplexId == multiplexId).ToList();
            }
        }

        public void SaveMovies(List<Movie> items, int multiplexId)
        {
            using (var db = new MovieDataContext())
            {
                db.Movies.DeleteAllOnSubmit(db.Movies.Where(x => x.MultiplexId == multiplexId));
                db.SubmitChanges();
                db.Movies.InsertAllOnSubmit(items);
                db.SubmitChanges();
            }
        }
    }
}
