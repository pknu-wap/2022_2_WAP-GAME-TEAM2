using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TransferMap : MonoBehaviour
{
    [SerializeField] private bool isSceneChange;
    [Tooltip("장소 전환용 좌표")]
    [SerializeField] private Vector2 v_targetPos;

    [Tooltip("씬 전환을 이용한 장소 전환 씬 문자열")] 
    [SerializeField] private string sceneName;
    
    // 카메라 영역 설정을 위한 박스 콜라이더
    [SerializeField] private BoxCollider2D targetBound;

    private CameraManager theCamera;
    private PlayerController thePlayer;

    void Start()
    {
        theCamera = CameraManager.instance;
        thePlayer = PlayerController.instance;
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            if (isSceneChange)
            {
                thePlayer.gameObject.transform.position = v_targetPos;
                theCamera.transform.position =
                    new Vector3(v_targetPos.x, v_targetPos.y, theCamera.transform.position.z);
                SceneManager.LoadScene(sceneName);
            }
            else
            {
                thePlayer.gameObject.transform.position = v_targetPos;
                theCamera.SetBound(targetBound);
                theCamera.transform.position =
                    new Vector3(v_targetPos.x, v_targetPos.y, theCamera.transform.position.z);
            }
            
        }
    }
}
