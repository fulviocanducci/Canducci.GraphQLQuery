using Canducci.GraphQLQuery.Interfaces;
namespace Canducci.GraphQLQuery
{
   public class TypeQL : ITypeQL
   {
      public ITypeQLConfiguration Configuration { get; private set; }
      public IQueryType[] QueryTypes { get; private set; }
      public TypeQL(params IQueryType[] queryTypes)
        : this(new TypeQLConfiguration(Separation.Comma, ArgumentFormat.FormatDateTime), queryTypes) { }
      public TypeQL(ITypeQLConfiguration configuration, params IQueryType[] queryTypes)
      {
         QueryTypes = queryTypes;
         Configuration = configuration;
      }
      public string ToStringJson()
      {
         using (Builder stringSourceBuilder = new Builder())
         {
            stringSourceBuilder
                .AppendKeyOpen()
                .AppendQuotationMark()
                .AppendQuery()
                .AppendQuotationMark()
                .AppendPoints()
                .AppendQuotationMark()
                .AppendKeyOpen();
            foreach (IQueryType item in QueryTypes)
            {
               stringSourceBuilder
                 .AppendQueryType(item)
                 .AppendScalarArguments(item.Arguments, Configuration)
                 .AppendFields(item.Fields, Configuration);
            }
            stringSourceBuilder
              .AppendKeyClose()
              .AppendQuotationMark()
              .AppendKeyClose();
            return stringSourceBuilder.ToStringJson();
         }
      }
      public static implicit operator string(TypeQL typeQL)
      {
         return typeQL.ToStringJson();
      }
      public void Dispose()
      {
         Configuration = null;
         QueryTypes = null;
         System.GC.SuppressFinalize(this);
      }
   }
}
