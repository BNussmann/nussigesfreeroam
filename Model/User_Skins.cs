using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using nussigesfreeroam.Factories;
using nussigesfreeroam.models;

namespace nussigesfreeroam.Model
{
    class User_Skins : IScript
    {
        public static List<Accounts_Chardata> charData = new List<Accounts_Chardata>();

        [AsyncServerEvent("character:Done")]
        public static async Task SaveCharData(FactoryPlayer player, string[] playerData)
        {
            if(player != null && player.Exists)
            {
                try
                {
                    Alt.Log($"ID: {playerData}");
                    string json = JsonSerializer.Serialize(playerData);
                    var skinData = new Accounts_Chardata
                    {
                        playerid = User.GetPlayerAccountIdByUsername(player.name),
                        charData = json
                    };

                    await using (gtaContext db = new gtaContext())
                    {
                        db.Accounts_Chardata.Add(skinData);
                        db.SaveChanges();
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