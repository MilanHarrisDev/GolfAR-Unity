using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quobject.SocketIoClientDotNet.Client;
using Newtonsoft.Json;

public class Message
{
    public string id;
    public string data;
};

public class ConnectionManager : MonoBehaviour {

    public string serverURL = "http://localhost:3000";

    protected Socket socket = null;
	
    private void Start()
    {
        DoOpen();
    }
	
    private void OnDestroy()
    {
        DoClose();
    }
	
    private void DoOpen()
    {
        if (socket == null)
        {
            socket = IO.Socket (serverURL);
            socket.On("welcome", (data) =>
            {
                string str = data.ToString();
	
                Message message = JsonConvert.DeserializeObject<Message>(str);
                string debugMessage = "user#" + message.id + ": " + message.data;
                Debug.Log(debugMessage);
            });
        }
    }
	
    private void DoClose()
    {
        if (socket != null)
        {
            socket.Disconnect();
            socket = null;
        }
    }
}
