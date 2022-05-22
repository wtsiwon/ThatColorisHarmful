using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjPool : Singleton<ObjPool>
{
    Object poolObj;

    private Queue<Object> pool = new Queue<Object>();

    public Object GetObj()
    {
        Object obj = null;

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
    public Object GetObj(EColor color)
    {
        Object obj = GetObj();
        switch (color)
        {
            case EColor.Green:

                break;
            
        }//색의 종류
        return obj;
    }
    public void Return(Object obj)
    {
        pool.Enqueue(obj);
        obj.gameObject.SetActive(false);
    }
}
