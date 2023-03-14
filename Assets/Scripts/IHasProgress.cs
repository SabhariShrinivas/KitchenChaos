using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IHasProgress
{
    public event Action<float> OnProgressChanged;
    public event Action<bool> OnKitchenObjectBeingCooked;
}
