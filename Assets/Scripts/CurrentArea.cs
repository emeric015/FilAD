using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentArea : MonoBehaviour
{
    Area currentArea;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Area getArea() {
        return currentArea;
    }

    public void setArea(Area area) {
        this.currentArea = area;
    }
}
