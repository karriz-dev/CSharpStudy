using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Logger
{
    [Serializable]
    abstract class Packet
    {
        private long _timestamp = 0L;
        public long timestamp
        {
            get { return _timestamp; }
            set { _timestamp = value; }
        }

        private short _type = 0x0000;
        public short type
        {
            get { return _type; }
            set { _type = value; }
        }

        private Priority _priority;
        public Priority priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        public enum Priority
        {
            LOW = 0,
            NORMAL = 1,
            HIGH = 2,
        }

        public Packet(short type, Priority priority = Priority.NORMAL)
        {
            var timeSpan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));

            this.timestamp = (long)timeSpan.TotalSeconds;
            this.type = type;
            this.priority = priority;
        }
    }
}