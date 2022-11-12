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
	public bool charPrefab2picked = false;
	public bool charPrefab3picked = false;
	bool flag = true;
	
	
	public Transform charLocation;
	

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
		
		Debug.Log("Previous Character");
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
		if (context.started)
		{
			NextCharacter();
		}
		
	}
	public void OnSpecialAttack(InputAction.CallbackContext context)
	{
        if (context.started)
        {
			PreviousCharacter();
		}
		
	}
	public void OnAButton(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			PlayerConfigurationManager.Instance.SetPlayerPrefab2(PlayerIndex, characters[selectedCharacter]);
			charPrefab2picked = true;
		}
	}
	public void OnBButton(InputAction.CallbackContext context)
	{
		if (context.started)
		{
			PlayerConfigurationManager.Instance.SetPlayerPrefab3(PlayerIndex, characters[selectedCharacter]);
			charPrefab3picked = true;
		}
	}
	public void OnXButton(InputAction.CallbackContext context)
	{
		if (context.started) 
		{
			PlayerConfigurationManager.Instance.SetPlayerPrefab1(PlayerIndex, characters[selectedCharacter]);
			charPrefab1picked = true;
		}
	}

	private void Update()
	{	
		if(charPrefab1picked && charPrefab2picked && charPrefab3picked && flag)
        {
			
			readyPanel.SetActive(true);
			menuPanel.SetActive(false);
			flag = false;
			PlayerConfigurationManager.Instance.ReadyPlayer(PlayerIndex);
		}
		/*
		switch (selectedCharacter)
		{
			case 0:
				
				break;
			case 1:
				

			break;
			default:
				

		break;
		}
		*/

	}
}
