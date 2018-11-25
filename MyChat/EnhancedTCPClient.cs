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
        /// <summary>
        /// A function convert string to bytes and write the bytes to the stream
        /// If it is a score, add dashes/pluses. If it is a text add '\0' (for differentiating purpose)
        /// </summary>
        /// <param name="str">The string to convert</param>
        /// <param name="isScore">True if it is a score (number)</param>
        /// <param name="clientWhich">Which client triggered</param>
        public static void WriteString(this TcpClient client, string str, bool isScore, int clientWhich)
        {
            if (isScore)
            {
                if (clientWhich == 1) {
                    // mark player 1 with dashes
                    str += "--";
                }
                else if (clientWhich == 2) {
                    // mark player 2 with pluses
                    str += "++";
                }
            } else { str += '\0'; }
            
            // Convert text/score to bytes
            byte[] bytes = Encoding.ASCII.GetBytes(str);
            var stream = client.GetStream();
            // and Write to the stream
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        /// <summary>
        /// A function convert answer (true/false) to bytes and write the bytes to the stream
        /// </summary>
        /// <param name="answer">The answer to convert</param>
        public static void WriteAnswer(this TcpClient client, string answer)
        {
            // Convert answer (true/false) to bytes
            byte[] bytes = Encoding.ASCII.GetBytes(answer);
            var stream = client.GetStream();
            // and Write to the stream
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        /// <summary>
        /// A function convert bytes (true/false) from the stream and convert them back to string
        /// take out the '\0's at the end if needed
        /// </summary>
        public static string ReadString(this TcpClient client)
        {
            // get the bytes from the stream
            var bytes = new byte[client.ReceiveBufferSize];
            var stream = client.GetStream();
            stream.Read(bytes, 0, client.ReceiveBufferSize);
            var msg = Encoding.ASCII.GetString(bytes);
            // and return the string without '\0' parts
            return msg.Substring(0, msg.IndexOf("\0", StringComparison.Ordinal));
        }
        
    }
}
