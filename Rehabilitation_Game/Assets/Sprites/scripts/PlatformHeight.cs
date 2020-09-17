using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHeight : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] float rate = 5;
    [SerializeField] GameObject ancher;

    float ancherHeight;
    Vector3 initialScale;
    float initialHeight;
    float targetHeight;
    // Start is called before the first frame update
    void Start()
    {
        ancherHeight = ancher.transform.position.y;
        initialScale = transform.localScale;
        initialHeight = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(ancher.transform.position.x, initialHeight + height / 2, transform.position.z);
        ancher.transform.position = new Vector3(ancher.transform.position.x, ancherHeight + height, ancher.transform.position.z);
        transform.localScale = initialScale + new Vector3(0, height, 0);
        moveToHeight();
    }

    void moveToHeight()
    {
        if (targetHeight - height > 0.05)
        {
            height += rate * Time.deltaTime;
        }
        else if(height - targetHeight > 0.05)
        {
            height -= rate * Time.deltaTime;
        }
    }

    public void setHeight(float height)
    {
        targetHeight = height;
    }
}
