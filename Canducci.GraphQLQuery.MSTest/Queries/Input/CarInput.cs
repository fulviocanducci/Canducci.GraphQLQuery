using Canducci.GraphQLQuery.CustomTypes;
using Canducci.GraphQLQuery.MSTest.Queries.Datas;
using HotChocolate.Types;
namespace Canducci.GraphQLQuery.MSTest.Queries.Input
{
   public class CarInput : InputObjectType<Car>
   {
      protected override void Configure(IInputObjectTypeDescriptor<Car> descriptor)
      {
         Name = "car_input";
         descriptor.Name("car_input");
         descriptor.Field(x => x.Id).Name("id").Type<IntType>();
         descriptor.Field(x => x.Title).Name("title").Type<StringType>();
         descriptor.Field(x => x.Active).Name("active").Type<BooleanType>();
         descriptor.Field(x => x.Time).Name("time").Type<TimeSpanType>();
         descriptor.Field(x => x.Value).Name("value").Type<DecimalType>();
         descriptor.Field(x => x.Purchase).Name("purchase").Type<DateType>();
      }
   }
}
