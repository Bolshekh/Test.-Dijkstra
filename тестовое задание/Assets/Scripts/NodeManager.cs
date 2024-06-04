using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class NodeManager : MonoBehaviour
{
	[SerializeField] TMPro.TMP_Dropdown NodeFrom;
	[SerializeField] TMPro.TMP_Dropdown NodeTo;
	[SerializeField] TMPro.TMP_Text ResultText;
	List<Node> nodes = new List<Node>();

	// Start is called before the first frame update
	void Start()
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag("Node");
		this.nodes = nodes.Select(n=> n.GetComponent<Node>()).ToList();

		//������������
		NodeNaming(nodes);

		//���������� ���������� ��� ������
		UIUpdate(nodes.Select(x => x.GetComponent<Node>()).ToList());
	}
	public void FindPath()
	{
		//�����. ����� �� ����������� ������
		Node nodeFrom = nodes.Where(n => n.NodeName == NodeFrom.options[NodeFrom.value].text).First();
		Node nodeTo = nodes.Where(n => n.NodeName == NodeTo.options[NodeTo.value].text).First();
		
		//��� �����
		var path = this.GetComponent<PathFinder>().SearchPath(nodes, nodeFrom, nodeTo);

		//������ ����������
		Debug.Log(string.Join(" ", path.Item1.Select(n => n.NodeName + ":" + n.Weight)) + "\n" + string.Join(" -> ", path.Item2.Select(n => n.NodeName)));
		ResultText.text = $"���������� ���� � ����� {nodeTo.NodeName}: {path.Item1.Where(n=> n == nodeTo).First().Weight}\n\n�������:\n{string.Join(" -> ", path.Item2.Select(n => n.NodeName))}";
		
		//����� ��������
		ResultLineUpdate(path.Item2);
	}
	void NodeNaming(GameObject[] nodes)
	{
		for (int i = 0; i < nodes.Length; i++)
		{
			//���� ������� + ����� 0-5
			string name = ((char)(((i) / 5) + 65)).ToString() + ((i) % 5).ToString();
			nodes[i].GetComponent<Node>().NodeName = name;
		}
	}
	void ResultLineUpdate(List<Node> points)
	{
		var lr =this.GetComponent<LineRenderer>();

		lr.positionCount = points.Count;
		foreach (Node node in points)
		{
			var pos = node.gameObject.transform.position;
			lr.SetPosition(points.IndexOf(node), new Vector3(pos.x,pos.y + 0.5f, pos.z));
		}

		lr.enabled = true;
	}
	void UIUpdate(List<Node> nodes)
	{
		//�������
		NodeFrom.options.Clear();
		NodeTo.options.Clear();

		//����������
		foreach (Node node in nodes)
		{
			NodeFrom.options.Add(new TMPro.TMP_Dropdown.OptionData(node.NodeName));
			NodeTo.options.Add(new TMPro.TMP_Dropdown.OptionData(node.NodeName));
		}

		//��������� ��������. ����� �������� ����������� ����� ������
		NodeFrom.value = 1; NodeTo.value = 1;
		NodeFrom.value = 0; NodeTo.value = 0;
	}
}
