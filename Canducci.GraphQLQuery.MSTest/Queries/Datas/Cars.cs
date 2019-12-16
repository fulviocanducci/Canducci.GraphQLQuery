using System;
using System.Collections.Generic;
using System.Linq;

namespace Canducci.GraphQLQuery.MSTest.Queries.Datas
{
   public class Cars: List<Car>
   {
      public Cars()
      {
         Add(new Car() { Id = 1, Value = 0, Title = "Car 1", Purchase = DateTime.Now.AddDays(-100).Date });
         Add(new Car() { Id = 2, Value = 0, Title = "Car 1", Purchase = DateTime.Now.AddDays(-100).Date, Time = TimeSpan.Parse("14:00:00") });
      }
      public Car AddCar(Car car)
      {
         car.Id = this.LastOrDefault().Id + 1;
         return car;
      }
   }
}
