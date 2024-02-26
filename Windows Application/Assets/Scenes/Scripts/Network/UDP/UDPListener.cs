using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;

public class UDPListener : MonoBehaviour
{
    MessageHandler handler;

    private const int port = 8089;
    private UdpClient udpClient;

    void Start()
    {
        handler = GetComponent<MessageHandler>();

        if (handler == null)
        {
            Debug.Log("UDPListener: Missing MessageHandler...");
            Destroy(this);
        }

        udpClient = new UdpClient(port);
        udpClient.BeginReceive(ReceiveData, null);
    }

    void ReceiveData(IAsyncResult result)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = udpClient.EndReceive(result, ref endPoint);
        string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

        // Handle message...
        Debug.Log(receivedMessage);
        handler.ProcessMessage(receivedMessage);

        // Continue listening for messages...
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