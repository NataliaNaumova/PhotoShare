using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL.Interface.DTO
{
    public class DalProfile : IEntity
    {

        public int Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public byte[] Avatar { get; set; }
    }
}
