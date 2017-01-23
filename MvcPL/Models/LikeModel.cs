using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcPL.Models
{
    public class LikeModel
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public int PhotoId { get; set; }

        public string UserLogin { get; set; }
    }
}