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
      public Query()
      {
         Name = "Query";         
         Sources = new Sources();
      }
      protected override void Configure(IObjectTypeDescriptor descriptor)
      {         
         ConfigureTypeSource(descriptor);
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
