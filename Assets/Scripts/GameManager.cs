using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public MatchSettings matchSettings;

    private void Awake()
    {

        if (instance != null)
        {
            Debug.LogError("more than 1 game manager in scene!");

        } else
        {

            instance = this;

        }
        

    }

    #region Player Tracking

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void RegisterPlayer(string netID, Player player)
    {

        string playerID = "Player " + netID;
        players.Add(playerID, player);
        player.transform.name = playerID;

    }

    public static void UnRegisterPlayer(string playerID)
    {

        players.Remove(playerID);

    }

    public static Player GetPlayer(string playerID)
    {

        return players[playerID];

    }

    private void OnGUI()
    {

        GUILayout.BeginArea(new Rect(200, 200, 200, 500));
        GUILayout.BeginVertical();

        foreach (string playerID in players.Keys)
        {

            GUILayout.Label(playerID + " - " + players[playerID].transform.name);

        }

        GUILayout.EndVertical();
        GUILayout.EndArea();

    }
    #endregion
}
