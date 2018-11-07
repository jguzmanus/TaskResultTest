using System;
using System.Collections;
using System.Collections.Generic;

namespace TaskResultTest
{
   public partial class Infinity : IEnumerable<object>
   {
      IEnumerator<object> IEnumerable<object>.GetEnumerator()
      {
         for (;;)
            yield return Infinity.Enumerable;
      }

      public IEnumerator GetEnumerator()
      {
         for (;;)
            yield return Infinity.Enumerable;
      }

      public Infinity LongCount(
         Func<object, bool> predicate = default(Func<object, bool>))
      {
         return Infinity.Enumerable;
      }

      public Infinity Count(
         Func<object, bool> predicate = default(Func<object, bool>))
      {
         return Infinity.Enumerable;
      }

      public static readonly Infinity Enumerable = new Infinity();
   }
}