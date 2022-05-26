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
    private Rigidbody2D rb;
    public EColor eColor;
    public float dspd;
    public bool isCan;
    private float spd;


    private void Start()
    {
        spd = dspd;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * spd;
        GameManager.Instance.time = 100;
    }
    //public void SetBox(EColor eColor, int index)
    //{
    //    int rndObj = Random.Range(1, 21);
    //    int rndColor = Random.Range(1, 7);
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision == null)
        {
            Debug.Log(collision);
            return;//�ƴ� �ƹ��͵� ���� ���� �����϶�� �߹߷þ�
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
        if (rb == null)
        {
            Debug.Log(gameObject.name);
        }
        if (collision.CompareTag("SlowZone"))
        {
            if (GameManager.Instance.Score >= 1000000)
            {
                spd = 4;
            }
            else if (GameManager.Instance.Score >= 100000)
            {
                spd = 3;
            }
            else if (GameManager.Instance.Score >= 10000)
            {
                spd = 2;
            }
            else
            {
                spd = 1;
            }
            rb.velocity = Vector2.down * spd;//??? ��� ���� �ұ�Ģ���� ����..
        }
    }
    public bool IsCheckGreen(Obj obj)
    {
        if (obj.eColor == EColor.Green) return true;
        else return false;
    }
}