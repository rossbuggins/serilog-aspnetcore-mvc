using System.Collections.Generic;

namespace Serilog.AspNetCore.Mvc
{
    public interface IDictionaryHelper
    {
        IDictionary<string, object> Add(IDictionary<string, object> values, string key, object value);
        void Concatenate(IDictionary<string, object> values, string name1, string name2, string joinWith, string newName);
        IDictionary<string, object> InitializeValueStore();
    }
}