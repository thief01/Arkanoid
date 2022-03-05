using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FXExplode : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void Play()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        animator.SetTrigger("Play");
        PrefabCollector<FXExplode>.Instance.Destroy(this, 1);
    }
}
