using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DotShower : MonoBehaviour
{
    [SerializeField] Transform playerTransform = null;
    [SerializeField] TextMeshProUGUI dotShowerText = null;
    [SerializeField] TargetTransform target = null;
    [SerializeField] Vector3 heightOffset = new Vector3();
    
    void Start()
    {
        heightOffset = new Vector3(0,playerTransform.localScale.y + 3.50f, 0);
    }
    
    void Update()
    {
        if (target.Value == null)
            return;

        transform.position = playerTransform.position + heightOffset;

        Vector3 dir = playerTransform.forward - target.Value.position;

        Quaternion lookDir = Quaternion.LookRotation(dir);

        transform.rotation = Quaternion.Euler(0f, lookDir.y, 0f);

        float dot = Vector3.Dot(playerTransform.forward.normalized, target.Value.position.normalized);

        dotShowerText.text = $"{target.Value.name}  @dir: {dot}";
    }
}

