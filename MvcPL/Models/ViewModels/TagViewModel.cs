using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MvcPL.Models.Helpers;

namespace MvcPL.Models.ViewModels
{
    public class TagViewModel
    {
        public TagModel Tag { get; set; }
        public PagedList<PhotoModel> Photos { get; set; }
    }
}