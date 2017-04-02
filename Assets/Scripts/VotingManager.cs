using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingManager {
    private List<Player> yeaVotes;
    private List<Player> nayVotes;
    private List<NPC> npcYeaVotes;
    private List<NPC> npcNayVotes;

    public VotingManager(){
        yeaVotes = new List<Player>();
        nayVotes = new List<Player>();
        npcYeaVotes = new List<NPC>();
        npcNayVotes = new List<NPC>();
    }

    void Vote(PlayerVoted e)
    {
        Player player = e.player;
        bool yea = e.yea;
        if (yea)
        {
            if (!yeaVotes.Contains(player))
            {
                yeaVotes.Add(player);
                PlayerShiftTask shiftYea = new PlayerShiftTask(player, true);
                Services.TaskManager.AddTask(shiftYea);
            }
            if (nayVotes.Contains(player))
            {
                nayVotes.Remove(player);
            }
        }
        else
        {
            if (!nayVotes.Contains(player))
            {
                nayVotes.Add(player);
                PlayerShiftTask shiftNay = new PlayerShiftTask(player, false);
                Services.TaskManager.AddTask(shiftNay);
            }
            if (yeaVotes.Contains(player))
            {
                yeaVotes.Remove(player);
            }
        }
    }

    bool DidMotionPass()
    {
        return (yeaVotes.Count + npcYeaVotes.Count) > (nayVotes.Count + npcNayVotes.Count);
    }

    public void StartNewVote()
    {
        yeaVotes.Clear();
        nayVotes.Clear();
        Services.EventManager.Register<PlayerVoted>(Vote);
    }
	
}
