using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class HandMove : MonoBehaviour
{
    [SerializeField] float Initial_known_equivalent_Angle = 0f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] float mouseAngle = 20;
    [SerializeField] float serverAngle = 20;
    [SerializeField] float angleRange = Mathf.PI / 2;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] float midpoint = 6f;
    [SerializeField] float velocity = 20f;
    [SerializeField] UPersian.Components.RtlText angleText;
    [SerializeField] CirclePgHandler pgHandler;
    [SerializeField] GameHandler gameManager;
    int HeightsInUnits = 12;
    float currentAngle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        get_angle_by_mouse();

        if (gameManager.IsServerConnected())
        {
            currentAngle = gameManager.GetServerAngle();
        }
        else
        {
            currentAngle = mouseAngle;
        }

        set_pg_angle();
        set_arch_inGame_angle();
    }

    public void set_arch_inGame_angle()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = ((currentAngle - 90) * Mathf.Deg2Rad * 180 / Mathf.PI) - degree_equivalent + Initial_known_equivalent_Angle;
        transform.rotation = Quaternion.Euler(temp);

    }

    public void get_angle_by_mouse()
    {
        float mouseposition = Input.mousePosition.y / Screen.height * HeightsInUnits;
        mouseposition = Mathf.Clamp(mouseposition, MinAngle, MaxAngle);
        mouseAngle = (((mouseposition - midpoint) / midpoint * angleRange) * Mathf.Rad2Deg) + 90 ;
        
    }

    private void set_pg_angle()
    {
        angleText.text = ((int)currentAngle).ToString();
        pgHandler.setAngle(currentAngle);
    }

    public float getArrowAngle()
    {
        return currentAngle;
    }

    public float getVelocity()
    {
        return velocity;
    }
    public void setVelocity(float velocity)
    {
        this.velocity = velocity;
    }
}
