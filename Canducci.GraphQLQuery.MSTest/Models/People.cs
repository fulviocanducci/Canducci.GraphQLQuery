namespace Canducci.GraphQLQuery.MSTest.Models
{
  public class People
  {
    public People()
      :this(0, "")
    {
    }
    public People(string name)
    {
      Id = 0; ;
      Name = name;
    }
    public People(int id, string name)
    {
      Id = id;
      Name = name;
    }
    public int Id { get; set; }
    public string Name { get; set; }
  }
}
