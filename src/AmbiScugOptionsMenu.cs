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

namespace AmbiScug.AmbiScugOptionsMenu
{
    public class AmbiScugOptionsMenu : OptionInterface
    {
        public AmbiScugOptionsMenu(AmbiScug plugin)
        {
            doublesurvCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleSurvCheckbox", false); // All of these are where the game saves your settings, the important part is the "Key" field, make sure this will never conflict with another mod's key by having a prefix, like your mod name
            doublemonkCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleMonkCheckbox", false);
            doublehuntCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleHuntCheckbox", false);
            doublegourCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleGourCheckbox", false);
            doubleartiCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleArtiCheckbox", false);
            doublerufflesCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleRufflesCheckbox", false);
            doublespearCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleSpearCheckbox", true);
            doublepancakeCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoublePancakeCheckbox", false);
            doublesofanthielCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleSofanthielCheckbox", false);


            karmaReinforceCheckBox = this.config.Bind<bool>("AmbiScug_Bool_KarmaReinforceCheckbox", false);
            eegGenerationCheckBox = this.config.Bind<bool>("AmbiScug_Bool_eegGenerationCheckBox", false);
            eegGenerationMalnutritionCheckBox = this.config.Bind<bool>("AmbiScug_Bool_eegGenerationMalnutritionCheckBox", false);


            artiSplodeCheckBox = this.config.Bind<bool>("AmbiScug_Bool_ArtiSplodeCheckbox", true);
            hunterSplodeCheckBox = this.config.Bind<bool>("AmbiScug_Bool_HunterSplodeCheckbox", false);
            skillIssueCheckBox = this.config.Bind<bool>("AmbiScug_Bool_SkillIssueCheckbox", false);
            skillIssueSlider = this.config.Bind<int>("AmbiScug_Int_SkillIssueSlider", 1);
            antiStunCheckBox = this.config.Bind<bool>("AmbiScug_Bool_AntiStunCheckbox", true);

            bulletTimesurvCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeSurvCheckbox", false);
            bulletTimemonkCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeMonkCheckbox", false);
            bulletTimehuntCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeHuntCheckbox", false);
            bulletTimegourCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeGourCheckbox", false);
            bulletTimeartiCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeArtiCheckbox", false);
            bulletTimerufflesCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeRufflesCheckbox", false);
            bulletTimespearCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeSpearCheckbox", false);
            bulletTimepancakeCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimePancakeCheckbox", false);
            bulletTimesofanthielCheckBox = this.config.Bind<bool>("AmbiScug_Bool_BulletTimeSofanthielCheckbox", false);
        }
        public override void Initialize()
        {
            var opTab1 = new OpTab(this, "AmbiScug");
            var opTab2 = new OpTab(this, "SupliScug");
            var opTab3 = new OpTab(this, "OtherScug");
            var opTab4 = new OpTab(this, "SUPERSCUG");
            this.Tabs = new[] { opTab1, opTab2, opTab3, opTab4 }; // Add the tabs into your list of tabs. If there is only a single tab, it will not show the flap on the side because there is not need to.

            // Tab 1
            OpContainer tab1Container = new OpContainer(new Vector2(0, 0));
            opTab1.AddItems(tab1Container);
            // Tab 3
            OpContainer tab2Container = new OpContainer(new Vector2(0, 0));
            opTab2.AddItems(tab2Container);
            // Tab 3
            OpContainer tab3Container = new OpContainer(new Vector2(0, 0));
            opTab2.AddItems(tab3Container);
            // Tab 4
            OpContainer tab4Container = new OpContainer(new Vector2(0, 0));
            opTab4.AddItems(tab4Container);


            UIelement[] UIArrayElements = new UIelement[] // Labels in a fixed box size + alignment
            {
                new OpCheckBox(doublesurvCheckBox, 50, 500), // Try to make your boolean toggles as "positive is true" simple wording, I accidentally write double negative long sentences a lot for my boolean names.
                new OpCheckBox(doublemonkCheckBox, 50, 480),
                new OpCheckBox(doublehuntCheckBox, 50, 460),
                new OpCheckBox(doublegourCheckBox, 50, 440),
                new OpCheckBox(doubleartiCheckBox, 50, 420),
                new OpCheckBox(doublerufflesCheckBox, 50, 400),
                new OpCheckBox(doublespearCheckBox, 50, 380),
                new OpCheckBox(doublepancakeCheckBox, 50, 360),
                new OpCheckBox(doublesofanthielCheckBox, 50, 340),

                //new OpCheckBox(doubleScugpupCheckBox, 50, 300),
            };
            opTab1.AddItems(UIArrayElements);

            UIelement[] UIArrayElements2 = new UIelement[] //create an array of ui elements
            {
                new OpLabel(0f, 550f, "AmbiScug options", true),

                new OpLabel(80f, 500f, "Enable dual-wielding for Survivor", false),
                new OpLabel(80f, 480f, "Enable dual-wielding for Monk", false),
                new OpLabel(80f, 460f, "Enable dual-wielding for Hunter", false),
                new OpLabel(80f, 440f, "Enable dual-wielding for Gourmand", false),
                new OpLabel(80f, 420f, "Enable dual-wielding for Arti", false),
                new OpLabel(80f, 400f, "Enable dual-wielding for Ruffles", false),
                new OpLabel(80f, 380f, "Enable dual-wielding for Spearmaster", false),
                new OpLabel(80f, 360f, "Enable dual-wielding for Exploding Pancakes With Mind", false),
                new OpLabel(80f, 340f, "Enable dual-wielding for Painworld", false),
                //new OpLabel(80f, 300f, "Enable dual-wielding for Scugpups(or something)", false),
            };
            opTab1.AddItems(UIArrayElements2);


            UIelement[] UIArrayElements3 = new UIelement[] // Labels in a fixed box size + alignment
            {
                new OpCheckBox(karmaReinforceCheckBox, 50, 500), // Try to make your boolean toggles as "positive is true" simple wording, I accidentally write double negative long sentences a lot for my boolean names.
                new OpCheckBox(eegGenerationCheckBox, 50, 480),
            };
            opTab2.AddItems(UIArrayElements3);

            UIelement[] UIArrayElements4 = new UIelement[] // Labels in a fixed box size + alignment
            {
                new OpLabel(0f, 550f, "SupliScug Options", true),
                
                new OpLabel(80f, 500f, "Enable karma reinforcement using all food", false),
                new OpLabel(80f, 480f, "Enable eeg generation using ALL of Sofanthiel's food", false),
                new OpLabel(80f, 460f, "Enable being malnourished after generating eeg", false),
            };
            opTab2.AddItems(UIArrayElements4);


            UIelement[] UIArrayElements5 = new UIelement[] // Labels in a fixed box size + alignment
            {
                new OpCheckBox(artiSplodeCheckBox, 50, 500), // Try to make your boolean toggles as "positive is true" simple wording, I accidentally write double negative long sentences a lot for my boolean names.
                new OpCheckBox(skillIssueCheckBox, 50, 460),
                new OpSlider(skillIssueSlider, new Vector2(10, 10), 100, false),
                //new OpCheckBox(antiStunCheckBox, 50, 440),
            };
            opTab3.AddItems(UIArrayElements5);

            UIelement[] UIArrayElements6 = new UIelement[] //create an array of ui elements
            {
                new OpLabel(0f, 550f, "OtherScug options", true),

                new OpLabel(80f, 500f, "Enable Artificer exploding on death", false),
                //new OpLabel(80f, 480f, "Enable Hunter exploding on death", false),
                new OpLabel(80f, 460f, "Enable the thing that let's you take several spear hits cuz scavs are scavs, slider for how many spears your able to take", false),
                //new OpLabel(80f, 440f, "Enable Anti-Stun Protection(ASP)[does nothing right now]", false),
                //new OpLabel(80f, 420f, "", false),
                //new OpLabel(80f, 400f, "", false),
                //new OpLabel(80f, 380f, "", false),
                //new OpLabel(80f, 360f, "", false),
                //new OpLabel(80f, 340f, "", false),
            };
            opTab3.AddItems(UIArrayElements6);


            UIelement[] UIArrayElements7 = new UIelement[] // Labels in a fixed box size + alignment
            {
                new OpCheckBox(bulletTimesurvCheckBox, 50, 500), // Try to make your boolean toggles as "positive is true" simple wording, I accidentally write double negative long sentences a lot for my boolean names.
                new OpCheckBox(bulletTimemonkCheckBox, 50, 480),
                new OpCheckBox(bulletTimehuntCheckBox, 50, 460),
                new OpCheckBox(bulletTimegourCheckBox, 50, 440),
                new OpCheckBox(bulletTimeartiCheckBox, 50, 420),
                new OpCheckBox(bulletTimerufflesCheckBox, 50, 400),
                new OpCheckBox(bulletTimespearCheckBox, 50, 380),
                new OpCheckBox(bulletTimepancakeCheckBox, 50, 360),
                new OpCheckBox(bulletTimesofanthielCheckBox, 50, 340),
            };
            opTab4.AddItems(UIArrayElements7);

            UIelement[] UIArrayElements8 = new UIelement[] //create an array of ui elements
            {
                new OpLabel(0f, 550f, "SUPERSCUG options", true),
                new OpLabel(120f, 550f, "Press control for Bullet-Time", true),
                new OpLabel(120f, 500f, "These could be counted as cheating", true),

                new OpLabel(80f, 500f, "Enable Bullet-Time for Survivor", false),
                new OpLabel(80f, 480f, "Enable Bullet-Time for Monk", false),
                new OpLabel(80f, 460f, "Enable Bullet-Time for Hunter", false),
                new OpLabel(80f, 440f, "Enable Bullet-Time for Gourmand", false),
                new OpLabel(80f, 420f, "Enable Bullet-Time for Arti", false),
                new OpLabel(80f, 400f, "Enable Bullet-Time for Ruffles", false),
                new OpLabel(80f, 380f, "Enable Bullet-Time for Spearmaster", false),
                new OpLabel(80f, 360f, "Enable Bullet-Time for Exploding Pancakes With Mind", false),
                new OpLabel(80f, 340f, "Enable Bullet-Time for Painworld", false),
                //new OpLabel(80f, 300f, "Enable dual-wielding for Scugpups(or something)", false),
            };
            opTab4.AddItems(UIArrayElements8);
        }
        public override void Update()
        {
            base.Update();
            sprite1.rotation++;
            sprite2.rotation++;
            numberGoUp++;
            //AwriBlush.rotation = Mathf.Sin(numberGoUp / 10) * 15;
        }

