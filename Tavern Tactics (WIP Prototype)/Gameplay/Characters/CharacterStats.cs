using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public CharacterSheet charSheet;

    [Header("Resource Stats and Status")]
    public string charName;

    public int maxHp;
    public int curHp;

    public bool isDead;

    public int maxMp;
    public int curMp;

    public int actions;

    public StatusEffect[] statuses = new StatusEffect[20];
    public int[] statusTimer = new int[20];

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

    [Header("Debug")]
    public bool infActions;

    bool hasWarned = false;

    //----------------------------------------------------------------------------------------------
    #region System functions
    private void Spawn()
    {
        if (charSheet != null)
        {
            SetStats();
        }
        else 
        {
            Debug.LogError($"{this.gameObject.name} has no Character Sheet and has been disabled");
            this.gameObject.SetActive(false);
            return;
        }
    }

    private void SetStats()
    {
        charName = charSheet.charName;

        maxHp = charSheet.maxHp;
        curHp = charSheet.curHp;

        maxMp = charSheet.maxMp;
        curMp = charSheet.curMp;

        statuses = charSheet.statuses;
        statusTimer = charSheet.statusTimer;

        strength = charSheet.strength;
        intelligence = charSheet.intelligence;
        defense = charSheet.defense;
        magDefense = charSheet.magDefense;
        luck = charSheet.luck;

        speed = charSheet.speed;
        dexterity = charSheet.dexterity;

        moveSpeed = charSheet.moveSpeed;
        weaponRange = charSheet.weaponRange;

        gold = charSheet.gold;
        inventory = charSheet.inventory;

        equipment = charSheet.equipment;
    }

    public void ActionCounted()
    {
        //Debug check
        if (!infActions)
        {
            //remove action point and check if character is at 0
            actions -= 1;
            if (actions <= 0)
            {
                EndTurn();
            }
        }
        else 
        {
            if (!hasWarned)
            {
                Debug.LogWarning($"{this.gameObject.name} has inf actions enabled!");
                hasWarned = true;
            }
        }
    }

    public void StartTurn() 
    {
        if (!isDead)
        {
            actions = 2;
        }
        else 
        {
            EndTurn();
        }
    }

    public void EndTurn() 
    {
        //Check statuses and trigger effects 
        if (statuses != null && !isDead) 
        {
            EffectsAtTurnEnd();

            //Reduces counts of all status timers and checks if they hit zero
            for (int i = 0; i < statuses.Length; i++)
            {
                if (statuses[i] != null)
                {
                    statusTimer[i] -= 1;

                    if (statusTimer[i] <= 0)
                    {
                        RemoveStatus(statuses[i].statusName);
                        i -= 1;
                    }
                }
            }
        }

        TurnManager.Instance.NextTurn();
    }
    #endregion

    //----------------------------------------------------------------------------------------------
    #region Main functions
    public bool CheckStatus(string _status) 
    {
        if (statuses != null) 
        {
            for (int i = 0; i < statuses.Length; i++)
            {
                if (statuses[i] != null && statuses[i].statusName == _status)
                {
                    return true;
                }
            }
        }
        return false;
    }

    //Status Effect Methods
    public void AddStatus(StatusEffect _status) 
    {
        //Checks if the character already has the status and if they do it resets the timer rather than adding it again
        if (!CheckStatus(_status.statusName))
        {
            for (int i = 0; i < statuses.Length; i++)
            {
                if (statuses[i] == null)
                {
                    statuses[i] = _status;
                    statusTimer[i] = _status.timer;

                    break;
                }
            }
        }
        else 
        {
            Debug.Log("Resetting a timer");
            for (int i = 0; i < statuses.Length; i++)
            {
                if (statuses[i] != null && statuses[i].statusName == _status.statusName)
                {
                    statuses[i] = _status;
                    statusTimer[i] = _status.timer;

                    break;
                }
            }
        }
    }

    public void RemoveStatus(string _status) 
    {
        for (int i = 0; i < statuses.Length; i++) 
        {
            if (statuses[i] != null && statuses[i].statusName == _status)
            {
                statuses[i] = null;
                statusTimer[i] = 0;
            }
        }
    }

    public void EffectsAtTurnEnd() 
    {
        #region Debuffs
        if (CheckStatus("Poison"))
        {
            StatusBehaviors poison = new Poison();
            poison.ActivateEffect(this);
        }
        #endregion

        #region Buffs
        if (CheckStatus("Regen"))
        {
            StatusBehaviors regen = new Regen();
            regen.ActivateEffect(this);
        }
        #endregion
    }
    #endregion

    //----------------------------------------------------------------------------------------------
    #region Damage functions
    public void TakeDamage(int _dmg, string _type) 
    {
        int dodge = Random.Range(0, 100);

        if (dodge < 100 - (luck / 3))
        {
            //Initiate min and max damage values
            int minDmg = 0;
            int maxDmg = 0;

            //Calculate minDmg and maxDmg based on attack type
            switch (_type)
            {
                case "Physical":
                    minDmg = _dmg - ((defense * 5) / 2);
                    maxDmg = _dmg - ((defense * 7) / 2);
                    break;

                case "Magic":
                    minDmg = _dmg - ((magDefense * 5) / 2);
                    maxDmg = _dmg - ((magDefense * 7) / 2);
                    break;

                case "Piercing":
                    minDmg = _dmg;
                    maxDmg = _dmg;
                    break;

                default:
                    Debug.LogError($"{this.gameObject} took an improper damage type and received no damage! The _type was '{_type}' and _dmg '{_dmg}'");
                    break;
            }

            //Ensure neither can be nagative values thus healing rather than damaging
            if (minDmg < 0)
            {
                minDmg = 0;
            }
            if (maxDmg < 0)
            {
                maxDmg = 0;
            }

            //Apply damage between min and max values
            if (!CheckStatus("Guard") && _type == "Physical")
            {
                curHp -= Random.Range(minDmg, maxDmg);
            }
            else
            {
                curHp -= (Random.Range(minDmg, maxDmg) / 2);
            }
        }
        else
        {
            Debug.Log($"{this.name} dodged the attack!");
        }
    }

    public void HealDamage(int _heal) 
    {
        if (!CheckStatus("Zombie"))
        {
            curHp += _heal;
        }
        else 
        {
            curHp -= _heal;
        }
    }

    public void MeleeAttack() 
    {

    }

    public void UseSkill() 
    {

    }

    public void Die() 
    {
        isDead = true;
        Debug.Log($"{this.name} has died!!!");
    }
    #endregion

    //----------------------------------------------------------------------------------------------
    #region NPC functions
    public void SelectMoveLocation() 
    {

    }
    #endregion

    //----------------------------------------------------------------------------------------------
    #region Unity functions
    private void Awake()
    {
        Spawn();

        //For Testing
        TakeDamage(20, "Physical");

        statusTimer = new int[statuses.Length];

        for (int i = 0; i < statuses.Length; i++)
        {
            if (statuses[i] != null) 
            {
                statusTimer[i] = statuses[i].timer;
            }
        }
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (curHp > maxHp) 
        {
            curHp = maxHp;
        }

        if (curHp <= 0) 
        {
            Die();
        }
    }
    #endregion
}
