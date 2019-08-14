using System;
using System.Collections.Generic;

namespace Week3App.Models
{
    public partial class MovieTble
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Director { get; set; }
        public string Email { get; set; }
        public string Language { get; set; }
        public string Category { get; set; }

        public virtual CategoryTble CategoryNavigation { get; set; }
    }
}
