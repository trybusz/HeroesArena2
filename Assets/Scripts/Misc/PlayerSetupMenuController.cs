using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
using System.Collections.Generic;

public class PlayerSetupMenuController : MonoBehaviour
{
	private int PlayerIndex;
	public PlayerInput PlayerInput;
	public GameObject[] characters;
	public TextMeshProUGUI[] descriptions;
	public Sprite[] images;
	public Image image;
	public Image image1;
	public Image image2;
	public Image image3;
	public Image teamColor;
	public int team;

	public TextMeshProUGUI description;
	[SerializeField]
	private TextMeshProUGUI titleText;
	[SerializeField]
	private GameObject readyPanel;
	[SerializeField]
	private GameObject teamSelectPanel;
	[SerializeField]
	private GameObject menuPanel;
	[SerializeField]
	private GameObject instructionPanel;

	public int selectedCharacter = 0;
	public int selectedText = 0;
	private float ignoreInputTime = 1.5f;
	private bool inputEnabled;
	public bool charPrefab1picked = false;
	public GameObject charPrefab1;
	public GameObject charPrefab2;
	public GameObject charPrefab3;
	public bool charPrefab2picked = false;
	public bool charPrefab3picked = false;
	public GameObject playerPrefab;
	//public GameObject charPrefab;
	bool flag = true;
	public bool teamPicked = false;

	public bool showChars;
	public int char1num = -1;
	public int char2num = -1;
	public int char3num = -1;

	public float leftRightTime;
	public Vector2 movementInput;

	public Transform charLocation;
	public void Awake()
	{
		//DontDestroyOnLoad(this.gameObject);

		if (GameObject.Find("InstructionPanel") != null)
		{
			instructionPanel = GameObject.Find("InstructionPanel");
			instructionPanel.SetActive(false);
		}
	}

    public void Start()
    {
		showChars = false;
		image1.enabled = false;
		image2.enabled = false;
		image3.enabled = false;

		menuPanel.SetActive(true);
		teamSelectPanel.SetActive(false);
		readyPanel.SetActive(false);
		PlayerInput = GetComponent<PlayerInput>();
		PlayerIndex = PlayerInput.playerIndex;
		titleText.SetText("Player " + (PlayerIndex + 1).ToString());

	}



	public int CountInstances(List<int> list, int NameSearch)
	{
		return list.Count(n => n == NameSearch);
	}

	public void NextCharacter()
	{
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		description.SetText(descriptions[selectedCharacter].text);
		image.sprite = images[selectedCharacter];
	}

	public void PreviousCharacter()
	{
		
		selectedCharacter--;
		if (selectedCharacter < 0)
		{
			selectedCharacter += characters.Length;
		}
		description.SetText(descriptions[selectedCharacter].text);
		image.sprite = images[selectedCharacter];

	}
	public void SetCharPrefab1(GameObject prefab)
	{
		if (!inputEnabled) { return; }

		//Added this don't know why it wasnt here
		PlayerConfigurationManager.Instance.SetPlayerPrefab1(PlayerIndex, prefab);

		char1num = (selectedCharacter) % characters.Length;
		//image1.sprite = images[selectedCharacter];
	}
	public void SetCharPrefab2(GameObject prefab)
	{
		if (!inputEnabled) { return; }

		PlayerConfigurationManager.Instance.SetPlayerPrefab2(PlayerIndex, prefab);

		char2num = (selectedCharacter) % characters.Length;
		//image2.sprite = images[selectedCharacter];
	}
	public void SetCharPrefab3(GameObject prefab)
	{
		if (!inputEnabled) { return; }

		PlayerConfigurationManager.Instance.SetPlayerPrefab3(PlayerIndex, prefab);

		char3num = (selectedCharacter) % characters.Length;

		//image3.sprite = images[selectedCharacter];
	}

	public void OnNormalAttack2(InputAction.CallbackContext context)
	{
		movementInput = context.ReadValue<Vector2>();
	}
	public void ShowChars(InputAction.CallbackContext context)
	{
		if (context.started)
        {
			showChars = !showChars;
        }
        if (showChars)
        {
			image1.enabled = true;
			image2.enabled = true;
			image3.enabled = true;
		}
        else
        {
			image1.enabled = false;
			image2.enabled = false;
			image3.enabled = false;
		}

	}

