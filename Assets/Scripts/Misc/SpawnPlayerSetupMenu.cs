using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;

public class SpawnPlayerSetupMenu : MonoBehaviour
{
    public GameObject playerSetupMenuPrefab;
    public Transform MainLayoutTransform;
    private GameObject rootMenu;
    //public PlayerInput input;

    private void Awake()
    {
        rootMenu = GameObject.Find("MainLayout");
        if (rootMenu != null)
        {
            var menu = (GameObject)Instantiate(playerSetupMenuPrefab, rootMenu.transform);
            
            //menu.GetComponent<PlayerSetupMenuController>().SetPlayerIndex(input.playerIndex);
            //menu.GetComponent<PlayerSetupMenuController>().SetPlayerInput(input);

        }

    }
}
