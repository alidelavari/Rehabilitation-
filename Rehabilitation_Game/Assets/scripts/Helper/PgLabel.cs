using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class PgLabel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public RtlText num;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPos(float x, float number)
    {
        transform.position = new Vector2(x, transform.position.y);
        num.text = ((int)(number * 100 + .5)).ToString();
    }
}
