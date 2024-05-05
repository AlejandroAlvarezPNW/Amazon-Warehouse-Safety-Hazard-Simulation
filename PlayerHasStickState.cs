using UnityEngine;

public class PlayerHasStickState : PlayerBaseState
{
    public override void EnterState(PlayerStateManager player, Inventory playerInventory)
    {
            Debug.Log("Player performs action with the stick!");
            Debug.Log("Hello From the hasStick State ");
    }

    public override void UpdateState(PlayerStateManager player)
    {
        
    }

    public override void OnCollisionEnter(PlayerStateManager player, Collision collision){}
}
