using SQLite;

namespace KlassiRahaHaldamine.Models
{
    public class EventStudent
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public int EventId { get; set; }
        public int StudentId { get; set; }
    }
}

