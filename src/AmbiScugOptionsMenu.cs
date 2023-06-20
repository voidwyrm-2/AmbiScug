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
            survCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleSurvCheckbox", false); // All of these are where the game saves your settings, the important part is the "Key" field, make sure this will never conflict with another mod's key by having a prefix, like your mod name
            monkCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleMonkCheckbox", false);
            huntCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleHuntCheckbox", false);
            gourCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleGourCheckbox", false);
            artiCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleArtiCheckbox", false);
            rufflesCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleRufflesCheckbox", false);
            spearCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleSpearCheckbox", false);
            pancakeCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoublePancakeCheckbox", false);
            sofanthielCheckBox = this.config.Bind<bool>("AmbiScug_Bool_DoubleSofanthielCheckbox", false);
        }
        public override void Initialize()
        {
            var opTab1 = new OpTab(this, "Main");
            //var opTab2 = new OpTab(this, "Extra Config");
            this.Tabs = new[] { opTab1/*, opTab2 */}; // Add the tabs into your list of tabs. If there is only a single tab, it will not show the flap on the side because there is not need to.

            // Tab 1
            OpContainer tab1Container = new OpContainer(new Vector2(0, 0));
            opTab1.AddItems(tab1Container);
            /*
            for (int i = 0; i <= 600; i += 10) // Line grid to help align things, don't leave this in your final code. Almost every element starts from bottom-left.
            {
                Color c;
                c = Color.grey;
                if (i % 50 == 0) { c = Color.yellow; }
                if (i % 100 == 0) { c = Color.red; }
                FSprite lineSprite = new FSprite("pixel");
                lineSprite.color = c;
                lineSprite.alpha = 0.2f;
                lineSprite.SetAnchor(new Vector2(0.5f, 0f));
                Vector2 a = new Vector2(i, 0);
                lineSprite.SetPosition(a);
                Vector2 b = new Vector2(i, 600);
                float rot = Custom.VecToDeg(Custom.DirVec(a, b));
                lineSprite.rotation = rot;
                lineSprite.scaleX = 2f;
                lineSprite.scaleY = Custom.Dist(a, b);
                tab1Container.container.AddChild(lineSprite);
                a = new Vector2(0, i);
                b = new Vector2(600, i);
                lineSprite = new FSprite("pixel");
                lineSprite.color = c;
                lineSprite.alpha = 0.2f;
                lineSprite.SetAnchor(new Vector2(0.5f, 0f));
                lineSprite.SetPosition(a);
                rot = Custom.VecToDeg(Custom.DirVec(a, b));
                lineSprite.rotation = rot;
                lineSprite.scaleX = 2f;
                lineSprite.scaleY = Custom.Dist(a, b);
                tab1Container.container.AddChild(lineSprite);
            }
            */
            // You can put sprites with effects in the Remix Menu by using an OpContainer
            //sprite1 = new FSprite("Futile_White") { x = 500, y = 350, width = 100, height = 100 };
            //sprite2 = new FSprite("Futile_White") { x = 500, y = 250, width = 100, height = 100 };
            //sprite1.shader = Custom.rainWorld.Shaders["SmokeTrail"];
            //sprite2.shader = Custom.rainWorld.Shaders["GhostDistortion"];
            //tab1Container.container.AddChild(sprite1);
            //tab1Container.container.AddChild(sprite2);

            int sizeTest = 100;
            UIelement[] UIArrayElements = new UIelement[] // Labels in a fixed box size + alignment
            {
                new OpCheckBox(survCheckBox, 50, 500), // Try to make your boolean toggles as "positive is true" simple wording, I accidentally write double negative long sentences a lot for my boolean names.
                new OpCheckBox(monkCheckBox, 50, 480),
                new OpCheckBox(huntCheckBox, 50, 460),
                new OpCheckBox(gourCheckBox, 50, 440),
                new OpCheckBox(artiCheckBox, 50, 420),
                new OpCheckBox(rufflesCheckBox, 50, 400),
                new OpCheckBox(spearCheckBox, 50, 380),
                new OpCheckBox(pancakeCheckBox, 50, 360),
                new OpCheckBox(sofanthielCheckBox, 50, 340),
            };
            opTab1.AddItems(UIArrayElements);

            //OpRadioButtonGroup testRadioGroup = new OpRadioButtonGroup(testRadio); // Radio buttons needs to be created through a Radio button group which must be added to the canvas before adding its buttons.
            //opTab1.AddItems(testRadioGroup);
            //testRadioGroup.SetButtons(new OpRadioButton[]
            //{
            //    new OpRadioButton(50, 450){description = "test radio button 1, there can only be one"},
            //    new OpRadioButton(100, 450){description = "test radio button 2, there can only be one"}
            //});

            UIelement[] UIArrayElements2 = new UIelement[] //create an array of ui elements
            {
                new OpLabel(0f, 550f, "AmbiScug options", true),
                //new OpFloatSlider(testFloatSlider, new Vector2(85, 245), 200, 2, true){max = 1000, min = 100, hideLabel = false},
                //new OpLabelLong(new Vector2(25, 185), new Vector2(200,50), "Awri Lynn's awesomeness level\n and coolness factor slider", true),
                //new OpLabelLong(new Vector2(100,0),new Vector2(400,100), "Remix Menu Template examples is entirely just a menu mod to help modders make their own Remix menu, using dnSpy to look into the mod's code or downloading it from the wiki / Community.\n Try out the menu widgets then take the parts that you want from the code knowing that they will work", true),
                // Not adding a OpScrollBox, it's just a canvas to add more elements in less space, basically a pocket tab
                // Also not adding an OpSimpleImageButton as it's just a simple button with an OpImage stuck to it, has problems of both at once lmao

                new OpLabel(70f, 500f, "Enable dual-wielding for Survivor", false),
                new OpLabel(70f, 480f, "Enable dual-wielding for Monk", false),
                new OpLabel(70f, 460f, "Enable dual-wielding for Hunter", false),
                new OpLabel(70f, 440f, "Enable dual-wielding for Gourmand", false),
                new OpLabel(70f, 420f, "Enable dual-wielding for Arti", false),
                new OpLabel(70f, 400f, "Enable dual-wielding for Ruffles", false),
                new OpLabel(70f, 380f, "Enable dual-wielding for Spearmaster", false),
                new OpLabel(70f, 360f, "Enable dual-wielding for Exploding Pancakes With Mind", false),
                new OpLabel(70f, 340f, "Enable dual-wielding for Painworld", false),
            };
            opTab1.AddItems(UIArrayElements2);

            //OpContainer containerTab2 = new OpContainer(new Vector2(0, 0));
            //opTab2.AddItems(containerTab2);
            //FSprite AwriBG = new FSprite("atlases/NP/Awri_BG") { x = 300, y = 300, width = 600, height = 600 };
            //FSprite AwriRainbow = new FSprite("atlases/NP/Awri_Hat_Badge_MadeByPeskm_112") { x = 200, y = 456, width = 112, height = 112 };
            //FSprite AwriMagic = new FSprite("atlases/NP/Awri_real_magic_stuff_112") { x = 100, y = 500, width = 112, height = 112 };
            //AwriBlush = new FSprite("atlases/NP/AwriBlush_112") { x = 400, y = 344, width = 112, height = 112 };
            //FSprite AwriHappy = new FSprite("atlases/NP/AwriHappy_112") { x = 200, y = 344, width = 112, height = 112 };
            //containerTab2.container.AddChild(AwriBG);
            //containerTab2.container.AddChild(AwriRainbow);
            //containerTab2.container.AddChild(AwriMagic);
            //containerTab2.container.AddChild(AwriBlush);
            //containerTab2.container.AddChild(AwriHappy);
            /*
            UIArrayElements2 = new UIelement[]
            {
                    
            };
            */
            //opTab2.AddItems(UIArrayElements2);
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
        public static Configurable<bool> survCheckBox;
        public static  Configurable<bool> monkCheckBox;
        public static  Configurable<bool> huntCheckBox;
        public static Configurable<bool> gourCheckBox;
        public static Configurable<bool> artiCheckBox;
        public static Configurable<bool> rufflesCheckBox;
        public static Configurable<bool> spearCheckBox;
        public static Configurable<bool> pancakeCheckBox;
        public static Configurable<bool> sofanthielCheckBox;
        float numberGoUp = 0;
        //FSprite AwriBlush;
        private FSprite sprite1 = new FSprite("Futile_White");
        private FSprite sprite2 = new FSprite("Futile_White");

    }
}
