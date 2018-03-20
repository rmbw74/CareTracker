using System;
using CareTracker.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CareTracker.Models.SharedDependentViewModels
{
    public class SharedDependentViewModel
    {
        public ICollection<SharedDependentListItem> DependentsShared { get; set; }
        [Display(Name = "Who Do You Want To Share?")]
        public List<SelectListItem> DependentList { get; set; }
        
        [Required]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email Of User You Wish To Share With?")]
        public string ToUserEmail { get; set; }

        public SharedDependentViewModel(ApplicationUser user, ApplicationDbContext ctx)
        {
            this.DependentsShared = (from sd in ctx.SharedDependent
                                                    join du in ctx.DependentUser on sd.DependentUserId equals du.DependentUserId
                                                    join d in ctx.Dependent on du.DependentId equals d.DependentId
                                                    where sd.ToUser == user
                                                    select new SharedDependentListItem
                                                    {
                                                        DependentId = d.DependentId,
                                                        FirstName = d.FirstName,
                                                        LastName = d.LastName,
                                                        Birthday = d.Birthday,
                                                        FromUserName = du.User.FirstName,
                                                        FromUserEmail = du.User.UserName
                                                    }).ToList();



            this.DependentList = (from d in ctx.Dependent
                                  join du in ctx.DependentUser
                                  on d.DependentId equals du.DependentId
                                  where du.User == user
                                  select new Dependent
                                  {
                                      FirstName = d.FirstName,
                                      LastName = d.LastName,
                                      DependentId = d.DependentId

                                  })
                                  .OrderBy(d => d.LastName)
                                  .AsEnumerable()
                                 .Select(li => new SelectListItem
                                 {
                                     Text = li.LastName + "," + li.FirstName,
                                     Value = li.DependentId.ToString()
                                 })
                                 .ToList();
        }
    }
}
