using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFactory : AbsObjFactory
{
    [Header("Obj")]
    [SerializeField]
    private List<GreenObj> greenObj = new List<GreenObj>();
    [SerializeField]
    private List<OtherObj> otherObj = new List<OtherObj>();

    /// <summary>
    /// 색깔과 포지션을 받아 오브젝트를 알맞은 위치에 소환시켜줌
    /// </summary>
    /// <param name="eColor">Object의 색</param>
    /// <param name="pos">Object를 소환할 위치</param>
    /// <returns></returns>
    public override Obj CreateObj(EColor eColor, Vector3 pos)
    {
        Obj obj = null;

        if (eColor == EColor.Green)
        {
            obj = Instantiate(greenObj[RandomIndex(eColor)]);
        }
        else if (eColor == EColor.Other)
        {
            obj = Instantiate(otherObj[RandomIndex(eColor)]);
        }
        obj.transform.position = pos;

        return obj;
    }
    /// <summary>
    /// 초록색과 초록색이 아닌 오브젝트의 Capacity가 달라서 색을 받아 랜덤 index를 반환해주는 함수
    /// </summary>
    /// <param name="eColor">색</param>
    /// <returns>랜덤으로 뽑힌 index반환</returns>
    public override int RandomIndex(EColor eColor)
    {
        int rnd = 0;
        if (eColor == EColor.Green)
        {
            rnd = Random.Range(0, greenObj.Count);
        }
        else if(eColor == EColor.Other)
        {
            rnd = Random.Range(0, otherObj.Count);
        }
        return rnd;
    }
}
