using System;
using System.Data.Linq.Mapping;

namespace Cinemapart.UI.Models
{
    [Table]
    public class Schedule
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = false, DbType = "INT NOT NULL", CanBeNull = false, AutoSync = AutoSync.Always)]
        public int Id { get; set; }

        [Column(CanBeNull = false)]
        public int MovieId { get; set; }

        [Column(CanBeNull = false)]
        public int Hall { get; set; }

        [Column(CanBeNull = false)]
        public DateTime Time { get; set; }

        [Column(CanBeNull = false)]
        public Decimal Price { get; set; }
    }
}
