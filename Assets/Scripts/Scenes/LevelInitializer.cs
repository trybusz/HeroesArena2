using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
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
        var playerConfigs = PlayerConfigurationManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfigs.Length; i++)
        {
            var player = Instantiate(playerPrefab, PlayerSpawns[i].position, PlayerSpawns[i].rotation, this.transform);
            player.GetComponent<PlayerMaster>().InitializePlayer(playerConfigs[i]);
            player.GetComponent<PlayerMaster>().playerStart = PlayerSpawns[i];
            player.GetComponent<PlayerMaster>().healthBar = HealthBars[i];
        }


    }
}
