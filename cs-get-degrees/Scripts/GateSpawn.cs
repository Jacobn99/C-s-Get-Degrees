using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.PackageManager;
using UnityEngine;

public class GateSpawn : MonoBehaviour
{
    //[SerializeField] GameObject fullGate;

    [SerializeField] GameObject playerOneBarriers;
    [SerializeField] GameObject playerTwoBarriers;
    [SerializeField] float speed;
    [SerializeField] List<GameObject> players;
    private List<Transform> startLocs;
    Dictionary<GameObject, gateObject> gates;
    [SerializeField] List<GameObject> gatePrefabs;
    [SerializeField] List<GameObject> alternateGates;

    // Start is called before the first frame update
    void Start()
    {
        gates = new Dictionary<GameObject, gateObject>();

        StartCoroutine(makeGate());
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < playerOneBarriers.transform.childCount; i++)
        {
            GameObject Go1 = playerOneBarriers.transform.GetChild(i).gameObject;
            GameObject Go2 = playerTwoBarriers.transform.GetChild(i).gameObject;

            int superSpeedOne = 1;
            if (Go1.transform.localPosition.z > 0)
            {
                superSpeedOne = 10;
            } else
            {
                superSpeedOne = 1;
            }

            int superSpeedTwo = 1;
            if (Go2.transform.localPosition.z > 0)
            {
                superSpeedTwo = 10;
            }
            else
            {
                superSpeedTwo = 1;
            }

            Go1.transform.position -= new Vector3(0, 0, superSpeedOne * speed*1);
            Go2.transform.position -= new Vector3(0, 0, superSpeedTwo * speed * 1);

        }
    }

    IEnumerator makeGate()
    {
        gateObject gate_object = GateDataController.getGateData(5);

        //gate_object.gateCount = 1;
        //try
        //{
        //Debug.Log("SPAWING: " + gate_object.gateCount);
        GameObject gate1 = Instantiate(getGatePrefab(gate_object), playerOneBarriers.transform);
        gate1.transform.position -= new Vector3(0, 0, -25);
        GameObject gate2 = Instantiate(getGatePrefab(gate_object), playerTwoBarriers.transform);
        gate2.transform.position -= new Vector3(0, 0, -25);

        GateDataController.setUpGate(gate1, gate_object);
        GateDataController.setUpGate(gate2, gate_object);

        gates.Add(gate1, gate_object);
        gates.Add(gate2, gate_object);
        //}
        //catch (IndexOutOfRangeException e)
        //{
        //    e.ToString();
        //}

        yield return new WaitForSeconds(1.5f);
        StartCoroutine(makeGate());
    }

    GameObject getGatePrefab(gateObject gate)
    {
        if (gate.gateCount == 1)
        {
            int rand = UnityEngine.Random.Range(0, 1);
            if (rand == 0)
            {
                return alternateGates.ElementAt(0);
            }
            else
            {
                return gatePrefabs.ElementAt(gate.gateCount - 1);
            }
        }
        else { return gatePrefabs.ElementAt(gate.gateCount - 1); }
    }
}
