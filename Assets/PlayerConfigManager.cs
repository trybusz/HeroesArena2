using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerMaster> playerMasters;
    [SerializeField]
    private int MaxPlayers = 4;

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("[Singleton] Trying to instantiate a seccond instance of a singleton class.");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerMasters = new List<PlayerMaster>();
        }

    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("player joined " + pi.playerIndex);
        pi.transform.SetParent(transform);

        if (!playerMasters.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            playerMasters.Add(new PlayerMaster(pi));
        }
    }

    public List<PlayerMaster> GetplayerMasters()
    {
        return playerMasters;
    }

    public void SetPlayerPrefab1(int index, GameObject prefab)
    {
        playerMasters[index].charPrefab1 = prefab;
    }
    public void SetPlayerPrefab2(int index, GameObject prefab)
    {
        playerMasters[index].charPrefab2 = prefab;
    }
    public void SetPlayerPrefab3(int index, GameObject prefab)
    {
        playerMasters[index].charPrefab3 = prefab;
    }

    public void ReadyPlayer(int index)
    {
        playerMasters[index].isReady = true;
        if (playerMasters.Count == MaxPlayers && playerMasters.All(p => p.isReady == true))
        {
            SceneManager.LoadScene("Map1");
        }
    }
}
