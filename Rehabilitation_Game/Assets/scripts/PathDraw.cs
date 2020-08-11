using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathDraw : MonoBehaviour
{
    [SerializeField] GameObject Arrow;
    [SerializeField] float max_x_camera = 21.333f;
    [SerializeField] float max_y_camera = 12;
    float Angle;
    float Velocity;

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
        }
        Angle = Arrow.GetComponent<Throw>().Angle;
        Velocity = Arrow.GetComponent<Throw>().Velocity;
    }
    public void DrawThePath()
    {
        FindObjectOfType<CircleManager>().clear_circles();
        float pathStep = 1f;
        float Y_positions_circle;
        float X_positions_circle;
        float Delta_X_position;
        float g = 10f;
        float startingRaduis = 2f;
        float distance = 1f;
        float initial_Y_Arch = transform.position.y;
        float X_changable_circule = transform.position.x;
        //Debug.Log(X_changable_circule);
        while (X_changable_circule <= max_x_camera / 2 - 1)
        {
            X_positions_circle = X_changable_circule;
            Delta_X_position = X_changable_circule - transform.position.x;
            Y_positions_circle = -g / (2 * (Mathf.Pow(Mathf.Cos(Angle) * Velocity, 2))) * Delta_X_position + Delta_X_position * Mathf.Tan(Angle) + initial_Y_Arch;
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

        }
    }
}
