using System.Data.Common;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using nussigesfreeroam.Extensions;
using nussigesfreeroam.Factories;
using nussigesfreeroam.models;

namespace nussigesfreeroam.Model
{
    class ServerVehicles : IScript
    {
        public static List<Vehicles> VehiclesList = new List<Vehicles>();
        public static async Task CreateServerVehicle(FactoryPlayer player, FactoryVehicle veh, string model)
        {
            if(!player.Exists || player == null)
            {
                return;
            }
            else
            {
                try
                {
                    var vehData = new Vehicles
                    {
                        spawnHash = veh.GetHashCode(),
                        spawnName = model,
                        lastPosX = veh.Position.X,
                        lastPosY = veh.Position.Y,
                        lastPosZ = veh.Position.Z,
                        lastRotX = veh.Rotation.Pitch,
                        lastRotY = veh.Rotation.Roll,
                        lastRotZ = veh.Rotation.Yaw,
                        created = DateTime.Now
                    };
                    VehiclesList.Add(vehData);

                    await using (gtaContext db = new gtaContext())
                    {
                        db.Vehicles.Add(vehData);
                        db.SaveChanges();
                    }

                    veh.SetVehicleId((long)vehData.id);
                }
                catch (Exception e)
                {
                    Alt.Log($"{e}");
                }
            }
        }

        public static async void spawnAllServerVehicles(uint vehHash, Position vehPos, Rotation vehRot, int id)
        {
            var newVeh = await AltAsync.CreateVehicle(vehHash, vehPos, vehRot);
            FactoryVehicle saveFactoryVehicle = (FactoryVehicle)newVeh;
            saveFactoryVehicle.vehicleId = id;
        }

        public static async void spawnServerVehiclesById(FactoryPlayer player, int id)
        {
            var veh = VehiclesList.FirstOrDefault(x => x.id == id);
            await AltAsync.CreateVehicle((uint)veh.spawnHash, new Position(player.Position.X, player.Position.Y, player.Position.Z), new Rotation(player.Rotation.Pitch, player.Rotation.Roll, player.Rotation.Yaw));
        }

        public static void updateVehiclePos(FactoryPlayer player, FactoryVehicle veh)
        {
            if(!player.Exists || player == null)
            {
                return;
            }
            else
            {
                try
                {
                    lock (VehiclesList)
                    {
                        long vehID = veh.GetVehicleId();
                        if (veh == null || !veh.Exists || vehID == 0) return;
                        var vehs = VehiclesList.FirstOrDefault(v => (long)v.id == veh.vehicleId);
                        if (vehs != null)
                        {
                            vehs.lastPosX = veh.Position.X;
                            vehs.lastPosY = veh.Position.Y;
                            vehs.lastPosZ = veh.Position.Z;
                            vehs.lastRotX = veh.Rotation.Pitch;
                            vehs.lastRotY = veh.Rotation.Roll;
                            vehs.lastRotZ = veh.Rotation.Yaw;
                        }

                        using (gtaContext db = new gtaContext())
                        {
                            db.Vehicles.Update(vehs);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception e)
                {
                    Alt.Log($"{e}");
                }
            }
        }
    }
}