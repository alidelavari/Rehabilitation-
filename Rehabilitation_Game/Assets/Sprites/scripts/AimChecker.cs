using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimChecker : MonoBehaviour
{
    static string triggerName = "AimTrigger";
    static bool onAim = false;
    public static int numInAim = 0;

    bool isIn;

    // Start is called before the first frame update
    void Start()
    {
        isIn = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        if (isIn)
        {
            numInAim--;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == triggerName && !isIn)
        {
            isIn = true;
            numInAim++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == triggerName && isIn)
        {
            isIn = false;
            numInAim--;
        }
    }
}
