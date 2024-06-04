using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
	public Tuple<List<Node>, List<Node>> SearchPath(List<Node> nodes, Node From, Node To)
	{
		List<Node> nodeVector = nodes.Select(n => From).ToList();

		foreach (Node node in nodes) { node.Weight = 999999; node.IsSeenNode = false; }

		From.Weight = 0;
		From.IsSeenNode = true;

		var res = CountWeights(From, nodeVector, nodes);

		return new Tuple<List<Node>, List<Node>>(nodes, GetPath(From, To));
	}
	List<Node> CountWeights(Node From, List<Node> NodeVector, List<Node> OriginalNodes)
	{
		foreach (Node node in From.ConnectedNodes.OrderBy(cn => cn.Distance(From.gameObject)).Where(n => !n.IsSeenNode))
		{
			float weight = node.Distance(From.gameObject) + From.Weight;
			if (node.Weight > weight)
			{
				node.Weight = weight;
				NodeVector[OriginalNodes.IndexOf(node)] = node;
				node.PreviosNode = From;
			}
		}

		foreach (Node node in From.ConnectedNodes.OrderBy(cn => cn.Distance(From.gameObject)).Where(n => !n.IsSeenNode))
		{
			node.IsSeenNode = true;
			CountWeights(node, NodeVector, OriginalNodes);
		}

		return NodeVector;
	}

	List<Node> GetPath(Node Start, Node End)
	{
		List<Node> nodes = new List<Node>();
		nodes.Add(End);
		while (Start != End)
		{
			End = End.PreviosNode;
			nodes.Add(End);
		}
		return nodes.Reverse<Node>().ToList();
	}
}
