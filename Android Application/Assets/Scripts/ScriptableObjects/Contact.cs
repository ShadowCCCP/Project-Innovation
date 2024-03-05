using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Contact", menuName = "Contact")]
public class Contact : ScriptableObject
{
    public string ContactName;
    public string Details;
    public Sprite Icon;
}
