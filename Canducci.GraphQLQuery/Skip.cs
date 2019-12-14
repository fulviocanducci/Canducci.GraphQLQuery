using Canducci.GraphQLQuery.Abstracts;
namespace Canducci.GraphQLQuery
{
   public class Skip : Directive
   {
      public Skip(string name)
         : base(name)
      {
      }

      public override string Layout
      {
         get
         {
            return "@skip(if:${0})";
         }
      }

      public static Skip Create(string name) => new Skip(name);
   }
}
