using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class TagModel
    {
        public TagModel()
        {
            Photos = new List<PhotoModel>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PhotoModel> Photos { get; set; }
    }
}