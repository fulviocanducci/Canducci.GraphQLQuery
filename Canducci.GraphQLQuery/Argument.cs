using Canducci.GraphQLQuery.Interfaces;
using System;

namespace Canducci.GraphQLQuery
{
  public class Argument<T> : IArgument
  {
    public string Name { get; private set; }
    public object Value { get; private set; }
    public Type TypeValue { get; private set; }
    public ArgumentFormat ArgumentFormat { get; private set; }
    public Argument(string name, T value)      
    {
      Name = name;
      Value = value;
      ArgumentFormat = ArgumentFormat.None;
      TypeValue = typeof(T);
    }
    public Argument(string name, T value, ArgumentFormat argumentFormat)
    {
      Name = name;
      Value = value;
      ArgumentFormat = argumentFormat;
      TypeValue = typeof(T);
    }
    public static Argument<T> Create(string name, T value) => Create(name, value, ArgumentFormat.None);    
    public static Argument<T> Create(string name, T value, ArgumentFormat argumentFormat)
    {
      return new Argument<T>(name, value, argumentFormat)
      {
        TypeValue = typeof(T)
      };
    }
  }
}