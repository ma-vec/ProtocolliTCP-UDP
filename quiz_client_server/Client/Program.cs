using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace quiz_client_server
{
    class Program
    {
        static void Main(string[] args)
        {
            StartClient();
        }

        public static void StartClient()
        {
            byte[] bytes = new byte[1024];

            try
            {
                // Ottieni l'indirizzo IP del server
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 11000);

                // Crea un socket TCP/IP
                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

                // Connetti il socket all'endpoint remoto
                sender.Connect(remoteEP);
                Console.WriteLine("Socket connesso a {0}", sender.RemoteEndPoint.ToString());

                // Messaggio iniziale da inviare
                string strToSend = "ciao";

                // Codifica la stringa in un array di byte
                byte[] msg = Encoding.ASCII.GetBytes(strToSend);
                int bytesSent = sender.Send(msg);
                Console.WriteLine("Messaggio inviato: {0}", strToSend);

                // Ricevi la risposta dal server
                int bytesRec = sender.Receive(bytes);
                Console.WriteLine("Risposta ricevuta: {0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));

                // Richiedi input all'utente
                Console.WriteLine("Inserisci la tua scelta:");
                /*Console.WriteLine("{0}", Encoding.ASCII.GetString(bytes, 0, bytesRec));
                int S1 = sender.Receive(bytes);
                Console.WriteLine("{0}", Encoding.ASCII.GetString(bytes, 0, S1));
                int S2 = sender.Receive(bytes);
                Console.WriteLine("{0}", Encoding.ASCII.GetString(bytes, 0, S2));
                int S3 = sender.Receive(bytes);
                Console.WriteLine("{0}", Encoding.ASCII.GetString(bytes, 0, S3));*/
                string scelta = "1,1";

                // Invia la scelta al server
                byte[] ris = Encoding.ASCII.GetBytes(scelta);
                int bytesSent1 = sender.Send(ris);
                Console.WriteLine("Scelta inviata: {0}", scelta);

                // Rilascia il socket
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }
        }
    }
}