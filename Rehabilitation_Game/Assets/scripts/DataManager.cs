using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DataManager : MonoBehaviour
{
    [SerializeField] Ancher ancher;
    [SerializeField] Aim aim;
    [SerializeField] GameObject levelText;
    [SerializeField] float waitTime = 1f;

    [SerializeField] int possibleFails;
    [SerializeField] int numLevels;
    [SerializeField] int level;
    [SerializeField] int angle;
    [SerializeField] int ancherLocation;
    [SerializeField] int targetLocation;
    [SerializeField] int targetMovement;
    [SerializeField] int targetScale;

    int numberFailed = 0;
    string dataPath;
    FileStream dataFile;
    StreamReader srData;

    // Start is called before the first frame update
    void Start()
    {
        openDataFile();
        countNumLevels();
        readNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void success()
    {
        Throw.setFreeze(true);
        Invoke("readNextLevel", waitTime);
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
        Debug.Log(pos);
        dataFile.Position = pos;
        srData.DiscardBufferedData();
        possibleFails = int.Parse(srData.ReadLine());
    }

    void setLevel(int level)
    {
        levelText.GetComponent<TextMeshProUGUI>().SetText(level.ToString());
    }

    void setPB()
    {
        FindObjectOfType<PgBar>().setValue((float)(level - 1)/numLevels);
    }

    void openDataFile()
    {
        //path of the file
        dataPath = Application.dataPath + "/data";

        if(!File.Exists(dataPath))
        {
            File.WriteAllText(dataPath, "");
        }
        dataFile = File.OpenRead(dataPath);
        srData = new StreamReader(dataFile);
    }

    void readNextLevel()
    {
        foreach(Throw obj in FindObjectsOfType<Throw>())
        {
            if (obj.colision)
                Destroy(obj.gameObject);
        }
        string levelString = string.Empty;

        if ((levelString = srData.ReadLine()) != null)
        {
            string[] levelData = new string[5];
            levelData = levelString.Split('_');
            level = int.Parse(levelData[0]);

            for(int i = 1; i < levelData.Length; i++)
            {
                string data = levelData[i];
                if (data[0].Equals('A'))
                    angle = int.Parse(data.Substring(1));
                else if (data[0].Equals('L'))
                    ancherLocation = int.Parse(data.Substring(1));
                else if (data[0].Equals('T'))
                    targetLocation = int.Parse(data.Substring(1));
                else if (data[0].Equals('M'))
                    targetMovement = int.Parse(data.Substring(1));
                else if (data[0].Equals('S'))
                    targetScale = int.Parse(data.Substring(1));
            }
            setLevel(level);
            aim.setAngle(angle);
            ancher.setLocation(ancherLocation);
            aim.setLocation(targetLocation);
            aim.setMoveRange(targetMovement);
            aim.setScale(targetScale);
            setPB();

            numberFailed = 0;
            Throw.setFreeze(false);
        }

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
                    angle = int.Parse(data.Substring(1));
                else if (data[0].Equals('L'))
                    ancherLocation = int.Parse(data.Substring(1));
                else if (data[0].Equals('T'))
                    targetLocation = int.Parse(data.Substring(1));
                else if (data[0].Equals('M'))
                    targetMovement = int.Parse(data.Substring(1));
                else if (data[0].Equals('S'))
                    targetScale = int.Parse(data.Substring(1));
            }
            setLevel(level);
            aim.setAngle(angle);
            ancher.setLocation(ancherLocation);
            aim.setLocation(targetLocation);
            aim.setMoveRange(targetMovement);
            aim.setScale(targetScale);
            setPB();

            numberFailed = 0;
            Throw.setFreeze(false);
        }

    }
}
