using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

public class ClientHandle : MonoBehaviour
{
    public static void Welcome(Packet _packet)
    {
        string _msg = _packet.ReadString();
        int _myId = _packet.ReadInt();

        Debug.Log($"Message from server: {_msg}");
        Client.instance.myId = _myId;
        ClientSend.WelcomeReceived();

        Client.instance.udp.Connect(((IPEndPoint)Client.instance.tcp.socket.Client.LocalEndPoint).Port);
    }

    public static void UDPTest(Packet _packet)
    {
        string _msg = _packet.ReadString();

        Debug.Log($"Received packet via UDP. Contains message: {_msg}");
        ClientSend.UDPTestReceived();
    }
    public static void SpawnNewPlayer(Packet _packet)
    {
        int _myId = _packet.ReadInt();
        string _myUsername = _packet.ReadString();
        float _myPositionX = _packet.ReadFloat();
        float _myPositionY = _packet.ReadFloat();
        //int _myRotation = _packet.ReadInt();
        
        Debug.Log("Received a message : Spawn New Player");
        PlayersRender.instance.addPlayer(new Player(_myUsername, Vector3.zero));
    }
}
