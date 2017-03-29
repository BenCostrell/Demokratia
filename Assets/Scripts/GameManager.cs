using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    private List<Player> players;
    public Vector3[] spawnPoints;
    public Sprite[] playerSprites;

	void Awake()
    {
        InitializeServices();
    }
    
    // Use this for initialization
	void Start () {
        InitializePlayers();
	}
	
	// Update is called once per frame
	void Update () {
        Services.TaskManager.Update();
	}

    void InitializeServices()
    {
        Services.GameManager = this;
        Services.EventManager = new EventManager();
        Services.TaskManager = new TaskManager();
        Services.PrefabDB = Resources.Load<PrefabDB>("Prefabs/PrefabDB");
        Services.VotingManager = new VotingManager();
    }

    void InitializePlayers()
    {
        players = new List<Player>();
        for (int i = 0; i < 3; i++)
        {
            GameObject playerObj = Instantiate(Services.PrefabDB.Player, spawnPoints[i], Quaternion.identity) as GameObject;
            players.Add(playerObj.GetComponent<Player>());
            players[i].playerNum = i + 1;
            players[i].gameObject.GetComponent<SpriteRenderer>().sprite = playerSprites[i];
        }
    }
}
