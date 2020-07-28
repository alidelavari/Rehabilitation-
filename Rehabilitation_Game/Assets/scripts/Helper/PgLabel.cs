using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PgLabel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPos(float x)
    {
        transform.position = new Vector2(x, transform.position.y);
    }
}
