using System.Data.Common;
using AltV.Net;
using AltV.Net.Async.Elements.Entities;
using AltV.Net.Data;
using nussigesfreeroam.Model;

namespace nussigesfreeroam.Factories
{
    public class FactoryVehicle : AsyncVehicle
    {
        public int vehicleId { get; set; }
        public FactoryVehicle(ICore server, IntPtr nativePointer, uint id) : base(server, nativePointer, id)
        {
        }
    }
}
