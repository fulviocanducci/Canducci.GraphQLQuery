using Canducci.GraphQLQuery.Interfaces;
using System.Collections.Generic;
namespace Canducci.GraphQLQuery
{
  public class Fields: List<IField>
  {
    public Fields(params IField[] fields)
    {
      AddRange(fields);
    }
  }
}
