
using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;
using AltV.Net.Enums;
using nussigesfreeroam.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace nussigesfreeroam.Extensions
{
    public static class Extensions
    {
        public static bool IsInRange(this Position currentPosition, Position otherPosition, float distance)
            => currentPosition.Distance(otherPosition) <= distance;
        
        /** FVehicle Extensions **/

        public static bool HasVehicleId(this IVehicle vehicle)
        {
            var myVehicle = (FactoryVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return false;
            return myVehicle.vehicleId != 0;
        }

        public static void SetVehicleId(this IVehicle vehicle, long vehicleId)
        {
            var myVehicle = (FactoryVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return;
            myVehicle.vehicleId = (int)vehicleId;
        }
        

        public static long GetVehicleId(this IVehicle vehicle)
        {
            var myVehicle = (FactoryVehicle)vehicle;
            if (myVehicle == null || !myVehicle.Exists) return 0;
            return (long)myVehicle.vehicleId;
        }


    }
}