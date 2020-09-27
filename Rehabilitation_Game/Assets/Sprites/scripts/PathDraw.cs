using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject Aim;
    [SerializeField] PutArrow putArrow;
    [SerializeField] float circleRedius = .1f;
    [SerializeField] float circleXdistance = .3f;
    [SerializeField] int circleNumbers = 20;
    [SerializeField] float max_x_camera = 21.333f;
    [SerializeField] float max_y_camera = 12;
    CircleManager cm;

    // Start is called before the first frame update
    void Start()
    {
        Aim = FindObjectOfType<Aim>().gameObject;
        cm = FindObjectOfType<CircleManager>();
        putArrow = FindObjectOfType<PutArrow>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (Arrow.GetComponent<Throw>().stratingPoint)
        {
            DrawThePath();
        } else
        {
            cm.clear_circles();
            Destroy(this.gameObject);
        }
    }
    public void DrawThePath()
    {
        cm.clear_circles();

        Vector3 pos = putArrow.transform.position;
        float segmentScale = circleXdistance;
        Vector3 g;
        if (GameHandler.arrowIsDynamics)
        {
            g = Vector3.down * FindObjectOfType<Ground>().getGravityAcceleration();// * 1.1f;
        }
        else
        {
            g = Vector3.zero;
        }
        float theta = Arrow.transform.rotation.z * 2 / 3 * Mathf.PI;
        float velocity = FindObjectOfType<HandMove>().getVelocity();
        Vector3 segVelocity = new Vector3(velocity * Mathf.Cos(theta), velocity * Mathf.Sin(theta));

        for (int i = 1; i < circleNumbers; i++)
        {
            // Time it takes to traverse one segment of length segScale (careful if velocity is zero)
            float segTime = (segVelocity.sqrMagnitude != 0) ? segmentScale / segVelocity.magnitude : 0;
            segVelocity = segVelocity + g * segTime;
            pos = pos + segVelocity * segTime;
            cm.draw_circle(pos.x, pos.y, circleRedius);
            RaycastHit hit;
            if (Physics.Raycast(pos, transform.forward, out hit))
            {
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                Debug.Log(hit.collider.gameObject.name);
            }
        }
        /*
        float x = transform.position.x;
        float y;

        for (int i=0; i<circleNumbers; i++)
        {
            y = calculateHeight(x - Arrow.transform.position.x);
            cm.draw_circle(x, y, circleRedius);

            x += circleXdistance;
            if (x > Aim.transform.position.x)
            {
                break;
            }
        }

        x = Aim.transform.position.x;
        y = calculateHeight(x - Arrow.transform.position.x);
        cm.draw_circle(x, y, circleRedius);*/
    }

    private float calculateHeight(float x)
    {
        float y;
        float g = -FindObjectOfType<Ground>().getGravityAcceleration();// * 1.1f;
        float theta = Arrow.transform.rotation.z * 2 / 3 * Mathf.PI;
        float velocity = FindObjectOfType<HandMove>().getVelocity();
        float initialY = Arrow.transform.position.y;
        if (GameHandler.arrowIsDynamics)
        {
            y = g / 2 * Mathf.Pow(x / (Mathf.Cos(theta) * velocity), 2);
            y += Mathf.Tan(theta) * x;
            y += initialY;
        } else
        {
            y = Mathf.Tan(theta) * x;
            y += initialY;
        }
        return y;
    }
}
