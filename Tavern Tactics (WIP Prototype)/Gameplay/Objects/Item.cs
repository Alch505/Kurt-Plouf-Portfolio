using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Item", order = 1)]
public class Item : ScriptableObject
{
    [Header("Main settings")]
    public string itemName;

    /*Order for sorting:
    HpHeal
    MpHeal
    StatusHeal
    Weapon
    Torso
    Legs
    Accessory
    */
    [Tooltip("Order for sorting: \nHpHeal\nMpHeal\nStatusHeal\nWeapon\nTorso\nLegs\nAccessory")]
    public string category;

    public int sortId;

    public MonoBehaviour behaviour;

    [Header("Equipment Settings")]
    public bool isEquipment;

    [Tooltip("0 - Weapon, 1 - Torso, 2 - Legs, 3 - Accessory")]
    public int equipmentType;

    public int attack;
    public int defense;
    public int speed;
}
