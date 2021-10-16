using UnityEngine;

public class FloorTile : MonoBehaviour
{
    public Material[] colors;

    public string state = "Null";

    public Transform curCharacter;

    void Start()
    {
        
    }

    void Update()
    {
        GetCharacter();

        SetColor();

        if (TurnManager.Instance.isPlayerTurn == true)
        {
            PlayerTurn();
        }
        else
        {
            state = "Null";
        }
    }

    private void GetCharacter() 
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, 15f, LayerMask.GetMask("Character")))
        {
            curCharacter = hit.transform;
        }
        else
        {
            curCharacter = null;
        }

        if (curCharacter != null && curCharacter.gameObject.tag == "Player") 
        {
            state = "Null";
        }
    }

    private void PlayerTurn() 
    {
        if (!Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("Range")))
        {
            state = "Null";
        }

        switch (TurnManager.Instance.playerTurnState)
        {
            case "Null":
                state = "Null";
                break;
            case "Moving":
                if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("MovementRange")))
                {
                    if (curCharacter == null) 
                    {
                        state = "Move";
                    }
                }
                break;
            case "MeleeAttack":
                if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("MeleeRange")))
                {
                    if (curCharacter != null)
                    {
                        if (curCharacter.gameObject.tag != "Player")
                        {
                            state = "Attack";
                        }
                    }
                    else 
                    {
                        state = "Attack";
                    }
                }
                break;
            case "MagicAttack":
                if (Physics.CheckSphere(transform.position, 0.5f, LayerMask.GetMask("MagicRange")))
                {
                    if (curCharacter != null)
                    {
                        if (curCharacter.gameObject.tag != "Player")
                        {
                            state = "Attack";
                        }
                    }
                    else
                    {
                        state = "Attack";
                    }
                }
                break;
        }
    }

    private void SetColor() 
    {
        switch (state)
        {
            //0 - White, 1 - Blue, 2 - Red
            case "Null":
                GetComponent<MeshRenderer>().material = colors[0];
                break;
            case "Move":
                GetComponent<MeshRenderer>().material = colors[1];
                break;
            case "Attack":
                GetComponent<MeshRenderer>().material = colors[2];
                break;
        }
    }
}
