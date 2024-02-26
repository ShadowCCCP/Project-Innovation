using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.IO;

public class TCPListener : MonoBehaviour
{
    private const int port = 7087;
    private TcpListener listener;
    private bool isListening = false;

    void Start()
    {
        StartListening();
    }

    void StartListening()
    {
        try
        {
            listener = new TcpListener(IPAddress.Any, port);
            listener.Start();
            Debug.Log("Waiting for connection...");

            // Start listening for connections in a separate thread
            isListening = true;
            Thread listenThread = new Thread(ListenForConnections);
            listenThread.Start();
        }
        catch (SocketException e)
        {
            Debug.Log("SocketException: " + e);
        }
    }

    void ListenForConnections()
    {
        while (isListening)
        {
            TcpClient client = listener.AcceptTcpClient();
            Debug.Log("Client connected.");

            // Handle client communication in a separate thread
            Thread clientThread = new Thread(() => HandleClientCommunication(client));
            clientThread.Start();
        }
    }

    void HandleClientCommunication(TcpClient client)
    {
        NetworkStream stream = client.GetStream();

        byte[] buffer = new byte[1024];
        int bytesRead;
        StringBuilder message = new StringBuilder();

        while (true)
        {
            bytesRead = 0;

            try
            {
                // Read data from the client
                bytesRead = stream.Read(buffer, 0, buffer.Length);
            }
            catch (IOException)
            {
                // Client disconnected
                break;
            }

            if (bytesRead == 0)
            {
                // Client disconnected
                break;
            }

            // Convert bytes to string
            string dataReceived = Encoding.ASCII.GetString(buffer, 0, bytesRead);
            message.Append(dataReceived);

            // Do something with the received data (e.g., display in Unity)
            if (!dataReceived.Contains("Windows")) Debug.Log(dataReceived);
        }

        // Clean up the client connection
        stream.Close();
        client.Close();
    }

    void OnDestroy()
    {
        isListening = false;
        if (listener != null)
        {
            listener.Stop();
        }
    }
}