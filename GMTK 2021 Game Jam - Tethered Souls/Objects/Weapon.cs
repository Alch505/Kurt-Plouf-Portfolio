using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon", order = 1)]
public class Weapon : ScriptableObject
{
    public string weaponName;

    public bool isAutomatic;

    public int maxAmmo;

    public float rate;
    public int damage;
}
