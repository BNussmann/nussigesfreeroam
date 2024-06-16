using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AltV.Net;
using AltV.Net.Elements.Entities;
using nussigesfreeroam.Factories;
using nussigesfreeroam.models;

namespace nussigesfreeroam.Model
{
    class User
    {
        public static List<Accounts> Player = new List<Accounts>();
        public static void CreatePlayerAccount(FactoryPlayer player, string username, long discordId)
        {
            if (player == null || !player.Exists) return;

            var PlayerData = new Accounts
            {
                discordId = discordId,
                name = username,
                isBanned = false,
                adminLevel = 0,
                socialClub = player.SocialClubId,
                hwid = player.HardwareIdHash,
                regDate = DateTime.Now,
                kills = 0,
                deaths = 0,
                level = 0,
                money = 5000

            };

            player.name = username;

            try
            {
                Player.Add(PlayerData);

                using (gtaContext db = new gtaContext())
                {
                    db.Accounts.Add(PlayerData);
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                Alt.Log($"{e}");
            }
        }

        public static bool ExistDiscordId(long discordId)
        {
            var pl = Player.FirstOrDefault(p => p.discordId == discordId);

            if (pl != null)
            {
                return true;
            }

            return false;
        }

        public static int GetPlayerAccountIdByUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return 0;
            var pl = Player.FirstOrDefault(p => p.name == username);

            if (pl != null)
            {
                return pl.id;
            }

            return 0;
        }

        public static int GetPlayerOnline(IPlayer player)
        {
            try
            {
                if (player == null || !player.Exists) return 0;
                var pl = Player.FirstOrDefault(p => p.socialClub == player.SocialClubId);
                if (pl != null)
                {
                    return pl.id;
                }
                return 0;
            }
            catch(Exception e)
            {
                Alt.Log($"{e}");
                return 0;
            }
        }

        public static Accounts GetPlayerByCharId(int charId)
        {
            var pl = Player.FirstOrDefault(p => (int)p.id == charId);

            return pl;
        }

        public static bool isBanned(long discordId)
        {
            var pl = Player.FirstOrDefault(p => p.discordId == discordId);

            if (pl.isBanned)
            {
                return true;
            }

            return false;
        }

        public static long GetPlayerAccountId(IPlayer player)
        {
            if (player == null || !player.Exists) return 0;
            var pl = Player.FirstOrDefault(p => p.discordId == player.DiscordId);

            if (pl != null)
            {
                return pl.discordId;
            }

            return 0;
        }

        public static void setUserData(FactoryPlayer player)
        {
            if (player == null || !player.Exists) return;
            var pl = Player.FirstOrDefault(p => p.discordId == player.DiscordId);
            
            if (pl != null)
            {
                player.discordId = pl.discordId;
                player.name = pl.name;
                player.adminLevel = pl.adminLevel;
                player.socialClub = player.SocialClubId;
                player.hwid = player.HardwareIdHash;
                player.kills = pl.kills;
                player.deaths = pl.deaths;
                player.level = pl.level;
                player.money = pl.money;
            }
        }
    }
}