using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagment.Api.Domain.Models;

public class User : BaseEntity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public string HomeNumber { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
    public string Password { get; set; }
    public bool EmailConfirmed { get; set; }
    public bool IsClient { get; set; }
    public bool IsAgent { get; set; }
    public Guid CompanyId { get; set; }
    public virtual Company Company { get; set; }

    public virtual ICollection<UserProperty> UserProperties { get; set; }

}
