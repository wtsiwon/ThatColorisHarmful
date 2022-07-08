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
    public const float DSPD = 10;//���� �ӵ�(������ ����)
    public const float SLOWSPD = 5;
    public const float FASTSPD = 50;
    

    //������ ���� ���� �������� �ӵ��� ������
    private float[] increment = new float[6] {10000, 50000, 200000, 500000, 1000000, 2000000};
    private Dictionary<float, float> incrementdic = new Dictionary<float, float>();

    public float spd { get; set; }

    public bool isCan;//��ư�� ���ȳ�?(�̹� �ѹ� Ʋ�Ȱų� �����ϰų� �� �� �ֳ�?)
    public bool isSlow;//�ӵ��� ���� ���°�?

    private Rigidbody2D rb;

    private void OnEnable()
    {
        isCan = true;
        spd = DSPD;
        Debug.Log(spd);
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * spd;
        GameManager.Instance.time = 100;
    }

    private void Update()
    {
        if (SlowCheck() == true)
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
                    //increment[i]
                }
            }
            rb.velocity = Vector2.down * spd;
        }
    }
    /// <summary>
    /// ������ ���� �ϳ� �ƴϳ� �Ǻ����ִ��Լ�
    /// </summary>
    /// <param name="obj">�Ǻ��� ������Ʈ</param>
    /// <returns></returns>
    private bool SlowCheck()
    {
        if (transform.position.y < 3 && isCan == true)
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
        spd = SLOWSPD;
    }
    /// <summary>
    /// ������ ��
    /// </summary>
    private void SlowEnd()
    {
        spd = DSPD;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
    /// <summary>
    /// �ʷϻ��̳� �ƴϳ� Ȯ���ϴ� �Լ�
    /// </summary>
    /// <param name="obj">Ȯ���� ������Ʈ</param>
    /// <returns></returns>
    public bool IsCheckGreen(Obj obj)
    {
        if (obj.eColor == EColor.Green) return true;
        else return false;
    }
}
