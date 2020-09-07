using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PgBar : MonoBehaviour
{
    [SerializeField] float pgTarget = 0.0f;
    [SerializeField] float rate = 0.1f;
    float prePgTarget = 0;
    RectTransform rectTransform;
    public PgLabel pgLabel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject pg_label = GameObject.Find("pg_label");
        transform.localScale = new Vector2((float)-0.01, 1);
        rectTransform = GetComponent<RectTransform>();
        prePgTarget = pgTarget;
    }

    // Update is called once per frame
    void Update()
    {
        if ((int)(pgTarget*5) - (int)(prePgTarget*5) > 0)
        {
            FindObjectOfType<CongBox>().show((int)(pgTarget*100));
            prePgTarget = pgTarget;
        }
        if (transform.localScale.x - pgTarget < -0.001)
            StartToMove(true);
        else if (transform.localScale.x - pgTarget > 0.001)
            StartToMove(false);
    }

    void StartToMove(bool forward)
    {
        float scale = transform.localScale.x;
        if(forward)
            scale += rate * Time.deltaTime;
        else
            scale -= rate * Time.deltaTime;
        transform.localScale = new Vector2(scale, 1);
        pgLabel.SetPos(-234 + rectTransform.rect.width * transform.localScale.x, transform.localScale.x);
    }

    public void setValue(float val)
    {
        prePgTarget = pgTarget;
        pgTarget = val;
    }
}
