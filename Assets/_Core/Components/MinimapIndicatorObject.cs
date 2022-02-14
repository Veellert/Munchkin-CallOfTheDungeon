using UnityEngine;

public class MinimapIndicatorObject : MonoBehaviour
{
    [Header("Indicator Type")]
    [SerializeField] private eMinimapIndicator _indicator;
    public eMinimapIndicator Indicator { get => _indicator; private set => _indicator = value; }
}
