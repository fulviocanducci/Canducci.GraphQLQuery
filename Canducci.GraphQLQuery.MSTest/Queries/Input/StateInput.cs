using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using Canducci.GraphQLQuery.MSTest.Queries.Types;
using HotChocolate.Types;
namespace Canducci.GraphQLQuery.MSTest.Queries.Input
{
   public class StateInput : InputObjectType<State>
   {
      protected override void Configure(IInputObjectTypeDescriptor<State> descriptor)
      {
         Name = "state_input";
         descriptor.Name("state_input");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.Cities).Name("cities").Type<ListType<CityInput>>();
      }
   }
}
