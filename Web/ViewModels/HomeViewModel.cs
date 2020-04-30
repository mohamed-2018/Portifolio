using Core.Entities;
using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.ViewModels
{
    public class HomeViewModel
    {       // to create object of owner and porifolioItem 
        public Owner Owner { get; set; }
        public List<PortifolioItem> PortifolioItems { get; set; }
    }
}
