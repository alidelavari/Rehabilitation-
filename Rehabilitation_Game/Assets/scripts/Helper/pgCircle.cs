using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pgCircle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float angle = transform.rotation.eulerAngles.z;
        if (angle > 180)
            angle = 0;
        transform.rotation = Quaternion.Euler(0, 0, angle + 1);
    }
}
