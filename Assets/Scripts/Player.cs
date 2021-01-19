using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    private string name;
    private Vector3 location;
    private GameObject parent;
    private Area currentArea;

    public Player(string name, Vector3 location)
    {
        this.name = name;
        this.location = location;
    }

    public void updateLocation(Vector3 newLocation) {
        location = newLocation;
    }

    public Vector3 getLocation() {
        return location;
    }

    public void setLocation(Vector3 location) {
        this.location = location;
    }

    public string getName() {
        return name;
    }

    public void setParent(GameObject parent) {
        this.parent = parent;
    }

    public GameObject getParent() {
        return parent;
    }

    public Area getArea()  {
        return currentArea;
    }

    public void setArea(Area area) {
        this.currentArea = area;
    }
}