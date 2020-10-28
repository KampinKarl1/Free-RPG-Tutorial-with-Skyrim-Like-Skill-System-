using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventFinder : MonoBehaviour
{
    [SerializeField] List<AnimationClip> punchAnimations = new List<AnimationClip>();
    [SerializeField] List<AnimationClip> kickAnimations = new List<AnimationClip>();

    [SerializeField] private Object punchObject = null;
    [SerializeField] private Object kickObject = null;

    private void Start()
    {
        PassObjectToAnimationEventsInList(punchAnimations, punchObject);
        PassObjectToAnimationEventsInList(kickAnimations, kickObject);
    }

    private void PassObjectToAnimationEventsInList(List <AnimationClip> anims, Object o) 
    {
        foreach (AnimationClip c in anims)
        {
            AnimationEvent e = c.events[0];
            e.objectReferenceParameter = o;
        }            
    }
}
