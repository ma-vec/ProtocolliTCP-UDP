using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

// Socket Listener acts as a server and listens to the incoming
// messages on the specified port and protocol.
public class SocketListener
{
    public static int Main(String[] args)
    {
        StartServer();
        return 0;
    }

    public static void StartServer()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);
        string result="4";

        try
        {
            Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            listener.Bind(localEndPoint);
            listener.Listen(10);
            Console.WriteLine("Waiting for a connection...");

            while (true)
            {
                // Aspetta una connessione in modo continuo
                Socket handler = listener.Accept();
                string data = null;
                byte[] bytes = new byte[1024];

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data != null)
                    {
                        break;
                    }
                }

                Console.WriteLine("Text received: {0}", data);
                //DOMANDA 1
                byte[] q1 = Encoding.ASCII.GetBytes("Quale e' la capitale della Norvegia?\n");
                handler.Send(q1);
                byte[] qd1 = Encoding.ASCII.GetBytes("1) Oslo\n");
                handler.Send(qd1);
                byte[] qd2 = Encoding.ASCII.GetBytes("2) Botttanuco\n");
                handler.Send(qd2);
                byte[] qd3 = Encoding.ASCII.GetBytes("3) Otranto\n");
                handler.Send(qd3);
              

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
                    if (data != null)
                    {
                        break;
                    }

                }
                string[] arrayChars = data.Split(",");
                if (arrayChars[0]=="1")
                {
                    if (arrayChars[1] == "1")
                        result = "BRAVO BOMBERONE";
                    else result = "PAGLIACCIOOOO";
                }

                byte[] risultato = Encoding.ASCII.GetBytes(result);
                handler.Send(risultato);

                handler.Shutdown(SocketShutdown.Both);
                handler.Close();
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }

        Console.WriteLine("\nPress any key to continue...");
        Console.ReadKey();
    }

}