using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace nussigesfreeroam.models
{
    public partial class Accounts
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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


    }
}
