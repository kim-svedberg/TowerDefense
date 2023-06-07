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
    /// <summary>
    /// This class represents a currency manager that handles the game's currency system.
    /// </summary>
    public class CurrencyManager
    {
        public int currentValue = 20;
        public bool purchased;

        /// <summary>
        /// This method increases the current currency value by the specified amount.
        /// </summary>
        public void GainCurrency(int value)
        {
            currentValue += value;
        }

        /// <summary>
        /// Attempts to purchase a tower by deducting its price from the current currency value. 
        /// If the player can afford the tower, the tower's price is subtracted from the current value, and the method returns true. 
        /// Otherwise, it returns false.
        /// </summary>
        public bool TryToPurchaseTower(Tower towerToPlace)
        {
            if (Affordable(towerToPlace)) 
            {
                currentValue -= towerToPlace.price;
                return purchased = true;
            }
            return purchased = false;
        }

        /// <summary>
        /// Checks if the player can afford to purchase a tower. 
        /// It compares the current currency value with the tower's price. 
        /// If the player has enough currency, it returns true; otherwise, it returns false.
        /// </summary>
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
