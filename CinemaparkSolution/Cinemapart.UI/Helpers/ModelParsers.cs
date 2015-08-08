using Cinemapart.UI.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Cinemapart.UI.Helpers
{
    public static class ModelParsers
    {
        public static List<Multiplex> ParseMultiplexCollection(this string xml)
        {
            TextReader textReader = new StringReader(xml);
            var xElement = XElement.Load(textReader);

            return (from item in xElement.Descendants("item")
                    select new Multiplex
                    {
                        City = item.GetAttributeOrDefault("city"),
                        Title = item.GetAttributeOrDefault("title"),
                        Id = item.GetAttributeIntOrDefault("id")
                    }).ToList();
        }

        public static List<Movie> ParseMovieCollection(this string xml, int multiplexId)
        {
            TextReader textReader = new StringReader(xml);
            var xElement = XElement.Load(textReader);

            return (from item in xElement.Descendants("item")
                    select new Movie
                    {
                        Title = item.GetAttributeOrDefault("title"),
                        Id = item.GetAttributeIntOrDefault("id"),
                        MultiplexId = multiplexId
                    }).ToList();
        }

        public static List<Schedule> ParseScheduleCollection(this string xml, int movieId)
        {
            TextReader textReader = new StringReader(xml);
            var xElement = XElement.Load(textReader);

            var items = (from item in xElement.Descendants("date")
                         select new
                         {
                             Date = item.GetAttributeOrDefault("name"),
                             Halls = (from hall in item.Elements("hall")
                                      select new
                                      {
                                          HallId = hall.GetAttributeIntOrDefault("id"),
                                          Sessions = (from session in hall.Elements("item")
                                                      select new
                                                      {
                                                          Id = session.GetAttributeIntOrDefault("id"),
                                                          Time = session.GetAttributeOrDefault("time"),
                                                          Price = session.GetAttributeIntOrDefault("price")
                                                      }).ToList()
                                      }).ToList()
                         }).ToList();

            var res = (from m in items
                       from h in m.Halls
                       from s in h.Sessions
                       select new Schedule
                       {
                           Id = s.Id,
                           MovieId = movieId,
                           Hall = h.HallId,
                           Price = s.Price,
                           Time = m.Date.GetTime(s.Time)
                       }).ToList();

            return res;
        }
    }
}
