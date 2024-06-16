using AltV.Net;
using AltV.Net.Data;
using nussigesfreeroam.Model;
using nussigesfreeroam.models;


namespace nussigesfreeroam.Database
{
    internal class DatabaseHandler
    {
        internal static void LoadAllPlayers()
        {
            try
            {
                using (var db = new gtaContext())
                {
                    User.Player = new List<Accounts>(db.Accounts);
                    Alt.Log($"{User.Player.Count} Spieler wurden geladen.");
                }
            }
            catch(Exception e)
            {
                Alt.Log($"{e}");
            }
        }

        internal static void LoadAllPlayerSkins()
        {
            try
            {
                using (var db = new gtaContext())
                {
                    User_Skins.charData = new List<Accounts_Chardata>(db.Accounts_Chardata);
                    Alt.Log($"{User_Skins.charData.Count} Aussehen wurden geladen.");
                }
            }
            catch(Exception e)
            {
                Alt.Log($"{e}");
            }
        }

        internal static void LoadAllVehicles()
        {
            try
            {
                using (var db = new gtaContext())
                {
                    ServerVehicles.VehiclesList = new List<Vehicles>(db.Vehicles);
                    Alt.Log($"{ServerVehicles.VehiclesList.Count} Server-Vehicles wurden geladen.");
                    foreach (var veh in db.Vehicles)
                    {
                        uint hash = Alt.Hash(veh.spawnName);
                        ServerVehicles.spawnAllServerVehicles(hash, new Position(veh.lastPosX, veh.lastPosY, veh.lastPosZ), new Rotation(veh.lastRotX, veh.lastRotY, veh.lastRotZ), veh.id);
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