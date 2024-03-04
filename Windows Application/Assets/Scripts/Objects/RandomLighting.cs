using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLighting : MonoBehaviour
{
    [SerializeField]
    Transform lightningHolder;
    [SerializeField]
    GameObject lightningPrefab;
    [SerializeField]
    int lightningCount = 3;
    bool onGoingLightning;
    [SerializeField]
    int timeSpan = 4;

    void Update()
    {
        if (!onGoingLightning)
        {
            onGoingLightning = true;
            StartCoroutine(waitAmountOfTime(Random.Range(0, timeSpan)));

        }
    }

    IEnumerator instantiateLightningEffect() 
    {
        List<GameObject> lightningEffects = new List<GameObject>();
        for(int i =0; i<lightningCount; i++)
        {
           GameObject lightning =  Instantiate(lightningPrefab, lightningHolder);
            lightningEffects.Add(lightning);
        }

        yield return new WaitForSeconds(2);
        for(int i=0;i< lightningEffects.Count;i++)
        {
            Destroy(lightningEffects[i]);

        }
        onGoingLightning = false;
    }

    IEnumerator waitAmountOfTime(int seconds)
    {
        yield return new WaitForSeconds(seconds);
        StartCoroutine(instantiateLightningEffect());
    }
}
