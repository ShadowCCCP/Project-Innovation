using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Text;

public class NetworkSender : MonoBehaviour
{
    private const string serverAddress = "your_windows_ip_address"; // Change this to your Windows machine's IP address
    private const int port = 8888;

    public string keyword = "testKey"; // Define the same keyword used in the Windows application

    void Start()
    {
        ConnectToServer();
    }

    void ConnectToServer()
    {
        try
        {
            TcpClient client = new TcpClient(serverAddress, port);
            Debug.Log("Connected to Windows application.");

            // Get the output stream from the socket
            NetworkStream stream = client.GetStream();

            // Send the keyword to the Windows application
            byte[] keywordBytes = Encoding.ASCII.GetBytes(keyword);
            stream.Write(keywordBytes, 0, keywordBytes.Length);

            // Close the connection
            stream.Close();
            client.Close();
        }
        catch (SocketException e)
        {
            Debug.Log("Error connecting to server: " + e);
        }
    }
}