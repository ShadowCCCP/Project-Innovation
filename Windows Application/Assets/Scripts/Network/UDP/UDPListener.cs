using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine.UIElements;

public class UDPListener : MonoBehaviour
{
    Queue messageQueue = new Queue();

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

    private void LateUpdate()
    {
        StartCoroutine(MainThreadDelegate());
    }

    void ReceiveData(IAsyncResult result)
    {
        IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, port);
        byte[] receivedBytes = udpClient.EndReceive(result, ref endPoint);
        string receivedMessage = Encoding.ASCII.GetString(receivedBytes);

        // Handle message...
        messageQueue.Enqueue(receivedMessage);

        // Continue listening for messages...
        udpClient.BeginReceive(ReceiveData, null);
    }

    IEnumerator MainThreadDelegate()
    {
        while (messageQueue != null && messageQueue.Count > 0)
        {
            yield return new WaitForSeconds(0);

            try
            {
                string message = messageQueue.Dequeue().ToString();
                handler.ProcessMessage(message);
            }
            catch (Exception) { }
        }
    }

    void OnDestroy()
    {
        if (udpClient != null)
        {
            udpClient.Close();
        }
    }
}