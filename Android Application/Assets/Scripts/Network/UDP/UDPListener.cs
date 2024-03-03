using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System;
using UnityEngine.UIElements;
using TMPro;

public class UDPListener : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textField;

    Queue messageQueue = new Queue();

    MessageHandler handler;

    private const int port = 7087;
    private UdpClient udpClient;

    void Start()
    {
        handler = GetComponent<MessageHandler>();

        if (handler == null)
        {
            Debug.Log("UDPListener: Missing MessageHandler...");
            Destroy(this);
        }

        GetComponent<MultiCastLockAcquirer>().enabled = true;

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
        Debug.Log(receivedMessage);
        messageQueue.Enqueue(receivedMessage);

        // Continue listening for messages...
        udpClient.BeginReceive(ReceiveData, null);
    }

    IEnumerator MainThreadDelegate()
    {
        while (messageQueue != null && messageQueue.Count > 0)
        {
            yield return new WaitForSeconds(0);

            string message = messageQueue.Dequeue().ToString();
            textField.text = message;
            handler.ProcessMessage(message);
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