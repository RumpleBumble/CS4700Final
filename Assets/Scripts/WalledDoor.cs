using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalledDoor : MonoBehaviour
{
    public WeightButton.activationColor color;
    
    // Start is called before the first frame update
    void Start()
    {
        WeightButton.setColor(color, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
