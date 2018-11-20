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
        public static void WriteString(this TcpClient client, string msg, bool isScore, int clientWhich)
        {
            if (isScore)
            {
                if (clientWhich == 1)
                {
                    msg += "--";
                }
                else if (clientWhich == 2)
                {
                    msg += "++";
                }
            } else
            {
                msg += '\0';
            }
            
            //Console.WriteLine(msg);
            byte[] bytes = Encoding.ASCII.GetBytes(msg);
            var stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();
        }

        public static void WriteAnswer(this TcpClient client, string answer)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(answer);
            var stream = client.GetStream();
            stream.Write(bytes, 0, bytes.Length);
            stream.Flush();

        }

        public static string ReadAnswer(this TcpClient client)
        {
            var bytes = new byte[client.ReceiveBufferSize];
            var stream = client.GetStream();
            stream.Read(bytes, 0, client.ReceiveBufferSize);
            var answer = Encoding.ASCII.GetString(bytes);
            return answer;

        }



        public static string ReadString(this TcpClient client)
        {
            var bytes = new byte[client.ReceiveBufferSize];
            Console.WriteLine("dash byte: " + bytes[bytes.Length - 1]);
            var stream = client.GetStream();
            stream.Read(bytes, 0, client.ReceiveBufferSize);
            var msg = Encoding.ASCII.GetString(bytes);
            return msg.Substring(0, msg.IndexOf("\0", StringComparison.Ordinal));
        }

        //public static void updateScoreBoard(this TcpClient client, int score)
        //{
        //    ((MainWindow)System.Windows.Application.Current.MainWindow).Player1Score.Content = score.ToString();
        //}

        
       
    }
}
