using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    // Start is called before the first frame update
    bool serverConnected = false;
    private bool serverFlag = true;
    private bool mouseFlag = true;
    private bool shotedByServer = false;
    [SerializeField] Message messageObj;
    [SerializeField] SocketListener socketListener;
    float serverAngle = 20;
    [SerializeField] Button ConnectServerBtn;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (serverConnected && serverFlag)
        {
            messageObj.showServerMsg();
            serverFlag = false;
            mouseFlag = true;
        }
        else if (!serverConnected && mouseFlag)
        {
            messageObj.showMouseMsg();
            serverFlag = true;
            mouseFlag = false;
        }
    }

    public void DisConnectedFromServer()
    {
        serverConnected = false;
    }

    public void ConncetedToServer()
    {
        serverConnected = true;
    }

    public bool IsServerConnected()
    {
        return serverConnected;
    }

    public void Shot()
    {
        shotedByServer = true;
    }

    public void DisShot()
    {
        shotedByServer = false;
    }

    public bool IsServerShoted()
    {
        return shotedByServer;
    }

    public void SetServerAngle(float angle)
    {
        serverAngle = angle;
    }

    public float GetServerAngle()
    {
        return serverAngle;
    }

    public void ConnectServer()
    {
        socketListener.ConnectToTcpServer();
    }

    public void Quit()
    {
        Application.Quit();
    }
}
