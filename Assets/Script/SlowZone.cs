using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowZone : Singleton<SlowZone>
{
    public Obj instanceobj;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        var Obj = collision.GetComponent<Obj>();
        if(collision.CompareTag("Green") || collision.CompareTag("OtherColor"))
        {
            instanceobj = Obj;
            Obj.isCan = true;
        }
    }
}
