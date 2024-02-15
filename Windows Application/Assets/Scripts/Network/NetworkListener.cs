using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class NetworkListener : MonoBehaviour
{
    private const int port = 8089;
    private UdpClient udpClient;

    void Start()
    {
        udpClient = new UdpClient(port);
        udpClient.BeginReceive(ReceiveData, null);
    }

    void ReceiveData(IAsyncResult result)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = udpClient.EndReceive(result, ref endPoint);
        string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

        // Handle received message
        Debug.Log("Received message: " + receivedMessage);

        // Continue listening for messages
        udpClient.BeginReceive(ReceiveData, null);
    }

    void OnDestroy()
    {
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}