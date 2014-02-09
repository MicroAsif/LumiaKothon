using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LumiaKothon.FacebookTools
{
    public static class KeyValuePairUtils
    {
        
        public static TValue GetValue<TKey, TValue>(this IEnumerable<KeyValuePair<TKey, TValue>> pairs, TKey key)
        {
            try
            {
                 foreach (KeyValuePair<TKey, TValue> pair in pairs)
            {
                if (key.Equals(pair.Key))
                    return pair.Value;
            }

            throw new Exception("the value is not found in the dictionary");
            
            }
            catch (Exception)
            {
                 throw new Exception("the value is not found in the dictionary");
                
            }
        }
    }
}
