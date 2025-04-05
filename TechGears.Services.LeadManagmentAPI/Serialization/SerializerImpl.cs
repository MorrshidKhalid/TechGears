using System.Runtime.Serialization.Json;
using System.Text;

namespace TechGears.Services.LeadManagmentAPI.SerializDeserializ
{
    public class SerializerImpl : ISerializer
    {
        public bool Deserializ(string json)
        {
            DataContractJsonSerializer serializer = new(typeof(bool));
            using MemoryStream stream = new(Encoding.UTF8.GetBytes(json));
            
            return (bool)serializer.ReadObject(stream); ;
        }
    }
}
