namespace ProfilePort.DataModel
{
    public class Interest
    {
       public int InterestId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }
    }
}
