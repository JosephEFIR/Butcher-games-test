using System.Collections.Generic;
using UnityEngine;

namespace Project.Scripts.Level
{
    public class Path
    {
        private readonly List<Vector3> _points;
        private readonly List<float> _cumulative;

        public float TotalLength { get; }
        public bool IsFinished(float distance) => distance >= TotalLength;

        public Path(List<Vector3> points)
        {
            _points = points;
            _cumulative = new List<float> { 0f };

            for (int i = 1; i < points.Count; i++)
            {
                _cumulative.Add(_cumulative[i - 1] + Vector3.Distance(points[i - 1], points[i]));
            }

            TotalLength = _cumulative[_cumulative.Count - 1];
        }

        public Vector3 GetPoint(float distance)
        {
            distance = Mathf.Clamp(distance, 0f, TotalLength);
            for (int i = 1; i < _points.Count; i++)
            {
                if (distance <= _cumulative[i])
                {
                    float segLen = _cumulative[i] - _cumulative[i - 1];
                    float t = segLen > 0.0001f ? (distance - _cumulative[i - 1]) / segLen : 0f;
                    return Vector3.Lerp(_points[i - 1], _points[i], t);
                }
            }
            return _points[_points.Count - 1];
        }

        public Vector3 GetDirection(float distance)
        {
            distance = Mathf.Clamp(distance, 0f, TotalLength);
            for (int i = 1; i < _points.Count; i++)
            {
                if (distance <= _cumulative[i])
                {
                    Vector3 d = _points[i] - _points[i - 1];
                    d.y = 0f;
                    return d.normalized;
                }
            }
            Vector3 last = _points[_points.Count - 1] - _points[_points.Count - 2];
            last.y = 0f;
            return last.normalized;
        }
    }
}