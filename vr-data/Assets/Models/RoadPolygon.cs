using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Helpers;
using UnityEngine;

namespace Assets
{
    public enum RoadType
    {
        Highway,
        MajorRoad,
        MinorRoad,
        Rail,
        Path
    }

    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer), typeof(LineRenderer))]
    internal class RoadPolygon : MonoBehaviour
    {
        private Tile _tile;
        public string Id { get; set; }
        public RoadType Type { get; set; }
        private List<Vector3> _verts;

        public void Initialize(string id, Tile tile, List<Vector3> verts, string halfWidth)
        {
            Id = id;
            _tile = tile;
            Type = halfWidth.ToRoadType();
            _verts = verts;

            LineRenderer lineRenderer = gameObject.GetComponent<LineRenderer>();
            lineRenderer.material = new Material(Shader.Find("Particles/Additive"));
            lineRenderer.SetColors(Color.blue, Color.blue);
            lineRenderer.SetVertexCount(_verts.Count);
        }

        private void Update()
        {
            DrawLines();
        }

        private void DrawLines()
        {
            float roadWidth = Extensions.ToWidthFloat(Type) * _tile.transform.lossyScale.x;
            Transform worldTransform = GameObject.Find("World").transform;

            float worldRotationRadians = worldTransform.rotation.eulerAngles.y * Mathf.Deg2Rad;

            LineRenderer lineRenderer = GetComponent<LineRenderer>();
            lineRenderer.SetWidth(roadWidth, roadWidth);

            for (int i = 0; i < _verts.Count; i++)
            {
                Vector3 preparedVertex = _verts[i];
                preparedVertex = RotateVectorXZAroundPointXZ(preparedVertex, worldRotationRadians);
                preparedVertex.Scale(_tile.transform.lossyScale);
                preparedVertex.y = -0.01f;
                preparedVertex += _tile.transform.parent.position;
                
                lineRenderer.SetPosition(i, preparedVertex);
            }

        }

        // this assumes the local transform is always rotating around 0,0,0. Should be okay because roads are in tiles.
        private Vector3 RotateVectorXZAroundPointXZ(Vector3 vector, float rotationInRadians)
        {
            Vector3 resultantVector = new Vector3();
            rotationInRadians *= -1;

            resultantVector.x = (vector.x * Mathf.Cos(rotationInRadians)) - (vector.z * Mathf.Sin(rotationInRadians));
            resultantVector.z = (vector.x * Mathf.Sin(rotationInRadians)) + (vector.z * Mathf.Cos(rotationInRadians));
            resultantVector.y = vector.y;

            return resultantVector;
        }

    }
}
