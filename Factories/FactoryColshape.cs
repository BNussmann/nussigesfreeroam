using AltV.Net;
using AltV.Net.Async.Elements.Entities;

namespace nussigesfreeroam.Factories
{
    public class FactoryColshape : AsyncColShape
    {
        public float Radius { get; set; }
        public FactoryColshape(ICore server, IntPtr nativePointer, uint id) : base(server, nativePointer, id)
        {

        }
    }
}
