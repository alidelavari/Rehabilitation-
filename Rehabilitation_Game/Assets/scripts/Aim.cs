using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] float screenWidthInUnit = 30f;
    [SerializeField] float screenHeightInUnit = 1.6f;
    [SerializeField] float movementRange;
    [SerializeField] UPersian.Components.RtlText angleText;
    [SerializeField] float velocity = 20f;
    Vector3 mainPos;

    int direction = 1;
    // Start is called before the first frame update
   void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = transform.position;
        if (pos.y - mainPos.y > movementRange ||
            mainPos.y - pos.y > movementRange)
        {
            direction *= -1;
        }
        transform.Translate(direction * Vector3.up * movementRange * Time.deltaTime);
    }

    public void setAngle(int angle)
    {
        Vector3 initialPos = FindObjectOfType<HandMove>().transform.position;
        Vector3 pos = transform.position;
        float x = (pos.x - initialPos.x);
        float theta = angle * Mathf.Deg2Rad;
        float v0 = velocity;
        pos.y = Physics2D.gravity.y / 2 * Mathf.Pow(x/Mathf.Cos(theta)/v0, 2) + Mathf.Tan(theta) * x;
        pos.y = pos.y / screenHeightInUnit + initialPos.y;
        transform.position = pos;
        mainPos = pos;


        angleText.text = (angle + 90).ToString() + " درجه";
        //transform.position = pos;
    }

    public void setLocation(int fromLeft)
    {
        mainPos = transform.position;
        mainPos.x = Screen.width * fromLeft / 100 / screenWidthInUnit;
        transform.position = mainPos;
    }

    public void setMoveRange(int movement)
    {
        movementRange = Screen.width * movement / 200 / screenWidthInUnit;
    }

    public void setScale(float scale)
    {
        transform.localScale = new Vector3(scale, scale);
    }
}
