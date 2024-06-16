using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using Microsoft.VisualBasic;

namespace nussigesfreeroam.Factories
{
    public class FactoryPlayer : AsyncPlayer
    {
        public int id { get; set; }
        public long discordId { get; set; }
        public string name { get; set; }
        public int adminLevel { get; set; }
        public ulong socialClub { get; set; }
        public bool isBanned {get; set;}
        public ulong hwid {get; set;}
        public DateTime regDate {get; set;}
        public int kills {get; set;}
        public int deaths {get; set;}
        public int level {get; set;}
        public int money {get; set;}
        public int teamID { get; set; } = 0;
        public FactoryPlayer(ICore core, IntPtr nativePointer, uint id) : base(core, nativePointer, id)
        {
        }
    }
}
