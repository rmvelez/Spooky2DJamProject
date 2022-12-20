using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateSpikeAttack : BossState
{
    private int nrOfRepetitions = 2;
    private int currentNrOfRepetitions = 0;


    public BossStateSpikeAttack(BossController bossController) : base(bossController)
    {

    }

    public override void OnStateEnter()
    {
        nrOfRepetitions = Random.Range(bossController.minNrOfSpikeAttackRepetitions, bossController.maxNrOfSpikeAttackRepetitions + 1);
        Debug.Log("Entered spikeattack");
    }

    public override void OnStateExit()
    {
    }

    public override void OnStateFixedUpdate()
    {
    }

    public override void OnStateUpdate()
    {
        bossController.animator.SetTrigger("Spike");
    }

    public void OnSpikeRelease()
    {
        Debug.Log("OnSpikeRelease()");

        GameObject spikeSpawnerGO = GameObject.Instantiate(bossController.spikeSpawnerPrefab, bossController.transform.position, bossController.transform.rotation);
        SpikeSpawner spikeSpawner = spikeSpawnerGO.GetComponent<SpikeSpawner>();
        spikeSpawner.moveVector = (PlayerController.GetInstance().transform.position - bossController.transform.position).normalized;
    
    }

    public void OnSpikeAnimFirstFrame()
    {
        Debug.Log("OnSpikeAnimFirstFrame()");
        currentNrOfRepetitions++;
        if (currentNrOfRepetitions > nrOfRepetitions) bossController.ChangeBossState(new BossStateAboutToFly(bossController));
    }
}
