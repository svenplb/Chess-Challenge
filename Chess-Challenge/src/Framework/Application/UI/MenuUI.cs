﻿using Raylib_cs;
using System.Numerics;
using System;
using System.IO;

namespace ChessChallenge.Application
{
    public static class MenuUI
    {
        public static void DrawButtons(ChallengeController controller)
        {
            Vector2 buttonPos = UIHelper.Scale(new Vector2(260, 210));
            Vector2 buttonSize = UIHelper.Scale(new Vector2(260, 55));
            float spacing = buttonSize.Y * 1.2f;
            float breakSpacing = spacing * 0.6f;

            // Game Buttons
            if (NextButtonInRow("Human vs AACE", ref buttonPos, spacing, buttonSize))
            {
                var whiteType = controller.HumanWasWhiteLastGame ? ChallengeController.PlayerType.AACE : ChallengeController.PlayerType.Human;
                var blackType = !controller.HumanWasWhiteLastGame ? ChallengeController.PlayerType.AACE : ChallengeController.PlayerType.Human;
                controller.StartNewGame(whiteType, blackType);
            }
            if (NextButtonInRow("AACE vs AACE", ref buttonPos, spacing, buttonSize))
            {
                controller.StartNewBotMatch(ChallengeController.PlayerType.AACE, ChallengeController.PlayerType.AACE);
            }
            if (NextButtonInRow("(n) AACE vs (o) AACE", ref buttonPos, spacing, buttonSize))
            {
                controller.StartNewBotMatch(ChallengeController.PlayerType.AACE, ChallengeController.PlayerType.old_AACE);
            }

            // Page buttons
            buttonPos.Y += breakSpacing;

            if (NextButtonInRow("Save Games", ref buttonPos, spacing, buttonSize))
            {
                string pgns = controller.AllPGNs;
                string directoryPath = Path.Combine(FileHelper.AppDataPath, "Games");
                Directory.CreateDirectory(directoryPath);
                string fileName = FileHelper.GetUniqueFileName(directoryPath, "games", ".txt");
                string fullPath = Path.Combine(directoryPath, fileName);
                File.WriteAllText(fullPath, pgns);
                ConsoleHelper.Log("Saved games to " + fullPath, false, ConsoleColor.Blue);
            }
            if (NextButtonInRow("POWER", ref buttonPos, spacing, buttonSize))
            {
                FileHelper.OpenUrl("https://seblague.github.io/chess-coding-challenge/documentation/");
            }

            // Window and quit buttons
            buttonPos.Y += breakSpacing;

            bool isBigWindow = Raylib.GetScreenWidth() > Settings.ScreenSizeSmall.X;
            string windowButtonName = isBigWindow ? "small" : "BIG BIG";
            if (NextButtonInRow(windowButtonName, ref buttonPos, spacing, buttonSize))
            {
                Program.SetWindowSize(isBigWindow ? Settings.ScreenSizeSmall : Settings.ScreenSizeBig);
            }
            if (NextButtonInRow("Exit (ESC)", ref buttonPos, spacing, buttonSize))
            {
                Environment.Exit(0);
            }

            bool NextButtonInRow(string name, ref Vector2 pos, float spacingY, Vector2 size)
            {
                bool pressed = UIHelper.Button(name, pos, size);
                pos.Y += spacingY;
                return pressed;
            }
        }
    }
}