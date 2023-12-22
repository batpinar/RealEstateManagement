using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagment.Api.Domain.Models;

public class Company : BaseEntity
{
    public string CompanyName { get; set; }
    public string Address { get; set; }
    public int PostCode { get; set; }
    public string PhoneNumber { get; set; }
    public string FaxNumber { get; set; }

    public virtual ICollection<User> Users { get; set; }

}
