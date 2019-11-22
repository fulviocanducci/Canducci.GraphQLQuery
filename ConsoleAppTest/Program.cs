using System;
using Canducci.GraphQLQuery;

namespace ConsoleAppTest
{
  class Program
  {
    static void Main(string[] args)
    {
      #region old
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
      //TypeQL ql = new TypeQL(
      //  "peoples_by_created",
      //  new Arguments(
      //    Argument<DateTime>.Create("created", DateTime.Parse("03/03/1993"))
      //  ),
      //  new Fields(
      //    Field.Create("id", "_id"),
      //    Field.Create("name", "_name"),
      //    Field.Create("created"),
      //    Field.Create("active")
      //  ));

      //typeQL.Arguments.Add(typeof(int), new Argument("id", 1));
      //typeQL.Arguments.Add(typeof(string), new Argument("name", "\"a\""));

      //var ql = new TypeQL(
      //  "nome",
      //  new Arguments(
      //    Argument<int>.Create("id", 1),
      //    Argument<string>.Create("name", "name"),
      //    Argument<bool>.Create("active", false),
      //    Argument<DateTime>.Create("created", DateTime.Now.AddDays(-1))
      //  ),
      //  new Fields(
      //    Field.Create("id"),
      //    Field.Create("name"),
      //    Field.Create("active"),
      //    Field.Create("created"),
      //    Field.Create("state", 
      //      new Fields(
      //        Field.Create("id"),
      //        Field.Create("uf")
      //      )
      //    )
      //  )
      //);

      //var ql = new TypeQL(
      //    "countries_by_name",
      //    new Arguments(
      //      new Argument<bool>("load", true),
      //      new Argument<string>("name", "presidente")
      //    ),
      //    new Fields(
      //      new Field("id"),
      //      new Field("name"),
      //      new Field("stateId"),
      //      new FieldRelationship("state",,"s",
      //        new Fields(               
      //          new Field("id"),
      //          new Field("uf")
      //        )
      //      )
      //    )         
      //);
      #endregion

      var ql = new TypeQL(new QueryType(
        name: "people_find",
        alias: "people_find_0",
        arguments: new Arguments(new Argument<int>("id", 1)),
        fields: new Fields(
          new Field("id"),
          new Field("name")
        )
      ), 
      new QueryType(
        alias: "people_find_1",
        arguments: new Arguments(new Argument<int>("id", 1)),
        name: "people_find",
        fields: new Fields(
          new Field("id"),
          new Field("name")
        )
      ));

      //string str = (ql);
      System.Console.WriteLine(ql.ToStringJson());
    }
  }
}
