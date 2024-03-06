using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{

    bool flashlightActive = false;
    float battery = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(flashlightActive) 
        { 
            powerCooldown(false); 
        }
        else
        {
            powerCooldown(true);
        }

    }

    public void OnOffFlashlight()
    {
        if (flashlightActive)
        {
            flashlightActive = false;
        }
        else
        {
            flashlightActive = true;
        }

        UDPSender.SendBroadcast("Flashlight");
        UIManager.Instance.ShowFlashlightOverlay(flashlightActive);
    }

    void powerCooldown(bool charging)
    {
        if (!charging)
        {
            if (battery > 0)
            {
                battery -= 0.0001f; //Debug.Log(battery);
            }
            else
            {
                OnOffFlashlight();
            }
        }
        else
        {
            if (battery < 1)
            {
                   battery += 0.00005f;
            }
        }

        UIManager.Instance.UpdateFlashlightSlider(battery);
    }
}
