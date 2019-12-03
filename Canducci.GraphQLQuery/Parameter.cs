﻿namespace Canducci.GraphQLQuery
{
   public class Parameter
   {
      public string Name { get;  }
      public Parameter(string name)
      {
         Name = name ?? throw new System.ArgumentNullException(nameof(name));
      }
   }
}