using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using HotChocolate.Types;

namespace Canducci.GraphQLQuery.MSTest.Queries.Types
{
   public class RemoveType : ObjectType<Remove>
   {
      protected override void Configure(IObjectTypeDescriptor<Remove> descriptor)
      {
         Name = "remove_type";
         descriptor.Name("remove_type");
         descriptor.Field(x => x.Status).Name("status").Type<BooleanType>();
         descriptor.Field(x => x.Count).Name("count").Type<IntType>();
         descriptor.Field(x => x.Description).Name("description").Type<StringType>();
      }
   }
}
