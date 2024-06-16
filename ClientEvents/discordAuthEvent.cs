using System.Net.Http;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using nussigesfreeroam.Factories;
using nussigesfreeroam.Model;

namespace nussigesfreeroam.DiscordAuthEvent
{
    public class DiscordAuth : IScript
    {
        [AsyncClientEvent("Server:DiscordAuthDone")]
        public static async Task DiscordAuthDone(FactoryPlayer player, string token)
        {
            using var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
            var httpResponse = await httpClient.GetAsync("https://discordapp.com/api/users/@me");
            if (httpResponse.IsSuccessStatusCode)
            {
                var responseContent = await httpResponse.Content.ReadAsStringAsync();
                var responseJson = Newtonsoft.Json.Linq.JObject.Parse(responseContent);
                var id = responseJson.Value<string>("id");
                var username = responseJson.Value<string>("username");
                var discriminator = responseJson.Value<string>("discriminator");
                var checkid = 0L;
                Alt.Log($"ID: {id}");
                Alt.Log($"Name: {username}");
                if (!string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(username))
                {
                    if (User.Player != null && User.Player.Any())
                    {
                        checkid = User.Player.FirstOrDefault(x => x.discordId == Convert.ToInt64(id))?.discordId ?? 0L;
                    }
                    if (id != null && username != null)
                    {
                        Alt.Log($"ID: {id}");
                        Alt.Log($"Name: {username}");
                        if (checkid != 0)
                        {
                            if (User.isBanned(Convert.ToInt64(id)))
                            {
                                player.Kick("This account is permanently banned!");
                            }
                            else
                            {
                                Alt.Log($"IDvorhanden: {id}");
                                Alt.Log($"Namevorhanden: {username}");
                                User.setUserData(player);
                                Alt.Emit("Menu::SelectTeam");
                                //player.Model = (uint)AltV.Net.Enums.PedModel.FreemodeMale01;
                                //player.Spawn(new Position(0,0,72));
                                //player.discordId = (short)User.GetPlayerAccountId(player);
                                player.Emit("Vue:LoadPrompt", "two-option", "CHOOSE TEAM", "Select the team you want to fight for!", "TEAM1", "TEAM2", "Server:Choose:Team");
                                Alt.Log("Vue Prompt rausgeballert");
                            }
                        }
                        else
                        {
                            Alt.Log($"ID_NICHT_vorhanden: {id}");
                            Alt.Log($"Name_NICHT_vorhanden: {username}");
                            await AltAsync.Do(() => User.CreatePlayerAccount(player, username, Convert.ToInt64(id)));
                            //player.accountId = (short)User.GetPlayerAccountId(player);
                            /*player.Model = (uint)AltV.Net.Enums.PedModel.FreemodeMale01;
                            await AltAsync.Do(() => player.Spawn(new Position(-1606.1802f, 2106.7385f, 68.20288f), 0));
                            player.Rotation = new AltV.Net.Data.Rotation(0f, 0f, -0.8410563f);
                            await Task.Delay(1000);
                            player.EmitLocked("Client:ShowCharCreator");*/
                            player.Emit("Vue:LoadPrompt", "two-option", "CHOOSE TEAM", "Select the team you want to fight for!", "TEAM1", "TEAM2", "Server:Choose:Team");
                            Alt.Log("Vue Prompt rausgeballert");
                            return;
                        }
                    }
                    else
                    {
                        player.Kick("Authorization failed");
                    }
                }
            }
        }
    }
}


