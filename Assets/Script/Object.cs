using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EColor
{
    Green,
    Other
}
public class Object : MonoBehaviour
{
    private Rigidbody2D rb;
    public bool isSlow;
    public EColor eColor;
    public float dspd;
    private float spd;


    private void Start()
    {
        spd = dspd;
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector3.down * spd;
        GameManager.Instance.time = 100;
    }
    //public void SetBox(EColor eColor, int index)
    //{
    //    int rndObj = Random.Range(1, 21);
    //    int rndColor = Random.Range(1, 7);
    //}
   
   
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ground"))
        {
            GameManager.Instance.hp -= 1;
            Destroy(gameObject);
        }
        if (collision.CompareTag("SlowZone"))
        {
            isSlow = true;
            if (GameManager.Instance.Score >= 10000000)
            {
                spd = 4;
            }
            else if (GameManager.Instance.Score >= 1000000)
            {
                spd = 3;
            }
            else if(GameManager.Instance.Score >= 100000)
            {
                spd = 2;
            }
            else
            {
                spd = 1;
            }
            rb.velocity = Vector3.down * spd;
        }
    }
    private void OnDestroy()
    {
        if (eColor == EColor.Green)
        {
            CameraManager.Instance.Shake();
        }
        else
        {
            //²¨Áö¼À
        }
        GameManager.Instance.SpawnObj();
        GameManager.Instance.time = 0;

    }
}
