using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Throw : MonoBehaviour
{
    static bool freeze;
    [SerializeField] float Initial_known_equivalent_Angle = 0f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] public float Velocity = 20f;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] float midpoint = 6f;
    public float Angle =Mathf.PI/4;
    int HeightsInUnits = 12;
    int WidthInUnits = 16;
    public bool stratingPoint = true;
    public bool colision = false;
    GameObject aimObject;
    Vector3 distanceFromAim;
    bool collideAim = false;

    public static void setFreeze(bool val)
    {
        freeze = val;
    }

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    // Update is called once per frame
    void Update()
    {
        if (stratingPoint)
        {
            set_velocity_angle();
            if (!freeze)
                LanchOnclick();
        }
        else if(!colision)
        {
            set_arch_inGame_angle_afterThrow();
        }
        else if(collideAim)
        {
            //stick to aim
            gameObject.transform.position = aimObject.transform.position + distanceFromAim;
        }

        /////////////////////////////////////////////////////////
        DataManager dm = FindObjectOfType<DataManager>();
        if (Input.GetKeyDown(KeyCode.Return))
        {
            dm.success();           
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            dm.fail();
        }
        /////////////////////////////////////////////////////////
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
            //FindObjectOfType<CircleManager>().clear_circles();


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

        DataManager dm = FindObjectOfType<DataManager>();
        if (collision.gameObject.name == "Aim")
        {
            aimObject = collision.gameObject;
            distanceFromAim = transform.position - aimObject.transform.position;
            collideAim = true;
            dm.success();
        }
        else
            dm.fail();
    }
    public void set_velocity_angle()
    {
        float mouseposition = Input.mousePosition.y / Screen.height * HeightsInUnits;
        mouseposition= Mathf.Clamp(mouseposition, MinAngle, MaxAngle);
        Angle = (mouseposition - midpoint) / midpoint * Mathf.PI / 4;
    }

}
