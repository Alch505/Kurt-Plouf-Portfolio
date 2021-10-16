using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Character Sheet", menuName = "Character Sheet")]
public class CharacterSheet : ScriptableObject
{
    [Header("Resource Stats and Status")]
    public string charName;

    public int maxHp;
    public int curHp;

    public int maxMp;
    public int curMp;

    public int actions;

    public StatusEffect[] statuses;
    public int[] statusTimer;

    [Header("Attack and Defense stats")]
    public int strength;
    public int intelligence;
    public int defense;
    public int magDefense;
    public int luck;

    [Header("Turn Order stats")]
    public int speed;
    public int dexterity;

    [Header("Range Stats")]
    public int moveSpeed;
    public int weaponRange;

    [Header("Inventory")]
    public int gold;
    public Item[] inventory;

    [Tooltip("0 - Weapon, 1 - Torso, 2 - Legs, 3 - Accessory")]
    public Item[] equipment;
}
