﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface.DTO
{
    public class DalTag : IEntity
    {
        public DalTag()
        {
            Photos = new List<DalPhoto>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<DalPhoto> Photos { get; set; }

    }
}
