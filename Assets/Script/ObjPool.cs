using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : Singleton<ObjPool>
{
    Obj poolObj;

    private Queue<Obj> pool = new Queue<Obj>();

    public Obj GetObj()
    {
        Obj obj = null;

        if (pool.Count > 0)
        {
            obj = pool.Dequeue();
            obj.gameObject.SetActive(true);
        }
        else
        {
            obj = Instantiate(poolObj);
        }
        return obj;
    }
    public Obj GetObj(EColor color)
    {
        Obj obj = GetObj();
        switch (color)
        {
            case EColor.Green:

                break;
            
        }//색의 종류
        return obj;
    }
    public void Return(Obj obj)
    {
        pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
