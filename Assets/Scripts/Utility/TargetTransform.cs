using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class TargetTransform : ScriptableObject
{
    private Transform target = null;
    public Transform Value => target;

    public void SetTarget(Transform t) => target = t;
}
