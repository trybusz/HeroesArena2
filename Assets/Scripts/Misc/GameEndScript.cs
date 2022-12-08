using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndScript : MonoBehaviour
{
    private PlayerMaster pm0;
    private PlayerMaster pm1;
    private PlayerMaster pm2;
    private PlayerMaster pm3;
    public PlayerMaster[] playerMasters;
    public List<PlayerMaster> blueTeam;
    public List<PlayerMaster> redTeam;
    public static int winCase;
    public int redTeamScore;
    public int blueTeamScore;
    public TextMeshProUGUI Score;

    // Start is called before the first frame update
    void Start()
    {
        playerMasters = GameObject.FindObjectsOfType<PlayerMaster>();
       
        for(int i = 0; i < playerMasters.Length; i++)
        {
            if (playerMasters[i].team == 0)
            {
                blueTeam.Add(playerMasters[i]);
            }
            else
            {
                redTeam.Add(playerMasters[i]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (blueTeam.Count == 2 && redTeam.Count == 2)
        {
            blueTeamScore = blueTeam[0].charPrefab1.GetComponent<CharacterParent>().thisCharScore + blueTeam[1].charPrefab1.GetComponent<CharacterParent>().thisCharScore +
                            blueTeam[0].charPrefab2.GetComponent<CharacterParent>().thisCharScore + blueTeam[1].charPrefab2.GetComponent<CharacterParent>().thisCharScore +
                            blueTeam[0].charPrefab3.GetComponent<CharacterParent>().thisCharScore + blueTeam[1].charPrefab3.GetComponent<CharacterParent>().thisCharScore;
            redTeamScore = redTeam[0].charPrefab1.GetComponent<CharacterParent>().thisCharScore + redTeam[1].charPrefab1.GetComponent<CharacterParent>().thisCharScore +
                            redTeam[0].charPrefab2.GetComponent<CharacterParent>().thisCharScore + redTeam[1].charPrefab2.GetComponent<CharacterParent>().thisCharScore +
                            redTeam[0].charPrefab3.GetComponent<CharacterParent>().thisCharScore + redTeam[1].charPrefab3.GetComponent<CharacterParent>().thisCharScore;
            Score.SetText(blueTeamScore.ToString() + ":" + redTeamScore.ToString());
            // all of blue team is dead
            if (blueTeam[0].char1isDeadFlag && blueTeam[0].char2isDeadFlag && blueTeam[0].char3isDeadFlag
                && blueTeam[1].char1isDeadFlag && blueTeam[1].char2isDeadFlag && blueTeam[1].char3isDeadFlag)
            {
                //red survived
                winCase = 1;
                SceneManager.LoadScene("EndScreen");
            }
            // all of red team is dead
            else if (redTeam[0].char1isDeadFlag && redTeam[0].char2isDeadFlag && redTeam[0].char3isDeadFlag
                && redTeam[1].char1isDeadFlag && redTeam[1].char2isDeadFlag && redTeam[1].char3isDeadFlag)
            {
                //blue team survived
                winCase = 0;
                SceneManager.LoadScene("EndScreen");
            }
        }

        if (blueTeam.Count == 1 && redTeam.Count == 1)
        {
            blueTeamScore = blueTeam[0].charPrefab1.GetComponent<CharacterParent>().thisCharScore +
                            blueTeam[0].charPrefab2.GetComponent<CharacterParent>().thisCharScore + 
                            blueTeam[0].charPrefab3.GetComponent<CharacterParent>().thisCharScore;
            redTeamScore = redTeam[0].charPrefab1.GetComponent<CharacterParent>().thisCharScore +
                            redTeam[0].charPrefab2.GetComponent<CharacterParent>().thisCharScore +
                            redTeam[0].charPrefab3.GetComponent<CharacterParent>().thisCharScore;
            Score.SetText(blueTeamScore.ToString() + ":" + redTeamScore.ToString());
            // all of blue team is dead
            if (blueTeam[0].char1isDeadFlag && blueTeam[0].char2isDeadFlag && blueTeam[0].char3isDeadFlag)
            {
                //red survived
                winCase = 1;
                SceneManager.LoadScene("EndScreen");
            }
            // all of red team is dead
            else if (redTeam[0].char1isDeadFlag && redTeam[0].char2isDeadFlag && redTeam[0].char3isDeadFlag)
            {
                //blue team survived
                winCase = 0;
                SceneManager.LoadScene("EndScreen");
            }
        }

        if (blueTeamScore > 100)
            {
                //blue team captured the point
                winCase = 2;
                SceneManager.LoadScene("EndScreen");
            }
            else if (redTeamScore > 100)
            {
                //red team captured the point
                winCase = 3;
                SceneManager.LoadScene("EndScreen");

            }
        }
    
}
