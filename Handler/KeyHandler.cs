using AltV.Net;
using AltV.Net.Async;
using AltV.Net.Elements.Entities;
using nussigesfreeroam.Model;
using nussigesfreeroam.models;


namespace nussigesfreeroam.Keyhandler
{
    internal class KeyHandler
    {
        [AsyncClientEvent("Server:KeyHandler:PressE")]
        public void PressE(IPlayer player)
        {
            if (player == null || !player.Exists) return;
            player.EmitLocked("Client:NativeUi:Open", player);
        }
    }
}