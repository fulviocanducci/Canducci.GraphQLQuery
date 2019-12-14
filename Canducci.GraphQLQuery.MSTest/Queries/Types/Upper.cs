using System.Globalization;

namespace Canducci.GraphQLQuery.MSTest.Queries.Types
{
   public class Upper : Interfaces.IDirective
   {
      public string Layout
      {
         get
         {
            return "@{0}";
         }
      }

      public string Name { get; } = "upper";

      public string Convert()
      {
         return string.Format(CultureInfo.InvariantCulture, Layout, Name);
      }
   }
}
