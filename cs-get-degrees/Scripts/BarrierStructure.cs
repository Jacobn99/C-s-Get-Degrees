using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierStructure : MonoBehaviour
{

    private gateData gateData;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public gateData getGateData()
    {
        return gateData;
    }

    public void setGateData(gateData data)
    {
        gateData = data;
    }
}
