using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace MyChat
{
    public static class EnhancedTCPClient
    {
        public static void WriteString(this TcpClient client, string msg)
        {
            msg += '\0';

            //Console.WriteLine(msg);
            byte[] bytes = Encoding.ASCII.GetBytes(msg);
            var stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }



        public static string ReadString(this TcpClient client)
        {
            var bytes = new byte[client.ReceiveBufferSize];
            var stream = client.GetStream();
            stream.Read(bytes, 0, client.ReceiveBufferSize);
            var msg = Encoding.ASCII.GetString(bytes);
            return msg.Substring(0, msg.IndexOf("\0", StringComparison.Ordinal));
        }

        public static void WriteScore(this TcpClient client, int score)
        {
            string scorestring = score.ToString();
            byte[] bytes = Encoding.ASCII.GetBytes(scorestring);
            var stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
        }

        public static int ReadScore(this TcpClient client)
        {
            var bytes = new byte[client.ReceiveBufferSize];
            var stream = client.GetStream();
            stream.Read(bytes, 0, client.ReceiveBufferSize);
            var score = Encoding.ASCII.GetString(bytes);
            return Int32.Parse(score);

        }

       
    }
}
