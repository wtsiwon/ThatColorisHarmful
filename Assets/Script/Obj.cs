using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Green,
    Other
}
public class Obj : MonoBehaviour
{
    public EColor eColor;
    public const float dspd = 10;//���� �ӵ�(������ ����)
    public const float slowspd = 5;

    //������ ���� ���� �������� �ӵ��� ������
    private float[] increment = new float[6] {10000, 50000, 200000, 500000, 1000000, 2000000};
    
    public float spd { get; set; }

    public bool isCan;//��ư�� ���ȳ�?(�̹� �ѹ� Ʋ�Ȱų� �����ϰų� �� �� �ֳ�?)
    public bool isSlow;//�ӵ��� ���� ���°�?

    private Rigidbody2D rb;

    private void OnEnable()
    {
        isCan = true;
        spd = dspd;

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * spd;
        GameManager.Instance.time = 100;
    }

    private void Update()
    {
        if (SlowCheck(this) == true)
        {
            isSlow = true;
            Slow();
        }
        else
        {
            isSlow = false;
            SlowEnd();
        }
        if (isSlow == true)
        {
            for (int i = 0; i < increment.Length; i++)
            {
                if(GameManager.Instance.Score > increment[i])
                {

                }
            }
            rb.velocity = Vector2.down * spd;//??? ��� ���� �ұ�Ģ����s
        }
    }
    /// <summary>
    /// ������ ���� �ϳ� �ƴϳ� �Ǻ����ִ��Լ�
    /// </summary>
    /// <param name="obj">�Ǻ��� ������Ʈ</param>
    /// <returns></returns>
    private bool SlowCheck(Obj obj)
    {
        if (obj.transform.position.y < 10 && isCan == false)
        {
            return true;
        }
        else return false;
    }
    /// <summary>
    /// ������ 
    /// </summary>
    private void Slow()
    {
        spd = slowspd;
    }
    /// <summary>
    /// ������ ��
    /// </summary>
    private void SlowEnd()
    {
        spd = dspd;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            Debug.Log(collision);
            return;
        }
        if (collision.CompareTag("Ground"))
        {
            GameManager.Instance.hp -= 1;
            Destroy(gameObject);
        }
        else if (collision.CompareTag("Border"))
        {
            Destroy(gameObject);
        }
        
    }
    public bool IsCheckGreen(Obj obj)
    {
        if (obj.eColor == EColor.Green) return true;
        else return false;
    }
}
