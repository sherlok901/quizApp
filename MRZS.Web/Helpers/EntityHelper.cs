using System;
using System.Collections.Generic;
using System.Data.Objects.DataClasses;
using System.Linq;

namespace MRZS.Web.Helpers
{
    public static class EntityHelper
    {
        public static IEnumerable<EntityObject> Randomize(this IQueryable<EntityObject> entities)
        {
            return entities.ToArray().OrderBy(a => Guid.NewGuid());
        }
    }
}