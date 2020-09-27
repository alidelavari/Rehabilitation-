using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Throw : MonoBehaviour
{
    static bool freeze;
    [SerializeField] float Initial_known_equivalent_Angle = 0f;//45degree
    [SerializeField] float degree_equivalent = 0f;
    [SerializeField] float MaxAngle = 12f;
    [SerializeField] float MinAngle = 0f;
    [SerializeField] AudioClip pathAudio;
    [SerializeField] float midpoint = 6f;
    GameHandler gm;
    int HeightsInUnits = 12;
    int WidthInUnits = 16;
    float velocity = 20f;

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
        gm = FindObjectOfType<GameHandler>();
        if (!GameHandler.arrowIsDynamics)
            GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    // Update is called once per frameThr
    void Update()
    {
        if (stratingPoint)
        {
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
        if (Input.GetKeyDown(KeyCode.Return)) 
        {
            gm.success();           
        } else if (Input.GetKeyDown(KeyCode.Escape))
        {
            gm.fail();
        }
        /////////////////////////////////////////////////////////
    }

    private void FixedUpdate()
    {
        if (GameHandler.arrowIsDynamics)
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * (FindObjectOfType<Ground>().getGravityAcceleration() + Physics2D.gravity.y));
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
        bool shot = (gm.IsServerConnected()) ? gm.IsServerShoted() : Input.GetMouseButtonDown(0);
        if (shot)
        {
            FindObjectOfType<PredictManager>().Throw();
        }
    }

    public void throwArrow()
    {
        gm.DisShot();
        AudioSource.PlayClipAtPoint(pathAudio, Camera.main.transform.position);
        //clear Path
        //FindObjectOfType<CircleManager>().clear_circles();

        velocity = FindObjectOfType<HandMove>().getVelocity();
        float Angle = transform.rotation.z * 2 / 3 * Mathf.PI;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        GetComponent<Rigidbody2D>().velocity = new Vector2(velocity * Mathf.Cos(Angle), velocity * Mathf.Sin(Angle));
        GetComponent<SpriteRenderer>().sortingOrder = 1002;
        stratingPoint = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);

        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        //Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<PolygonCollider2D>());
        colision = true;

        if (collision.gameObject.name == "Aim")
        {
            aimObject = collision.gameObject;
            distanceFromAim = transform.position - aimObject.transform.position;
            collideAim = true;
            gm.success();
        }
        else
            gm.fail();
    }

}
