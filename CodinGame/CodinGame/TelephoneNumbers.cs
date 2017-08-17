using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;

/**
 * Auto-generated code below aims at helping you parse
 * the standard input according to the problem statement.
 **/
class TelephoneNumbers
{
    class Node
    {
        public char Value { get; set; }
        public List<Node> Nodes { get; private set; }

        public Node(char value)
        {
            Value = value;
            Nodes = new List<Node>();
        }

        public Node GetNode(char value)
        {
            Node node = null;

            if (Nodes.Count > 0)
            {
                node = Nodes.FirstOrDefault(n => n.Value.Equals(value));
            }

            if (node == null)
            {
                node = new Node(value);
                Nodes.Add(node);
            }

            return node;
        }

        public int GetNumberOfNodes()
        {
            int numberOfNodes = Nodes.Count;
            foreach (Node node in Nodes)
            {
                numberOfNodes += node.GetNumberOfNodes();
            }

            return numberOfNodes;
        }
    }




    static void Main(string[] args)
    {
        Node baseNode = new Node('b');
        Node currentNode = null;
        int N = int.Parse(Console.ReadLine());
        for (int i = 0; i < N; i++)
        {
            string telephone = Console.ReadLine();
            currentNode = baseNode;
            foreach (char number in telephone)
            {
                currentNode = currentNode.GetNode(number);
            }
        }


        // The number of elements (referencing a number) stored in the structure.
        Console.WriteLine(baseNode.GetNumberOfNodes());
    }

    static void Log(string message, params object[] pars)
    {
        Console.Error.WriteLine(string.Format(message, pars));
    }
}