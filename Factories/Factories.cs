using AltV.Net;
using AltV.Net.Data;
using AltV.Net.Elements.Entities;

namespace nussigesfreeroam.Factories
{
    public class VehicleFactory : IEntityFactory<IVehicle>
    {
        public IVehicle Create(ICore server, IntPtr nativePointer, uint id)
        {
            return new FactoryVehicle(server, nativePointer, id);
        }
    }

    public class AccountsFactory : IEntityFactory<IPlayer>
    {
        public IPlayer Create(ICore core, IntPtr playerPointer, uint id)
        {
            return new FactoryPlayer(core, playerPointer, id);
        }
    }

    public class ColshapeFactory : IBaseObjectFactory<IColShape>
    {
        public IColShape Create(ICore server, IntPtr entityPointer, uint id)
        {
            return new FactoryColshape(server, entityPointer, id);
        }
    }
}
