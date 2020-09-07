using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movingAtstart : MonoBehaviour
{
    [SerializeField] float Initial_known_equivalent_Angle = -0.17f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] float Velocity = 20f;
    [SerializeField] float Angle = Mathf.PI / 4;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] float midpoint = 6f;
    [SerializeField] float max_x_camera = 16;
    [SerializeField] float max_y_camera = 12;
    int HeightsInUnits = 12;
    int WidthInUnits = 16;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void set_Bow_inGame_angle()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = (Angle * 180 / Mathf.PI) - degree_equivalent + Initial_known_equivalent_Angle;
        transform.rotation = Quaternion.Euler(temp);


    }

    public void set_velocity_angle()
    {
        float mouseposition = Input.mousePosition.y / Screen.height * HeightsInUnits;
        mouseposition = Mathf.Clamp(mouseposition, MinAngle, MaxAngle);
        Angle = (mouseposition - midpoint) / midpoint * Mathf.PI / 4;
    }
}
