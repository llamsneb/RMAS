using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RMAS.Models.SearchViewModels
{
    public class SearchViewModel
    {    
        public string EventName { get; set; }
        public DateTime EventDate { get; set; }
    }
}
