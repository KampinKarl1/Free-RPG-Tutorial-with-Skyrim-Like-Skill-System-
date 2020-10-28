using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MultiModelCharacter : MonoBehaviour
{
    [SerializeField] private GameObject activeModel = null;
    [SerializeField, Tooltip("Set as true if you want to keep the current model active")] 
    private bool keepCurrent = false;
    [SerializeField] private List<GameObject> modelsToChooseFrom = new List<GameObject>();
    
    void Start()
    {
        if (modelsToChooseFrom.Count == 0)
            return;

        foreach (GameObject model in modelsToChooseFrom)
            model.SetActive(false);

        if (keepCurrent)
        {
            activeModel.SetActive(true);
            return;
        }

        activeModel = modelsToChooseFrom[Random.Range(0, modelsToChooseFrom.Count)];
        activeModel.SetActive(true);
    }
}
