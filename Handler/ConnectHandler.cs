using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;
using nussigesfreeroam.models;

namespace nussigesfreeroam.ConnectHandler
{
    public class ConnectHandler : IScript
    {
         [ScriptEvent(ScriptEventType.PlayerConnect)]
        public void OnPlayerConnect_Handler(IPlayer player, string reason)
        {
            if (player == null || !player.Exists) return;
            player.Emit("discord:BeginAuth");
            Alt.Log("test");
        }

    }
}