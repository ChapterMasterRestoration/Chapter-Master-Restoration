using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChapterMaster.Tree
{
    // https://stackoverflow.com/a/2012855
    public delegate void TreeVisitor(Node node);
    public abstract class Node
    {
        public Node Parent;
        public bool Emancipated = false;
        private List<Node> children = new List<Node>();
        public Node()
        {

        }

        public Node AddChild(Node node)
        {
            node.Parent = this;
            children.Add(node);
            return this;
        }

        public Node AddChildren(params Node[] node)
        {
            foreach (Node n in node)
            {
                n.Parent = this;
                children.Add(n);
            }
            return this;
        }

        public List<Node> GetChildren()
        {
            return children;
        }
        
        public int GetNumberOfChildren()
        {
            return children.Count;
        }
        public void Traverse(Node node, TreeVisitor visitor)
        {
            visitor(node);
            for (int kid = 0; kid < node.GetNumberOfChildren(); kid++)
                Traverse(node.GetChildren()[kid], visitor);
        }

    }
}
