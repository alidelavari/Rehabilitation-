using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] float Initial_known_equivalent_Angle = 0f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] public float Velocity = 20f;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] float midpoint = 6f;
    public float Angle =Mathf.PI/4;
    int HeightsInUnits = 12;
    int WidthInUnits = 16;
    float initial_position_x_circle;
    Vector2 starting_position;
    public bool stratingPoint = true;
    public bool colision = false;
    Vector2 PositionColision;
    // Start is called before the first frame update
    void Start()
    {
        starting_position = new Vector2(transform.position.x, transform.position.y);
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        if (stratingPoint)
        {
            set_velocity_angle();
            LanchOnclick();
        }
        else if(!colision)
        {
            set_arch_inGame_angle_afterThrow();
        }
    }
    void set_arch_inGame_angle_afterThrow()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;
        float Angle_arch = Mathf.Atan(velocity.y / velocity.x);
        Vector3 temp = transform.rotation.eulerAngles;
        temp.z = (Angle_arch * 180 / Mathf.PI) - degree_equivalent + Initial_known_equivalent_Angle;
        transform.rotation = Quaternion.Euler(temp);
    }

    public void LanchOnclick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //clear Path
            FindObjectOfType<CircleManager>().clear_circles();


            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            GetComponent<Rigidbody2D>().velocity = new Vector2(Velocity*Mathf.Cos(Angle), Velocity * Mathf.Sin(Angle));
            stratingPoint = false;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<PolygonCollider2D>());
        colision = true;
    }
    public void set_velocity_angle()
    {
        float mouseposition = Input.mousePosition.y / Screen.height * HeightsInUnits;
        mouseposition= Mathf.Clamp(mouseposition, MinAngle, MaxAngle);
        Angle = (mouseposition - midpoint) / midpoint * Mathf.PI / 4;
    }

}
