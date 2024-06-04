using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Node : MonoBehaviour
{
	private string nName;
	public string NodeName
	{
		get => nName;
		set
		{
			nName = value;
			UpdateNodes();
		}
	}
	public List<Node> ConnectedNodes;
	public float Weight { get; set; }
	public bool IsSeenNode { get; set; }

	public Node PreviosNode { get; set; }

	[SerializeField] TMPro.TMP_Text nodeName;
	[SerializeField] GameObject lineRenderer;
	List<LineRenderer> lineRenderers = new List<LineRenderer>();

	private void Start()
	{
		UpdateNodes();
	}
	void UpdateNodes()
	{
		ClearLines();
		nodeName.text = NodeName;
		foreach (var node in ConnectedNodes)
		{
			LineRenderer lr = Instantiate(lineRenderer).GetComponent<LineRenderer>();
			lineRenderers.Add(lr);

			lr.positionCount = 2;
			lr.SetPosition(0, this.transform.position);
			lr.SetPosition(1, node.transform.position);
		}
	}
	void ClearLines()
	{
		foreach (var line in lineRenderers)
		{
			Destroy(line.gameObject);
		}
	}
	public float Distance(GameObject from)
	{
		return Vector3.Distance(this.transform.position, from.transform.position);
	}
}
