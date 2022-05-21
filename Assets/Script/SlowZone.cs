using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : Singleton<SlowZone>
{
    public GameObject instanceobj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Green") || collision.CompareTag("OtherColor"))
        {
            instanceobj = collision.gameObject;
            GameManager.Instance.OK = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        instanceobj = null;
        GameManager.Instance.OK = false;
    }
}
