using System;
using Canducci.GraphQLQuery;

namespace ConsoleAppTest
{
  class Program
  {
    static void Main(string[] args)
    {
      //TypeQL typeQL = new TypeQL(
      //  name: "states_state",
      //  arguments: new Arguments(
      //    new Argument<bool>("load", true),
      //    new Argument<string>("state", "SP")
      //  ),
      //  fields: new Fields
      //  {          
      //    new Field("id"),
      //    new Field("uf")/*,
      //    new Item("country", "items", new List<IItem> ()
      //    {
      //      new Item("id"),
      //      new Item("name")
      //    })*/
      //  }
      //);

      //ITypeQL ql = new TypeQL(
      //  name: "countries_by_name",
      //  arguments: new Arguments(
      //    new Argument<bool>("load", true),
      //    new Argument<string>("name", "pir")
      //  ),
      //  fields: new Fields(
      //    new Field("id"),
      //    new Field("name"),
      //    new Field("stateId"),
      //    new Field("state",
      //      new Fields(
      //        new Field("uf")
      //      )
      //    )
      //  )
      //);

      //id name created active
      TypeQL ql = new TypeQL(
        "peoples_by_created",
        new Arguments(
          Argument<DateTime>.Create("created", DateTime.Parse("03/03/1993"))
        ),
        new Fields(
          Field.Create("id", "_id"),
          Field.Create("name", "_name"),
          Field.Create("created"),
          Field.Create("active")
        ));

      //typeQL.Arguments.Add(typeof(int), new Argument("id", 1));
      //typeQL.Arguments.Add(typeof(string), new Argument("name", "\"a\""));

      

      System.Console.WriteLine(ql.Render());
    }
  }
}
