using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface.DTO
{
    public class DalLike : IEntity
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public int PhotoId { get; set; }
    }
}
