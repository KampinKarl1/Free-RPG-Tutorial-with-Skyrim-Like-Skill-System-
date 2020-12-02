using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshColliderFixer : MonoBehaviour
{
    private void Awake()
    {
        MeshCollider[] meshCols = FindObjectsOfType<MeshCollider>();

        for (int i = 0; i < meshCols.Length; ++i) 
        {
            if (!meshCols[i].GetComponent<BSPTree>())
                meshCols[i].gameObject.AddComponent<BSPTree>();
        }
    }
}
