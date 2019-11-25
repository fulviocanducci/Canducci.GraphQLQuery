﻿using Canducci.GraphQLQuery;
using Canducci.GraphQLQuery.MSTest.Models;
using Models;
using System;

namespace ConsoleAppTest
{
  class Program
  {
    static void Main(string[] args)
    {
      //AddCar
      //Car car = new Car()
      //{
      //  Id = 0,
      //  Title = "Car 1",
      //  Purchase = DateTime.Now.AddDays(-100),
      //  Value = 10000.00M,
      //  Active = true
      //};
      //TypeQL typeQL = new TypeQL(
      //  new QueryType(
      //    "car_add",
      //    new Arguments(new Argument<Car>("car", car)),
      //    new Fields(
      //      new Field("id"),
      //      new Field("title"),
      //      new Field("purchase"),
      //      new Field("value"),
      //      new Field("active")
      //    )
      //  )
      //);
      //System.Console.WriteLine(typeQL);

      //Car Edit
      //Car car = new Car()
      //{
      //  Id = 1,
      //  Title = "Car 1",
      //  Purchase = DateTime.Parse("2019-08-14 23:54:18"),
      //  Value = 11000.00M,
      //  Active = true
      //};
      //TypeQL typeQL = new TypeQL(
      //  new QueryType(
      //    "car_edit",
      //    new Arguments(new Argument<Car>("car", car)),
      //    new Fields(
      //      new Field("id"),
      //      new Field("title"),
      //      new Field("purchase"),
      //      new Field("value"),
      //      new Field("active")
      //    )
      //  )
      //);
      //System.Console.WriteLine(typeQL);

      ////Car Find
      //TypeQL typeQL = new TypeQL(
      //  new QueryType(
      //    "car_find",
      //    new Arguments(new Argument<int>("id", 1)),
      //    new Fields(
      //      new Field("id"),
      //      new Field("title"),
      //      new Field("purchase"),
      //      new Field("value"),
      //      new Field("active")
      //    )
      //  )
      //);
      //System.Console.WriteLine(typeQL);

      //Car Delete
      //TypeQL typeQL = new TypeQL(
      //  new QueryType(
      //    "car_delete",
      //    new Arguments(new Argument<int>("id", 0)),
      //    new Fields(
      //      new Field("description"),
      //      new Field("operation"),
      //      new Field("status")
      //    )
      //  )
      //);
      //System.Console.WriteLine(typeQL);

      //Car List
      var item = new Items() { Id = Guid.Empty, Title = "Item 10", Updated = null };
      TypeQL typeQL = new TypeQL(
        new QueryType(
          "item_add",
          new Arguments(
            new Argument<Items>("item", item)
          ),
          new Fields(
            new Field("id"),
            new Field("title"),
            new Field("updated")
          )
        )
      );
      System.Console.WriteLine(typeQL);

      //DateTime? data = null;
      //var result0 = data?.GetType() == typeof(DateTime);
      //var result1 = Nullable.GetUnderlyingType(typeof(DateTime?));

      //System.Console.WriteLine(default(Guid));


    }
  }
}
