namespace ProfilePort.DataModel
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string NoteContent { get; set; }

        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }

    }
}
