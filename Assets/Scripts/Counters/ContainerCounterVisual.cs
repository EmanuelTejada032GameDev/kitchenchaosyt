using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private ContainerCounter containerCounter;
    private const string OPEN_CLOSE = "OpenClose";

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Start()
    {
        containerCounter.OnContainerCounterInteract += OnContainerCounterInteract;
    }

    private void OnContainerCounterInteract(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
