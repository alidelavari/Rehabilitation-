using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TargetColision : MonoBehaviour
{
    [SerializeField]AudioClip shot;
    // Start is called before the first frame update
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioSource.PlayClipAtPoint(shot, Camera.main.transform.position);
        //SceneManager.LoadScene("WinScreen");
    }
}
