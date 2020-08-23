using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PgBar : MonoBehaviour
{
    [SerializeField] float pgTarget = 0.5f;
    [SerializeField] float rate = 0.1f;
    RectTransform rectTransform;
    public PgLabel pgLabel;
    // Start is called before the first frame update
    void Start()
    {
        GameObject pg_label = GameObject.Find("pg_label");
        transform.localScale = new Vector2(0, 1);
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x - pgTarget < -0.0001)
            StartToMove(true);
        else if (transform.localScale.x - pgTarget > 0.0001)
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
        pgLabel.SetPos(transform.position.x + rectTransform.rect.width * transform.localScale.x, transform.localScale.x);
    }
}
