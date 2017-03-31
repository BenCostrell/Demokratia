using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    public int playerNum;

    void Start()
    {
    }

    void Update()
    {
        CheckVote();
    }
    

    void CheckVote()
    {
        float input = Input.GetAxis("Horizontal_P" + playerNum);
        if (Mathf.Abs(input) > 0.1f)
        {
            bool yea = true;
            if (input > 0)
            {
                yea = false;
            }
            Services.EventManager.Fire(new PlayerVoted(this, yea));
        }
    }
}
