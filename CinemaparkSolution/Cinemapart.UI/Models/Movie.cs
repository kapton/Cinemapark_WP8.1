using System.Data.Linq.Mapping;

namespace Cinemapart.UI.Models
{
    [Table]
    public class Movie
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, DbType = "INT NOT NULL", CanBeNull = false, AutoSync = AutoSync.Always)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public string Title { get; set; }

        [Column(CanBeNull = false)]
        public int MultiplexId { get; set; }
    }
}
