using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using HotChocolate.Types;

namespace Canducci.GraphQLQuery.MSTest.Queries.Types
{
   public class StateType : ObjectType<State>
   {
      protected override void Configure(IObjectTypeDescriptor<State> descriptor)
      {
         Name = "state_type";
         descriptor.Name("state_type");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Name).Name("name").Type<StringType>();
         descriptor.Field(x => x.Cities).Name("cities").Type<ListType<CityType>>();
      }
   }
}