        // Configurable values. They are bound to the config in constructor, and then passed to UI elements.
        // They will contain values set in the menu. And to fetch them in your code use their NAME.Value. For example to get the boolean testCheckBox.Value, to get the integer testSlider.Value
        //public readonly Configurable<TYPE> NAME;        
        public static Configurable<bool> doublesurvCheckBox;
        public static Configurable<bool> doublemonkCheckBox;
        public static Configurable<bool> doublehuntCheckBox;
        public static Configurable<bool> doublegourCheckBox;
        public static Configurable<bool> doubleartiCheckBox;
        public static Configurable<bool> doublerufflesCheckBox;
        public static Configurable<bool> doublespearCheckBox;
        public static Configurable<bool> doublepancakeCheckBox;
        public static Configurable<bool> doublesofanthielCheckBox;

        //public static Configurable<bool> doubleScugpupCheckBox;


        public static Configurable<bool> karmaReinforceCheckBox;
        public static Configurable<bool> eegGenerationCheckBox;
        public static Configurable<bool> eegGenerationMalnutritionCheckBox;


        public static Configurable<bool> artiSplodeCheckBox;
        public static Configurable<bool> hunterSplodeCheckBox;
        public static Configurable<bool> skillIssueCheckBox;
        public static Configurable<bool> antiStunCheckBox;
        public static Configurable<int> skillIssueSlider;


        public static Configurable<bool> bulletTimesurvCheckBox;
        public static Configurable<bool> bulletTimemonkCheckBox;
        public static Configurable<bool> bulletTimehuntCheckBox;
        public static Configurable<bool> bulletTimegourCheckBox;
        public static Configurable<bool> bulletTimeartiCheckBox;
        public static Configurable<bool> bulletTimerufflesCheckBox;
        public static Configurable<bool> bulletTimespearCheckBox;
        public static Configurable<bool> bulletTimepancakeCheckBox;
        public static Configurable<bool> bulletTimesofanthielCheckBox;
        float numberGoUp = 0;
        private FSprite sprite1 = new FSprite("Futile_White");
        private FSprite sprite2 = new FSprite("Futile_White");

    }
}
