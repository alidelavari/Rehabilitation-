using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UPersian.Components;

public class HandMove : MonoBehaviour
{
    [SerializeField] float Initial_known_equivalent_Angle = 0f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] float velocity = 20f;
    [SerializeField] GameHandler gameManager;
    float currentAngle;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        currentAngle = gameManager.GetCurrentAngle();
        set_arch_inGame_angle();
    }

    public void set_arch_inGame_angle()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = ((currentAngle - 90) * Mathf.Deg2Rad * 180 / Mathf.PI) - degree_equivalent + Initial_known_equivalent_Angle;
        transform.rotation = Quaternion.Euler(temp);

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
