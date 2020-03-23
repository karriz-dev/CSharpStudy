using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Logger
{
    [Serializable]
    class TestPacket : Packet
    {
        private string message = null;

        public TestPacket(string message) : base(0x0001)
        {
            this.message = message;
        }

        public override string ToString()
        {
            return message;
        }
    }
}