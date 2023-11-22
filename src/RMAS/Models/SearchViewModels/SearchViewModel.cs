using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using RMAS.Models.Validation;
using RMAS.Models.BaseViewModels;

namespace RMAS.Models.SearchViewModels
{
    public class SearchViewModel : BaseViewModel
    {
        public SearchViewModel()
        {
            Reservations = new List<Reservation>();
        }

        [ValidateSearchDate]
        public DateOnly? EventDate { get; set; }
    }
}
