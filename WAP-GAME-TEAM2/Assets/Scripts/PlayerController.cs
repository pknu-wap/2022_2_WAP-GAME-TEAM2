using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;

    [SerializeField] private float fSpeed;
    private float _fApplyRunSpeed;
    private bool _bRunFlag;
    private bool _bCanMove;

    [SerializeField] private int iWalkCount;
    private int _curWalkCount;

    private Vector2 _dir;

    private Animator _anim;
    private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Light2D flashLight;
    

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        _bCanMove = true;
    }

    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0) && _bCanMove)
        {
            _bCanMove = false;
            StartCoroutine(PlayerMoveCoroutine());
        }

        flashLight.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    IEnumerator PlayerMoveCoroutine()
    {
        while (Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                _fApplyRunSpeed = fSpeed;
                _bRunFlag = true;
            }
            else
            {
                _fApplyRunSpeed = 0f;
                _bRunFlag = false;
            }

            _dir.Set(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (Mathf.Abs(_dir.x) > 0)
                _dir.y = 0;

            while (_curWalkCount < iWalkCount)
            {
                transform.Translate(new Vector2(
                    _dir.x * (fSpeed + _fApplyRunSpeed), _dir.y * (fSpeed + _fApplyRunSpeed)));

                if (_bRunFlag)
                    ++_curWalkCount;
                ++_curWalkCount;
                yield return new WaitForSeconds(0.01f);
            }

            _curWalkCount = 0;
        }

        _bCanMove = true;
    }
    
}
