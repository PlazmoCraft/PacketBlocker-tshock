using System;
using Terraria;
using TerrariaApi.Server;
using TShockAPI;

namespace PacketBlockerPlugin
{
    [ApiVersion(2, 1)]
    public class PacketBlocker : TerrariaPlugin
    {
        public override string Name => "PacketBlocker";
        public override string Author => "мистер мастер";
        public override Version Version => new Version(1, 0, 0);
        public override string Description => "Блокирует пакеты инвентаря для защиты SSC";

        public PacketBlocker(Main game) : base(game) { }

        public override void Initialize()
        {
            // Регистрируем хук перехвата сетевых пакетов через TSAPI
            ServerApi.Hooks.NetGetData.Register(this, OnGetData);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                ServerApi.Hooks.NetGetData.Deregister(this, OnGetData);
            }
            base.Dispose(disposing);
        }

        private void OnGetData(GetDataEventArgs args)
        {
            PacketTypes packetType = args.MsgID;

            // Блокируем пакеты слотов и инвентаря от клиентов
            if (packetType == PacketTypes.PlayerSlot || packetType == PacketTypes.ClientSyncedInventory)
            {
                args.Handled = true;
            }
        }
    }
}