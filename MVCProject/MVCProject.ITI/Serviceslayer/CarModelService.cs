using MvcProject.iti.DataAccessLayer.Repository.GenericRepo;
using MVCProject.ITI.DataAccessLayer.Data;
using MVCProject.ITI.DataAccessLayer.Entities;
public class CarModelService
{
    private readonly IGenericRepository<CarModel> _carModelRepo;

    public CarModelService(IGenericRepository<CarModel> carModelRepo)
    {
        _carModelRepo = carModelRepo;
    }
    //public List<CarModel> GetAll()
    //{
    //    return _carModelRepo.GetTableNoTracking()
    //                        .ToList();
    //}
    public IQueryable<CarModel> GetTable()
    {
        return _carModelRepo.GetTableNoTracking();
    }
    public CarModel GetById(Guid id)
    {
        return _carModelRepo.GetById(id);
    }

    public void Add(CarModel model)
    {
        _carModelRepo.Add(model);
        _carModelRepo.SaveChanges();
    }

    public void Update(CarModel model)
    {
        _carModelRepo.Update(model);
        _carModelRepo.SaveChanges();
    }

    public void Delete(CarModel model)
    {
        _carModelRepo.Delete(model);
        _carModelRepo.SaveChanges();
    }
}
