using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UPersian.Components;

public class PgLabel : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public RtlText num;

    RectTransform rect;

    void Start()
    {
        rect = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPos(float x, float number)
    {
        rect.localPosition = new Vector3(x, rect.localPosition.y, rect.localPosition.z);
        num.text = ((int)(number * 100 + .5)).ToString() + "%";
    }
}
