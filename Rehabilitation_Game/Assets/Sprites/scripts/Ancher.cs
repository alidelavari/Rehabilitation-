using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ancher : MonoBehaviour
{
    [SerializeField] float screenWidthInUnit = 30f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setLocation(int fromLeft)
    {
        Vector3 pos = transform.position;
        pos.x = Screen.width * fromLeft / 100 / screenWidthInUnit;
        transform.position = pos;
    }
}
