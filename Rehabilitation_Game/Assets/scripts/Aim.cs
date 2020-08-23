﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] float screenWidthInUnit = 30f;
    [SerializeField] int movementRange;
    // Start is called before the first frame update
   void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void seAngle(int angle)
    {

    }

    public void setLocation(int fromLeft)
    {
        Vector3 pos = transform.position;
        pos.x = Screen.width * fromLeft / 100 / screenWidthInUnit;
        transform.position = pos;
    }

    public void setMoveRange(int movement)
    {
        movementRange = movement;
    }

    public void setScale(int scale)
    {
        transform.localScale = new Vector3(scale, scale);
    }
}
