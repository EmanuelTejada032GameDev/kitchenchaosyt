using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private CuttingCounter cuttingCounter;
    private const string CUT = "Cut";

    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }

    private void Start()
    {
        cuttingCounter.OnCut += CuttingOunter_OnCut;
    }

    private void CuttingOunter_OnCut(object sender, EventArgs e)
    {
        animator.SetTrigger(CUT);
    }
}