	public void OnNormalAttack(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			if (!teamSelectPanel.activeInHierarchy)
			{
				NextCharacter();
			}
			else
			{
				//color is blue
				if (teamColor.color.r == 0)
				{
					//set color to red
					teamColor.color = new Color32(255, 0, 0, 255);

				}
				else
				{
					//set color to blue
					teamColor.color = new Color32(0, 51, 225, 255);

				}
				
			}

		}
	}
	public void OnSpecialAttack(InputAction.CallbackContext context)
	{
        if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
        {
			if (!teamSelectPanel.activeInHierarchy)
			{
				PreviousCharacter();
			}
			else
			{
				//color is blue
				if (teamColor.color.r == 0)
				{
					//set color to red
					teamColor.color = new Color32(255, 0, 0, 255);

				}
				//color is red
				else
				{
					//set color to blue
					teamColor.color = new Color32(0, 51, 225, 255);

				}
			}

		}
	}
	public void OnAButton(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			if (!teamSelectPanel.activeInHierarchy)
			{
				if (charPrefab1 != characters[selectedCharacter] && charPrefab3 != characters[selectedCharacter]) {
					PlayerConfigurationManager.Instance.SetPlayerPrefab2(PlayerIndex, characters[selectedCharacter]);
					charPrefab2picked = true;
					charPrefab2 = characters[selectedCharacter];
					char2num = (selectedCharacter) % characters.Length;
				}
			}
            else
            {
				//color is blue
				if(teamColor.color.r == 0 && CountInstances(PlayerConfigurationManager.Instance.GetTeams(), 0) < 2)
                {
					team = 0;
				}
                else if(CountInstances(PlayerConfigurationManager.Instance.GetTeams(), 1) < 2)
                {
					team = 1;
				}
				teamPicked = true;
				PlayerConfigurationManager.Instance.GetTeams().Add(team);
			}
			
		}
	}
	public void OnBButton(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			if (charPrefab1 != characters[selectedCharacter] && charPrefab2 != characters[selectedCharacter])
			{
				PlayerConfigurationManager.Instance.SetPlayerPrefab3(PlayerIndex, characters[selectedCharacter]);
                charPrefab3picked = true;
				charPrefab3 = characters[selectedCharacter];
				char3num = (selectedCharacter) % characters.Length;
			}
		}
	}
	public void OnXButton(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers) 
		{
			if (charPrefab2 != characters[selectedCharacter] && charPrefab3 != characters[selectedCharacter])
			{
				PlayerConfigurationManager.Instance.SetPlayerPrefab1(PlayerIndex, characters[selectedCharacter]);
				charPrefab1picked = true;
				charPrefab1 = characters[selectedCharacter];
				char1num = (selectedCharacter) % characters.Length;
			}
		}
	}

	private void Update()
	{
		if (movementInput.x > 0.2 && leftRightTime < Time.timeSinceLevelLoad && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			if (!teamSelectPanel.activeInHierarchy)
			{
				NextCharacter();
			}
			else
			{
				//color is blue
				if (teamColor.color.r == 0)
				{
					//set color to red
					teamColor.color = new Color32(255, 0, 0, 255);

				}
				else
				{
					//set color to blue
					teamColor.color = new Color32(0, 51, 225, 255);

				}

			}

		}

		if (movementInput.x < -0.2 && leftRightTime < Time.timeSinceLevelLoad && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			if (!teamSelectPanel.activeInHierarchy)
			{
				PreviousCharacter();
			}
			else
			{
				//color is blue
				if (teamColor.color.r == 0)
				{
					//set color to red
					teamColor.color = new Color32(255, 0, 0, 255);

				}
				else
				{
					//set color to blue
					teamColor.color = new Color32(0, 51, 225, 255);

				}

			}

		}
		if (leftRightTime < Time.timeSinceLevelLoad)
		{
			leftRightTime = Time.timeSinceLevelLoad + 0.2f;
		}

		if (charPrefab1picked && charPrefab2picked && charPrefab3picked && flag)
        {
			
			menuPanel.SetActive(false);
			teamSelectPanel.SetActive(true);

            if (teamPicked)
            {
				flag = false;
				readyPanel.SetActive(true);
				teamSelectPanel.SetActive(false);
				Destroy(this.gameObject.GetComponent<PlayerInput>());
				playerPrefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
				playerPrefab.GetComponent<PlayerMaster>().charPrefab1 = charPrefab1;
				playerPrefab.GetComponent<PlayerMaster>().charPrefab2 = charPrefab2;
				playerPrefab.GetComponent<PlayerMaster>().charPrefab3 = charPrefab3;
				playerPrefab.GetComponent<PlayerMaster>().playerStart = this.transform;
				playerPrefab.GetComponent<PlayerMaster>().team = team;
				PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
				
			}
			
		}
		if (showChars)
        {
			if (char1num != -1)
            {
				image1.sprite = images[char1num];
			}
			if (char2num != -1)
			{
				image2.sprite = images[char2num];
			}
			if (char3num != -1)
			{
				image3.sprite = images[char3num];
			}
		}
	}
}
