using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainController : MonoBehaviour
{
    private int friendsOne = 0;
    private int gpaOne = 400; //between 0 and 400
    private int friendsTwo = 0;
    private int gpaTwo = 400; //between 0 and 400
    [SerializeField] private TextMeshProUGUI playerOneF;
    [SerializeField] private TextMeshProUGUI playerOneG;
    [SerializeField] private TextMeshProUGUI playerTwoF;
    [SerializeField] private TextMeshProUGUI playerTwoG;

    [SerializeField] private MinigameController miniC;

    // Start is called before the first frame update
    void Start()
    {
        updateText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void updateText()
    {
        playerOneF.text = friendsOne.ToString();
        playerOneG.text = (gpaOne/100.0).ToString("0.00");
        playerTwoF.text = friendsTwo.ToString();
        playerTwoG.text = (gpaTwo / 100.0).ToString("0.00");
        updateTotalScore();
    }

    private void resetScore()
    {
        if (miniC.GetScore(1) != 0)
        {
            miniC.AddScore(1, -1 * miniC.GetScore(2));
        }
        if (miniC.GetScore(2) != 1)
        {
            miniC.AddScore(2, -1*miniC.GetScore(2));
        }
    }

    private void updateTotalScore()
    {
        float playerOneScore = gpaOne / (friendsOne+1);
        float playerTwoScore = gpaTwo / (friendsTwo+1);
        resetScore();
        if (playerOneScore > playerTwoScore)
        {
            miniC.AddScore(1, 1);
        } else if (playerTwoScore > playerOneScore)
        {
            miniC.AddScore(2, 1);
        }
    }

    public void setFriends(int player, int amount)
    {
        if (player == 0)
        {
            friendsOne = amount;
        } else
        {
            friendsTwo = amount;
        }
        updateText();
    }

    public void setGPA(int player, int amount)
    {
        if (player == 0)
        {
            gpaOne = amount;
        }
        else
        {
            gpaTwo = amount;
        }
        updateText();
    }

    public int getFriends(int player)
    {
        if (player == 0)
        {
            return friendsOne;
        }
        else
        {
            return friendsTwo;
        }
    }

    public int getGPA(int player)
    {
        if (player == 0)
        {
            return gpaOne;
        }
        else
        {
            return gpaTwo;
        }
    }
}
