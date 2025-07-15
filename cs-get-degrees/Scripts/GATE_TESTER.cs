using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GATE_TESTER : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(loop());

    }

    IEnumerator loop()
    {
        yield return new WaitForSeconds(2);
        gateObject g = GateDataController.getGateData(1);
        /*Debug.Log(g.gateCount);
        Debug.Log(g.gates.ToArray()[0].calculation.calc);
        Debug.Log(g.gates.ToArray()[0].calculation.people);
        Debug.Log(g.gates.ToArray()[0].calculation.amount);*/
        Debug.Log(g.gates.ToArray()[0].gateText);
        StartCoroutine(loop());

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
