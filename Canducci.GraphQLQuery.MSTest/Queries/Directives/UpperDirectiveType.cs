using HotChocolate.Types;
namespace Canducci.GraphQLQuery.MSTest.Queries.Directives
{
   public class UpperDirectiveType : DirectiveType
   {
      protected override void Configure(IDirectiveTypeDescriptor descriptor)
      {
         descriptor.Name("upper");
         descriptor.Description("Upper case letters");
         descriptor.Location(DirectiveLocation.Field);
         descriptor.Location(DirectiveLocation.FragmentSpread);
         descriptor.Location(DirectiveLocation.InlineFragment);

         descriptor.Use(next => async context =>
         {
            await next.Invoke(context);
            if (context.Result is string s)
            {
               context.Result = s.ToUpper();
            }
         });
      }
   }
}
