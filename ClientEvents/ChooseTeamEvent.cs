using System.Net.Http;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;

namespace nussigesfreeroam.EnterVerhicleEvent
{
    public class ChooseTeam : IScript
    {
        [AsyncClientEvent("Server:Choose:Team")]
        public static void ChoosePlayerTeam(FactoryPlayer player, string teamName)
        {
            if (player == null || !player.Exists) return;
            if(teamName == null) return;

            if(teamName == "yes")
            {
                player.teamID = 1;
                player.Model = (uint)AltV.Net.Enums.PedModel.FreemodeMale01;
                player.Spawn(new Position(0,15,72));
                User.setUserData(player);
            }
            else if(teamName == "no")
            {
                player.teamID= 2;
                player.Model = (uint)AltV.Net.Enums.PedModel.FreemodeMale01;
                player.Spawn(new Position(0,0,72));
                User.setUserData(player);
            }
            Alt.Log($"TEAMNAME: {teamName}");
        }
    }
}


