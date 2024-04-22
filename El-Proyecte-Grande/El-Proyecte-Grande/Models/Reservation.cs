namespace AspCinema.Models;

public class Reservation
{
    public int Id { get; set; }
    public Screening Screening { get; set; }
    public Person Customer { get; set; }
    public bool Reserved { get; set; }
    private string Customer_Name { get; set; }
    public string Customer_Mail { get; set; }
    public string Customer_Phone { get; set; }
    
}