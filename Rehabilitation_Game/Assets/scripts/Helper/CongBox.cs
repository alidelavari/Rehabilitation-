using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CongBox : MonoBehaviour
{
    [SerializeField] float timeForShow = 2;
    [SerializeField] GameObject scoreText;
    float timeLast = 0;
    bool showFlag = false;

    // Start is called before the first frame update
    void Start()
    {
        setVisiblity(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (showFlag)
            if (timeLast < timeForShow)
            {
                timeLast += Time.deltaTime;
            } else 
            {
                timeLast = 0;
                showFlag = false;
                setVisiblity(false);
            }
    }

    void setVisiblity(bool vis)
    {
        this.GetComponent<CanvasGroup>().alpha = vis?1:0;
        this.GetComponent<CanvasGroup>().interactable = vis;
        Throw.setFreeze(vis);
    }

    public void show(int score)
    {
        showFlag = true;
        setVisiblity(true);
        scoreText.GetComponent<TextMeshProUGUI>().SetText(score.ToString());
    }
}
