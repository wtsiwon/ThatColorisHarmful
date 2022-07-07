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
    public const float dspd = 10;//원래 속도(변하지 않음)
    public const float slowspd = 5;

    //점수가 높을 수록 느려지는 속도가 빨라짐
    private float[] increment = new float[6] {10000, 50000, 200000, 500000, 1000000, 2000000};
    
    public float spd { get; set; }

    public bool isCan;//버튼이 눌렸나?(이미 한번 틀렸거나 성공하거나 할 수 있나?)
    public bool isSlow;//속도가 느려 졋는가?

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
            rb.velocity = Vector2.down * spd;//??? 어떨땐 나고 불규칙적인s
        }
    }
    /// <summary>
    /// 느리가 가야 하냐 아니냐 판별해주는함수
    /// </summary>
    /// <param name="obj">판별할 오브젝트</param>
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
    /// 느리게 
    /// </summary>
    private void Slow()
    {
        spd = slowspd;
    }
    /// <summary>
    /// 느리게 끝
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
