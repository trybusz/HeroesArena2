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

    }
}
