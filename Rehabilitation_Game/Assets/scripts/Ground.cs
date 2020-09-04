using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    [SerializeField] float g = 9.81f;

    public float getGravityAcceleration()
    {
        return g;
    }

    public void setGravityAcceleration(float GA)
    {
        g = GA;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
