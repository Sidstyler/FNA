﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace GameEngine
{
    static class Global
    {
        public static Game1 game;
        public static Random random = new Random();
        public static string levelName = "level";

        public static void Initialize(Game1 inputGame)
        {
            game = inputGame;
        }
    }
}
