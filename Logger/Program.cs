using System;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace Logger
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger.isDebug = true;
            Logger.isLogFileSave = false;
            Logger.LogLevel = Logger.DEBUG_LEVEL.ERROR;

            var methodInfo = System.Reflection.MethodBase.GetCurrentMethod();
            var tag = "class: " + methodInfo.DeclaringType.FullName + ", method: " + methodInfo.Name;

            try
            {
                DateTime prevTime = DateTime.Now;

                // 1. memory stream에 저장
                MemoryStream prevSendStream = new MemoryStream();

                // 2. 직렬화 진행
                BinaryFormatter serializer = new BinaryFormatter();

                Packet packet = new TestPacket("Hello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello SerrueglaiurbHello Serrueglaiurbgugializing");

                serializer.Serialize(prevSendStream, packet);
                prevSendStream.Position = 0;

                // 3. 압축
                /*
                    [ INFO, 2020. 3. 23. 오후 4:40:24 ] class: Logger.Program, method: Main => Deflate Compressed from 288 to 198bytes
                    [ INFO, 2020. 3. 23. 오후 4:40:24 ] class: Logger.Program, method: Main => GZip Compressed from 288 to 216bytes
                
                    실험 결과 Deflate Compress가 더 좋은 성능으로 데이터 압축을 함
                */
                byte[] compressedByte = Compress(prevSendStream.ToArray());

                Logger.Info(tag, "compress " + prevSendStream.ToArray().Length + " to " + compressedByte.Length + " bytes.");

                // 4. 압축 풀기
                MemoryStream decompressedStream = Decompress(compressedByte);
                decompressedStream.Position = 0;

                // 5. 역직렬화 진행
                Packet obj = (TestPacket)serializer.Deserialize(decompressedStream); // 역 직렬화

                Logger.Info(tag, obj.ToString());
                Logger.Info(tag, (DateTime.Now - prevTime).ToString());
            }
            catch (Exception ex)
            {
                Logger.Error(tag + "=>\r\n" + ex.StackTrace, ex);
            }
        }
        public static Byte[] Compress(Byte[] buffer)
        {
            Byte[] compressedByte;
            using (MemoryStream ms = new MemoryStream())
            {
                using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Compress))
                {
                    ds.Write(buffer, 0, buffer.Length);
                }

                compressedByte = ms.ToArray();
            }
            return compressedByte;
        }

        public static MemoryStream Decompress(Byte[] buffer)
        {
            MemoryStream resultStream = new MemoryStream();
            using (MemoryStream ms = new MemoryStream(buffer))
            {
                using (DeflateStream ds = new DeflateStream(ms, CompressionMode.Decompress))
                {
                    ds.CopyTo(resultStream);
                    ds.Close();
                }
            }
            return resultStream;
        }
    }
}
