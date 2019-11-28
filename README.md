# Canducci GraphQLQuery

[![Nuget](https://img.shields.io/nuget/v/Canducci.GraphQLQuery?style=plastic)](https://www.nuget.org/packages/Canducci.GraphQLQuery/)
[![Nuget](https://img.shields.io/nuget/dt/Canducci.GraphQLQuery?style=plastic)](https://www.nuget.org/packages/Canducci.GraphQLQuery/)
[![GitHub](https://img.shields.io/github/license/fulviocanducci/Canducci.GraphQLQuery?style=plastic)](https://github.com/fulviocanducci/Canducci.GraphQLQuery/blob/master/LICENSE)
[![Build Status](https://travis-ci.org/fulviocanducci/Canducci.GraphQLQuery.svg?branch=master)](https://travis-ci.org/fulviocanducci/Canducci.GraphQLQuery)
![Github Workflows](https://github.com/fulviocanducci/Canducci.GraphQLQuery/workflows/.NET%20Core/badge.svg)
## Package Installation

```code
PM> Install-Package Canducci.GraphQLQuery
```

## Examples:
 
__A Query Type Only__

###### Code



```c#
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

###### Result

```json
{"query":"{cars{id,title,purchase,value,active}}"}
```

#

__Multiple Query Type__

###### Code:

```c#
TypeQL typeQL = new TypeQL(
  new QueryType("states",
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

###### Example:

```json
{"query":"{data:states{id,uf}countries{id,name}}"}
```

#

__Query Type with Alias__

###### Code:

```c#
TypeQL typeQL = new TypeQL(
     new QueryType(
	 "states", 
	 "data", // <-alias
	  new Fields(
            new Field("id", "_id"), // <-alias
            new Field("uf", "_uf") // <-alias
          )
     )
);
```

###### Example:

```json
{"query":"{data:states{_id:id,_uf:uf}}"}
```

#

__Query Type with Argument Complex Type Class__


This type of argument, which is a complex type, is used for data creation and update operations, but can also be used for multi-parameter searches.

###### Code:

```C#
Car car = new Car
{
    Id = 0,
    Title = "Example",
    Active = true,
    Purchase = DateTime.ParseExact("1970-01-01 01:00:00", @"yyyy-MM-dd hh\:mm\:ss", CultureInfo.InvariantCulture),
    Value = 1000M
};
TypeQL typeQL = new TypeQL(
    new QueryType(
        "car_add",
        new Arguments(new Argument("car", car)),
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


###### Example:

```json
{"query":"{car_add(car:{id:0,title:\"Example\",purchase:\"1970-01-01 01:00:00\",value:1000,active:true}){id,title,purchase,value,active}}"}
```