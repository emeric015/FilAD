using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientSend : MonoBehaviour
{
    private static void SendTCPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.tcp.SendData(_packet);
    }

    private static void SendUDPData(Packet _packet)
    {
        _packet.WriteLength();
        Client.instance.udp.SendData(_packet);
    }

    #region Packets
    public static void WelcomeReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.welcomeReceived))
        {
            _packet.Write(Client.instance.myId);
            string name = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerLabel>().name;
            _packet.Write("TOTO");

            SendTCPData(_packet);
        }
    }

    public static void UDPTestReceived()
    {
        using (Packet _packet = new Packet((int)ClientPackets.updTestReceived))
        {
            _packet.Write("I am a client powered by UDP");

            SendUDPData(_packet);
        }
    }

    public static void UdpMyPlayerMoved()
    {
        using (Packet _packet = new Packet((int)ClientPackets.udpPlayerMovement))
        {
            _packet.Write("I MOVED");

            SendUDPData(_packet);
        }

    }
    #endregion
}
