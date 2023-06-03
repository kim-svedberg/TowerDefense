using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.TextureAtlases;
using Spline;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using TowerDefense.Bullets;
using TowerDefense.Enemies;
using TowerDefense.Particles;
using TowerDefense.Towers;
using TowerDefense.UI;
using TowerDefense.Currencies;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace TowerDefense.Currencies
{
    public class CurrencyManager
    {
        public int currentValue = 20;
        public bool purchased;

        public void GainCurrency(int value)
        {
            currentValue += value;
        }

        public bool TryToPurchaseTower(Tower towerToPlace)
        {
            if (Affordable(towerToPlace)) //if you can afford it
            {
                currentValue -= towerToPlace.price;
                return purchased = true;
            }
            return purchased = false;
        }

        public bool Affordable(Tower towerToPlace)
        {
            if(currentValue >= towerToPlace.price)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
