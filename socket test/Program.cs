using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Collections;

namespace socket_test
{
    class Program
    {
        
        static void Main(string[] args)
        {
            try
            {
                Socket neuerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Raw, ProtocolType.Unspecified);
                IPAddress myIpAdress;
                myIpAdress = IPAddress.Parse("192.168.178.20");
                IPEndPoint myEndpoint = new IPEndPoint(myIpAdress, 0);
                neuerSocket.Bind(myEndpoint);

                Console.SetWindowSize(Console.WindowWidth+70, Console.WindowHeight);
                Console.WriteLine("schön lauschen.... auf : " + myEndpoint);
                Console.WriteLine();


                neuerSocket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.HeaderIncluded, true);



                byte[] byTrue = new byte[4] { 1, 0, 0, 0 };
                byte[] byOut = new byte[4];
                neuerSocket.IOControl(IOControlCode.ReceiveAll, byTrue, byOut);

                while (true)
                {

                    byte[] buffer;
                    buffer = new byte[100000];
                    int test = neuerSocket.Receive(buffer);
                    if (test > 0)
                    {
                        string frank = (buffer[12] + "." + buffer[13] + "." + buffer[14] + "." + buffer[15]);
                        Console.WriteLine(" Version : " + (buffer[0] >> 4)
                            + "  ||  " + "Header Length : " + ((buffer[0] & 15) * 4)
                            + "  ||  " + "TTL : " + (buffer[8])
                            + "  ||  " + "Source Adress : " + (buffer[12] + "." + buffer[13] + "." + buffer[14] + "." + buffer[15])
                            + "  ||  " + "Destination Adress : " + (buffer[16] + "." + buffer[17] + "." + buffer[18] + "." + buffer[19])
                            + "  ||  " + "Packet Length : " + (buffer[2] << 8 | buffer[3])
                            + "  ||  " + "Protocol Type : " + (buffer[9]));

                        //if (frank == "192.168.178.51" & buffer[9] == 1)
                        //{
                        //    Console.WriteLine("das war frank");
                        //    Console.WriteLine("das ist franks ipadresse : " + frank + " mit einem echo icmp request. Protocol Type : " + buffer[9]);
                        //    Console.Read();
                        //}

                    }


                    //int read = neuerSocket.Receive(buffer);
                    //if (read >= 20)
                    //{
                    //    Console.WriteLine("Packet from {0} to {1},
                    //        new IPAddress((long)BitConverter.ToUInt32(buffer, 12)),
                    //        new IPAddress((long)BitConverter.ToUInt32(buffer, 16)),
                    //    );
                    //}
                }
            }
            catch (SocketException ex)
            {
                Console.WriteLine("sone scheisse: "+ (ex));
            }



        }
    }
}
