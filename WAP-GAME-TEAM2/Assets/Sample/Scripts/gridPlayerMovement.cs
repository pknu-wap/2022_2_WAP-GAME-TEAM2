using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gridPlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    public Transform target;

    public LayerMask obsticals;


    public GameObject touchedGO; 

    public bool inputIsActive; 

    void Start()
    {
        target.parent = null; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.position, movementSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.position) <= .0f)
        {
            if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1f)
            {
                if(!Physics2D.OverlapCircle(target.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.1f , obsticals) && inputIsActive ==true)
                {
                    target.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }

                if (!Physics2D.OverlapCircle(target.position + new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f), 0.1f, obsticals) && inputIsActive == true)
                {
                    target.position += new Vector3(Input.GetAxisRaw("Horizontal"), 0f, 0f);
                }

              

            }

            if (Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1f)
            {
                if (!Physics2D.OverlapCircle (target.position + new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f), 0.1f, obsticals) && inputIsActive == true)
                {

                    target.position += new Vector3(0f, Input.GetAxisRaw("Vertical"), 0f);
                }
                    
            }


        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "Pushable")
        {

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        touchedGO = null; 
    }
}
