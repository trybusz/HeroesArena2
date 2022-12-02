
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigurationManager : MonoBehaviour
{
   
    [SerializeField]
    private List<PlayerConfiguration> playerConfigs;
    
    [SerializeField]
    public int MaxPlayers = 4;

    public static PlayerConfigurationManager Instance { get; private set; }

    public static List<int> teams;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            Instance = this;
            //DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
            teams = new List<int>();
        }

    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("player joined " + pi.playerIndex);
        pi.transform.SetParent(transform);

        if (!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex) && playerConfigs.Count != MaxPlayers)
        {
            playerConfigs.Add(new PlayerConfiguration(pi));
            Debug.Log("Player Config added for index " + pi.playerIndex);
           
        }
        else
        {
            Debug.Log("Player already in scene, or max amount of players alowed are in the scene");
        }
    }

    public List<PlayerConfiguration> GetPlayerConfigs()
    {
        return playerConfigs;
    }
    public List<int> GetTeams()
    {
        return teams;
    }

    public void SetPlayerPrefab1(int index, GameObject prefab)
    {
        playerConfigs[index].charPrefab1 = prefab;
    }
    public void SetPlayerPrefab2(int index, GameObject prefab)
    {
        playerConfigs[index].charPrefab2 = prefab;
    }
    public void SetPlayerPrefab3(int index, GameObject prefab)
    {
        playerConfigs[index].charPrefab3 = prefab;
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].isReady = true;
        Debug.Log(playerConfigs[index].charPrefab1);
        if (playerConfigs.Count == MaxPlayers && playerConfigs[0].isReady && playerConfigs[1].isReady && playerConfigs[2].isReady && playerConfigs[3].isReady)
        {
            Debug.Log("Scene Load");
            SceneManager.LoadScene(SceneSelectScript.selectedStage);
        }
    }
}

[System.Serializable]
public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
        Debug.Log("PlayerConfig" + pi.user);
    }

    public PlayerInput Input { get; private set; }
    public int PlayerIndex { get; private set; }
    public bool isReady { get; set; }
    public GameObject charPrefab1 { get; set; }
    public GameObject charPrefab2 { get; set; }
    public GameObject charPrefab3 { get; set; }
    

}