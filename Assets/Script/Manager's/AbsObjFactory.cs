using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsObjFactory : MonoBehaviour
{
    public abstract Obj CreateObj(EColor eColor, int index, Vector3 pos);
}
