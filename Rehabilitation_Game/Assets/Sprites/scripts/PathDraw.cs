using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] GameObject Aim;
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
    }

    // Update is called once per frame
    void Update()
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
        cm.draw_circle(x, y, circleRedius);
    }

    private float calculateHeight(float x)
    {
        float g = -FindObjectOfType<Ground>().getGravityAcceleration() * 1.13f;
        float theta = Arrow.transform.rotation.z * 2 / 3 * Mathf.PI;
        float velocity = FindObjectOfType<HandMove>().getVelocity();
        float initialY = Arrow.transform.position.y;
        float y = g / 2 * Mathf.Pow(x / Mathf.Cos(theta) / velocity, 2) + Mathf.Tan(theta) * x;
        y = y / 1.55f + initialY;
        return y;
    }
}
