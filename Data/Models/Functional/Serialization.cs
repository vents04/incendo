using System;
using System.Text.Json;

namespace Data.Models
{
    public class Serialization
    {
        public static T DeserializeInheritor<T>(string type, string json)
        {
            try
            {
                Type typeObj = typeof(T).Assembly.GetType(type);
                if (!typeof(T).IsAssignableFrom(typeObj)) throw new ContractException(ContractError.InvalidParameterType);
                return (T)JsonSerializer.Deserialize(json, typeObj);
            }
            catch
            {
                throw new ContractException(ContractError.InvalidParameter);
            }
        }

        public static byte[] DecodeBase64(string base64)
        {
            try
            {
                return Convert.FromBase64String(base64);
            }
            catch
            {
                throw new ContractException(ContractError.InvalidBase64Encoding);
            }
        }
    }
}