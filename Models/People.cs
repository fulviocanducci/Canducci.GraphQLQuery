namespace Canducci.GraphQLQuery.MSTest.Models
{
  public class People
  {
    public People()
    {
    }
    public People(int id, string name, System.DateTime? created, bool active, decimal value, System.TimeSpan hours)
    {
      Id = id;
      Name = name;
      Created = created;
      Active = active;
      Value = value;
      Hours = hours;
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public System.DateTime? Created { get; set; }
    public bool Active { get; set; }
    public decimal Value { get; }
    public System.TimeSpan Hours { get; set; }
  }
}
