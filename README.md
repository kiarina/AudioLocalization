# AudioLocalization

Unreal Engine 5.8のThird Person C++ project上で、アバター頭部へ追従する
HRTF Listenerの左右PCMから、変化音が現在のカメラ正面に対して左・右の
どちらで鳴ったかを推定する実験環境です。

## 現在できること

- Resonance Audio `BINAURAL_HIGH`でmono音源をbinaural stereoへ変換
- Main Output Submixから48 kHzの左右PCMを常時ring bufferへ取得
- 30 ms window、15 ms hopのRMS onset検出
- 相互相関のlagと耳間レベル差（ILD）による波形だけからの左右推定
- Third Person Pawnのview locationとcamera yawへAudio Listenerを追従
- 10個の音源へ接触して0.75秒間隔のchirp再生を個別にON/OFF
- 音源状態をOFF＝青、ON＝赤＋拡大で表示
- HUD左下・右下へ左右波形、RMS、lag、ILD、相関、推定結果を表示
- 合成virtual microphone信号を使うAutomation Test

## 必要環境

- Unreal Engine 5.8
- C++ build toolchain（macOSではXcode）
- Resonance Audio plugin（UE 5.8同梱）

macOS arm64、UE 5.8.0で検証しています。UE 5.8.3でもC++ build、Automation Test、での起動（10音源とResonance Audio Listenerの初期化）を確認しています。`.uproject`では研究時に使用した
Unreal MCP関連pluginも有効です。MCPを使用しない場合でも、通常のEditor操作とPIEは可能です。

## 起動とビルド

`AudioLocalization.uproject`をUE 5.8で開き、C++ moduleのbuild後に
`Lvl_ThirdPerson`をPIEで実行します。macOSでcommand line buildする例です。

```sh
export UE_ROOT=/path/to/UE_5.8

"$UE_ROOT/Engine/Build/BatchFiles/Mac/Build.sh" \
  AudioLocalizationEditor Mac Development \
  "$PWD/AudioLocalization.uproject" -WaitMutex -NoHotReloadFromIDE
```

Third Personの標準操作で歩き、浮いている球のtriggerへ接触します。青い球は停止中、
赤く拡大した球は反復再生中です。HUDにはHRTF後の左右波形と現在の推定値が表示されます。

## 構成

- `AAudioLocalizationPulseSource`: procedural chirp、HRTF spatialization、接触toggle
- `AAudioLocalizationExperiment`: Listener追従、Submix capture、ring buffer、onsetと左右推定
- `FAudioLocalizationSubmixListener`: audio render threadからのPCM取得
- `AAudioLocalizationHUD`: 左右波形と推定指標の描画
- `AudioLocalization.Signal.VirtualMicrophoneSide`: 波形判定のAutomation Test

左右の球は耳位置を理解するための可視化です。入力は独立した2個のworld-space microphoneで
録音したものではなく、頭部中心のHRTF Listenerが生成したbinaural L/R出力です。
判定器には音源座標を渡さず、座標はground truthの計算だけに使用します。

## 検証結果

- 短時間HRTF試験: 10方位×5回、50/50正解
- continuous onset: 10/10検出、miss 0、false positive 0
- 頭部Listener固定時のcontinuous左右判定: 9/10
- Pawnを100 cm移動するとListener位置と各固定音源の相対方位が変化
- 接触toggle: OFF → ON → OFFを確認
- 音源表示: per-source Dynamic Material Instanceで青 → 赤 → 青を確認
- Automation Test: 1/1成功

詳細な条件、失敗した試行、JSON結果、MCP操作記録は
[labsの研究記録](https://github.com/kiarina/labs/tree/main/2026/07/23/unreal-audio-localization)
に保存しています。

## 次の研究

現在は実験基盤の構築までです。次は専用Submixによる対象world audioの分離と、
複数音源・連続背景音・銃声や大声のような突発音を組み合わせた評価へ進みます。
同時音源数、SNR、遮蔽、反射・残響、距離、camera回転速度を独立条件として、
event recall/precisionとside accuracyを分けて測定する必要があります。

## Contentについて

このrepositoryにはprojectをそのまま開くため、Unreal Engine Third Person template由来の
Contentが含まれます。Epic Games提供Contentの利用にはUnreal Engineの該当ライセンス条件が
適用されます。このrepositoryを公開すること自体は、当該Contentへ別のライセンスを付与する
ものではありません。
