using System;
using System.Collections.Generic;
using System.Text;

namespace Bambora.Common
{
    public class Validator
    {
        public static T ThrowIfNull<T>(T obj, string name) where T : class
        {
            if (obj == null)
            {
                throw new ArgumentNullException(name);
            }

            return obj;
        }
    }
}
