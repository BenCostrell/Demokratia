using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VotingManager {
    private List<Player> yeaVotes;
    private List<Player> nayVotes;

    public VotingManager(){
        yeaVotes = new List<Player>();
        nayVotes = new List<Player>();
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
            }
            if (yeaVotes.Contains(player))
            {
                yeaVotes.Remove(player);
            }
        }
    }

    bool DidMotionPass()
    {
        return yeaVotes.Count > nayVotes.Count;
    }

    void StartNewVote()
    {
        yeaVotes.Clear();
        nayVotes.Clear();
        Services.EventManager.Register<PlayerVoted>(Vote);
    }
	
}
