using Canducci.GraphQLQuery.CustomTypes;
using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using Canducci.GraphQLQuery.MSTest.Queries.Input;
using Canducci.GraphQLQuery.MSTest.Queries.Types;
using HotChocolate.Types;
using System;
using System.Linq;

namespace Canducci.GraphQLQuery.MSTest.Queries
{
   public partial class Query : ObjectType
   {
      public Sources Sources { get; }
      public Cities Cities { get; }
      public States States { get; }
      public Cars Cars { get; }
      public Query()
      {
         Name = "Query";         
         Sources = new Sources();
         Cities = new Cities();
         States = new States();
         Cars = new Cars();
      }
      protected override void Configure(IObjectTypeDescriptor descriptor)
      {         
         ConfigureTypeSource(descriptor);
         ConfigureTypeState(descriptor);
         ConfigureTypeCity(descriptor);
         ConfigureTypeCar(descriptor);
      }

      private void ConfigureTypeCar(IObjectTypeDescriptor descriptor)
      {
         descriptor
           .Field("cars")
           .Type<ListType<CarType>>()
           .Resolver(context =>
           {
              return Cars.ToList();
           });

         descriptor
           .Field("car_add")
           .Argument("input", x => x.Type<CarInput>())
           .Type<CarType>()
           .Resolver(context =>
           {
              Car car = context.Argument<Car>("input");
              Cars.AddCar(car);
              return car;
           });
      }

      private void ConfigureTypeCity(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("cities")
            .Type<ListType<CityType>>()
            .Resolver(context =>
            {
               return Cities.ToList();
            });

         descriptor
            .Field("city_find")
            .Argument("id", x => x.Type<IntType>())
            .Type<CityType>()
            .Resolver(context =>
            {
               int id = context.Argument<int>("id");
               return Cities.Where(x => x.Id == id).FirstOrDefault();
            });
      }

      private void ConfigureTypeState(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("states")
            .Type<ListType<StateType>>()
            .Resolver(context =>
            {
               return States;
            });

         descriptor
            .Field("state_add")
            .Argument("input", x => x.Type<StateInput>())
            .Type<StateType>()
            .Resolver(context =>
            {
               State state = context.Argument<State>("input");               
               return States.AddState(state);
            });

         descriptor
            .Field("state_find")
            .Argument("input", x => x.Type<IntType>())
            .Type<StateType>()
            .Resolver(context =>
            {
               int id = context.Argument<int>("id");
               return States.Where(x => x.Id == id).FirstOrDefault();
            });
      }

      private void ConfigureTypeSource(IObjectTypeDescriptor descriptor)
      {
         descriptor
            .Field("sources")
            .Type<ListType<SourceType>>()
            .Resolver(context =>
            {               
               return Sources;
            });

         descriptor
           .Field("source_add")
           .Type<SourceType>()
           .Argument("input", x => { x.Type<SourceInput>(); })
           .Resolver(context =>
           {
              Source source = context.Argument<Source>("input");
              source.Id = Sources.Count == 0 ? 1 : Sources.OrderBy(x => x.Id).LastOrDefault().Id;
              Sources.Add(source);
              return source;
           });


         descriptor
           .Field("source_param_add")
           .Type<SourceType>()
           .Argument("id", x => { x.Type<IntType>(); })
           .Argument("name", x => { x.Type<StringType>(); x.DefaultValue(null); })
           .Argument("value", x => { x.Type<DecimalType>(); x.DefaultValue(null); })
           .Argument("created", x => { x.Type<DateTimeType>(); x.DefaultValue(null); })
           .Argument("active", x => { x.Type<BooleanType>(); x.DefaultValue(null); })
           .Argument("time", x => { x.Type<TimeSpanType>(); x.DefaultValue(null); })
           .Resolver(context =>
           {
              int id = context.Argument<int>("id");
              string name = context.Argument<string>("name");
              decimal? value = context.Argument<decimal?>("value");
              DateTime? created = context.Argument<DateTime?>("created");
              bool? active = context.Argument<bool?>("active");
              TimeSpan? time = context.Argument<TimeSpan?>("time");
              if (id == 0)
              {
                 id = Sources.Count == 0 ? 1 : Sources.OrderBy(x => x.Id).LastOrDefault().Id;
              }
              Source source = new Source()
              {
                 Id = id,
                 Name = name,
                 Value = value,
                 Created = created,
                 Active = active,
                 Time = time
              };
              Sources.Add(source);
              return source;
           });


         descriptor
           .Field("source_edit")
           .Type<SourceType>()
           .Argument("input", x => { x.Type<SourceInput>(); })
           .Resolver(context =>
           {
              Source source = context.Argument<Source>("input");
              Source update = Sources.FirstOrDefault(x => x.Id == source.Id);
              if (update != null)
              {
                 update.Name = source.Name;
                 update.Time = source.Time;
                 update.Value = source.Value;
                 update.Active = source.Active;
                 update.Created = source.Created;
                 return source;
              }
              return update;
           });

         descriptor
           .Field("source_find")
           .Type<SourceType>()
           .Argument("id", x => { x.Type<IntType>(); })
           .Resolver(context =>
           {
              int id = context.Argument<int>("id");
              return Sources.FirstOrDefault(x => x.Id == id);
           });

         descriptor
           .Field("source_remove")
           .Type<RemoveType>()
           .Argument("id", x => { x.Type<IntType>(); x.DefaultValue(0); })
           .Resolver(context =>
           {
              int id = context.Argument<int>("id");
              int count = 0;
              Source source = Sources.FirstOrDefault(x => x.Id == id);
              if (source != null)
              {
                 count = Sources.Remove(source) ? 1 : 0;
              }
              return Remove.Create(count);
           });

         descriptor
           .Field("source_in")
           .Type<ListType<SourceType>>()
           .Argument("id_in", x => { x.Type<ListType<IntType>>(); })
           .Resolver(context =>
           {
              int[] id_in = context.Argument<int[]>("id_in");
              return Sources.Where(x => id_in.Contains(x.Id)).ToList();
           });
      }
   }
}
