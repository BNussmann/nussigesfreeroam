using AltV.Net;
using AltV.Net.Elements.Entities;
using AltV.Net.Async;
using nussigesfreeroam.Factories;


namespace nussigesfreeroam
{
    public class Main : AsyncResource
    {    
        public override IEntityFactory<IPlayer> GetPlayerFactory()
        {
            return new AccountsFactory();
        }

        public override IBaseObjectFactory<IColShape> GetColShapeFactory()
        {
            return new ColshapeFactory();
        }

        public override IEntityFactory<IVehicle> GetVehicleFactory()
        {
            return new VehicleFactory();
        }    
        public override void OnStart()
        {
            
            try
            {
                Database.DatabaseHandler.LoadAllPlayers();
                Database.DatabaseHandler.LoadAllPlayerSkins();
                Database.DatabaseHandler.LoadAllVehicles();

                Environment.SetEnvironmentVariable("COMPlus_legacyCorruptedState­­ExceptionsPolicy", "1");
                Alt.Log("Server ist gestartet.");
            } 
            catch (Exception e)
            {
                Alt.Log($"{e}");
            }

        }

        

        public override void OnStop()
        {
            foreach (var player in Alt.GetAllPlayers().Where(p => p != null && p.Exists)) player.Kick("Server wird heruntergefahren...");
            Alt.Log("Server ist gestoppt.");
        }
    }
}