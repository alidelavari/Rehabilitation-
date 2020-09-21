using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Btns : MonoBehaviour
{
    [SerializeField]
    GameHandler gameHandler;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ServerConnect()
    {
        if (gameHandler.IsServerConnected())
            gameHandler.DisConnectedFromServer();
        else
            gameHandler.ConnectServer();
    }

    public void Quit()
    {
        Debug.Log("quit");
        gameHandler.Quit();
    }
}
