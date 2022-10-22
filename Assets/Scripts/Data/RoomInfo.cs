using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UTS
{
    [System.Serializable]
    public class RoomInfo
    {
        public string Name;

        public RoomInfo(string roomName)
        {
            Name = roomName;
        }
    }
}