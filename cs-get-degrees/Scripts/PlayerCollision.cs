using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    private List<GameObject> _friendObjects = new List<GameObject>();
    [SerializeField] private FriendManager _friendManager;
    [SerializeField] private MainController mc;
    [SerializeField] private int playerNum;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "barrier" && !_friendObjects.Contains(other.gameObject))
        {
            //AddPlayer(gameObject);
            doGateEffect(other.gameObject);
        }
    }
    private void Start()
    {

    }

    private void AddPlayer(GameObject player)
    {
        _friendObjects.Add(_friendManager.SpawnFriend(player));
    }

    private void doGateEffect(GameObject barrier)
    {
        gateData gd = barrier.GetComponent<BarrierStructure>().getGateData();
        calcualation calc = gd.calculation;
        changeData cd = GateDataController.doCalculation(mc.getFriends(playerNum), mc.getGPA(playerNum), calc);
        mc.setFriends(playerNum, cd.newPeople);
        mc.setGPA(playerNum, cd.newGpa);

        //Debug.Log(_friendManager.getFriends().Count + " " + cd.newPeople);
        //if (_friendManager.getPlayerFriends(gameObject).Count != cd.newPeople) 
        //{
            _friendManager.setFriends(cd.newPeople, gameObject); 
        //}
    }
}
