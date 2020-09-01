using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian;
using System.IO;

public class DataManager : MonoBehaviour
{
    [SerializeField] Ancher ancher;
    [SerializeField] Aim aim;
    [SerializeField] UPersian.Components.RtlText levelText;

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
        readNextLevel();
        numberFailed = 0;
    }

    public void fail()
    {
        numberFailed++;
        if (numberFailed >= possibleFails && level > 1)
        {
            failedLevel();
            numberFailed = 0;
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
        levelText.text = "مرحله\n" + level;
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
            angle = int.Parse(levelData[1]);
            ancherLocation = int.Parse(levelData[2]);
            targetLocation = int.Parse(levelData[3]);
            targetMovement = int.Parse(levelData[4]);
            targetScale = int.Parse(levelData[5]);
            setLevel(level);
            aim.setAngle(angle);
            ancher.setLocation(ancherLocation);
            aim.setLocation(targetLocation);
            aim.setMoveRange(targetMovement);
            aim.setScale(targetScale);
            setPB();
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
            string[] levelData = new string[5];
            levelData = levelString.Split('_');
            level = int.Parse(levelData[0]);
            angle = int.Parse(levelData[1]);
            ancherLocation = int.Parse(levelData[2]);
            targetLocation = int.Parse(levelData[3]);
            targetMovement = int.Parse(levelData[4]);
            targetScale = int.Parse(levelData[5]);
            setLevel(level);
            aim.setAngle(angle);
            ancher.setLocation(ancherLocation);
            aim.setLocation(targetLocation);
            aim.setMoveRange(targetMovement);
            aim.setScale(targetScale);
            setPB();
        }

    }
}
