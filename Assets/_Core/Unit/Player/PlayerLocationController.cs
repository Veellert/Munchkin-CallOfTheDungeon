using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��������� ���������� �� ������������ ������ �� ��������
/// </summary>
public class PlayerLocationController : MonoBehaviour
{
    [SerializeField] private eLocations _currentLocation;

    /// <summary>
    /// ��������� ��������� �� ������ �������
    /// </summary>
    public void MoveToNextLocation()
    {
        int nextLoc = (int)_currentLocation + 1;

        if (nextLoc < System.Enum.GetValues(typeof(eLocations)).Length)
            SetLocation(nextLoc);
    }

    /// <summary>
    /// ������������� ������� �� ������
    /// </summary>
    /// <param name="location">����� �������</param>
    private void SetLocation(int location)
    {
        _currentLocation = (eLocations)location;

        switch (_currentLocation)
        {
            case eLocations.Lobby: SceneLoader.Lobby(); break;
            case eLocations.Level1: SceneLoader.FirstLevel(); break;
            case eLocations.Boss1: SceneLoader.FirstBoss(); break;
            default: SceneLoader.Lobby(); break;
        }
    }
}

/// <summary>
/// ������ �������
/// </summary>
public enum eLocations
{
    Lobby,
    Level1,
    Boss1,
}