using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;
using nussigesfreeroam.models;


namespace nussigesfreeroam.VehicleHandler
{
    internal class KeyHandler : IScript
    {
        public async void registerNewCar(FactoryPlayer player, FactoryVehicle veh)
        {
            if(!player.Exists || player == null || veh == null || !veh.Exists) return;

            

        }
    }
}