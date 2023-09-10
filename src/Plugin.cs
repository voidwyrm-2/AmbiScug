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
using System.Security.Policy;

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

        public static readonly bool IsInvFlirtable = true;
        public static bool NoDamage;

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


            On.Player.SubtractFood += Player_KarmaReinforce;
            On.Player.SwallowObject += Player_EegGeneration;
            On.MoreSlugcats.SingularityBomb.ctor += SingularityBombZeroMode_ctor;
            On.Player.Update += Player_CheckIfInvIsPlayed;

            On.Player.Die += Player_ArtiSplode;
            On.Creature.Violence += Player_Creature_SkillIssue;
            On.Player.Update += Player_NoDamage;

            On.Player.Update += Player_OnMushrooms;
        }

        private void Player_OnMushrooms(On.Player.orig_Update orig, Player self, bool eu)
        {
            if (Input.GetKey(KeyCode.LeftControl) && self.SlugCatClass == SlugcatStats.Name.White && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimesurvCheckBox.Value == true)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && self.SlugCatClass == SlugcatStats.Name.Yellow && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimemonkCheckBox.Value == true)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && self.SlugCatClass == SlugcatStats.Name.Red && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimehuntCheckBox.Value == true)
            {
                self.mushroomEffect = 1.0f;
            }

            if (Input.GetKey(KeyCode.LeftControl) && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimegourCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Gourmand)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimeartiCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimerufflesCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Rivulet)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimespearCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Spear)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimepancakeCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Saint)
            {
                self.mushroomEffect = 1.0f;
            }
            if (Input.GetKey(KeyCode.LeftControl) && AmbiScugOptionsMenu.AmbiScugOptionsMenu.bulletTimesofanthielCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Sofanthiel)
            {
                self.mushroomEffect = 1.0f;
            }
        }
        
        private void Player_NoDamage(On.Player.orig_Update orig, Player self, bool eu)
        {
            if(NoDamage && self.dead == false)
            {
                self.playerState.permanentDamageTracking = 0.0;
            }
            orig(self, eu);
        }


        int Dmg = 0;
        private void Player_Creature_SkillIssue(On.Creature.orig_Violence orig, Creature self, BodyChunk source, Vector2? directionAndMomentum, BodyChunk hitChunk, PhysicalObject.Appendage.Pos hitAppendage, Creature.DamageType type, float damage, float stunBonus)
        {
            
            if(self is Player && AmbiScugOptionsMenu.AmbiScugOptionsMenu.skillIssueCheckBox.Value == true)
            {
                Dmg++;
                if(Dmg == AmbiScugOptionsMenu.AmbiScugOptionsMenu.skillIssueSlider.Value)
                {
                    NoDamage = false;
                    //self.Die();
                }
                else if(Dmg < AmbiScugOptionsMenu.AmbiScugOptionsMenu.skillIssueSlider.Value)
                {
                    NoDamage = true;
                }
            }
            orig(self, source, directionAndMomentum, hitChunk, hitAppendage, type, damage, stunBonus);
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
                    room.AddObject(new Explosion(room, self, pos, 7, 250f, 14f, 50f, 650f, 0.25f, self, 0.7f, 160f, 1f));
                    //new Explosion.Explosion(Room room, PhysicalObject sourceObject, Vector2 pos, int lifetime, float rad, float force, float damage, float stun, float deafen, Creature killlagHolder, float killTagHolderDmgFactor, float minStun, float backgroundNoise)
                    room.AddObject(new Explosion.ExplosionLight(pos, 280f, 1f, 7, color));
                    room.AddObject(new Explosion.ExplosionLight(pos, 230f, 1f, 3, new Color(1f, 1f, 1f)));
                    room.AddObject(new ExplosionSpikes(room, pos, 14, 30f, 9f, 7f, 170f, color));
                    room.AddObject(new ShockWave(pos, 330f, 0.045f, 5, false));

                    room.ScreenMovement(pos, default, 1.3f);
                    room.PlaySound(SoundID.Bomb_Explode, pos);
                    room.InGameNoise(new Noise.InGameNoise(pos, 9000f, self, 1f));
            }
        }


        private void Player_KarmaReinforce(On.Player.orig_SubtractFood orig, Player self, int sub)
        {
            orig(self, sub);
            if (AmbiScugOptionsMenu.AmbiScugOptionsMenu.karmaReinforceCheckBox.Value == true && self.KarmaIsReinforced == false)
            {

                if (self.SlugCatClass == SlugcatStats.Name.White && self.FoodInStomach < 0 && self.FoodInStomach == 8)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(8);

                }
                if (self.SlugCatClass == SlugcatStats.Name.Yellow && self.FoodInStomach < 0 && self.FoodInStomach == 5)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(5);
                
                }
                if (self.SlugCatClass == SlugcatStats.Name.Red && self.FoodInStomach < 0 && self.FoodInStomach == 9)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(9);
                
                }

                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Gourmand && self.FoodInStomach < 0 && self.FoodInStomach == 11)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(11);
                
                }
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer && self.FoodInStomach < 0 && self.FoodInStomach == 9)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(9);
                
                }
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Rivulet && self.FoodInStomach < 0 && self.FoodInStomach == 6)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(6);
                
                }
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Spear && self.FoodInStomach < 0 && self.FoodInStomach == 10)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(10);
                
                }
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Saint && self.FoodInStomach < 0 && self.FoodInStomach == 5)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(5);
                
                }
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Sofanthiel && self.FoodInStomach < 0 && self.FoodInStomach == 12 && IsInvFlirtable == true)
                {
                    (self.abstractCreature.world.game.session as StoryGameSession).saveState.deathPersistentSaveData.reinforcedKarma = true;
                    self.SubtractFood(12);
                
                }
                /*
                if (!(self.SlugCatClass == SlugcatStats.Name.White && self.FoodInStomach < 0))
                {
                    if (self is not null && self is Player && self.FoodInStomach < 0 && self.FoodInStomach == )
                    {

                    }

                }
                */

            }
        }

        /*
        public bool ShouldBeMalnourished = false;
        public int MalnutritionTickTimer = 0;

        private void Player_TemporaryMalnutritionTest(On.Player.orig_Update orig, Player self, bool eu)
        {
            orig(self, eu);
            MalnutritionTickTimer++;
            if (AmbiScugOptionsMenu.AmbiScugOptionsMenu.eegGenerationCheckBox.Value == true && AmbiScugOptionsMenu.AmbiScugOptionsMenu.eegGenerationMalnutritionCheckBox.Value == true)
            {
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Sofanthiel)
                {
                    if (ShouldBeMalnourished == false && isInvPlayed == true && IsInvFlirtable == true && MalnutritionTickTimer < 160)
                    {
                        self.SetMalnourished(true);
                    }
                }
            }
            else if (MalnutritionTickTimer == 160 && ShouldBeMalnourished == true && isInvPlayed == true && IsInvFlirtable == true)
            {
                self.SetMalnourished(false);
                ShouldBeMalnourished = false;
                MalnutritionTickTimer = 0;
            }
        }
        */

        public static bool EegMode = false;
        public static bool IsInvPlayed = false;
        //public bool AfterEggGeneration = false;

        private void Player_CheckIfInvIsPlayed(On.Player.orig_Update orig, Player self, bool eu)
        {
            orig(self, eu);
            if (AmbiScugOptionsMenu.AmbiScugOptionsMenu.eegGenerationCheckBox.Value == true)
            {
                if (ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Sofanthiel && self.MaxFoodInStomach == 12 && IsInvFlirtable == true)
                {

                    IsInvPlayed = true;

                }
                else
                {
                    IsInvPlayed = false;
                }
            }
        }

        private void SingularityBombZeroMode_ctor(On.MoreSlugcats.SingularityBomb.orig_ctor orig, SingularityBomb self, AbstractPhysicalObject abstractPhysicalObject, World world)
        {
            orig(self, abstractPhysicalObject, world);
            if (AmbiScugOptionsMenu.AmbiScugOptionsMenu.eegGenerationCheckBox.Value == true)
            {
                if (EegMode == true && ModManager.MSC && IsInvFlirtable == true && IsInvPlayed)
                {

                    self.zeroMode = true;

                }
            }
        }

        private void Player_EegGeneration(On.Player.orig_SwallowObject orig, Player self, int grasp)
        {
            orig(self, grasp);
            if (AmbiScugOptionsMenu.AmbiScugOptionsMenu.eegGenerationCheckBox.Value == true)
            {
                if (ModManager.MSC && IsInvPlayed && self.FoodInStomach < 0 && self.FoodInStomach == 12 && IsInvFlirtable == true)
                {
                    if(self.input[0].jmp && self.input[0].pckp && self.input[0].thrw && self.input[0].x == 0 && self.input[0].y != 0)
                    {
                        self.SubtractFood(12);
                        self.objectInStomach = new AbstractConsumable(self.room.world, MoreSlugcatsEnums.AbstractObjectType.SingularityBomb, null, self.abstractPhysicalObject.pos, self.room.game.GetNewID(), -1, -1, null);
                        EegMode = true;
                        //ShouldBeMalnourished = true;
                    }
                }
            }
        }


        /*
        private Player.ObjectGrabability Player_DoubleScugpup(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Player && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doubleScugpupCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        */

        private Player.ObjectGrabability Player_DoubleSurvivor(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.White && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublesurvCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleMonk(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.Yellow && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublemonkCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleHunter(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && self.SlugCatClass == SlugcatStats.Name.Red && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublehuntCheckBox.Value == true)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }

        private Player.ObjectGrabability Player_DoubleGourmand(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublegourCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Gourmand)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleArtificer(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doubleartiCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Artificer)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleRuffles(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublerufflesCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Rivulet)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleSpearmaster(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && (!AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublespearCheckBox.Value == true) && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Spear)
            {
                return (Player.ObjectGrabability)2;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoublePancakes(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublepancakeCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Saint)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }
        private Player.ObjectGrabability Player_DoubleSofanthiel(On.Player.orig_Grabability orig, Player self, PhysicalObject obj)
        {
            if (obj is Spear && AmbiScugOptionsMenu.AmbiScugOptionsMenu.doublesofanthielCheckBox.Value == true && ModManager.MSC && self.SlugCatClass == MoreSlugcatsEnums.SlugcatStatsName.Sofanthiel)
            {
                return (Player.ObjectGrabability)1;
            }
            return orig(self, obj);
        }


    }
}