using System.Collections.Generic;
using System.Linq;

namespace EVENicknameBot
{
    internal static class UrlHelper
    {
        internal static string GetUrlWithParameters(Method method, string url, Dictionary<string, string> parameters)
        {
            url += method;

            if (parameters.Count != 0)
            {
                url += "?";
            }

            foreach (var key in parameters.Keys)
            {
                url += $"{key}={parameters[key]}&";
            }

            return url;
        }
    }
}
