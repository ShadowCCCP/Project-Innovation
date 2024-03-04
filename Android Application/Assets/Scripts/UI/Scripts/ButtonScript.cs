using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public void InvokeStartGame()
    {
        EventBus<StartGameEvent>.Publish(new StartGameEvent());
    }

    public void InvokeRestartGame()
    {
        EventBus<GameRestartEvent>.Publish(new GameRestartEvent());
    }
}
