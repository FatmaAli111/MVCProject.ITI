namespace MVCProject.ITI.ViewModels
{
    public class PassengerSplitVM
    {
        public Guid Id { get; set; } 
        public string Name { get; set; } = string.Empty;
        public float ShareAmount { get; set; }
        public float SharePercentage { get; set; }
    }
}
