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
        public override Version Version => new Version(1, 0, 1);
        public override string Description => "Блокирует пакеты 21 и 85";

        public PacketBlocker(Main game) : base(game) { }

        public override void Initialize()
        {
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
            int msgId = (int)args.MsgID;

            // Блокируем только пакеты 21 (UpdateItemDrop) и 85 (QuickStackChests)
            if (msgId == 21 || msgId == 85)
            {
                args.Handled = true;
            }
        }
    }
}