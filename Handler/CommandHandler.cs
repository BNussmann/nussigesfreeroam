using System.Globalization;
using System.Numerics;
using System.Runtime.Serialization;
using AltV.Net;
using AltV.Net.Client.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Resources.Chat.Api;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;
using nussigesfreeroam.Extensions;
using nussigesfreeroam.models;
using AltV.Net.Client.Elements.Entities;
using Ped = AltV.Net.Client.Elements.Entities.Ped;


namespace nussigesfreeroam.CommandHandler
{
    internal class CommandHandler : IScript
    {
        public enum GtaWeather
        {
            ExtraSunny = 0,
            Clear = 1,
            Clouds = 2,
            Smog = 3,
            Foggy = 4,
            Overcast = 5,
            Rain = 6,
            Thunder = 7,
            LightRain = 8,
            SmoggyLightRain = 9,
            VeryLightSnow = 10,
            WindyLightSnow = 11,
            LightSnow = 12,
            Christmas = 13,
            Halloween = 14
        }

        [Command("pos")]
        public void savePos(FactoryPlayer player, string title)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            Vector3 position; Vector3 rotation; string line;
            if(title != "")
            {
                if (player.IsInVehicle)
                {
                    position = player.Vehicle.Position;
                    rotation = player.Vehicle.Rotation;

                    line = $"VehiclePos | {title}: {position.X.ToString(new CultureInfo("en-US"))}, {position.Y.ToString(new CultureInfo("en-US"))}, {position.Z.ToString(new CultureInfo("en-US"))}, {rotation.Z.ToString(new CultureInfo("en-US"))}";
                }
                else
                {
                    position = player.Position;
                    rotation = player.Rotation;

                    line = $"OnFoot | {title}: {position.X.ToString(new CultureInfo("en-US"))}, {position.Y.ToString(new CultureInfo("en-US"))}, {position.Z.ToString(new CultureInfo("en-US"))}, {rotation.Z.ToString(new CultureInfo("en-US"))}";

                }
                    Alt.Log($"Position gespeichert:{line}");
            }
            else
            {
                return;
            }



            /*  string positionsFile = "C:/Users/nikla/Desktop/altv-freeroam";
                try
                {
                    using (StreamWriter file = new StreamWriter(positionsFile, true))
                    {
                        file.WriteLine(line);
                        file.Flush();
                        file.Close();
                    }
                }
                catch (Exception e)
                {
                    Alt.Log($"Couldn't open path {positionsFile}");
                    Alt.Log($"{e}");
                } */
        }

        [Command("veh")]
        public void spawnVeh(FactoryPlayer player, string model)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            IVehicle veh = Alt.CreateVehicle(model, new Position(player.Position.X, player.Position.Y-3, player.Position.Z), player.Rotation);
            veh.EngineOn = true;
        }

        [Command("gotoaplow")]
        public void gotoaplow(FactoryPlayer player) {
            try 
            {
                player.Position = new Position(-459.503754f, 1258.93933f, 139.112091f);
            } 
            catch (Exception e) 
            {
                Alt.Log($"{e}");
            }
        }

        [Command("day")]
        public void dayTime(FactoryPlayer player)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            player.Emit("Client:SetTime:Day");
        }

        [Command("weapon")]
        public void weapon(FactoryPlayer player)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            player.GiveWeapon(AltV.Net.Enums.WeaponModel.BullpupRifle, 9999, true);   
        }

        [Command("regveh")]
        public async void registerVehicle(FactoryPlayer player, string model)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            uint vehHash = Alt.Hash(model);
            FactoryVehicle veh = (FactoryVehicle)Alt.CreateVehicle(model, player.Position, player.Rotation);
            await ServerVehicles.CreateServerVehicle(player, veh, model);
        }

        [Command("rep")]
        public void repair(FactoryPlayer player)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            player.Vehicle.Repair();
        }

        [Command("shop")]
        public void gotoShopMlo(FactoryPlayer player, int input)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            int inputint = (int)input;
            if(inputint == (int)0)
            {
                player.Position = new Position(-217.79341f, -519.9165f, 34.115723f);
            }
            else
            {
                player.Position = new Position(-1564.0879f, -493.7802f, 35.497437f);
            }
        }

        [Command("weather")]
        public void setWeather(FactoryPlayer player, string input)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;
            GtaWeather weatherEnum;
            uint weatherid = 0;

            if(Enum.TryParse(input, true, out weatherEnum))
            {
                weatherid = (uint)weatherEnum;
            }

            player.SetWeather(weatherid);
        }

        public static async Task SpawnPed(FactoryPlayer player)
        {
            if (player == null || !player.Exists || player.adminLevel < 1) return;

            var newPed = Alt.CreatePed(AltV.Net.Enums.PedModel.Clown01SMY, new Vector3(0, 0, 72), new Vector3(0, 0, 0));

            newPed.SetNetworkOwner(player);
            newPed.NetworkOwner.Emit("ped_task", newPed);
            await Task.Delay(5000);
            newPed.Destroy();
        }
    }
}