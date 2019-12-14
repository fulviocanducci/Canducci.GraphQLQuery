using Canducci.GraphQLQuery.Interfaces;
using System;
using System.Globalization;

namespace Canducci.GraphQLQuery.Abstracts
{
   public abstract class Directive : IDirective, IDisposable
   {
      public abstract string Layout { get; }
      public string Name { get; private set; }

      public Directive()
      {        
      }

      public Directive(string name)
      {
         Name = name ?? throw new ArgumentNullException(nameof(name));
      }

      public virtual string Convert()
      {
         return string.Format(CultureInfo.InvariantCulture, Layout, Name);
      }

      public void Dispose()
      {
         Name = null;
         GC.SuppressFinalize(this);
      }

      public static Include Include(string name) => new Include(name);

      public static Skip Skip(string name) => new Skip(name);
   }
}
