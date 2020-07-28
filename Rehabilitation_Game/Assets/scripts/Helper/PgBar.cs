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
        pgLabel = (PgLabel)pg_label.GetComponent(typeof(PgLabel));
        transform.localScale = new Vector2(0, 1);
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localScale.x < pgTarget)
            StartToMove();
    }

    void StartToMove()
    {
        float scale = rate * Time.deltaTime + transform.localScale.x;
        transform.localScale = new Vector2(scale, 1);
        pgLabel.SetPos(transform.position.x + rectTransform.rect.width * transform.localScale.x);
    }
}
