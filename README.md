# HolographicRemotingOpenXR
- Unity 2020.3.35f1
- MixedRealityToolkit 2.8.0

# MRTK導入
- MixedRealityFeatureToolから以下をUnityプロジェクトにインポート
    - Mixed Reality Toolkit Foundation 2.8.0
    - Mixed Reality Toolkit Standardassets 2.8.0
    - Mixed Reality OpenXR Plugin 1.4.1

- OpenXRのインポートに失敗する場合には`MixedRealityFeatureTool`のキャッシュを削除して再ダウンロード

- MRTKの設定からOpenXRを選択
    ![alt](./document/01.png)

# Unityプロジェクト設定
- `Build Serrings` の `Platform` は `Windows Standalone` を選択
- `Player Settings` の `XR Plug-in Management` を以下のように設定
    ![alt](./document/setting1.png)
    ![alt](./document/setting2.png)

# Unity Scene設定
- HoloLens 2アプリと同様に `MixedRealityToolkit`のシーン設定を行う
- HolographicRemoting用に `HolographicRemoting` プレハブをシーンに追加
- ![alt](./document/02.png)

# UPM
- https
    - `git+https://github.com/akihiro0105/HolographicRemotingOpenXR.git?path=/HolographicRemotingOpenXR/Assets/com.akihiro.holographicremoting/`

or

- ssh
    - `git+ssh://git@github.com/akihiro0105/HolographicRemotingOpenXR.git?path=/HolographicRemotingOpenXR/Assets/com.akihiro.holographicremoting/`