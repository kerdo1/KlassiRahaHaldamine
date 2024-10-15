using SQLite;

public class Event
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; } 
    public string Name { get; set; }
    public DateTime EventDate { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
 
    public Event()
    {
    }
}
