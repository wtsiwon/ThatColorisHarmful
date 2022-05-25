using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class ObjFactory : AbsObjFactory 
{
    [Header("Obj")]
    [SerializeField]
    private GreenObj[] greenObj;
    private OtherObj[] otherObj;

    public override Obj CreateObj(EColor eColor, int index, Vector3 pos)
    {
        Obj obj = null;

        if(eColor == EColor.Green)
        {
            obj = Instantiate(greenObj[index]);
        }
        else if(eColor == EColor.Other)
        {
            obj = Instantiate(otherObj[index]);
        }
        obj.transform.position = pos;

        return obj;
    }
}
