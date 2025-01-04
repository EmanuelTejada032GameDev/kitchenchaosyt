using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{

    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnGameObjectVisual;
    [SerializeField] private GameObject stoveOnSizingParticlesVisual;


    private void Start()
    {
        stoveCounter.OnStateChange += StoveCounter_OnStateChanged;
    }

    private void StoveCounter_OnStateChanged(object sender, StoveCounter.OnStoveStateChangeEventArgs e)
    {
        bool showStoveOnVisual = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;

        stoveOnGameObjectVisual.SetActive(showStoveOnVisual);
        stoveOnSizingParticlesVisual.SetActive(showStoveOnVisual);
    }
}
