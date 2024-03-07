using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StickyNoteManager : MonoBehaviour
{
    [SerializeField]
    GameObject popUpPrefab;

    GameObject activePopUp;

     bool activeNote = false;

    float timertime;

    void Start()
    {

    }

    void Update()
    {
        timertime += Time.deltaTime;

        if (timertime > 1)
        {
            activeNote = false;
            Destroy(activePopUp);

        }
    }
    public void OnFlashLightHover(StickyNote note)
    {
        if (!activeNote)
        {
            activePopUp = Instantiate(popUpPrefab, note.transform.position + new Vector3(0, 0.2f, -0.2f), Quaternion.Euler(-90, 0, 0));
            activePopUp.GetComponentInChildren<TextMeshPro>().SetText(note.popUpText);
            activeNote = true;
            timertime = 0;
        }
    }

}
