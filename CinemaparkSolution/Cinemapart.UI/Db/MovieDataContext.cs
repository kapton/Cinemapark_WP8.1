using Cinemapart.UI.Models;
using System.Data.Linq;

namespace Cinemapart.UI.Db
{
    public class MovieDataContext : DataContext
    {
        public static string ConnectionString = "Data Source=isostore:/Movie.sdf";

        public Table<Multiplex> Multiplexes;
        public Table<Movie> Movies;
        public Table<Schedule> Schedules;

        public MovieDataContext()
            : base(ConnectionString)
        { }
    }
}
