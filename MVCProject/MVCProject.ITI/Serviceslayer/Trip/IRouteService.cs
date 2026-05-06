using MVCProject.ITI.Models.Trip;

namespace MVCProject.ITI.Serviceslayer.Trip
{
    public interface IRouteService
    {
        // It take the first city and the second one and show the available routes between them.
        Task<List<RouteOption>> GetRoutesAsync(string origin, string destination);
    }
}
