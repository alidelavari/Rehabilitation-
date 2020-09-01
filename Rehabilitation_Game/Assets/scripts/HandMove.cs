using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class HandMove : MonoBehaviour
{
    [SerializeField] float Initial_known_equivalent_Angle = 0f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] float Angle = Mathf.PI / 4;
    [SerializeField] float angleRange = Mathf.PI / 3;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] float midpoint = 6f;
    [SerializeField] UPersian.Components.RtlText angleText;
    int HeightsInUnits = 12;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        set_angle();
        set_arch_inGame_angle();
    }

    public void set_arch_inGame_angle()
    {
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = (Angle * 180 / Mathf.PI) - degree_equivalent + Initial_known_equivalent_Angle;
        transform.rotation = Quaternion.Euler(temp);

    }

    public void set_angle()
    {
        float mouseposition = Input.mousePosition.y / Screen.height * HeightsInUnits;
        mouseposition = Mathf.Clamp(mouseposition, MinAngle, MaxAngle);
        Angle = (mouseposition - midpoint) / midpoint * angleRange;
        angleText.text = ((int)(Angle * Mathf.Rad2Deg)).ToString();
    }
}
