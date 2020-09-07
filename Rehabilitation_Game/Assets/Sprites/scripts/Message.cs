using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Message : MonoBehaviour
{
    // Start is called before the first frame update
    float alpha = 0;
    [SerializeField]
    float step = 0.001f;
    [SerializeField]
    int length = 100;
    [SerializeField]
    int i = 0;
    bool showMode = true;
    [SerializeField]
    Sprite serverSprite;
    [SerializeField]
    Sprite mouseSprite;

    void Start()
    {
        showMouseMsg();
    }

    // Update is called once per frame
    void Update()
    {
        if(length == i)
        {
            showMode = false;
            i = 0;
        }

        i = i + 1;

        if (showMode && alpha < 0.99)
            alpha += step;
        else if(alpha > 0.01)
            alpha -= step;

        handleMessageAlpha();
    }

    public void showServerMsg()
    {
        showMode = true;
        i = 0;
        Image image = GetComponent<Image>();
        image.sprite = serverSprite;
    }

    public void showMouseMsg()
    {
        showMode = true;
        i = 0;
        Image image = GetComponent<Image>();
        image.sprite = mouseSprite;
    }

    void handleMessageAlpha()
    {
        Image image = GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, alpha);
    }
}
