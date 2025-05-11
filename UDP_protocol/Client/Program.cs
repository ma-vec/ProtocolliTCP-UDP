// Client-Side Implementation Of UDP: 
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class StudentUDPClient
{
    public static void Main(string[] args)
    {
        UdpClient udpc = new UdpClient("127.0.0.1", 7878);
        IPEndPoint ep = null;
        while (true)
        {
            Console.Write("Enter Your Name: ");
            string studentName = Console.ReadLine();


            // Check weather student entered name to start conversation 
            if (studentName == "")
            {
                Console.Write("You did not enter your name. Closing...");
                break;
            }
            // Data to send 
            byte[] msg = Encoding.ASCII.GetBytes(studentName);
            udpc.Send(msg, msg.Length);

            // received Data 
            byte[] rdata = udpc.Receive(ref ep);
            string job = Encoding.ASCII.GetString(rdata);
            Console.WriteLine(job);
        }
    }
}

