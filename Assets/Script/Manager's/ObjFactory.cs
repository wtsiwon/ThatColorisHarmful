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
    /// ����� �������� �޾� ������Ʈ�� �˸��� ��ġ�� ��ȯ������
    /// </summary>
    /// <param name="eColor">Object�� ��</param>
    /// <param name="pos">Object�� ��ȯ�� ��ġ</param>
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
    /// �ʷϻ��� �ʷϻ��� �ƴ� ������Ʈ�� Capacity�� �޶� ���� �޾� ���� index�� ��ȯ���ִ� �Լ�
    /// </summary>
    /// <param name="eColor">��</param>
    /// <returns>�������� ���� index��ȯ</returns>
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
