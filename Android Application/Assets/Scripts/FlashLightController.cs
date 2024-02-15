using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{

    bool flashlightActive = false;
    [SerializeField]
    GameObject spotLight;
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

        spotLight.SetActive(flashlightActive);
        UIManager.Instance.ShowFlashlightOverlay(flashlightActive);
    }

    void powerCooldown(bool charging)
    {
        if (!charging)
        {
            if (battery > 0)
            {
                battery -= 0.0001f;
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
