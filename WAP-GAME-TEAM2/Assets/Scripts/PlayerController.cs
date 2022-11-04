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
    private bool isPause; 
    public bool IsPause
    {
        get => isPause;
        set => isPause = value;
    }

    [SerializeField] private int iWalkCount;
    private int _curWalkCount;

    private Vector2 _dir;

    private Animator _anim;
    [SerializeField] private Animator _baloonAnim;
    
    private BoxCollider2D _boxCollider;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private Light2D flashLight;

    private AudioManager theAudio;
    [SerializeField] private string stepSound;
    
#region Singleton
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
    #endregion Singleton

    void Start()
    {
        _anim = GetComponent<Animator>();
        _boxCollider = GetComponent<BoxCollider2D>();
        theAudio = AudioManager.instance;
        _bCanMove = true;
    }

    void Update()
    {
        if (!isPause && (Input.GetAxisRaw("Horizontal") != 0|| Input.GetAxisRaw("Vertical") != 0) && _bCanMove)
        {
            _bCanMove = false;
            StartCoroutine(PlayerMoveCoroutine());
        }
        
        //flashLight.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y + 0.5f);
    }

    IEnumerator PlayerMoveCoroutine()
    {
        while (!isPause && Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
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
            if (_dir.x != 0)
                _dir.y = 0;
            
            _anim.SetFloat("DirX", _dir.x);
            _anim.SetFloat("DirY", _dir.y);
            
            bool checkCollisionFlag = CheckCollision();
            if (checkCollisionFlag)
                break;
            
            _anim.SetBool("Walking", true);
            theAudio.PlaySFX(stepSound);
            
            while (_curWalkCount < iWalkCount)
            {
                transform.Translate(new Vector2(
                    _dir.x * (fSpeed + _fApplyRunSpeed), _dir.y * (fSpeed + _fApplyRunSpeed)));

                if (_bRunFlag)
                    ++_curWalkCount;
                ++_curWalkCount;
                yield return new WaitForSeconds(0.01f);
            }

            _anim.SetBool("Walking", false);
            _curWalkCount = 0;
        }

        _bCanMove = true;
    }
    
    protected bool CheckCollision()
    {
        RaycastHit2D hit;

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(_dir.x * fSpeed * iWalkCount , _dir.y * fSpeed * iWalkCount );

        _boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        _boxCollider.enabled = true;
        
        if (hit.transform)
            return true;
        return false;
    }

    public void SetBalloonAnim()
    {
        _baloonAnim.SetTrigger("Balloon");
    }

    public void SetPlayerDirAnim(string _dir, float val)
    {
        if (_dir == "DirX")
            _anim.SetFloat("DirY", 0f);
        else
            _anim.SetFloat("DirX", 0f);

        _anim.SetFloat(_dir, val);
    }
}
