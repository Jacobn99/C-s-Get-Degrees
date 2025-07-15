using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FriendManager : MonoBehaviour
{
    private List<Friend> _friends;
    private List<Friend> player1Friends;
    private List<Friend> player2Friends;
    private float _trackSize;
    private int maxFriendsPerRow;
    [SerializeField] float rowSpacing;
    [SerializeField] List<GameObject> players;
    [SerializeField] GameObject friendPrehab;

    // Start is called before the first frame update
    void Start()
    {
        maxFriendsPerRow = 5;
        _trackSize = 3F;

        _friends = new List<Friend>();
        player1Friends = new List<Friend>();
        player2Friends = new List<Friend>();
    }
    private void FixedUpdate()
    {
        orderFriends(player1Friends);
        orderFriends(player2Friends);
    }

    public GameObject getFriendPrehab()
    {
        return friendPrehab;
    }

    // Organizes friends from a player's friends list into the correct formation
    private void orderFriends(List<Friend> playerFriends)
    {
        if(playerFriends.Count == 0) { return; }
        Vector3 playerLoc = playerFriends.ElementAt(0).playerObject.transform.position;
        int friendCount = playerFriends.Count;
        int rows = (int)System.Math.Ceiling((float)(friendCount / (double)maxFriendsPerRow));
        int cols = maxFriendsPerRow;
        int i = 0;


        for (int row = 0; row < rows; row++)
        {
            cols = maxFriendsPerRow;
            if (cols > friendCount)
            {
                cols = friendCount % maxFriendsPerRow;
            }
            for (int col = 0; col < cols; col++)
            {
                //Debug.Log("rows: " + rows + ", cols: " + cols);

                Vector3 newLoc = playerLoc;
                float spacing = (_trackSize) / cols;

                //Debug.Log("(" + col + " - " + cols + " / " + 2 + ") * " + spacing);
                newLoc.x += (col - cols / 2) * spacing;
                newLoc.z -= rowSpacing * (float)(row + 1);

                if(playerFriends.Count - 1 >= i)
                {
                    playerFriends.ElementAt(i).friendObject.transform.position = newLoc;
                }
                i++;
            }

        }
    }

    // Returns _friends
    public List<Friend> getFriends()
    {
        return _friends;
    }

    // Sets size of a player's friends list
    public void setFriends(int count, GameObject player)
    {
        // Disgusting code, will fix later
        List<Friend> playerFriends = null;
        if (player == players.ElementAt(0)) { playerFriends = player1Friends; }
        else if (player == players.ElementAt(1)) { playerFriends = player2Friends; }
        else 
        {
            Debug.Log("player has no corresponding friends list");
            return; 
        }

        if (count > playerFriends.Count)
        {
            spawnFriends(count - playerFriends.Count, player);
        }
        else if (count < playerFriends.Count)
        {
            removeFriends(playerFriends.Count - count, playerFriends);
        }
        else { return; }
    }

    // Removes a given amount of Friend objects from a player's friends list
    public void removeFriends(int count, List<Friend> playerFriends)
    {
        if (count >= playerFriends.Count) { count = playerFriends.Count; }

        for (int i = 0; i < count; i++)
        {
            Friend friend = playerFriends.ElementAt(playerFriends.Count - 1);
            removeFriend(friend , playerFriends);
        }
    }

    // Removes a Friend object from a player's friends list and destroys the corresponding friend game object
    public void removeFriend(Friend friend, List<Friend> playerFriends)
    {
        Destroy(friend.friendObject);
        _friends.Remove(friend);
        playerFriends.Remove(friend);
        friend = null;
    }

    // Spawms a certain amount friends for specified player
    public void spawnFriends(int count, GameObject player)
    {
        for (int i = 0; i < count;i++)
        {
            SpawnFriend(player);
        }
    }

    // Spawms a friend for specified player
    public GameObject SpawnFriend(GameObject player)
    {
        GameObject friendObject = Instantiate(friendPrehab, player.transform.position, player.transform.rotation);
        Friend friend = new Friend(friendObject, player);
        List<Friend> playerFriends = null;

        _friends.Add(friend);

        if (player == players.ElementAt(0)) { playerFriends = player1Friends; }
        else if (player == players.ElementAt(1)) { playerFriends = player2Friends; }

        playerFriends.Add(friend);
        return friendObject;
    }
}

public class Friend
{
    public GameObject playerObject;
    public GameObject friendObject;

    public Friend(GameObject friend, GameObject player)
    {
        friendObject = friend;
        playerObject = player;
    }
}
