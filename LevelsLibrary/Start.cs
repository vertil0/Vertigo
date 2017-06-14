﻿using Otter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LevelsLibrary
{
    public class StartScene : Scene
    {
        public StartScene() : base()
        {
            Add(new MenuText());
        }
    }
    class Logo : Entity
    {
        Image img = new Image("Resources\\Logo.png");
        public Logo()
        {
            img.Alpha = 0;
            AddGraphic(img);
        }
        bool logo_vision = false;
        public override void Update()
        {
            base.Update();
            if (logo_vision == false)
            {
                img.Alpha += 0.01f;
                if (img.Alpha == 1)
                {
                    logo_vision = true;
                    System.Threading.Thread.Sleep(1000);
                }
            }
            else if (logo_vision == true)
            {
                img.Alpha -= 0.01f;
                if (img.Alpha == 0)
                {
                    Scene.Add(new MenuText());
                    RemoveSelf();
                };
            }
        }
    }
    class MenuText : Entity
    {
        RichText text1, text2;
        Image point = Image.CreateCircle(5, Color.Red);
        Image vertigo_name = new Image("Resources\\Vertrigo.png");

        public MenuText() : base()
        {
            this.Layer = -10000;
            text1 = new RichText("{waveRateY:10}{waveAmpY:1}Start!", 20);
            text2 = new RichText("{waveRateY:7}{waveAmpY:1}Exit", 20);
            text1.Color = Color.Green;
            text2.Color = Color.Black;
            text1.CenterOrigin();
            text2.CenterOrigin();
            point.CenterOrigin();
            vertigo_name.CenterOrigin();
            AddGraphic(vertigo_name, Game.Instance.HalfWidth, 40);
            AddGraphic(text1, Game.Instance.HalfWidth, Game.Instance.HalfHeight - 20+20);
            AddGraphic(text2, Game.Instance.HalfWidth, Game.Instance.HalfHeight + 20+20);
            AddGraphic(point, text1.Left - 10, text1.Y + 4);
        }


        int choose = 1;
        
        public override void Update()
        {
            base.Update();
           

            if (Input.KeyPressed(Key.W) || Input.KeyPressed(Key.S))
            {
                if (choose == 1)
                {
                    RemoveGraphic(point);
                    AddGraphic(point, text2.Left - 10, text2.Y + 4);
                    choose = 2;
                }
                else if (choose == 2)
                {
                    RemoveGraphic(point);
                    AddGraphic(point, text1.Left - 10, text1.Y + 4);
                    choose = 1;
                }
            }

            if (Input.KeyPressed(Key.Return))
            {
                if (choose == 1)
                {
                    Game.SwitchScene(Global.GetCurrentLevel());
                }
                else if (choose == 2)
                {
                    Game.Close();
                }
            }

        }
    }

}