using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkSender : MonoBehaviour
{
    private const string serverAddress = "192.168.178.70";
    private const int port = 8888;
    private TcpClient client;

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            Debug.Log("Connected to Windows application.");
        }
        catch (SocketException e)
        {
            Debug.Log("Error connecting to server: " + e);
        }
    }

    void OnDestroy()
    {
        if (client != null)
        {
            client.Close();
        }
    }

    void Update()
    {
        // Send data to Windows application (e.g., gyroscope data)
        SendData("Gyroscope Data: x=0.5, y=0.2, z=0.8");
    }

    void SendData(string data)
    {
        if (client == null || !client.Connected)
        {
            ConnectToServer();
            return;
        }

        try
        {
            NetworkStream stream = client.GetStream();
            byte[] bytesToSend = Encoding.ASCII.GetBytes(data);
            stream.Write(bytesToSend, 0, bytesToSend.Length);
        }
        catch (SocketException e)
        {
            Debug.Log("Error sending data: " + e);
        }
    }
}