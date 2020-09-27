using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    static bool isDynamics = true;

    public static bool arrowIsDynamics
    {
        get { return isDynamics; }
    }

    // Start is called before the first frame update
    bool serverConnected = false;
    private bool serverFlag = true;
    private bool mouseFlag = true;
    private bool shotedByServer = false;
    [SerializeField] Message messageObj;
    [SerializeField] SocketListener socketListener;
    [SerializeField] float angleRange = Mathf.PI;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] UPersian.Components.RtlText angleText;
    [SerializeField] CirclePgHandler pgHandler;
    int HeightsInUnits = 12;
    float serverAngle = 20;
    float mouseAngle = 20;
    float currentAngle;
    [SerializeField] Button ConnectServerBtn;
    [SerializeField] DB db;
    [SerializeField] Ancher ancher;
    [SerializeField] Aim aim;
    [SerializeField] PredictManager predictCircle;
    [SerializeField] UPersian.Components.RtlText levelText;
    [SerializeField] float waitTime = 1f;

    [SerializeField] int possibleFails;
    [SerializeField] int numLevels;
    [SerializeField] int level;
    [SerializeField] int targetAngle;
    [SerializeField] int ancherLocation;
    [SerializeField] int targetLocation;
    [SerializeField] int targetMovement;
    [SerializeField] int targetScale;
    [SerializeField] float consistency;

    int numberFailed = 0;
    string dataPath;
    FileStream dataFile;
    StreamReader srData;
    private Thread collectingDataThread;
    ArrayList angleDataList = new ArrayList();
    private int PATIENT_ID;
    private int SESSION;
    private int GAME_ID = 4;


    void Start()
    {
        openDataFile();
        countNumLevels();
        readNextLevel();

        collectingDataThread = new Thread(CollectData);
        collectingDataThread.IsBackground = true;
        collectingDataThread.Start();
        db.Open();
        int[] idSession = db.GetUserData();
        PATIENT_ID = idSession[0];
        SESSION = idSession[1];
        db.Close();
    }

    // Update is called once per frame
    void Update()
    {

        get_angle_by_mouse();
        HandleMsgBox();
        set_pg_angle();
        if (IsServerConnected())
            currentAngle = serverAngle;
        else
            currentAngle = mouseAngle;
    }

    public void HandleMsgBox()
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

    public void get_angle_by_mouse()
    {
        float mouseposition = Input.mousePosition.y / Screen.height * HeightsInUnits;
        mouseposition = Mathf.Clamp(mouseposition, MinAngle, MaxAngle);
        float midpoint = (MaxAngle - MinAngle) / 2;
        mouseAngle = (((mouseposition - midpoint) / (MaxAngle - MinAngle) * angleRange) * Mathf.Rad2Deg) + 90;

    }

    private void set_pg_angle()
    {
        angleText.text = ((int)GetCurrentAngle()).ToString();
        pgHandler.setAngle(GetCurrentAngle());
    }

    void CollectData()
    {
        while (true)
        {
            angleDataList.Add(currentAngle);
            Thread.Sleep(500);
        }
    }

    void SaveDataInDb()
    {
        for (int i = 0; i < angleDataList.Count; i++)
        {
            db.SaveAngles(1, 1, 4, (int)((float)angleDataList[i]), (int)((float)angleDataList[i]),
                (int)((float)angleDataList[i]), 0, 1, 0);
        }
    }

    public void success()
    {
        Throw.setFreeze(true);
        Invoke("readNextLevel", waitTime);
        db.Open();
        db.SaveSuccess(PATIENT_ID, SESSION, GAME_ID, targetAngle, 0, 1, 0);
        db.BookMarkLevel(PATIENT_ID, level);
        db.Close();
    }

    public void fail()
    {
        numberFailed++;
        if (numberFailed >= possibleFails && level > 1)
        {
            Throw.setFreeze(true);
            Invoke("failedLevel", waitTime);
        }

    }

    void countNumLevels()
    {
        numLevels = -1;
        long pos = dataFile.Position;
        dataFile.Position = 0;
        srData.DiscardBufferedData();
        while ((srData.ReadLine()) != null)
        {
            numLevels++;
        }
        dataFile.Position = pos;
        srData.DiscardBufferedData();
        possibleFails = int.Parse(srData.ReadLine());
    }

    void setLevel(int level)
    {
        levelText.text = level.ToString();
    }

    void setPB()
    {
        FindObjectOfType<PgBar>().setValue((float)(level - 1) / numLevels);
    }

    void openDataFile()
    {
        //path of the file
        dataPath = Application.dataPath + "/data";

        if (!File.Exists(dataPath))
        {
            File.WriteAllText(dataPath, "");
        }
        dataFile = File.OpenRead(dataPath);
        srData = new StreamReader(dataFile);
    }

    void readNextLevel()
    {
        foreach (Throw obj in FindObjectsOfType<Throw>())
        {
            if (obj.colision)
                Destroy(obj.gameObject);
        }
        string levelString = string.Empty;
        levelString = srData.ReadLine();

        extractData(levelString);

    }

    void failedLevel()
    {
        foreach (Throw obj in FindObjectsOfType<Throw>())
        {
            if (obj.colision)
                Destroy(obj.gameObject);
        }
        string levelString = string.Empty;

        dataFile.Position = 0;
        srData.DiscardBufferedData();
        for (int i = 1; i <= level; i++)
            levelString = srData.ReadLine();

        extractData(levelString);

    }

    private void extractData(string levelString)
    {
        if (levelString != null)
        {
            //RETURN3
            //1_A10_L10_T70_M5_S2
            //2_A15_L10_T70_M0_S1
            //3_A20_L10_T70_M5_S2
            //4_A25_L10_T70_M5_S2
            string[] levelData = new string[5];
            levelData = levelString.Split('_');
            level = int.Parse(levelData[0]);
            for (int i = 1; i < levelData.Length; i++)
            {
                string data = levelData[i];
                if (data[0].Equals('A'))
                    targetAngle = int.Parse(data.Substring(1));
                else if (data[0].Equals('L'))
                    ancherLocation = int.Parse(data.Substring(1));
                else if (data[0].Equals('T'))
                    targetLocation = int.Parse(data.Substring(1));
                else if (data[0].Equals('M'))
                    targetMovement = int.Parse(data.Substring(1));
                else if (data[0].Equals('E'))
                    targetScale = int.Parse(data.Substring(1));
                else if (data[0].Equals('C'))
                    consistency = float.Parse(data.Substring(1));
            }
            setLevel(level);
            ancher.setLocation(ancherLocation);
            aim.setLocation(targetLocation);
            aim.setAngle(targetAngle - 90);
            aim.setScale(targetScale);
            aim.setMoveRange(targetMovement);
            predictCircle.setConsistency(consistency);
            setPB();

            numberFailed = 0;
            Throw.setFreeze(false);
        }
    }

    public void finishGame()
    {
        Debug.Log("finish");
        Application.Quit();
    }

    private void OnDestroy()
    {
        dataFile.Close();
        db.Open();
        SaveDataInDb();
        db.Close();
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

    public float GetCurrentAngle()
    {
        if (IsServerConnected())
            return serverAngle;
        else
            return mouseAngle;
    }

    public void Shot()
    {
        shotedByServer = true;
    }

    public void DisShot()
    {
        shotedByServer = false;
    }

    public float getArrowAngle()
    {
        return currentAngle;
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
