using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] float circleRedius = .1f;
    [SerializeField] float circleXdistance = .3f;
    [SerializeField] int circleNumbers = 20;
    [SerializeField] float max_x_camera = 21.333f;
    [SerializeField] float max_y_camera = 12;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Arrow.GetComponent<Throw>().stratingPoint)
        {
            DrawThePath();
        } else
        {
            FindObjectOfType<CircleManager>().clear_circles();
            Destroy(this.gameObject);
        }
    }
    public void DrawThePath()
    {
        FindObjectOfType<CircleManager>().clear_circles();
        float x = transform.position.x;

        for (int i=0; i<circleNumbers; i++)
        {
            float y = calculateHeight(x - Arrow.transform.position.x);
            FindObjectOfType<CircleManager>().draw_circle(x, y, circleRedius);
            x += circleXdistance;
        }

        /*float pathStep = 1f;
        float Y_positions_circle;
        float X_positions_circle;
        float Delta_X_position;
        float startingRaduis = 2f;
        float distance = 1f;
        float initial_Y_Arch = transform.position.y;
        float X_changable_circule = transform.position.x;
        //Debug.Log(X_changable_circule);
        while (X_changable_circule <= max_x_camera / 2 - 1)
        {
            X_positions_circle = X_changable_circule;
            Delta_X_position = X_changable_circule - transform.position.x;
            Y_positions_circle = -g / (2 * (Mathf.Pow(Mathf.Cos(Angle) * velocity, 2))) * Delta_X_position + Delta_X_position * Mathf.Tan(Angle) + initial_Y_Arch;
            if (Y_positions_circle > max_y_camera - 1)
            {
                break;
            }
            if (Y_positions_circle < 1)
            {
                break;
            }
            FindObjectOfType<CircleManager>().draw_circle(X_positions_circle, Y_positions_circle, startingRaduis);
            startingRaduis -= 0.3f;
            X_changable_circule += distance;

        }*/
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
