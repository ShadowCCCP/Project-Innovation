using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICallManager
{
    void PickCall(CallEvent callEvent);
    void ShowCallEnded();
}
