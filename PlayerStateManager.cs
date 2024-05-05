using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateManager : MonoBehaviour
{
    PlayerBaseState currentState;
    public PlayerNoStickState NoStickState = new PlayerNoStickState();
    public PlayerHasStickState HasStick = new PlayerHasStickState();
    // Reference to the player's inventory
    public Inventory playerInventory;


    // Start is called before the first frame update
    void Start()
    {
        // Starting state for the state machine
        currentState = NoStickState;
        // "this" is a reference to the context(this EXACT Monobehavior script) 
        currentState.EnterState(this, playerInventory);
        
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    private void OnCollisionEnter(Collision collision)
    {
        currentState.OnCollisionEnter(this, collision);
    }

    public void SwitchState(PlayerBaseState state) 
    {
        currentState = state;
        state.EnterState(this, playerInventory);
    }



}
