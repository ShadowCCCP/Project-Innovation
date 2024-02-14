using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashLightController : MonoBehaviour
{

    bool flashlightActive = false;
    [SerializeField]
    GameObject spotLight;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
