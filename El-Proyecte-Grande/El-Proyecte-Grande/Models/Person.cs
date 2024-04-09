namespace AspCinema.Models;

public abstract class Person
{
    public int Id { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    
    protected Person(string username, string password)
    {
        Username = username;
        Password = password;
    }
}