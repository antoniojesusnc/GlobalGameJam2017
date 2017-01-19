using UnityEngine;
using UnityEditor;


[CreateAssetMenu(menuName = "Wind/New Wind Properties")]
public class WindProperties : ScriptableObject {

    [Header("Normal Properties")]
    public AnimationCurve strenghtCurve;
    public float timeBetweenWaves;
    public float timeBlowing;
    public float maxStrength;


}
