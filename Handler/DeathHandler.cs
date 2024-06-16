using AltV.Net;
using AltV.Net.Data;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;
using nussigesfreeroam.models;


namespace nussigesfreeroam.DeathHandler
{
    internal class DeathHandler : IScript
    {
        [ScriptEvent(ScriptEventType.PlayerDead)]
        public async void OnPlayerDead(FactoryPlayer player, FactoryPlayer killer, uint weapon)
        {
            await Task.Delay(5000);
            player.Health = 200;
            player.Spawn(new Position(0,0,72));
        }
    }
}