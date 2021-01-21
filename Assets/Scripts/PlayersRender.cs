using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayersRender : MonoBehaviour
{
    private List<Player> otherPlayers = new List<Player>();

    public GameObject prefab;


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
        //Player p1 = new Player("Jean", Vector3.zero);
        //addPlayer(p1);

        animator = gameObject.GetComponent<Animator>();
    }

    void UpdatePlayerMovement(Player player)
    {
        Debug.Log("Checking " + player.getName());

        Vector3 currentLocation = player.getLocation();
        Vector3 gameObjectLocation = player.getParent().transform.position;

        if(currentLocation != gameObjectLocation) {
            Debug.Log("Moving !");
            Vector3 diff = currentLocation - gameObjectLocation;

            player.getParent().transform.position = player.getLocation();
            if(Math.Abs(diff.x) <= 1 && Math.Abs(diff.y) <= 1) {
                player.getParent().GetComponent<Animator>().SetFloat("moveX", diff.x);
                player.getParent().GetComponent<Animator>().SetFloat("moveY", diff.y);
                player.getParent().GetComponent<Animator>().SetBool("moving", true);
            }
        }
        else {
            player.getParent().GetComponent<Animator>().SetBool("moving", false);
        }

        /*Animator animator = player.GetComponent<Animator>();

        if (change != Vector3.zero)
        {
            if(change.x != 0) {
                change.y = 0;
            }

            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        } else{
            animator.SetBool("moving", false);
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        System.Random rnd = new System.Random();
        if(rnd.Next(60) == 1 || true) {
            //float xChange = rnd.Next(3) - 1;
            //float yChange = rnd.Next(3) - 1;
            float xChange = -1;
            float yChange = 0;

            Vector3 change = new Vector3(xChange, yChange, 0);
            change *= 2f * Time.deltaTime;
            change += p1.getLocation();

            p1.setLocation(change);
        }

        foreach(Player player in otherPlayers) {
            UpdatePlayerMovement(player);
        }
    }

    public void addPlayer(Player player) {
        otherPlayers.Add(player);
        GameObject obj = Instantiate(prefab, player.getLocation(), Quaternion.identity);
        obj.gameObject.AddComponent<Animator>();
        
        player.setParent(obj);
        PlayerLabel label = obj.GetComponent<PlayerLabel>();
        label.name = player.getName();
        Debug.Log($"{player.name} just joined in ({player.location.x}, {player.location.y}).");
    }

    void removePlayer(Player player) {
        otherPlayers.Remove(player);
        //TODO remove gameObject
    }

    List<Player> getPlayers() {
        return otherPlayers;
    }
}
