using UnityEngine;

public class PlayerNoStickState : PlayerBaseState
{
    private Inventory inventory;

    // Pass the inventory reference when entering the state
    public override void EnterState(PlayerStateManager player, Inventory playerInventory)
    {
        inventory = playerInventory;
        /* Set the player's position
        Vector3 startingPosition = new Vector3(9.12f, 1.34f, 3.31f);
        player.transform.localPosition = startingPosition;*/
    }

    public override void UpdateState(PlayerStateManager player)
    {
        // Check for the stick in the inventory
        if (inventory != null && inventory.items.Count > 0)
        {
            GameObject stickObject = GameObject.Find("Stick");
            if (stickObject != null && inventory.items.Contains(stickObject))
            {
                Debug.Log("Found stick in inventory!");
                player.SwitchState(player.HasStick);
                return;
            }
        }
        else
        {
            Debug.Log("Inventory is empty!");
        }
    }
    public override void OnCollisionEnter(PlayerStateManager player, Collision collision) { }
}
