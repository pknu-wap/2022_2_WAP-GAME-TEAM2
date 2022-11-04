using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    public string characterName;

    public float speed;
    protected Vector3 vector;

    public Queue<string> queue;

    public int walkCount;
    protected int currentWalkCount;

    private bool notCoroutine = false;          // coroutine 중복 실행 방지
    protected bool canMove = true;

    public Animator animator;

    public BoxCollider2D boxCollider;
    public LayerMask layerMask;                 // 통과불가능한 레이어 설정

    public void Move(string _dir, int _frequency = 5)
    {
        queue.Enqueue(_dir);
        StartCoroutine(MoveCoroutine(_dir, _frequency));
        //if (!notCoroutine)
        //{
        //    notCoroutine = true;
        //    StartCoroutine(MoveCoroutine(_dir, _frequency));
        //}
    }

    IEnumerator MoveCoroutine(string _dir, int _frequency)
    {
        while(queue.Count != 0)
        {
            switch (_frequency)
            {
                case 1:
                    yield return new WaitForSeconds(4f);
                    break;
                case 2:
                    yield return new WaitForSeconds(3f);
                    break;
                case 3:
                    yield return new WaitForSeconds(2f);
                    break;
                case 4:
                    yield return new WaitForSeconds(1f);
                    break;
                case 5:
                    break;
            }

            string direction = queue.Dequeue();
            vector.Set(0, 0, vector.z);

            switch (direction)
            {
                case "UP":
                    vector.y = 1f;
                    break;
                case "DOWN":
                    vector.y = -1f;
                    break;
                case "RIGHT":
                    vector.x = 1f;
                    break;
                case "LEFT":
                    vector.x = -1f;
                    break;
            }
            animator.SetFloat("DirX", vector.x);
            animator.SetFloat("DirY", vector.y);

            // npc 충돌 방지
            //while(true)
            //{
            //    bool checkCollisionFlag = CheckCollision();
            //    if (checkCollisionFlag)
            //    {
            //        animator.SetBool("Walking", false);
            //        yield return new WaitForSeconds(1f);
            //    }
            //    else
            //    {
            //        break;
            //    }
            //}
            bool checkCollisionFlag = CheckCollision();
            if (!checkCollisionFlag)
            {
                animator.SetBool("Walking", true);

                // boxCollider를 미리 보내서 끼임 방지
                //boxCollider.offset = new Vector2(vector.x * 0.7f * speed * walkCount, vector.y * 0.7f * speed * walkCount);

                while (currentWalkCount < walkCount)
                {
                    transform.Translate(vector.x * speed, vector.y * speed, 0);

                    currentWalkCount++;

                    // if (currentWalkCount == walkCount * 0.5f + 2)
                    // {
                    //     boxCollider.offset = Vector2.zero;
                    // }

                    // 초가 작을 수록 캐릭터가 자연스럽게 움직인다. 
                    yield return new WaitForSeconds(0.005f);
                }
                currentWalkCount = 0;
                if (_frequency != 5)
                {
                    animator.SetBool("Walking", false);
                }
            }
        }
        animator.SetBool("Walking", false);
        canMove = true;
        notCoroutine = false;
    }

    protected bool CheckCollision()
    {
        // 실질적으로 움직임을 막는다.
        RaycastHit2D hit;

        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(vector.x * speed * walkCount, vector.y * speed * walkCount);


        //Vector2 start = new Vector2(transform.position.x * speed * walkCount,
        //    transform.position.y * speed * walkCount);      // 현재 캐릭터 위치
        //Vector2 end = start + new Vector2(vector.x * speed, vector.y * speed);        // 이동하고자하는 위치

        boxCollider.enabled = false;
        hit = Physics2D.Linecast(start, end, layerMask);
        boxCollider.enabled = true;

        // layMask에 막혔을 경우 이동 금지
        if (hit.transform != null)
        {
            return true;
        }
        return false;
    }
}
