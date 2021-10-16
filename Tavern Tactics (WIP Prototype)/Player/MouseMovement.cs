using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMovement : MonoBehaviour
{
    public GameObject playerCharacter;

    PlayerControls playerControls;

    public Transform targetTile;

    public Vector3 targetPos;
    public float speed = 5f;

    private void Awake()
    {
        playerControls = new PlayerControls();
    }

    private void OnEnable()
    {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    }

    void Start()
    {
        playerControls.Mouse.MouseClick.performed += _ => MouseClick();
    }

    void Update()
    {
        //MOVE TO A CHARACTER CLASS FOR USE WITH AI
        if (targetTile != null) 
        {
            if (targetPos != targetTile.position) 
            {
                targetPos = new Vector3(targetTile.position.x, transform.position.y, targetTile.position.z);
            }

            if (Vector3.Distance(transform.position, targetPos) > 0.001f) 
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos, speed * Time.deltaTime);
            }
        }
    }

    private void MouseClick() 
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, LayerMask.GetMask("FloorTile")))
        {
            if (hit.transform.tag == "Tile")
            {
                if (TurnManager.Instance.playerTurnState == "Moving" && hit.transform.GetComponent<FloorTile>().state == "Move")
                {
                    targetTile = hit.transform;
                    TurnManager.Instance.playerTurnState = null;
                    PlayerTurnUI.Instance.menuState = "MainMenu";
                    GetComponent<CharacterStats>().ActionCounted();
                }
            }
        }
    }
}
