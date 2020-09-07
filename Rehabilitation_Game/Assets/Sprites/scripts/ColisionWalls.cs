using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColisionWalls : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //SceneManager.LoadScene("GameOver");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //SceneManager.LoadScene("GameOver");
    }
}
