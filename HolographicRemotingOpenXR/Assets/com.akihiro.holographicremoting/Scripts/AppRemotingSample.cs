using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;

namespace Microsoft.MixedReality.OpenXR.BasicSample
{
    public class AppRemotingSample : MonoBehaviour
    {
        [SerializeField] private InputField textInput = null;
        [SerializeField] private Text outputText = null;
        [SerializeField] private Button connectButton = null;
        [SerializeField] private Button disconnectButton = null;
        [SerializeField] private Remoting.RemotingConfiguration remotingConfiguration = new Remoting.RemotingConfiguration { RemotePort = 8265, MaxBitrateKbps = 20000 };

        private void Awake()
        {
            // Editor実行時はEditor側のRemoting機能を利用
            if (Application.isEditor)
            {
                gameObject.SetActive(false);
                textInput.gameObject.SetActive(false);
                connectButton.gameObject.SetActive(false);
                return;
            }
            // 既にHolographicRemoting実行中か確認
            var XRDisplaySubsystems = new List<XRDisplaySubsystem>();
            SubsystemManager.GetInstances(XRDisplaySubsystems);
            foreach (XRDisplaySubsystem xrDisplaySubsystem in XRDisplaySubsystems)
            {
                if (xrDisplaySubsystem.running)
                {
                    var connectionValid = Remoting.AppRemoting.TryGetConnectionState(out Remoting.ConnectionState connectionState, out Remoting.DisconnectReason disconnectReason);
                    if (!connectionValid || connectionState == Remoting.ConnectionState.Disconnected)
                    {
                        gameObject.SetActive(false);
                    }
                    textInput.gameObject.SetActive(false);
                    connectButton.gameObject.SetActive(false);
                    return;
                }
            }
            // UI設定
            connectButton.onClick.AddListener(() => StartCoroutine(Remoting.AppRemoting.Connect(remotingConfiguration)));
            disconnectButton.onClick.AddListener(() => Remoting.AppRemoting.Disconnect());
            textInput.onValueChanged.AddListener(value => remotingConfiguration.RemoteHostName = value);
        }

        private void Update()
        {
            var ip = $"{remotingConfiguration.RemoteHostName}:{remotingConfiguration.RemotePort}";
            textInput.gameObject.SetActive(true);
            connectButton.gameObject.SetActive(true);
            if (Remoting.AppRemoting.TryGetConnectionState(out Remoting.ConnectionState connectionState, out Remoting.DisconnectReason disconnectReason))
            {
                switch (connectionState)
                {
                    case Remoting.ConnectionState.Connected:
                        textInput.gameObject.SetActive(false);
                        connectButton.gameObject.SetActive(false);
                        outputText.text = $"Connected to {ip}.";
                        break;
                    case Remoting.ConnectionState.Connecting:
                        outputText.text = $"Connecting to {ip}...";
                        break;
                    case Remoting.ConnectionState.Disconnected:
                        outputText.text = $"Disconnected to {ip}. Reason is {disconnectReason}";
                        break;
                }
            }
            else
            {
                outputText.text = $"Disconnected to {ip}. Reason is {disconnectReason}";
            }
        }
    }
}