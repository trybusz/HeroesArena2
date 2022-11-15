using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
//using TMPro;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    public Transform[] PlayerSpawns;

    [SerializeField]
    public GameObject[] HealthBars;

    [SerializeField]
    public GameObject playerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        //var playerConfigs = PlayerConfigurationManager.Instance;
        /*for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, this.transform);
            player.GetComponent<PlayerMaster>().InitializePlayer(playerConfigs[i]);
            player.GetComponent<PlayerMaster>().playerStart = PlayerSpawns[i];
            player.GetComponent<PlayerMaster>().healthBar = HealthBars[i];
        }
       

        var player0 = Instantiate(playerPrefab, PlayerSpawns[0].position, PlayerSpawns[0].rotation, this.transform);
        int p0index = player0.GetComponent<PlayerInput>().playerIndex;
        var player1 = Instantiate(playerPrefab, PlayerSpawns[0].position, PlayerSpawns[0].rotation, this.transform);
        var player2 = Instantiate(playerPrefab, PlayerSpawns[0].position, PlayerSpawns[0].rotation, this.transform);
        int p2index = player2.GetComponent<PlayerInput>().playerIndex;
        int p1index = player1.GetComponent<PlayerInput>().playerIndex;
        var player3 = Instantiate(playerPrefab, PlayerSpawns[0].position, PlayerSpawns[0].rotation, this.transform);
        int p3index = player3.GetComponent<PlayerInput>().playerIndex;

        player0.GetComponent<PlayerMaster>().InitializePlayer(playerConfigs[p2index]);
        player0.GetComponent<PlayerMaster>().playerStart = PlayerSpawns[p2index];
        player0.GetComponent<PlayerMaster>().healthBar = HealthBars[p2index];

       
        player1.GetComponent<PlayerMaster>().InitializePlayer(playerConfigs[p1index]);
        player1.GetComponent<PlayerMaster>().playerStart = PlayerSpawns[p1index];
        player1.GetComponent<PlayerMaster>().healthBar = HealthBars[p1index];

        
        player2.GetComponent<PlayerMaster>().InitializePlayer(playerConfigs[p3index]);
        player2.GetComponent<PlayerMaster>().playerStart = PlayerSpawns[p3index];
        player2.GetComponent<PlayerMaster>().healthBar = HealthBars[p3index];

        
        player3.GetComponent<PlayerMaster>().InitializePlayer(playerConfigs[p0index]);
        player3.GetComponent<PlayerMaster>().playerStart = PlayerSpawns[p0index];
        player3.GetComponent<PlayerMaster>().healthBar = HealthBars[p0index];
         */
    }
}
