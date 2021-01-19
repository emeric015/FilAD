using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayersRender : MonoBehaviour
{
    private List<Player> otherPlayers = new List<Player>();

    public GameObject prefab;
    // Start is called before the first frame update
    void Start()
    {
        Client.instance.ConnectToServer();
        Player p1 = new Player("Jean", Vector3.zero);
        addPlayer(p1);

    }

    void UpdatePlayerMovement(Player player)
    {
        player.getParent().transform.position = player.getLocation();

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
        foreach(Player player in otherPlayers) {
            UpdatePlayerMovement(player);
        }
    }

    void addPlayer(Player player) {
        otherPlayers.Add(player);
        GameObject obj = Instantiate(prefab, player.getLocation(), Quaternion.identity);
        player.setParent(obj);

        PlayerLabel label = obj.GetComponent<PlayerLabel>();
        label.name = player.getName();
    }

    void removePlayer(Player player) {
        otherPlayers.Remove(player);
        //TODO remove gameObject
    }

    List<Player> getPlayers() {
        return otherPlayers;
    }
}
