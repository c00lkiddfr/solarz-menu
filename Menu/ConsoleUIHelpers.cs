using System;
using System.IO;
using BepInEx;
using Photon.Pun;
using UnityEngine;
using StupidTemplate.Notifications;

namespace StupidTemplate.Menu
{
    public static class ConsoleUIHelpers
    {
        private static readonly string InputFileName = "StupidTemplate-ConsoleInput.txt";

        public static string InputFilePath => Path.Combine(Paths.GameRootPath, InputFileName);

        public static void OpenInputFile(string prompt)
        {
            try
            {
                File.WriteAllText(InputFilePath, "# Enter the value(s) for the next console action below:\r\n# " + prompt + "\r\n\r\n");

                ProcessStart("notepad.exe", InputFilePath);

                NotifiLib.SendNotification("<color=grey>[</color><color=cyan>INPUT</color><color=grey>]</color> " + prompt + " - open the file and enter the value, then press Confirm Input in the Console menu.");
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to open input file: {e}");
            }
        }

        public static string ReadInput()
        {
            try
            {
                if (!File.Exists(InputFilePath))
                    return string.Empty;

                string text = File.ReadAllText(InputFilePath);

                // Skip commented lines
                using StringReader sr = new(text);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("#"))
                        continue;

                    return line.Trim();
                }

                return string.Empty;
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to read input file: {e}");
                return string.Empty;
            }
        }

        public static void ShowPlayerListNotification()
        {
            try
            {
                var players = PhotonNetwork.PlayerList;

                if (players == null || players.Length == 0)
                {
                    NotifiLib.SendNotification("<color=grey>[</color><color=cyan>PLAYERS</color><color=grey>]</color> No players found.");
                    return;
                }

                string message = "<color=grey>[</color><color=cyan>PLAYERS</color><color=grey>]</color> Player list:\r\n";

                foreach (var p in players)
                {
                    message += $"{p.NickName} => {p.UserId}\r\n";
                }

                NotifiLib.SendNotification(message);
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to show player list: {e}");
            }
        }

        private static void ProcessStart(string fileName, string args)
        {
            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = fileName,
                    Arguments = $"\"{args}\"",
                    UseShellExecute = true,
                });
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to start process {fileName}: {e}");
            }
        }
    }
}
