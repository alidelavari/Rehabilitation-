using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformHeight : MonoBehaviour
{
    [SerializeField] float height;
    [SerializeField] GameObject ancher;

    float ancherHeight;
    Vector3 initialScale;
    float initialHeight;
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
    }
}
