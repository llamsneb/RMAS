using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RMAS.Models.Validation;

namespace RMAS.Models.SearchViewModels
{
    public class SearchViewModel
    {    
        public string EventName { get; set; }

        [ValidateSearchDate]
        public DateTime? EventDate { get; set; }
        public List<Event>? SearchResults { get; set; }
    }
}
