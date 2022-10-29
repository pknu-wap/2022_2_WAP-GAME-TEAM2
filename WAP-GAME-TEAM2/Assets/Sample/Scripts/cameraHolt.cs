using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraHolt : MonoBehaviour
{
    public GameObject player;
    public cameraFollow camS;
    public Camera mainCam;

    public bool center;
    public bool allowFollow;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camS = mainCam.GetComponent<cameraFollow>();
        StartCoroutine(centerOnAwake());
    }

    private void OnEnable()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        camS = mainCam.GetComponent<cameraFollow>();
        StartCoroutine(centerOnAwake());
        camS.follow = true;
        print("Enabled");
    }

    private void OnDisable()
    {
        center = false;
        StopCoroutine(centerOnAwake());
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player") && center == true)
        {
            camS.follow = false;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && center == true)
        {
            camS.follow = true;
        }
      

    }

    IEnumerator centerOnAwake()
    {
        yield return new WaitForSecondsRealtime(5);
        center = true;
        yield return null; 
    
    }
}
