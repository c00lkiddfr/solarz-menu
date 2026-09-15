using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using StupidTemplate.Notifications;
using StupidTemplate.Menu;
using StupidTemplate.Classes.Console;
using AdminConsole = StupidTemplate.Classes.Console.Console;

namespace StupidTemplate.Menu
{
    public static class SeralythMappings
    {
        // Reads generic input from ConsoleUIHelpers.ReadInput and executes a console command.
        // Input format: command,arg1,arg2,...
        // Arg parsing rules:
        //  - v(x,y,z) or x,y,z -> Vector3
        //  - q(x,y,z,w) -> Quaternion
        //  - true/false -> bool
        //  - integer numbers -> int (or long if large)
        //  - floating numbers -> float
        //  - otherwise treated as string

        public static void ExecuteFromInput()
        {
            string input = ConsoleUIHelpers.ReadInput();

            if (string.IsNullOrWhiteSpace(input))
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> No input found. Use Open Generic Input first.");
                return;
            }

            string[] parts = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 0)
            {
                NotifiLib.SendNotification("<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> Input format invalid.");
                return;
            }

            string command = parts[0].Trim();

            var args = new List<object>();

            for (int i = 1; i < parts.Length; i++)
            {
                string token = parts[i].Trim();
                object parsed = ParseToken(token);
                args.Add(parsed);
            }

            try
            {
                NotifiLib.SendNotification($"<color=grey>[</color><color=cyan>EXEC</color><color=grey>]</color> Executing {command} with {args.Count} arg(s)");

                if (args.Count == 0)
                    AdminConsole.ExecuteCommand(command, Photon.Realtime.ReceiverGroup.All);
                else
                    AdminConsole.ExecuteCommand(command, Photon.Realtime.ReceiverGroup.All, args.ToArray());
            }
            catch (Exception e)
            {
                NotifiLib.SendNotification($"<color=grey>[</color><color=red>ERROR</color><color=grey>]</color> Failed to execute: {e.Message}");
            }
        }

        private static object ParseToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return string.Empty;

            token = token.Trim();

            // Vector3 format: v(x,y,z) or x;y;z or x y z or x,y,z
            if ((token.StartsWith("v(", StringComparison.OrdinalIgnoreCase) && token.EndsWith(")")) || CountSeparators(token) >= 2)
            {
                string inner = token;
                if (inner.StartsWith("v(", StringComparison.OrdinalIgnoreCase) && inner.EndsWith(")"))
                    inner = inner.Substring(2, inner.Length - 3);

                var comps = inner.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (comps.Length >= 3 && float.TryParse(comps[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) && float.TryParse(comps[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) && float.TryParse(comps[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float z))
                {
                    return new Vector3(x, y, z);
                }
            }

            // Quaternion q(x,y,z,w)
            if (token.StartsWith("q(", StringComparison.OrdinalIgnoreCase) && token.EndsWith(")"))
            {
                string inner = token.Substring(2, token.Length - 3);
                var comps = inner.Split(new[] { ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
                if (comps.Length >= 4 && float.TryParse(comps[0], NumberStyles.Float, CultureInfo.InvariantCulture, out float x) && float.TryParse(comps[1], NumberStyles.Float, CultureInfo.InvariantCulture, out float y) && float.TryParse(comps[2], NumberStyles.Float, CultureInfo.InvariantCulture, out float z) && float.TryParse(comps[3], NumberStyles.Float, CultureInfo.InvariantCulture, out float w))
                {
                    return new Quaternion(x, y, z, w);
                }
            }

            // Boolean
            if (bool.TryParse(token, out bool b))
                return b;

            // Integer
            if (int.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out int iVal))
                return iVal;

            if (long.TryParse(token, NumberStyles.Integer, CultureInfo.InvariantCulture, out long lVal))
                return lVal;

            // Float
            if (float.TryParse(token, NumberStyles.Float, CultureInfo.InvariantCulture, out float fVal))
                return fVal;

            // Otherwise string
            return token;
        }

        private static int CountSeparators(string s)
        {
            int count = 0;
            foreach (char c in s)
            {
                if (c == ',' || c == ';' || c == ' ')
                    count++;
            }

            return count;
        }
    }
}
