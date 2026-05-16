namespace MVCProject.ITI.ViewModels
{
    public class AnalyticsViewModel
    {
        public double TotalDistance { get; set; }
        public double TotalSpent { get; set; }
        public int TripCount { get; set; }
        public double FuelCost { get; set; }
        public double Emissions { get; set; }

        public List<MonthlyChartPoint> MonthlySpending { get; set; } = new();
        public List<MonthlyChartPoint> MonthlyDistance { get; set; } = new();

        public double FuelPercentage { get; set; }
        public double TollsPercentage { get; set; }
        public double MaintenancePercentage { get; set; }
    }
}
