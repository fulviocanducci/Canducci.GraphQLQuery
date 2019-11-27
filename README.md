# Canducci GraphQLQuery

[![Nuget](https://img.shields.io/nuget/v/Canducci.GraphQLQuery?style=plastic)](https://www.nuget.org/packages/Canducci.GraphQLQuery/)
[![Nuget](https://img.shields.io/nuget/dt/Canducci.GraphQLQuery?style=plastic)](https://www.nuget.org/packages/Canducci.GraphQLQuery/)
[![GitHub](https://img.shields.io/github/license/fulviocanducci/Canducci.GraphQLQuery?style=plastic)](https://github.com/fulviocanducci/Canducci.GraphQLQuery/blob/master/LICENSE)

## Package Installation

```
PM> Install-Package Canducci.GraphQLQuery
```

## Examples

### Code:

```csharp
TypeQL typeQL = new TypeQL(
  new QueryType(
    "cars",          
    new Fields(
      new Field("id"),
      new Field("title"),
      new Field("purchase"),
      new Field("value"),
      new Field("active")
    )
  )
);
```

### Result:

```json
{"query":"{cars{id,title,purchase,value,active}}"}
```

---

### Code:

```
TypeQL typeQL = new TypeQL(
  new QueryType("states", "data",
      new Fields(
        new Field("id"),
        new Field("uf")
      )
  ),
  new QueryType("countries",
      new Fields(
        new Field("id"),
        new Field("name")
      )
  )
);
```

### Example:

```json
{"query":"{data:states{id,uf}countries{id,name}}"}
```