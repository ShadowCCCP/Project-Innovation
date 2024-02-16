using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class TCPSender : MonoBehaviour
{
    public const string serverAddress = "192.168.178.70";
    private const int port = 7087;
    private TcpClient client;

    bool connected;

    void ConnectToServer()
    {
        try
        {
            client = new TcpClient(serverAddress, port);
            Debug.Log("Connected to Windows application.");
            connected = true;
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
        if (!connected) ConnectToServer();
        else SendData("Android: Huhu!");
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