using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayersRender : MonoBehaviour
{
    private Dictionary<int, Player> otherPlayers = new Dictionary<int, Player>();

    public GameObject prefab;

    public Animator animator;

    public static PlayersRender instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Client.instance.ConnectToServer();

        animator = gameObject.GetComponent<Animator>();
    }

    void UpdatePlayerMovement(Player player)
    {
        Vector3 currentLocation = player.getLocation();
        Vector3 gameObjectLocation = player.getParent().transform.position;

        if(currentLocation != gameObjectLocation) {
            Vector3 diff = currentLocation - gameObjectLocation;

            player.getParent().transform.position = player.getLocation();
            player.getParent().GetComponent<Animator>().SetFloat("moveX", diff.x);
            player.getParent().GetComponent<Animator>().SetFloat("moveY", diff.y);
            player.getParent().GetComponent<Animator>().SetBool("moving", true);
        }
        else {
            player.getParent().GetComponent<Animator>().SetBool("moving", false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyValuePair<int, Player> entry in otherPlayers) {
            UpdatePlayerMovement(entry.Value);
        }
    }

    public void addPlayer(Player player) {
        otherPlayers.Add(player.getId(), player);
        GameObject obj = Instantiate(prefab, player.getLocation(), Quaternion.identity);
        obj.gameObject.AddComponent<Animator>();
        
        player.setParent(obj);
        PlayerLabel label = obj.GetComponent<PlayerLabel>();
        label.name = player.getName();
        Debug.Log($"{player.name} just joined in ({player.location.x}, {player.location.y}).");
    }

    void removePlayer(Player player) {
        if(player != null && otherPlayers.ContainsKey(player.getId())) {
            otherPlayers.Remove(player.getId());
            Destroy(player.getParent());
        }
    }

    private Dictionary<int, Player> getPlayers() {
        return otherPlayers;
    }

    public void setPlayerPosition(int id, Vector3 position){
        if(otherPlayers.ContainsKey(id)) {
            getPlayers()[id].location = position;
        }
    }
}