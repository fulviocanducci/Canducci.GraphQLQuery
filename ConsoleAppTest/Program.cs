using Canducci.GraphQLQuery;
using System;
using System.Collections.Generic;

namespace ConsoleAppTest
{
   class Program
   {
      static void Main(string[] args)
      {
         //TypeQL typeQL = new TypeQL(
         //   new Variables(
         //      "getSourceAdd",
         //      new Variable<Guid?>("id", null),
         //      new Variable<bool?>("active", null),
         //      new Variable<DateTime?>("created", null),
         //      new Variable<string>("name", "With Null"),
         //      new Variable<TimeSpan?>("time", null),
         //      new Variable<decimal?>("value", null)
         //   ),
         //   new QueryType(
         //        "source_param_add",
         //        new Fields(
         //           new Field("id"),
         //           new Field("active"),
         //           new Field("created"),
         //           new Field("name"),
         //           new Field("time"),
         //           new Field("value")
         //        ),
         //        new Arguments(
         //           new Argument("id", new Parameter("id")),
         //           new Argument("active", new Parameter("active")),
         //           new Argument("created", new Parameter("created")),
         //           new Argument("name", new Parameter("name")),
         //           new Argument("time", new Parameter("time")),
         //           new Argument("value", new Parameter("value"))
         //        )
         //    )
         //);



         //Source source = new Source
         //{
         //   Id = null,
         //   Active = null,
         //   Created = null,
         //   Name = "Source Null",
         //   Time = TimeSpan.Parse("12:00:00"),
         //   Value = 1500.15M
         //};
         //TypeQL typeQL = new TypeQL(
         //   new Variables(
         //      "getSourceAdd",
         //      new Variable<Source>("input", source, "source_input", true, format: Format.FormatClass)
         //   ),
         //   new QueryType(
         //        "source_add",
         //        new Fields(
         //           new Field("id"),
         //           new Field("active"),
         //           new Field("created"),
         //           new Field("name"),
         //           new Field("time"),
         //           new Field("value")
         //        ),
         //        new Arguments(new Argument("input", new Parameter("input")))
         //    )
         //);

         //TypeQL typeQL = new TypeQL(
         //   new QueryType(
         //        "source_add",
         //        new Fields(
         //           new Field("id"),
         //           new Field("active"),
         //           new Field("created"),
         //           new Field("name"),
         //           new Field("time"),
         //           new Field("value")
         //        ),
         //        new Arguments(new Argument("input", source))
         //    )
         //);

         //TypeQL typeQL = new TypeQL(
         //   new QueryType(
         //        "states_in",
         //        new Fields(
         //           new Field("id"),
         //           new Field("uf")
         //        ),
         //        new Arguments(
         //           new Argument("ids", new int[] { 11, 12, 13 }),
         //           new Argument("load", false)
         //       )
         //    )
         //);
         var ids = new List<int>();
         ids.Add(11);
         ids.Add(16);
         //var ids = new Source[] { new Source(), new Source() };
         TypeQL typeQL = new TypeQL(
            new Variables("getStates",
               new Variable<List<int>>("ids", ids),
               new Variable<bool>("load", true)
            ),
            new QueryType(
                 "states_in",
                 new Fields(
                    new Field("id"),
                    new Field("uf"),
                    new Field(
                       new QueryType("country", 
                        new Fields(
                           new Field("id"),
                           new Field("name")
                           )
                        )
                    )
                 ),
                 new Arguments(
                    new Argument(new Parameter("ids")),
                    new Argument(new Parameter("load"))
                )
             )
         );

         //TypeQL typeQL = new TypeQL(
         //   new Variables("getAll",
         //      new Variable("state_id", 11),
         //      new Variable("country_id", 1)
         //   ),
         //   new QueryType("state_find", "state",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf")
         //      ),
         //      new Arguments(new Argument(new Parameter("id", "state_id")))
         //   ),
         //   new QueryType("country_find", "country",
         //      new Fields(
         //         new Field("id"),
         //         new Field("name")
         //      ),
         //      new Arguments(new Argument(new Parameter("id", "country_id")))
         //   )
         //);
         //Source source = new Source
         //{
         //   Id = null,
         //   Active = true,
         //   Created = DateTime.Now,
         //   Name = "Source 1",
         //   Time = null,
         //   Value = 1000M
         //};
         //TypeQL typeQL = new TypeQL(
         //    new Variables(
         //        "getSourceAdd",
         //        new Variable("input", source, "source_input", true)
         //    ),
         //    new QueryType(
         //        "source_add",
         //        new Fields(
         //        new Field("id"),
         //        new Field("active"),
         //        new Field("created"),
         //        new Field("name"),
         //        new Field("time"),
         //        new Field("value")
         //        ),
         //        new Arguments(new Argument("input", new Parameter("input")))
         //    )
         //);

         //TypeQL typeQL = new TypeQL(
         //    new Variables("getAll",
         //        new Variable<int>("state_id", 11),
         //        new Variable<int>("country_id", 1)
         //    ),
         //    new QueryType("state_find", "state",
         //        new Fields(
         //            new Field("id"),
         //            new Field("uf")
         //        ),
         //        new Arguments(new Argument(new Parameter("id", "state_id")))
         //    ),
         //    new QueryType("country_find", "country",
         //        new Fields(
         //            new Field("id"),
         //            new Field("name")
         //        ),
         //        new Arguments(new Argument(new Parameter("id", "country_id")))
         //    )
         //);


         Console.WriteLine(typeQL.ToStringJson());


         //TypeQL typeQL = new TypeQL(
         //     new QueryType(
         //    "states",
         //    "data", // <-alias
         //     new Fields(
         //            new Field("id", "_id"), // <-alias
         //            new Field("uf", "_uf") // <-alias
         //          )
         //     )
         //);
         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "sources",
         //    new Fields(
         //      new Field("id"),
         //      new Field("name"),
         //      new Field("value"),
         //      new Field("created"),
         //      new Field("active"),
         //      new Field("time")
         //    )
         //  ),
         //  new QueryType(
         //    "states",
         //    new Fields(
         //      new Field("id"),
         //      new Field("uf")
         //    )
         //  ),
         //  new QueryType(
         //    "cars",
         //    new Fields(
         //      new Field("id"),
         //      new Field("title")
         //    )
         //  )
         //);

         //var _a = new Any("any", new string[] { "100", "200" });
         //var _i = new ID("id", new string[] { "100", "200" });
         //var _u = new Uri("http://localhost");
         //TypeQL typeQL = new TypeQL(
         //   new Variables("get",
         //      new Variable("id", _i),
         //      new Variable("any", _a),
         //      new Variable("uri", _u),
         //      new Variable("code", 100)
         //   ),
         //   new QueryType("items",
         //      new Fields(new Field("about")),
         //      new Arguments(
         //         new Argument(new Parameter("id")),
         //         new Argument(new Parameter("any")),
         //         new Argument(new Parameter("code")),
         //         new Argument(new Parameter("uri"))
         //      )
         //   )
         //);

         //Console.WriteLine(typeQL);

         //Source source = new Source()
         //{
         //   Time = TimeSpan.Parse("13:02:00")
         //};
         //TypeQL typeQL = new TypeQL(
         //   new Variables("getSource",
         //      new Variable("input", source, "source_input")
         //   ),
         //   new QueryType("source_add",
         //      new Fields(
         //         new Field("id"),
         //         new Field("name"),
         //         new Field("value"),
         //         new Field("active"),
         //         new Field("created")
         //      ),
         //      new Arguments(
         //         new Argument(
         //            new Parameter("input")
         //         )
         //      )
         //   )
         //);
         //Car car = new Car
         //{
         //   Active = true,
         //   Purchase = DateTime.ParseExact("1999-01-02 01:01:01", @"yyyy-MM-dd HH:mm:ss", null),
         //   Title = "title",
         //   Value = 15000M,
         //   Time = TimeSpan.Parse("13:12:00")
         //};
         //TypeQL typeQL = new TypeQL(
         //      new Variables("getCars",
         //         new Variable("input", car, "car_input", true)
         //      ),
         //      new QueryType("car_add",
         //         new Fields(
         //            new Field("id"),
         //            new Field("title"),
         //            new Field("purchase"),
         //            new Field("value"),
         //            new Field("active"),
         //            new Field("time")
         //         ),
         //         new Arguments(
         //            new Argument(new Parameter("input"))
         //         )
         //      )
         //   );
         //Console.WriteLine(typeQL);

         #region test
         //Car car = new Car
         //{
         //   Active = true,
         //   Purchase = DateTime.ParseExact("1999-01-02 01:01:01",@"yyyy-MM-dd HH:mm:ss", null),
         //   Title = "title",
         //   Value = 15000M
         //};
         //TypeQL typeQL = new TypeQL(
         //      new Variables("getCars",
         //         new Variable("input", car, "car_input"),
         //         new Variable("id",1)
         //      ),
         //      new QueryType("car_add",
         //         new Fields(
         //            new Field("id"),
         //            new Field("title"),
         //            new Field("purchase"),
         //            new Field("value"),
         //            new Field("active")
         //         ),
         //         new Arguments(
         //            new Argument(new Parameter("input"))
         //         )
         //      )
         //   );
         //System.Console.WriteLine(typeQL);

         //TypeQL typeQL = new TypeQL(           
         //   new QueryType("state_find",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf"),
         //         new Field(
         //            new QueryType("country",
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("name")
         //               )
         //            )
         //         )
         //      ),
         //      new Arguments(new Argument("id", 11))
         //   ),
         //   new QueryType("state_find", "d",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf"),
         //         new Field(
         //            new QueryType("country",
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("name")
         //               )
         //            )
         //         )
         //      ),
         //      new Arguments(new Argument("id", 12))
         //   )
         //);
         //System.Console.WriteLine(typeQL);


         //TypeQL typeQL = new TypeQL(
         //   new Variables(
         //      "getStates",
         //      new Variable("id", 1, true, 0)
         //   ),
         //   new QueryType("state_find",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf"),
         //         new Field(
         //            new QueryType("country",
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("name")
         //               )
         //            )
         //         )
         //      ),
         //      new Arguments(new Argument(new Parameter("id")))
         //   ),
         //   new QueryType("state_find", "d",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf"),
         //         new Field(
         //            new QueryType("country", 
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("name")
         //               )
         //            )
         //         )
         //      ),               
         //      new Arguments(new Argument("id", 12))
         //   )            
         //);
         //System.Console.WriteLine(typeQL);

         //Car car = new Car
         //{
         //   Id = 0,
         //   Title = "Example",
         //   Active = true,
         //   Purchase = System.DateTime.ParseExact("1970-01-01 01:00:00", @"yyyy-MM-dd hh\:mm\:ss", CultureInfo.InvariantCulture),
         //   Value = 1000M
         //};
         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "car_add",
         //    new Arguments(new Argument("car", car)),
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

         //TypeQL typeQL = new TypeQL(
         //   new QueryType("states", "data",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf")
         //      )
         //   ),
         //   new QueryType("countries",
         //      new Fields(
         //         new Field("id"),
         //         new Field("name")
         //      )
         //   )
         //);
         //System.Console.WriteLine(typeQL);

         //TypeQL typeQL = new TypeQL(
         //   new QueryType("state_find",
         //      "data",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf")
         //      ),
         //      new Variables("getStateFind",
         //         new Variable("id", 1, true, 0)                  
         //      )
         //   )
         //);
         //System.Console.WriteLine(typeQL);


         //TypeQL typeQL = new TypeQL(
         //   new QueryType("states","data",
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf"),
         //         new Field(
         //            new QueryType("country",
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("name")
         //               )
         //            )
         //         )
         //      )
         //   ),
         //   new QueryType("countries",
         //      new Fields(
         //         new Field("id"),
         //         new Field("name")
         //      )
         //   )
         //);
         //System.Console.WriteLine(typeQL);

         #region Comments
         //TypeQL typeQL = new TypeQL(
         //   new QueryType("state_find",
         //      new Arguments(
         //         new Argument("id", 11),
         //         new Argument("load", true)
         //      ),
         //      new Fields(
         //         new Field("id"),
         //         new Field("uf"),
         //         new Field(
         //            new QueryType("country",
         //               new Arguments(new Argument("car", new Car())),
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("name")
         //               )
         //            )
         //         )
         //      )
         //   ),
         //   new QueryType("country_find","data",
         //      new Arguments(
         //         new Argument("id", 1),
         //         new Argument("load", true)
         //      ),
         //      new Fields(
         //         new Field("id"),
         //         new Field("name"),
         //         new Field(
         //            new QueryType("state",
         //               new Arguments(new Argument("car", new Car())),
         //               new Fields(
         //                  new Field("id"),
         //                  new Field("uf")
         //               )
         //            )
         //         )
         //      )
         //   )
         //);
         //System.Console.WriteLine(typeQL);

         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "states",
         //    new Arguments(
         //       new Argument("load", true)
         //    ),
         //    new Fields(
         //      new Field("id"),
         //      new Field("uf"),
         //      new FieldRelationship("country",
         //         new Fields(
         //            new Field("id"),
         //            new Field("name")
         //         )
         //      )
         //    )
         //  )
         //);
         //System.Console.WriteLine(typeQL);


         //AddCar
         //Car car = new Car()
         //{
         //   Id = 0,
         //   Title = "Car 1",
         //   Purchase = DateTime.Now.AddDays(-100),
         //   Value = 10000.00M,
         //   Active = true
         //};
         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "car_add",
         //    new Fields(
         //      new Field("id"),
         //      new Field("title"),
         //      new Field("purchase"),
         //      new Field("value"),
         //      new Field("active")
         //    ),
         //    new Arguments(
         //      new Argument("input", car)
         //    )             
         //  )
         //);
         //System.Console.WriteLine(typeQL);

         //Car Edit
         //Car car = new Car()
         //{
         //   Id = 1,
         //   Title = "Car 1",
         //   Purchase = DateTime.Parse("2019-08-14 23:54:18"),
         //   Value = 11000.00M,
         //   Active = true
         //};
         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "car_edit",
         //    new Arguments(new Argument("car", car)),
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

         //Car Find
         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "car_find",
         //    new Arguments(new Argument("id", 1)),
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
         //var item = new Items() { Id = Guid.Empty, Title = "Item 10", Updated = null };
         //TypeQL typeQL = new TypeQL(
         //  new QueryType(
         //    "item_add",
         //    "",
         //    new Arguments(
         //      new Argument("id", 1),
         //      new Argument("name", "Maria")
         //    ),
         //    new Fields(
         //      new Field("id"),
         //      new Field("title"),
         //      new Field("updated")
         //    )
         //  )
         //);
         //System.Console.WriteLine(typeQL);

         //DateTime? data = null;
         //var result0 = data?.GetType() == typeof(DateTime);
         //var result1 = Nullable.GetUnderlyingType(typeof(DateTime?));

         //System.Console.WriteLine(default(Guid));
         #endregion

         #endregion

      }
   }
}
