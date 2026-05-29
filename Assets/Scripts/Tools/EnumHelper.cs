using UnityEngine;

namespace Tools
{
    public static class EnumHelper
    {
        public static T GetRandomEnumValue<T>() where T : System.Enum
        {
            var values = (T[])System.Enum.GetValues(typeof(T));
            var randomIndex = Random.Range(0, values.Length);

            return values[randomIndex];
        }
    }
}