using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateManagment.Api.Domain.Models;

public class UserProperty : BaseEntity
{
    public string PropertyType { get; set; }
    public double SquareMeters { get; set; }
    public int NumberOfRooms { get; set; }
    public int BuildingFloor { get; set; }
    public string WarmingType { get; set; }
    public Guid UserId { get; set; }
    public virtual User User { get; set; }
}
