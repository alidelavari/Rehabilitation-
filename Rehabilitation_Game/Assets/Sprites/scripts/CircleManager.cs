using System.Collections;

using System.Collections.Generic;

using UnityEngine;



public class CircleManager : MonoBehaviour

{

    public GameObject circlePrefabs;



    List<GameObject> circles = new List<GameObject>();



    public void draw_circle(float x, float y, float radius = 1)

    {

        GameObject circle = Instantiate(circlePrefabs, new Vector3(x, y, 0), Quaternion.identity);

        circle.transform.localScale = new Vector3(radius, radius, 0);

        circles.Add(circle);

    }



    public void clear_circles()

    {

        foreach (GameObject circle in circles)

        {

            Destroy(circle);

        }

        circles.Clear();

    }

}