using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Status Effect", menuName = "Status Effect", order = 2)]
public class StatusEffect : ScriptableObject
{
    public string statusName;

    public int timer;

}
