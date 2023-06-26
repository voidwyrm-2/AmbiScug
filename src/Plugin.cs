using System;
using BepInEx;
using Noise;
using On;
using SlugBase.Features;
using UnityEngine;
using static SlugBase.Features.FeatureTypes;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using HUD;
using MoreSlugcats;
using RWCustom;
using On.Menu;
using Menu.Remix.MixedUI;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IL;
using System.Xml.Schema;

namespace AmbiScug
{
    [BepInPlugin("NP.modambiscug", "AmbiScug", "1.0.0")]
    public class AmbiScug : BaseUnityPlugin
    {
        private AmbiScugOptionsMenu.AmbiScugOptionsMenu optionsMenuInstance;
        private bool initialized;
        public void RainWorld_OnModsInit(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
            orig(self);
            if (this.initialized)
            {
                return;
            }
            this.initialized = true;

            //Loading custom assets put png in folder next to mod  and import with line: Futile.atlasManager.LoadImage("atlases/your_png");
            //Futile.atlasManager.LoadImage("atlases/NP/Awri_BG");
            //Futile.atlasManager.LoadImage("atlases/NP/Awri_Hat_Badge_MadeByPeskm_112");
            //Futile.atlasManager.LoadImage("atlases/NP/Awri_real_magic_stuff_112");
            //Futile.atlasManager.LoadImage("atlases/NP/AwriBlush_112");
            //Futile.atlasManager.LoadImage("atlases/NP/AwriHappy_112");
            optionsMenuInstance = new AmbiScugOptionsMenu.AmbiScugOptionsMenu(this);
            try
            {
                MachineConnector.SetRegisteredOI("NP.modambiscug", optionsMenuInstance);
            }
            catch (Exception ex)
            {
                Debug.Log($"AmbiScug: Hook_OnModsInit options failed init error {optionsMenuInstance}{ex}");
                Logger.LogError(ex);
                Logger.LogMessage("WHOOPS");
            }
        }
        public void OnEnable()
        {
            On.RainWorld.OnModsInit += RainWorld_OnModsInit;
            On.Player.Grabability += Player_DoubleSurvivor;
            On.Player.Grabability += Player_DoubleMonk;
            On.Player.Grabability += Player_DoubleHunter;
            On.Player.Grabability += Player_DoubleGourmand;
            On.Player.Grabability += Player_DoubleArtificer;
            On.Player.Grabability += Player_DoubleRuffles;
            On.Player.Grabability += Player_DoubleSpearmaster;
            On.Player.Grabability += Player_DoublePancakes;
            On.Player.Grabability += Player_DoubleSofanthiel;

            On.Player.Grabability += Player_DoubleScugpup;

            On.Player.Die += Player_ArtiSplode;
            On.Player.Die += Player_HunterSplode;
            //On.Creature.Violence += Player_Creature_SkillIssue;
            //On.Player.Grabbed += Player_AntiStun;
        }

        /*
        private void Player_AntiStun(On.Player.orig_Grabbed orig, Player self, Creature.Grasp grasp)
        {
            if (AmbiScugOptionsMenu.AmbiScugOptionsMenu.antiStunCheckBox.Value == true)
            {

            }
        }

        int Dmg = 0;
        private void Player_Creature_SkillIssue(On.Creature.orig_Violence orig, Creature self, BodyChunk source, Vector2? directionAndMomentum, BodyChunk hitChunk, PhysicalObject.Appendage.Pos hitAppendage, Creature.DamageType type, float damage, float stunBonus)
        {
            orig(self, source, directionAndMomentum, hitChunk, hitAppendage, type, damage, stunBonus);
            if(self is Player && AmbiScugOptionsMenu.AmbiScugOptionsMenu.skillIssueCheckBox.Value == true)
            {
                Dmg++;
                if(Dmg == AmbiScugOptionsMenu.AmbiScugOptionsMenu.skillIssueSlider.Value)
                {
                    self.Die();
                }
            }
        }
        */

        private void Player_HunterSplode(On.Player.orig_Die orig, Player self)
        {
            orig(self);
            bool wasDead = self.dead;
            if (wasDead && self.dead && AmbiScugOptionsMenu.AmbiScugOptionsMenu.artiSplodeCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == SlugcatStats.Name.Red)
            {
                var room = self.room;
                var pos = self.mainBodyChunk.pos;
                AbstractCreature abstractCreature1 = new AbstractCreature(room.world, StaticWorld.GetCreatureTemplate(CreatureTemplate.Type.DaddyLongLegs), null, room.GetWorldCoordinate(pos), room.game.GetNewID());
                room.abstractRoom.entities.Add(abstractCreature1);
                abstractCreature1.Realize();
            }
        }

        private void Player_ArtiSplode(On.Player.orig_Die orig, Player self)
        {
            orig(self);
            bool wasDead = self.dead;
            if (wasDead && self.dead && AmbiScugOptionsMenu.AmbiScugOptionsMenu.artiSplodeCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer)
            {
                    // Adapted from ScavengerBomb.Explode
                    var room = self.room;
                    var pos = self.mainBodyChunk.pos;
                    var color = self.ShortCutColor();
                    room.AddObject(new Explosion(room, self, pos, 7, 250f, 6.2f, 2f, 280f, 0.25f, self, 0.7f, 160f, 1f));
                    room.AddObject(new Explosion.ExplosionLight(pos, 280f, 1f, 7, color));
                    room.AddObject(new Explosion.ExplosionLight(pos, 230f, 1f, 3, new Color(1f, 1f, 1f)));
                    room.AddObject(new ExplosionSpikes(room, pos, 14, 30f, 9f, 7f, 170f, color));
                    room.AddObject(new ShockWave(pos, 330f, 0.045f, 5, false));

                    room.ScreenMovement(pos, default, 1.3f);
                    room.PlaySound(SoundID.Bomb_Explode, pos);
                    room.InGameNoise(new Noise.InGameNoise(pos, 9000f, self, 1f));
            }
        }



        private Player.ObjectGrabability Player_DoubleScugpup(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Player && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doubleScugpupCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }


        private Player.ObjectGrabability Player_DoubleSurvivor(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.White && AmbiScugOptionsMenu.AmbiScugOptionsMenu.survCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }

        private Player.ObjectGrabability Player_DoubleMonk(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.Yellow && AmbiScugOptionsMenu.AmbiScugOptionsMenu.monkCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleHunter(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.Red && AmbiScugOptionsMenu.AmbiScugOptionsMenu.huntCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleGourmand(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.gourCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Gourmand)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleArtificer(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.artiCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleRuffles(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.rufflesCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Rivulet)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleSpearmaster(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && (!AmbiScugOptionsMenu.AmbiScugOptionsMenu.spearCheckBox.Value == true) && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Spear)
            {
                return (Player.ObjectGrabability)2;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoublePancakes(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.pancakeCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Saint)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleSofanthiel(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.sofanthielCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Sofanthiel)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }


    }
}