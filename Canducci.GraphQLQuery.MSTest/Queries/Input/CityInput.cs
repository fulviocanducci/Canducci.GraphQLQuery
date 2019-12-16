using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using HotChocolate.Types;
namespace Canducci.GraphQLQuery.MSTest.Queries.Input
{
   public class CityInput : InputObjectType<City>
   {
      protected override void Configure(IInputObjectTypeDescriptor<City> descriptor)
      {
         Name = "city_input";
         descriptor.Name("city_input");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.StateId).Name("stateId").Type<IntType>();
         descriptor.Field(x => x.State).Name("state").Type<StateInput>();
      }
   }
}
