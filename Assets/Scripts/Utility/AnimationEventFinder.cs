
#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AnimationEventFinder : MonoBehaviour
{
    [SerializeField] GameObject[] animationFBX_Array = null;

    [Header("Leave at 0 or empty except to pass to event")]
    [SerializeField] private float eventFloatParam = 0;
    [SerializeField] private int eventIntParam = 0;
    [SerializeField] private string eventStringParam = "";
    [SerializeField] private Object eventObjectRefParam = null;

    private void Start()
    {
        List<AnimationClip> anims = new List<AnimationClip>();

        foreach (GameObject fbx in animationFBX_Array)
        {
            foreach (AnimationClip clip in AnimationUtility.GetAnimationClips(fbx))
            {
                anims.Add(clip);
            }
        }

        PassParametersToAnimationEvents(anims, eventFloatParam, eventIntParam, eventStringParam, eventObjectRefParam);
    }

    private void PassParametersToAnimationEvents(List<AnimationClip> clips, float f, int i, string s, Object o) 
    {
        foreach (AnimationClip clip in clips) 
        {
            AnimationEvent [] events = AnimationUtility.GetAnimationEvents(clip);

            foreach (AnimationEvent e in events) 
            {
                e.floatParameter = f;
                e.intParameter = i;
                e.stringParameter = s;
                e.objectReferenceParameter = o;
            }
        }
    }
}
#endif