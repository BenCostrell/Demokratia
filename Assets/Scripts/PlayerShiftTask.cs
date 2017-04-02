using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShiftTask : Task {

    private Transform playerTransform;
    private Player player;
    private float timeElapsed;
    private float duration;
    private bool votedYea;
    private Vector3 initialPos;
    private Vector3 targetPos;

    public PlayerShiftTask(Player pl, bool yea)
    {
        player = pl;
        votedYea = yea;
    }

    protected override void Init()
    {
        timeElapsed = 0;
        duration = Services.GameManager.shiftTime;
        playerTransform = player.transform;
        initialPos = playerTransform.position;
        Services.EventManager.Register<PlayerVoted>(Interrupt);
        if (votedYea)
        {
            targetPos = new Vector3(Services.GameManager.yeaXPos, initialPos.y, initialPos.z);   
        }
        else
        {
            targetPos = new Vector3(Services.GameManager.nayXPos, initialPos.y, initialPos.z);
        }
    }

    internal override void Update()
    {
        timeElapsed += Time.deltaTime;

        playerTransform.position = Vector3.Lerp(initialPos, targetPos, Easing.QuadEaseOut(timeElapsed / duration));

        if (timeElapsed >= duration)
        {
            SetStatus(TaskStatus.Success);
        }
    }

    void Interrupt(PlayerVoted e)
    {
        if (e.player == player && e.yea != votedYea)
        {
            SetStatus(TaskStatus.Aborted);
        }
    }

    protected override void CleanUp()
    {
        Services.EventManager.Unregister<PlayerVoted>(Interrupt);
    }
}
