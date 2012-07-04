using System;
using System.Collections;
using System.Text;

namespace MRZS.Web
{
    public static class CollectionHelper
    {
        //public static string GetStringProperty(this IEnumerable collection, Func<object> func)
        //{
        //    StringBuilder result = new StringBuilder();


        //}

        public static string ToCommaSeparatedList<T>(this IEnumerable collection, Func<T, string> func)
        {
            var result = new StringBuilder();

            foreach (T item in collection)
            {
                result.Append(func(item)).Append(",");
            }
            return result.ToString().TrimEnd(',');
        }
    }
}