using System.Net.Http;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;

namespace nussigesfreeroam.EnterVerhicleEvent
{
    public class EnterVehicle : IScript
    {
    [ScriptEvent(ScriptEventType.PlayerEnterVehicle)]
    public void OnPlayerEnterVehicle(FactoryVehicle vehicle, FactoryPlayer player, byte seat)
        {
            player.EmitLocked("Client:ShowSpeedo");
        }
    
    [ScriptEvent(ScriptEventType.PlayerLeaveVehicle)]
    public void OnPlayerLeaveVehicle(FactoryVehicle vehicle, FactoryPlayer player, byte seat)
        {
            player.EmitLocked("Client:DestroySpeedo");
            ServerVehicles.updateVehiclePos(player, vehicle);
        }
    }
}


