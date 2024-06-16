using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace nussigesfreeroam.models
{
    public partial class Vehicles
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string spawnName {get; set;}
        public int spawnHash { get; set; }
        public float lastPosX { get; set; }
        public float lastPosY { get; set; }
        public float lastPosZ { get; set; }
        public float lastRotX { get; set; }
        public float lastRotY { get; set; }
        public float lastRotZ { get; set; }
        public DateTime created { get; set;}
    }
}