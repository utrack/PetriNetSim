using System.Collections.Generic;
using System.Linq;

namespace PetriNet
{
    /// <summary>
    /// This class contains static extensions over the Dictionary string,int used as token states.
    /// </summary>
    public static class MarkingDict
    {
        /// <summary>
        /// Compare two dicts for equality. Both dictionaries should have equal keyspace (!)
        /// </summary>
        /// <param name="dict1"></param>
        /// <param name="dict2"></param>
        /// <returns>True if equal, otherwise false.</returns>
        public static bool Compare(Dictionary<string, int> dict1, Dictionary<string, int> dict2)
        {
            return dict1.Keys.All(key => dict1[key] == dict2[key]);
        }
    }
}
