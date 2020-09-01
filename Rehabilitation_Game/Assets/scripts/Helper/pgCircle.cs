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
        float angle = GetComponent<RectTransform>().rotation.z;
        GetComponent<RectTransform>().Rotate(new Vector3(0, 0, angle + 1));
    }
}
