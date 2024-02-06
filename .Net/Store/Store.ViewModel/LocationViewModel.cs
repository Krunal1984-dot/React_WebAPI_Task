using System;
using System.ComponentModel.DataAnnotations;

namespace Store.ViewModel
{
    public class LocationViewModel
    {
        public int Id { get; set; }

        [Required]
        public string LocationName { get; set; }
        
        [Required]
        public string LocationStartTime { get; set; }

        [Required]
        public string LocationEndTime { get; set; }
    }
}