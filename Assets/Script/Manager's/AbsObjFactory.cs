using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsObjFactory : MonoBehaviour
{
    public abstract Obj CreateObj(EColor eColor, Vector3 pos);
    public abstract int RandomIndex(EColor eColor);
}
