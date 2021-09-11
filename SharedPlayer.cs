using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
using SharedHealthMod.Net;

namespace SharedHealthMod
{
    class SharedPlayer : ModPlayer
    {



        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            SharedHurt hurt = new SharedHurt
            {
                causerName = player.name,
                damage = !player.defendedByPaladin ? damage : damage / 2
            };
            hurt.Send(null, null, false);
            // return false;
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }

        /*public override void UpdateLifeRegen()
        {
            if (Main.netMode == NetmodeID.MultiplayerClient)
            {
                int lr = player.lifeRegen;
                for (int i = 0; i < Main.player.Length; i++)
                {
                    if (Main.player[i].lifeRegen - 1 > lr)
                    {
                        Main.NewText(lr.ToString());
                        lr = Main.player[i].lifeRegen;
                    }
                }
                player.lifeRegen = lr;
            }
        }*/

        /*public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            // base.Hurt(pvp, quiet, damage, hitDirection, crit);
        }

        public override void PostHurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            // base.PostHurt(pvp, quiet, damage, hitDirection, crit);
        }*/
    }
}
