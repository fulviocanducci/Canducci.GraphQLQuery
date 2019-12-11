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
        "sources",
        new Fields(
        new Field("id"),
        new Field("name"),
        new Field("value"),
        new Field("created"),
        new Field("active"),
        new Field("time")
        )
    )
);
```

###### Result

```json
{"query":"{sources{id,name,value,created,active,time}}"}
```

#

__Multiple Query Type__

###### Code:

```c#
TypeQL typeQL = new TypeQL(
    new QueryType(
        "sources",
        new Fields(
        new Field("id"),
        new Field("name"),
        new Field("value"),
        new Field("created"),
        new Field("active"),
        new Field("time")
        )
    ),
    new QueryType(
        "states",
        new Fields(
        new Field("id"),
        new Field("uf")
        )
    )
);
```

###### Example:

```json
{"query":"{sources{id,name,value,created,active,time}states{id,uf}}"}
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
Source source = new Source
{
    Id = null,
    Active = true,
    Created = DateTime.Now,
    Name = "Source 1",
    Time = null,
    Value = 1000M
};
TypeQL typeQL = new TypeQL(
    new QueryType(
        "source_add",
        new Fields(
        new Field("id"),
        new Field("active"),
        new Field("created"),
        new Field("name"),
        new Field("time"),
        new Field("value")
        ),
        new Arguments(new Argument("input", source))
    )
);
```


###### Example:

```json
{"query":"{car_add(car:{id:0,title:\"Example\",purchase:\"1970-01-01 01:00:00\",value:1000,active:true}){id,title,purchase,value,active}}"}
```

__Query Type with Variables Complex Type Class__

This complex type of variable is used for data creation and update operations, but can also be used for multi-parameter searches.

###### Code:

```c#
Source source = new Source
{
    Id = null,
    Active = true,
    Created = DateTime.Now,
    Name = "Source 1",
    Time = null,
    Value = 1000M
};
TypeQL typeQL = new TypeQL(
    new Variables(
        "getSourceAdd",
        new Variable<object>("input", source, "source_input", true)
    ),
    new QueryType(
        "source_add",
        new Fields(
        new Field("id"),
        new Field("active"),
        new Field("created"),
        new Field("name"),
        new Field("time"),
        new Field("value")
        ),
        new Arguments(new Argument("input", new Parameter("input")))
    )
);
```

###### Result:

```json
{"query":"query getSourceAdd($input:source_input!){source_add(input:$input){id,active,created,name,time,value}}","variables":{"input":{"id":null,"name":"Source 1","value":1000,"created":"2019-12-09T20:52:30.3620293-03:00","active":true,"time":null}}}
```

## Other

###### Code:

```c#
TypeQL typeQL = new TypeQL(
    new Variables("getAll",
        new Variable<int>("state_id", 11),
        new Variable<int>("country_id", 1)
    ),
    new QueryType("state_find", "state",
        new Fields(
            new Field("id"),
            new Field("uf")
        ),
        new Arguments(new Argument(new Parameter("id", "state_id")))
    ),
    new QueryType("country_find", "country",
        new Fields(
            new Field("id"),
            new Field("name")
        ),
        new Arguments(new Argument(new Parameter("id", "country_id")))
    )
);
```
###### Result:

```json
{"query":"query getAll($state_id:Int,$country_id:Int){state:state_find(id:$state_id){id,uf}country:country_find(id:$country_id){id,name}}","variables":{"state_id":11,"country_id":1}}
```

###### Code:

```c#
TypeQL typeQL = new TypeQL(
new QueryType(
        "states_in",
        new Fields(
            new Field("id"),
            new Field("uf")
        ),
        new Arguments(
            new Argument("ids", new int[] { 11, 12, 13 }),
            new Argument("load", false)
        )
    )
);
```
###### Result:
```json
{"query":"{states_in(ids:[11,12,13],load:false){id,uf}}"}
```