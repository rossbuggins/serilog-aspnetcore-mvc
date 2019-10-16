using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Serilog.AspNetCore.Mvc
{
    public class DictionaryHelper : IDictionaryHelper
    {
        ILogger<DictionaryHelper> _logger;
        public DictionaryHelper(ILogger<DictionaryHelper> logger)
        {
            this._logger = logger;
        }

        public virtual IDictionary<string, object> Add(
          IDictionary<string, object> values,
          string key,
          object value)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (key == null)
                throw new ArgumentNullException(nameof(key));

            if (value == null)
            {
                var ex =new ArgumentNullException(nameof(value));
                ex.Data.Add("key", key);
                throw ex;
            }

            if (values.ContainsKey(key))
            {
                _logger.LogDebug("Replacing {key} from {existing} to {new} ", key, values[key], value);
                values[key] = value;
            }
            else
                values.Add(key, value);

            return values;
        }


        public virtual void Concatenate(
            IDictionary<string, object> values,
            string name1,
            string name2,
            string joinWith,
            string newName)
        {
            if (values == null)
                throw new ArgumentNullException(nameof(values));

            if (name1 == null)
                throw new ArgumentNullException(nameof(name1));

            if (name2 == null)
                throw new ArgumentNullException(nameof(name2));

            if (joinWith == null)
                throw new ArgumentNullException(nameof(joinWith));

            if (newName == null)
                throw new ArgumentNullException(nameof(newName));

            if (values.ContainsKey(name1) && values.ContainsKey(name2))
                Add(values,
                    newName,
                    values[name1] + joinWith + values[name2]);
            else
                _logger.LogDebug("Cannot concatenate {name1} and {name2} as one or more do not exist. ", name1, name2);
        }

        public virtual IDictionary<string, object> InitializeValueStore()
        {
            return new Dictionary<string, object>();
        }

    }
}
