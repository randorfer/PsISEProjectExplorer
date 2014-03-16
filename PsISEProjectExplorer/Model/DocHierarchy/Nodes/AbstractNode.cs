﻿using PsISEProjectExplorer.EnumsAndOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PsISEProjectExplorer.Model.DocHierarchy.Nodes
{
    public abstract class AbstractNode : INode
    {
        public abstract NodeType NodeType { get; }
        public virtual string Path { get; private set; }
        public virtual string Name { get; private set; }
        public INode Parent { get; private set; }
        public ICollection<INode> Children { get; private set; }

        // less = will be before other nodes
        public virtual int OrderValue { get { return 0; } }

        

        public AbstractNode(string path, string name, INode parent)
        {
            if (path == null) {
                throw new ArgumentNullException("path");
            }
            this.Path = path;
            this.Name = name;
            this.Parent = parent;
            this.Children = new SortedSet<INode>(DefaultNodeComparer.NODE_COMPARER);
            if (this.Parent != null)
            {
                this.Parent.Children.Add(this);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType() || !(obj is INode))
            {
                return false;
            }
            INode node = (INode)obj;
            return (node.Path == this.Path);
        }

        public override int GetHashCode()
        {
            return this.Path.GetHashCode();
        }

    }
}