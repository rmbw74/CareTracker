using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CareTracker.Models.DependentViewModels
{
    public class ViewDependentsHomePageViewModel
    {
        public ICollection<UserDependent> UserDependents { get; set; }
    }
}