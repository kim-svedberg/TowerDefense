using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.BitmapFonts;
using MonoGame.Extended.TextureAtlases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CurrencyManager = TowerDefense.Currencies.CurrencyManager;

namespace TowerDefense.UI
{
    /// <summary>
    /// Responsible for drawing the tower menu UI as well as the coin UI. 
    /// </summary>
    internal class TowerMenu
    {
        Vector2 menuPos;
        Vector2 textPos;
        Vector2 coinPos;

        CurrencyManager currencyManager;

        public TowerMenu(CurrencyManager currencyManager)
        {
            this.currencyManager = currencyManager;
            menuPos = new Vector2(0, 0);
            textPos = new Vector2(80, 150);
            coinPos = new Vector2(20, 163);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(AssetManager.towerMenuTex, menuPos, Color.White);
            spriteBatch.Draw(AssetManager.coinTex, coinPos, Color.White);
            spriteBatch.DrawString(AssetManager.font, "" + currencyManager.currentValue, textPos, Color.Gold);

        }
    }
}
