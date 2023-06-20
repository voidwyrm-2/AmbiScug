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
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.White && AmbiScugOptionsMenu.AmbiScugOptionsMenu.monkCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleHunter(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.White && AmbiScugOptionsMenu.AmbiScugOptionsMenu.huntCheckBox.Value == true)
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