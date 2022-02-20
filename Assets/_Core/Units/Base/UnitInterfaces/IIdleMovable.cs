/// <summary>
/// Может ходить в спокойном состоянии
/// </summary>
public interface IIdleMovable
{
    /// <summary>
    /// Остановиться на точке
    /// </summary>
    void StayOnPoint();
    /// <summary>
    /// Удалить точку
    /// </summary>
    void RemovePoint();
    /// <summary>
    /// Инициализировать точку
    /// </summary>
    void InitPoint();
}
