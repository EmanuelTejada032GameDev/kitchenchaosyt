using System;
using UnityEngine;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;
    public class OnProgressChangedEventsArgs
    {
        public float progressNormalized;
    }
}
