using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostStateHidden : GhostState
{

    public GhostStateHidden(GhostController ghostController) : base(ghostController) { }

    public override void OnStateEnter()
    {
        ghostController.collider.enabled = false;
        moveSpeed = 0;
        useFading = false;
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        //if(Vector3.Distance(ghostController.transform.position, ghostController.playerController.transform.position) <= ghostController.distanceToSpawn)
        //{
        //    ghostController.ChangeGhostState(new GhostStateSpawning(ghostController));
        //}
    }
}
