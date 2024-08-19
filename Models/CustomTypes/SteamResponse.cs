

using System.ComponentModel.DataAnnotations;

namespace BacklogAPI.Models
{
    public class ResponseGameObject
    {
        public int Appid { get; set; }
        public int PlaytimeForever { get; set; }
        public int PlaytimeWindowsForever { get; set; }
        public int PlaytimeMacForever { get; set; }
        public int PlaytimeLinuxForever { get; set; }
        public int PlaytimeDeckForever { get; set; }
        public int RtimeLastPlayed { get; set; }
        public int PlaytimeDisconnected { get; set; }
    }

    public class SteamResponse
    {
        public int GameCount { get; set; }
        public List<ResponseGameObject> Games { get; set; }
    }

    public class RootSteamResponse
    {
        public SteamResponse Response { get; set; }
    }
}