namespace Finnce_Api.core.Notifications
{
    public class Notification
    {
        [Column("Id")]
        public Guid Id { get; set; }
        [StringLength(25)]
        public string Name { get; set; }
        public string Message { get; set; }

        [Required]
        [ForeignKey(nameof(User))]
        public string IdUser { get; set; }
        public User User { get; set; }
    }
}
