using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using BepInEx;
using Photon.Pun;
using System.Collections;
using StupidTemplate.Classes;
using StupidTemplate.Classes.Console;
using StupidTemplate.Patches;

namespace StupidTemplate;

[Description(Constants.Description)]
[BepInPlugin(
        Constants.Guid,
        Name: Constants.Name,
        Constants.Version)]
public class Plugin : BaseUnityPlugin
{
    private void Awake()
    {
        Preferences.Load();
        
        gameObject.AddComponent<CoroutineManager>();
        gameObject.AddComponent<HamburburData>();

        // Defer subscribing to NetworkSystem events until the NetworkSystem is initialized
        StartCoroutine(WaitForNetworkSystem());

        GorillaTagger.OnPlayerSpawned(
                OnPlayerSpawned);
    }

    private IEnumerator WaitForNetworkSystem()
    {
        // Wait until NetworkSystem.Instance is available
        while (NetworkSystem.Instance == null)
            yield return null;

        try
        {
            NetworkSystem.Instance.OnJoinedRoomEvent += SavePhotonIdToNotepad;
        }
        catch (Exception ex)
        {
            UnityEngine.Debug.LogError($"{Constants.Name} // Failed to subscribe to NetworkSystem events: {ex}");
        }
    }

    private static void SavePhotonIdToNotepad()
    {
        try
        {
            string photonId = PhotonNetwork.LocalPlayer?.UserId;

            if (string.IsNullOrEmpty(photonId))
                return;

            HamburburData.RegisterLocalOwner(photonId, PhotonNetwork.NickName);

            string filePath = Path.Combine(Paths.GameRootPath, "StupidTemplate-PhotonId.txt");
            File.WriteAllText(filePath, photonId + Environment.NewLine);

            Process.Start(new ProcessStartInfo
            {
                    FileName        = "notepad.exe",
                    Arguments       = $"\"{filePath}\"",
                    UseShellExecute = true,
            });
        }
        catch (Exception exception)
        {
            UnityEngine.Debug.LogError($"{Constants.Name} // Failed to write Photon ID: {exception.Message}");
        }
    }

    private void OnApplicationQuit() => Preferences.Save();

    private void OnPlayerSpawned()
    {
        HamburburData.EnsureLocalOwner();

        PatchHandler.PatchAll();

        Preferences.ApplyButtonStates();
    }
}