using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace QueueCore
{
    [Serializable]
    public class BinaryQueueMessage<T>
    {
        public DateTime DateTime { get; set; } = DateTime.Now;
        public T Data { get; set; }

        private static BinaryFormatter _binaryFormatter = new BinaryFormatter();
        public byte[] ToBytes()
        {
            using (var ms = new MemoryStream())
            {
                _binaryFormatter.Serialize(ms, this);
                return ms.ToArray();
            }
        }

        public static BinaryQueueMessage<T> FromBytes(byte[] bytes)
        {
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return _binaryFormatter.Deserialize(ms) as BinaryQueueMessage<T>;
            }
        }


    }
}
