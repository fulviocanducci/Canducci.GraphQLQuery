using Canducci.GraphQLQuery.Abstracts;

namespace Canducci.GraphQLQuery
{
   public class Include : Directive
   {
      public Include(string name)
         : base(name)
      {
      }
      public override string Layout
      {
         get
         {
            return "@include(if:${0})";
         }
      }

      public static Include Create(string name) => new Include(name);
   }
}
