using System;
using Newtonsoft.Json;
using static System.Console;

namespace Fareportal.AutomationFramework.RestClient.Core
{
    public static class Deserializer
    {
        /// <summary>
        /// Deserialize object to target type
        /// </summary>
        public static TDeserializedObject GetDeserializedObject<TDeserializedObject>(string jsonstring)
            where TDeserializedObject : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Error;
            TDeserializedObject result;

            try
            {
                result = JsonConvert.DeserializeObject<TDeserializedObject>(jsonstring, settings);
            }
            catch (JsonSerializationException jsonException)
            {
                WriteErrorMessage(typeof(TDeserializedObject), jsonException, jsonstring);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Deserialize object to target type
        /// Missing members will be ignored
        /// </summary>
        public static TDeserializedObject GetDeserializedObjectWithoutMissingMembers<TDeserializedObject>(string jsonstring)
            where TDeserializedObject : class
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.MissingMemberHandling = MissingMemberHandling.Ignore;
            TDeserializedObject result;

            try
            {
                result = JsonConvert.DeserializeObject<TDeserializedObject>(jsonstring, settings);
            }
            catch (JsonSerializationException jsonException)
            {
                WriteErrorMessage(typeof(TDeserializedObject), jsonException, jsonstring);
                throw;
            }
            return result;
        }

        /// <summary>
        /// Write the error to the console
        /// </summary>
        /// <param name="type">Type to deserialize</param>
        /// <param name="jsonstring">Incoming json string</param>
        private static void WriteErrorMessage(Type type, JsonSerializationException jsonException, string jsonstring)
        {
            WriteLine("--------------------Json deserialization error-----------------------");
            WriteLine($"Unable to deserialize JSON string to: {type}");
            WriteLine($"Original error: {jsonException.Message}");
            WriteLine($"Incoming json string: \r\n{jsonstring}");
            WriteLine("------------------------Error message end!---------------------------");
            WriteLine();
        }
    }
}