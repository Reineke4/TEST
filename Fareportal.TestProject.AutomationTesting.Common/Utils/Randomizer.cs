using System;

namespace Fareportal.TestProject.AutomationTesting.Common.Utils
{
    public static class Randomizer
    {
        private static readonly Random Random = new Random();
        private static readonly object SyncLock = new object();

        /// <summary>
        /// Return random number from range
        /// </summary>
        public static int RandomNumber(int min, int max)
        {
            lock (SyncLock)
            {
                return Random.Next(min, max + 1);
            }
        }
    }
}