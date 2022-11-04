using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MovingObject
{
    public float inter_MoveWaitTime;        // �̵� ��� �ð�
    private float current_interMWT;         // ������ ��� �ð� ���

    private Vector2 PlayerPos;              // �÷��̾��� ��ǥ
    private Vector2 AIPos;                  // AI�� ��ǥ

    private string direction;               // �̵� ����

    public bool chase = true;              // ���Ͻ� ����


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

        // ������ �õ�
        if (current_interMWT <= 0 && chase && canMove)
        {
            canMove = false;
            current_interMWT = inter_MoveWaitTime;      // �ѹ� �����̸� �ٽ� �ð� �ʱ�ȭ

            // direction�� player�� �߰��ϵ��� �Է�
            ToPlayerDirection();                        

            // ������ ���⿡ ��ֹ��� ���� ���
            if(base.CheckCollision())
            {
                // �ٸ� �������� �̵�
                direction = ToPlayerRedirection(direction);
            }
            base.Move(direction);                       // direction�������� �̵�
        }
    }

    private string ToPlayerRedirection(string _direction)
    {
        vector.Set(0, 0, vector.z);                     // MovingObject�� �ִϸ����� ����

        PlayerPos = PlayerController.instance.transform.position;
        AIPos = transform.position;

        // Player�� AI�� ��ġ ���̸� ������ ����
        // Player�� ����, �Ʒ��� �ִٸ� x, y�� �������� ������.
        Vector2 deltaVector = PlayerPos - AIPos;

        if (Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y))
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                return "UP";
            }
            else
            {
                return "DOWN";
                
            }

        }
        else
        {
            int randNum = Random.Range(0, 2);
            if (randNum == 0)
            {
                return "LEFT";
            }
            else
            {
                return "RIGHT";
            }
        }

        //if (_direction == "UP" || _direction == "DOWN")
        //{
        //    int randNum = Random.Range(0, 2);
        //    if (randNum == 0)
        //    {
        //        return "LEFT";
        //    }
        //    else
        //    {
        //        return "RIGHT";
        //    }

        //}
        //else if(_direction == "LEFT" || _direction == "RIGHT")
        //{
        //    int randNum = Random.Range(0, 2);
        //    if (randNum == 0)
        //    {
        //        return "UP";
        //    }
        //    else
        //    {
        //        return "DOWN";
        //    }
        //}

        return "DOWN";
    }

    // direction�� player�� �߰��ϵ��� �Է�
    private void ToPlayerDirection()
    {
        vector.Set(0, 0, vector.z);                     // MovingObject�� �ִϸ����� ����

        PlayerPos = PlayerController.instance.transform.position;
        AIPos = transform.position;

        // Player�� AI�� ��ġ ���̸� ������ ����
        // Player�� ����, �Ʒ��� �ִٸ� x, y�� �������� ������.
        Vector2 deltaVector = PlayerPos - AIPos;

        // �÷��̾�� �¿�� �� �ָ� ������ ���� ���
        if (Mathf.Abs(deltaVector.x) > Mathf.Abs(deltaVector.y))
        {
            // �������� �̵�
            if (deltaVector.x < 0)
            {
                vector.x = -1f;
                direction = "LEFT"; 
            }
            // ���������� �̵�
            else
            {
                vector.x = 1f;
                direction = "RIGHT";
            }
        }
        // �÷��̾�� ���Ϸ� �� �ָ� ������ ���� ���
        else
        {
            // �Ʒ������� �̵�
            if (deltaVector.y < 0)
            {
                vector.y = -1f;
                direction = "DOWN";
            }
            // �������� �̵�
            else
            {
                vector.y = 1f;
                direction = "UP";
            }
        }
    }
}
