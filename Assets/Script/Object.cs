using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Green,
    Other
}
public enum EObjType
{
   One,
   Two,
   Three,
   Four,
   Five,
   Six,
}
public class Object : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isSlow;
    public EColor eColor;
    public float dspd;
    private float spd;


    
    private void OnEnable()
    {
        spd = dspd;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.down * spd;
    }
    //public void SetBox(EColor eColor, int index)
    //{
    //    int rndObj = Random.Range(1, 21);
    //    int rndColor = Random.Range(1, 7);
    //}
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("SlowZone"))
        {
            isSlow = true;
            spd = 20;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("SlowZone"))
        {
            spd = dspd;
        }
    }
    private void OnDestroy()
    {
        if(eColor == EColor.Green)
        {
            //ÀÌÆÑÆ®
        }
        else
        {
            //²¨Áö¼À
        }
        //´ÙÀ½ ¹°°Ç ¶³¾îÁö±â
        GameManager.Instance.time = 0;
    }
}
