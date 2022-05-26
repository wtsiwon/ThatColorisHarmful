using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : Singleton<SlowZone>
{
    public GameObject instanceobj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Obj = collision.GetComponent<Obj>();
        if(collision.CompareTag("Green") || collision.CompareTag("OtherColor"))
        {
            instanceobj = collision.gameObject;
            Obj.isCan = true;
        }
    }
}
