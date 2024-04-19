using System;
using System.Runtime.Serialization;


namespace Prueba.Core.Responses
{
    [Serializable]
    [DataContract]
    public class ResponseQuery<T>
    {
        [DataMember]
        public bool Exitosos { get; set; }
        [DataMember]
        public T Result { get; set; }
        [DataMember]
        public string Mensaje { get; set; }        
    }
}
