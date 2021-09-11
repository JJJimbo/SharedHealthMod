using System.IO;
using Terraria.ModLoader;

namespace SharedHealthMod
{
	public class SharedHealthMod : Mod
	{
        public override void Load()
        {
            NetEasy.NetEasy.Load(this);
        }
        public override void HandlePacket(BinaryReader reader, int whoAmI)
        {
            NetEasy.NetEasy.HandleModule(reader, whoAmI);
        }
    }
}