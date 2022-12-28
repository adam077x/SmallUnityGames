using UnityEngine;

public static class Utils
{

    public static float AngleBetweenPoints(Vector2 start, Vector2 target)
    {
        return Mathf.Atan2(start.y - target.y, start.x - target.x) * Mathf.Rad2Deg;
    }

    public static float GetRandom360Angle()
    {
        float angle = Random.Range(0, 360);

        return angle;
    }

    public static Vector2 GetRandomPosition(Vector2 startPosition, float distance)
    {
        float angle = GetRandom360Angle();

        float rads = angle * Mathf.Rad2Deg;

        Vector2 position = new Vector2(Mathf.Cos(rads), Mathf.Sin(rads)) * distance + startPosition;

        return position;
    }
}
