﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interface.Entities
{
    public class LikeEntity
    {
        public int Id { get; set; }

        public int ProfileId { get; set; }

        public int PhotoId { get; set; }
    }
}