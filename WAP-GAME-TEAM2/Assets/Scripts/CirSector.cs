using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CirSector : MonoBehaviour
{
    private MovingObject theMovingObject; 

    public Transform target;                                // 체크 대상
        
    public float angleRange = 45f;                          // 시야각
    public float distance = 800f;                           // 시야 거리
    public bool isCollision = false;                      

    private Vector2 vector;                                 // 객체 애니메이터가 바라보고 있는 방향

    Color _blue = new Color(0f, 0f, 1f, 0.2f);
    Color _red = new Color(1f, 0f, 0f, 0.2f);

    Vector3 direction;                                      // 객체에서 대상까지의 벡터

    private void Start()
    {
        theMovingObject = GetComponent<MovingObject>();
    }

    void Update()
    {
        // 객체 애니메이터가 바라보고 있는 방향 업데이트
        vector.Set(theMovingObject.animator.GetFloat("DirX"), theMovingObject.animator.GetFloat("DirY"));
        // 객체에서 대상까지의 벡터
        direction = target.position - transform.position;
        
        // target과 객체 사이의 거리가 distance보다 작다면
        if (direction.magnitude < distance)
        {
            // (타겟-객체)벡터와 (객체방향)벡터 내적
            float dot = Vector3.Dot(direction.normalized, -transform.up);   // 임시 기본값(아래쪽을 보는 경우)

            // 캐릭터가 오른쪽을 보는 경우
            if (vector.x == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.right);
            }
            // 왼쪽을 보는 경우
            else if (vector.x == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.right);
            }
            // 위쪽을 보는 경우
            else if (vector.y == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.up);
            }
            // 아래쪽을 보는 경우
            else if (vector.y == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.up);       
            }

            // 두 벡터 모두 단위 벡터이므로 내적 결과에 cos의 역을 취해서 theta를 구함
            float theta = Mathf.Acos(dot);                              // 역 코사인
            // angleRange와 비교를 위해 각도로 변환
            float degree = Mathf.Rad2Deg * theta;                       // 각도로 변환

            // 시야각 판별
            if (degree <= angleRange / 2f)
            {
                isCollision = true;
            }
            else
            {
                isCollision = false;
            }
        }
        else
        {
            isCollision = false;
        }
        Debug.Log("isCollision: " + isCollision);
    }

    // 에디터에서의 draw gizmos
    private void OnDrawGizmos()
    {
        Vector3 drawVector = -transform.up;
        // vector.Set(theMovingObject.animator.GetFloat("DirX"), theMovingObject.animator.GetFloat("DirY"));
        direction = target.position - transform.position;

        // target과 객체 사이의 거리가 distance보다 작다면
        if (direction.magnitude < distance)
        {
            // (타겟-객체)벡터와 (객체방향)벡터 내적
            float dot = Vector3.Dot(direction.normalized, -transform.up);

            // 캐릭터가 오른쪽을 보는 경우
            if (vector.x == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.right);
                drawVector = transform.right;
            }
            // 왼쪽을 보는 경우
            else if (vector.x == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.right);
                drawVector = -transform.right;
            }
            // 위쪽을 보는 경우
            else if (vector.y == 1f)
            {
                dot = Vector3.Dot(direction.normalized, transform.up);
                drawVector = transform.up;
            }
            // 아래쪽을 보는 경우
            else if (vector.y == -1f)
            {
                dot = Vector3.Dot(direction.normalized, -transform.up);
                drawVector = -transform.up;
            }

            float theta = Mathf.Acos(dot);                                  // 역 코사인
            float degree = Mathf.Rad2Deg * theta;                           // 각도로 변환

            // 시야각 판별
            if (degree <= angleRange / 2f)
            {
                isCollision = true;
            }
            else
            {
                isCollision = false;
            }
        }
        else
        {
            isCollision = false;
        }
        Handles.color = isCollision ? _red : _blue;
        Handles.DrawSolidArc(transform.position, Vector3.forward, drawVector, angleRange / 2, distance);
        Handles.DrawSolidArc(transform.position, Vector3.forward, drawVector, -angleRange / 2, distance);
    }
}
