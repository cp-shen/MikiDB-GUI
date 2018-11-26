using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MikiDB.Utils {

    public static class EnumerableExtension {
        public static IEnumerable<IList<TSource>> Split<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate) {
            var list = new List<TSource>();

            foreach (var element in source) {
                if (predicate(element)) {
                    if (list.Count > 0) {
                        yield return list;
                        list = new List<TSource>();
                    }
                }
                else {
                    list.Add(element);
                }
            }

            if (list.Count > 0) {
                yield return list;
            }
        }
    }
}