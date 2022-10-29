using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public GameObject player;
    public Transform target;
    public Camera cam;

    public Vector3 camOffSet;
    public float smoothingRatio;

    public bool follow;
 

    // This value will change at the runtime depending on target movement. Initialize with zero vector.
    private Vector3 velocity = Vector3.zero;

    void Start()
    {
        follow = true;
    }


    // Update is called once per frame
    void Update()
    {

        player = GameObject.FindGameObjectWithTag("Player");
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        if (follow == true) { cam.transform.position = Vector3.Lerp(transform.position, target.position + camOffSet, 0.1f); }
       


    }


   
}
