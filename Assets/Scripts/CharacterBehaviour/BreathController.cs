using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

using static MyUtils;

[RequireComponent(typeof(SkinnedMeshRenderer))]
public class BreathController : MonoBehaviour
{
  [Tooltip("Shortest possible time between breaths (in seconds).")]
  [Range(0.1f, 10.0f)]
  public float BreathingIntervalMin = 3.0f;

  [Tooltip("Longest possible time between breaths (in seconds).")]
  [Range(0.1f, 10.0f)]
  public float BreathingIntervalMax = 5.0f;

  [Tooltip("Duration of the inhale (in seconds).")]
  [Range(0.1f, 5.0f)]
  public float InhaleDuration = 3.0f;

  [Tooltip("Inhale shape key strength")]
  [Range(0.0f, 1.0f)]
  public float InhaleStrength = 0.3f;

  [Tooltip("Pause between inhale and exhale (in seconds).")]
  [Range(0.1f, 5.0f)]
  public float BreathHoldDuration = 0.5f;

  [Tooltip("Duration of the exnhale (in seconds).")]
  [Range(0.1f, 5.0f)]
  public float ExhaleDuration = 2.0f;

  [Tooltip("Exhale shape key strength: mouth")]
  [Range(0.0f, 1.0f)]
  public float ExhaleStrength = 0.03f;

  [Tooltip("Exhale shape key strength: jaw")]
  [Range(0.0f, 1.0f)]
  public float ExhaleJawStrength = 0.08f; // Suprisingly a lot of jaw movement TBH

  public AnimationCurve breathingInterpolationCurve = AnimationCurve.EaseInOut(0f, 0f, 0f, 0f);


  private string SK_BREATH_EXHALE_L = "MOUTH-btm_lip_out.L";
  private string SK_BREATH_EXHALE_R = "MOUTH-btm_lip_out.R";
  private string SK_BREATH_INHALE_R = "NASAL-flare.R";
  private string SK_BREATH_INHALE_L = "NASAL-flare.L";
  private string SK_BREATH_INHALE2_R = "NASAL-sneer.R";
  private string SK_BREATH_INHALE2_L = "NASAL-sneer.L";
  private string SK_BREATH_JAW = "jaw-down";

  private SkinnedMeshRenderer bodyMesh;

  void Start()
  {
    bodyMesh = GetComponent<SkinnedMeshRenderer>();

    StartCoroutine(BreathingCourotine());
  }

  ///  Nice name btw.

  public IEnumerator BreathingCourotine()
  {
    while (true)
    {
      var interval = UnityEngine.Random.Range(BreathingIntervalMin, BreathingIntervalMax);
      yield return MyUtils.wait(interval);

      // inhale: nose flares
      var keyframes = CalculateBreathingInterpolation(InhaleDuration);
      float dt = InhaleDuration / (float)keyframes.Length;
      foreach (float progress in keyframes)
      {
        ApplyBreathShapeKeys_Nostrils(progress);
        yield return MyUtils.wait(dt);
      }
      ClearBreathShapeKeys();

      yield return MyUtils.wait(BreathHoldDuration);

      // exhale
      keyframes = CalculateBreathingInterpolation(ExhaleDuration);
      dt = ExhaleDuration / (float)keyframes.Length;
      foreach (float progress in keyframes)
      {
        ApplyBreathShapeKeys_Mouth(progress);
        yield return MyUtils.wait(dt);
      }
      ClearBreathShapeKeys();
    }
  }

  private float[] CalculateBreathingInterpolation(float duration)
  {
    int steps = (int)Math.Ceiling(duration / Time.deltaTime) / 2;
    float dt = duration / (float)steps;

    Func<float, float> ff = x => 1.0f - Math.Abs(x * 2f - 1f);
    float[] keyframes = Enumerable.Range(1, steps - 1).Select(
      x => breathingInterpolationCurve.Evaluate(ff(x / (float)steps))
    // x => ff(x / (float)(steps))
    ).ToArray();
    return keyframes;
  }

  private void ApplyBreathShapeKeys_Nostrils(float progress)
  {
    var str = progress * InhaleStrength;
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_INHALE_L, str);
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_INHALE_R, str);
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_INHALE2_L, str);
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_INHALE2_R, str);
  }

  private void ApplyBreathShapeKeys_Mouth(float progress)
  {
    var str = progress * ExhaleStrength;
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_EXHALE_L, str);
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_EXHALE_R, str);
    SafeSetBlendShapeWeight(bodyMesh, SK_BREATH_JAW, ExhaleJawStrength * progress);
  }

  private void ClearBreathShapeKeys()
  {
    ApplyBreathShapeKeys_Mouth(0.0f);
    ApplyBreathShapeKeys_Nostrils(0.0f);
  }
}
