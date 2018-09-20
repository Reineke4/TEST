using System.Collections.Generic;
using System.Linq;

namespace Fareportal.TestProject.AutomationTesting.Common.Utils
{
    public static class Extensions
    {
        /// <summary>
        /// Check if strings are equals (not case sensitive)
        /// </summary>
        /// <param name="actual">Current string</param>
        /// <param name="expected">Incomming string</param>
        public static bool IsEqualsTo(this string actual, string expected)
        {
            if (actual == null || expected == null)
                return false;
            return actual.ToLower().Trim().Equals(expected.ToLower().Trim());
        }

        /// <summary>
        /// Check if string contains (not case sensitive)
        /// </summary>
        /// <param name="actual">Current string</param>
        /// <param name="expected">Incomming string</param>
        public static bool IsContains(this string actual, string expected)
        {
            return actual.ToLower().Trim().Contains(expected.ToLower().Trim());
        }

        /// <summary>
        /// Check if array == null or has length == 0
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return (list == null || !list.Any());
        }
    }
}