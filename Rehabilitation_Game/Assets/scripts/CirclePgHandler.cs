using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CirclePgHandler : MonoBehaviour
{
    Image image;
    [SerializeField]
    [Range(0, 180)]
    float angle = 90;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();

    }

    // Update is called once per frame
    void Update()
    {
        double fillAmount = (angle / 180) * 0.5;
        image.fillAmount = (float)fillAmount;
    }

    public void setAngle(float angle)
    {
        this.angle = angle;
    }
}
