using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    // https://stackoverflow.com/a/2012855
    delegate void TreeVisitor(Node node);
    abstract class Node
    {
        private List<Node> children = new List<Node>();
        public Node()
        {

        }

        public void AddChild(Node node)
        {
            children.Add(node);
        }

        public void AddChildren(params Node[] node)
        {
            foreach (Node n in node)
            {
                children.Add(n);
            }
        }

        public List<Node> GetChildren()
        {
            return children;
        }
        public void Traverse(Node node, TreeVisitor visitor)
        {
            visitor(node);
            foreach (Node kid in node.children)
                Traverse(kid, visitor);
        }
    }
}
