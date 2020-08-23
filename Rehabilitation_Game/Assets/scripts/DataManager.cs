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
    [SerializeField] int level;
    [SerializeField] int angle;
    [SerializeField] int ancherLocation;
    [SerializeField] int targetLocation;
    [SerializeField] int targetMovement;
    [SerializeField] int targetScale;

    string dataPath;
    FileStream dataFile;
    StreamReader srData;

    // Start is called before the first frame update
    void Start()
    {
        openDataFile();
        readNextLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setLevel(int level)
    {
        levelText.text = "مرحله\n" + level;
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
        possibleFails = int.Parse(srData.ReadLine());
    }

    public void readNextLevel()
    {
        foreach(Throw obj in FindObjectsOfType<Throw>())
        {
            if (obj.colision)
                Destroy(obj.gameObject);
        }
        string levelString = string.Empty;

        if ((levelString = srData.ReadLine()) != null)
        {
            Debug.Log(levelString);
            string[] levelData = new string[5];
            levelData = levelString.Split('_');
            level = int.Parse(levelData[0]);
            angle = int.Parse(levelData[1]);
            ancherLocation = int.Parse(levelData[2]);
            targetLocation = int.Parse(levelData[3]);
            targetMovement = int.Parse(levelData[4]);
            targetScale = int.Parse(levelData[5]);
            setLevel(level);
            aim.seAngle(angle);
            ancher.setLocation(ancherLocation);
            aim.setLocation(targetLocation);
            aim.setMoveRange(targetMovement);
            aim.setScale(targetScale);
        }
    }
}
