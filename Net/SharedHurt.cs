using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using NetEasy;
using Terraria.DataStructures;
using Microsoft.Xna.Framework;

namespace SharedHealthMod.Net
{
    [Serializable]
    public class SharedHurt : Module
    {
        public string causerName;
        public int damage;

        protected override void Receive()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient && causerName != Main.LocalPlayer.name)
            {
                Main.LocalPlayer.statLife -= CalculateDamage();
                Main.PlaySound(SoundID.PlayerHit, Main.LocalPlayer.position);
                if (Main.LocalPlayer.statLife < 1)
                {
                    Main.LocalPlayer.KillMe(PlayerDeathReason.ByCustomReason(RandomDeathMessage()), 0.0, 0);
                }
            }
        }

        private int CalculateDamage()
        {
            return (int)((MathHelper.Clamp((damage - Main.LocalPlayer.statDefense / 2), 1, Math.Abs(damage)) * MathHelper.Clamp(1 - Main.LocalPlayer.endurance, 0, 1)) * (Main.LocalPlayer.hasPaladinShield ? 0.75 : 1));
        }

        private string RandomDeathMessage()
        {
            string me = Main.LocalPlayer.name.ToUpper();
            string causer = causerName.ToUpper();
            int r = Main.rand.Next(0, 8);

            switch (r) {
                case 0:
                    return causer + " KILLED " + me;
                case 1:
                    return me + " WAS KILLED BY " + causer;
                case 2:
                    return causer + " HAS FAILED " + me;
                case 3:
                    return me + " WAS KILLED BY " + causer + "'S FAILURE";
                case 4:
                    return causer + "'S MISTAKE KILLED " + me;
                case 5:
                    return me + " WAS KILLED BY " + causer + "'S MISTAKE";
                case 6:
                    return causer + "'S MISTAKE COST " + me + (Main.LocalPlayer.Male ? " HIS LIFE" : " HER LIFE");
                case 7:
                    return causer + "'S FAILURE COST " + me + (Main.LocalPlayer.Male ? " HIS LIFE" : " HER LIFE");
                default:
                    return "";

            }
        }
    }
}
