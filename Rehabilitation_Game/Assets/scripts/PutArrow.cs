using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutArrow : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] float waitTime = .5f;
    GameObject arrow;
    float timeAfterThrow;
    // Start is called before the first frame update
    void Start()
    {
        instantiateArrow();
    }

    // Update is called once per frame
    void Update()
    {
        if (timeAfterThrow > waitTime)
        {
            instantiateArrow();
        }
        if (arrow.GetComponent<Throw>().stratingPoint)
        {
            arrow.transform.position = transform.position;
            arrow.transform.rotation = transform.rotation;
        }
        else
        {
            timeAfterThrow += Time.deltaTime;
        }
    }

    private void instantiateArrow()
    {
        arrow = Instantiate(arrowPrefab, transform.position, transform.rotation);
        timeAfterThrow = 0;
    }
}
