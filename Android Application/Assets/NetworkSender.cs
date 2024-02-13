using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkSender : MonoBehaviour
{
    private const int port = 8888;

    void Start()
    {
        SendBroadcast();
    }

    void SendBroadcast()
    {
        UdpClient udpClient = new UdpClient();
        udpClient.EnableBroadcast = true;

        // Define the message to send for discovery
        string discoveryMessage = "Gyroscope: DATA";

        // Convert message string to bytes
        byte[] bytes = Encoding.ASCII.GetBytes(discoveryMessage);

        // Broadcast the discovery message to the local network
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Broadcast, port);
        udpClient.Send(bytes, bytes.Length, endPoint);
        udpClient.Close();
    }
}