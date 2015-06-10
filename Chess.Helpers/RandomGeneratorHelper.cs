using System;
using System.Collections.Generic;

namespace Chess.Helpers
{
    public static class RandomGeneratorHelper
    {
        private static readonly Random Random = new Random();

        public static TResult GetRandomValueFromList<TResult>(IList<TResult> list)
        {
            if (list == null || list.Count == 0) return default(TResult);
            var index = Random.Next(0, list.Count);
            return list[index];
        }

        public static bool GetRandomBoolValue()
        {
            return Convert.ToBoolean(Random.Next(0, 2));
        }
    }
}
