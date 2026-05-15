using System.ComponentModel.DataAnnotations;
using MVCProject.ITI.DataAccessLayer.Entities;

namespace MVCProject.ITI.Areas.Admin.ViewModels;

public class AdminDashboardViewModel
{
    public int TotalUsers { get; set; }
    public int TotalTrips { get; set; }
    public int TotalVehicles { get; set; }
    public int TotalCarModels { get; set; }
    public int TotalFuelPrices { get; set; }
    public float TotalRevenue { get; set; }
    public float TotalDistanceKm { get; set; }
    public List<AdminTripListItemViewModel> RecentTrips { get; set; } = [];
    public List<AdminUserListItemViewModel> RecentUsers { get; set; } = [];
}

public class AdminUserListItemViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";
    public bool EmailConfirmed { get; set; }
    public bool LockoutEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public IList<string> Roles { get; set; } = [];
    public bool IsLockedOut => LockoutEnd.HasValue && LockoutEnd > DateTimeOffset.UtcNow;
}

public class AdminUserEditViewModel
{
    public Guid Id { get; set; }
    public string Email { get; set; } = "";
    public string FullName { get; set; } = "";

    [Display(Name = "Email confirmed")]
    public bool EmailConfirmed { get; set; }

    public bool IsAdmin { get; set; }
    public bool IsLockedOut { get; set; }

    /// <summary>Only the seeded super-admin can grant or revoke the Admin role.</summary>
    public bool CanManageAdmins { get; set; }
}

public class AdminTripListItemViewModel
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = "";
    public string UserFullName { get; set; } = "";
    public string OriginName { get; set; } = "";
    public string DestinationName { get; set; } = "";
    public float DistanceKm { get; set; }
    public int PassengerCount { get; set; }
    public DateTime TripDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public bool IsFavorite { get; set; }
}

public class AdminTripDetailsViewModel
{
    public Guid Id { get; set; }
    public string UserEmail { get; set; } = "";
    public string UserFullName { get; set; } = "";
    public string VehicleName { get; set; } = "";
    public string OriginName { get; set; } = "";
    public string DestinationName { get; set; } = "";
    public float DistanceKm { get; set; }
    public int DurationMinutes { get; set; }
    public int PassengerCount { get; set; }
    public bool IsReturnTrip { get; set; }
    public bool IsFavorite { get; set; }
    public bool IsAcOn { get; set; }
    public DateTime TripDate { get; set; }
    public DateTime CreatedAt { get; set; }
    public float? TotalCost { get; set; }
    public float? CostPerPassenger { get; set; }
    public List<string> PassengerNames { get; set; } = [];
}

public class AdminVehicleListItemViewModel
{
    public Guid Id { get; set; }
    public string OwnerEmail { get; set; } = "";
    public string NickName { get; set; } = "";
    public string Make { get; set; } = "";
    public string Model { get; set; } = "";
    public bool IsDefault { get; set; }
    public int TripCount { get; set; }
}

public class AdminCarModelViewModel
{
    public Guid? Id { get; set; }

    [Required, MaxLength(80)]
    public string Make { get; set; } = "";

    [Required, MaxLength(80)]
    public string Model { get; set; } = "";

    [Range(1990, 2100)]
    public int Year { get; set; } = DateTime.UtcNow.Year;

    public FuelTypeEnum FuelType { get; set; }

    [Range(0.1, 100)]
    public float WltpMixed { get; set; }

    public float? TankCapacity { get; set; }
    public float? BatteryCapacity { get; set; }
}

public class AdminFuelPriceViewModel
{
    public Guid? Id { get; set; }

    public FuelTypeEnum FuelType { get; set; }

    [Required, MaxLength(100)]
    public string Region { get; set; } = "Egypt";

    [Range(0.01, 10000)]
    public float PricePerUnit { get; set; }

    [Required, MaxLength(10)]
    public string Currency { get; set; } = "EGP";

    [DataType(DataType.Date)]
    public DateTime RecordedDate { get; set; } = DateTime.UtcNow.Date;
}

public class AdminFuelEfficiencyViewModel
{
    public Guid? Id { get; set; }

    [Required]
    public Guid VehicleId { get; set; }

    [Range(0.01, 100)]
    public float ConsumptionRate { get; set; }

    [Required, MaxLength(20)]
    public string Unit { get; set; } = "L/100km";

    public string? VehicleLabel { get; set; }
}
