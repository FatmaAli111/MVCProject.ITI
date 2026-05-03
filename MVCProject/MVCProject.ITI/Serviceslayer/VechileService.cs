using Microsoft.EntityFrameworkCore;
using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Entities;

public class VehicleService
{
    private readonly IGenericRepository<Vehicle> _vehicleRepo;

    public VehicleService(IGenericRepository<Vehicle> vehicleRepo)
    {
        _vehicleRepo = vehicleRepo;
    }

    public IEnumerable<Vehicle> GetAll()
    {
        return _vehicleRepo.GetTableNoTracking()
                           .Include(v => v.CarModel)
                           .ToList();
    }

    public Vehicle GetById(Guid id)
    {
        return _vehicleRepo.GetById(id);
    }
    public Vehicle? GetByUserId(Guid id)
    {
        return _vehicleRepo.GetTableNoTracking().FirstOrDefault(v=>v.Id==id);
    }
    public void Add(Vehicle vehicle)
    {
        _vehicleRepo.Add(vehicle);
        _vehicleRepo.SaveChanges();
    }

    public void Update(Vehicle vehicle)
    {
        _vehicleRepo.Update(vehicle);
        _vehicleRepo.SaveChanges();
    }

    public void Delete(Guid id)
    {
        var vehicle = _vehicleRepo.GetById(id);
        if (vehicle != null)
        {
            _vehicleRepo.Delete(vehicle);
            _vehicleRepo.SaveChanges();
        }
    }
    public void SetDefaultVehicle(Guid vehicleId, Guid userId)
    {
        var userVehicles = _vehicleRepo.GetTableNoTracking()
                                       .Where(v => v.UserId == userId)
                                       .ToList();

        foreach (var v in userVehicles)
        {
            v.IsDefault = false;
            _vehicleRepo.Update(v);
        }

        var selectedVehicle = _vehicleRepo.GetById(vehicleId);
        selectedVehicle.IsDefault = true;

        _vehicleRepo.Update(selectedVehicle);

        _vehicleRepo.SaveChanges();
    }
}
