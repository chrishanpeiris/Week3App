using System;
using System.Collections.Generic;

namespace Week3App.Models
{
    public partial class CategoryTble
    {
        public CategoryTble()
        {
            MovieTble = new HashSet<MovieTble>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        public virtual ICollection<MovieTble> MovieTble { get; set; }
    }
}
