using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using HotChocolate.Types;

namespace Canducci.GraphQLQuery.MSTest.Queries.Types
{
   public class CityType : ObjectType<City>
   {
      protected override void Configure(IObjectTypeDescriptor<City> descriptor)
      {
         Name = "city_type";
         descriptor.Name("city_type");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.StateId).Name("stateId").Type<IntType>();
         descriptor.Field(x => x.State).Name("state").Type<StateType>();
      }
   }
}
