﻿using EnaiumToolKit.Framework.Screen;
using EnaiumToolKit.Framework.Screen.Elements;
using Microsoft.Xna.Framework;
using StardewValley;
using StardewValley.Characters;
using StardewValley.Monsters;

namespace TeleportNPC.Framework.Gui
{
    public class TeleportNpcScreen : ScreenGui
    {
        public TeleportNpcScreen()
        {
            foreach (var npc in Utility.getAllCharacters())
            {
                CharacterType? type = GetCharacterType(npc);
                if (type == null || npc?.currentLocation == null)
                    continue;
                Point tile = npc.getTileLocationPoint();

                AddElement(new Button(
                    $"{ModEntry.GetInstance().Helper.Translation.Get("button.teleport")}{npc.displayName}",
                    $"{ModEntry.GetInstance().Helper.Translation.Get("button.teleport")}{npc.displayName}")
                {
                    OnLeftClicked = () => { Teleport(npc.currentLocation.Name, tile.X, tile.Y); }
                });
            }
        }

        private void Teleport(string locationName, int tileX, int tileY)
        {
            Game1.exitActiveMenu();
            Game1.player.swimming.Value = false;
            Game1.player.changeOutOfSwimSuit();
            Game1.warpFarmer(locationName, tileX, tileY, false);
        }

        private CharacterType? GetCharacterType(NPC npc)
        {
            if (npc is Monster)
                return null;
            if (npc is Horse)
                return CharacterType.Horse;
            if (npc is Pet)
                return CharacterType.Pet;
            return CharacterType.Villager;
        }
    }

    public enum CharacterType
    {
        Player = 1,

        Horse = 2,

        Pet = 3,

        Villager = 4
    }
}