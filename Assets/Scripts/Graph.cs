using UnityEngine;
using UnityEngine.UIElements;

public class Graph : MonoBehaviour
{
	[SerializeField]
	Transform pointPrefab;

	[SerializeField, Range(10, 100)]
	int resolution = 10;

	[SerializeField]
	FunctionLibrary.FunctionName function;

	Transform[] points;

	void Awake()
	{
		points = new Transform[resolution * resolution];

		float step = 2f / resolution;
		Vector3 Scale = Vector3.one * step;

		for (int i = 0; i < points.Length; i++)
		{
			Transform point = points[i] = Instantiate(pointPrefab);
			point.localScale = Scale;
			point.SetParent(transform, false);
		}
	}
	void Update()
	{
		FunctionLibrary.Function f = FunctionLibrary.GetFunction(function);  // Basically pointer function
		float step = 2f / resolution;


		float time = Time.time;
		float v = 0.5f * step - 1f;
		for (int i = 0, x = 0, z = 0; i < points.Length; i++, x++)
		{
			if (x == resolution)
			{
				x = 0;
				z++;
				v = (z + 0.5f) * step - 1f;
			}
			float u = (x + 0.5f) * step - 1f;
			points[i].localPosition = f(u, v, time);
		}
	}
}
