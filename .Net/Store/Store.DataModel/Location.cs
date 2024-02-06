using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store.DataModel
{
    [Table("Locations")]
    public class Location
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
    }
}