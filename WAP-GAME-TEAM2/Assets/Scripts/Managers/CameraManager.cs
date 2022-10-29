using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using Unity.VisualScripting;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    [SerializeField] private GameObject target;
    [SerializeField] private float fFollowSpeed;
    [SerializeField] private bool b_isFollow;

    private Vector3 v_targetPos;

    [SerializeField] private BoxCollider2D _bound;
    private Vector3 v_maxBound;
    private Vector3 v_minBound;

    private float f_halfHeight;
    private float f_halfWidth;

    private Camera theCamera;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        theCamera = GetComponent<Camera>();
        v_minBound = _bound.bounds.min;
        v_maxBound = _bound.bounds.max;
        f_halfHeight = theCamera.orthographicSize;
        f_halfWidth = f_halfHeight * Screen.width / Screen.height;
        target = PlayerController.instance.gameObject;
    }
    
    void Update()
    {
        if (b_isFollow)
        {
            Vector2 targetPosition = target.transform.position;
            v_targetPos.Set(targetPosition.x, targetPosition.y, transform.position.z);

            transform.position = Vector3.Lerp(transform.position, v_targetPos, fFollowSpeed * Time.deltaTime);

            float ClampedX = Mathf.Clamp(transform.position.x, v_minBound.x + f_halfWidth, v_maxBound.x - f_halfWidth);
            float ClampedY = Mathf.Clamp(transform.position.y, v_minBound.y + f_halfHeight, v_maxBound.y - f_halfHeight);
            transform.position = new Vector3(ClampedX, ClampedY, transform.position.z);
        }
    }

    public void SetBound(BoxCollider2D _newBound)
    {
        _bound = _newBound;
        v_minBound = _bound.bounds.min;
        v_maxBound = _bound.bounds.max;
    }
}
