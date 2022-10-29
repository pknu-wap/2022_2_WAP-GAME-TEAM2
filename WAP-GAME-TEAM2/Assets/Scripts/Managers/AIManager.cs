using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MovingObject
{
    public float inter_MoveWaitTime;        // 이동 대기 시간
    private float current_interMWT;         // 실질적 대기 시간 계산

    private Vector2 PlayerPos;              // 플레이어의 좌표
    private Vector2 AIPos;                  // AI의 좌표

    private string direction;               // 이동 방향

    public bool chase = false;              // 참일시 추적

    // Start is called before the first frame update
    void Start()
    {
        queue = new Queue<string>();
        current_interMWT = inter_MoveWaitTime;
    }

    // Update is called once per frame
    void Update()
    {
        current_interMWT -= Time.deltaTime;

        // 움직임 시도
        if (current_interMWT <= 0 && chase)
        {
            current_interMWT = inter_MoveWaitTime;      // 한번 움직이면 다시 시간 초기화

            // direction에 player를 추격하도록 입력
            ToPlayerDirection();                        

            // 움직일 방향에 장애물이 있을 경우
            if(base.CheckCollision())
            {
                return;
            }
            base.Move(direction);                       // direction방향으로 이동
        }
    }

    // direction에 player를 추격하도록 입력
    private void ToPlayerDirection()
    {
        vector.Set(0, 0, vector.z);                     // MovingObject의 애니메이터 결정

        PlayerPos = PlayerManager.instance.transform.position;
        AIPos = new Vector2(this.transform.position.x, this.transform.position.y);

        // Player와 AI의 위치 차이를 가지는 벡터
        // Player가 왼쪽, 아래에 있다면 x, y가 음수값을 가진다.
        Vector2 deltaVector = PlayerPos - AIPos;

        // 플레이어와 좌우로 더 멀리 떨어져 있을 경우
        if (Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y))
        {
            // 왼쪽으로 이동
            if (deltaVector.x < 0)
            {
                vector.x = -1f;
                direction = "LEFT"; 
            }
            // 오른쪽으로 이동
            else
            {
                vector.x = 1f;
                direction = "RIGHT";
            }
        }
        // 플레이어와 상하로 더 멀리 떨어져 있을 경우
        else
        {
            // 아래쪽으로 이동
            if (deltaVector.y < 0)
            {
                vector.y = -1f;
                direction = "DOWN";
            }
            // 위쪽으로 이동
            else
            {
                vector.y = 1f;
                direction = "UP";
            }
        }
    }
}
