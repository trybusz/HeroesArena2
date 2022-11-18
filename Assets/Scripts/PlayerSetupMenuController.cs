using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerSetupMenuController : MonoBehaviour
{
	private int PlayerIndex;
	public PlayerInput PlayerInput;
	public GameObject[] characters;
	public TextMeshProUGUI[] descriptions;
	public Sprite[] images;
	
	public TextMeshProUGUI description;
	[SerializeField]
	private TextMeshProUGUI titleText;
	[SerializeField]
	private GameObject readyPanel;
	[SerializeField]
	private GameObject menuPanel;
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
	
	
	public Transform charLocation;
    public void Awake()
    {
		DontDestroyOnLoad(this.gameObject);
    }

    public void Start()
    {
		menuPanel.SetActive(true);
		readyPanel.SetActive(false);
		PlayerInput = GetComponent<PlayerInput>();
		PlayerIndex = PlayerInput.playerIndex;
		titleText.SetText("Player " + (PlayerIndex + 1).ToString());

	}

	public void NextCharacter()
	{
		selectedCharacter = (selectedCharacter + 1) % characters.Length;
		description.SetText(descriptions[selectedCharacter].text);
	}

	public void PreviousCharacter()
	{
		
		selectedCharacter--;
		if (selectedCharacter < 0)
		{
			selectedCharacter += characters.Length;
		}
		description.SetText(descriptions[selectedCharacter].text);

	}
	public void SetCharPrefab1(GameObject prefab)
	{
		if (!inputEnabled) { return; }

	}
	public void SetCharPrefab2(GameObject prefab)
	{
		if (!inputEnabled) { return; }

		PlayerConfigurationManager.Instance.SetPlayerPrefab2(PlayerIndex, prefab);
	}
	public void SetCharPrefab3(GameObject prefab)
	{
		if (!inputEnabled) { return; }

		PlayerConfigurationManager.Instance.SetPlayerPrefab3(PlayerIndex, prefab);
	}

	public void OnNormalAttack(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			NextCharacter();
		}
		
	}
	public void OnSpecialAttack(InputAction.CallbackContext context)
	{
        if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
        {
			PreviousCharacter();
		}
		
	}
	public void OnAButton(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			PlayerConfigurationManager.Instance.SetPlayerPrefab2(PlayerIndex, characters[selectedCharacter]);
			charPrefab2picked = true;
			charPrefab2 = characters[selectedCharacter];
		}
	}
	public void OnBButton(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers)
		{
			PlayerConfigurationManager.Instance.SetPlayerPrefab3(PlayerIndex, characters[selectedCharacter]);
			charPrefab3picked = true;
			charPrefab3 = characters[selectedCharacter];
		}
	}
	public void OnXButton(InputAction.CallbackContext context)
	{
		if (context.started && PlayerConfigurationManager.Instance.GetPlayerConfigs().Count == PlayerConfigurationManager.Instance.MaxPlayers) 
		{
			PlayerConfigurationManager.Instance.SetPlayerPrefab1(PlayerIndex, characters[selectedCharacter]);
			charPrefab1picked = true;
			charPrefab1 = characters[selectedCharacter];
		}
	}

	private void Update()
	{	
		if(charPrefab1picked && charPrefab2picked && charPrefab3picked && flag)
        {
			
			readyPanel.SetActive(true);
			menuPanel.SetActive(false);
			flag = false;
			Destroy(this.gameObject.GetComponent<PlayerInput>());

			playerPrefab = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
			//charPrefab1.
			playerPrefab.GetComponent<PlayerMaster>().charPrefab1 = charPrefab1;
			playerPrefab.GetComponent<PlayerMaster>().charPrefab2 = charPrefab2;
			playerPrefab.GetComponent<PlayerMaster>().charPrefab3 = charPrefab3;
			playerPrefab.GetComponent<PlayerMaster>().playerStart = this.transform;
			PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
		}

	}
}
