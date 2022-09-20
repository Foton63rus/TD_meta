using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class UTNode
    {
        [SerializeField]
        private UTNode _root;
        public UTNode Root
        {
            get => _root; 
            internal set => _root = value;
        }
        
        private List<UTNode> _parent;
        public List<UTNode> GetAllParents
        {
            get => _parent;
        }

        public UTNode GetParent(int i = 0)
        {
            return _parent[i];
        }

        public void SetParent(UTNode parentNode)
        {
            if (!_parent.Contains(parentNode))
            {
                _parent.Add(parentNode);
            }
            parentNode.SetChild(this);
        }

        public void DeleteParent(UTNode parentNode)
        {
            if (_parent.Contains(parentNode))
            {
                _parent.Remove(parentNode);
            }
        }
        
        private List<UTNode> _children;
        
        public List<UTNode> GetAllChildrens
        {
            get => _children;
        }

        public UTNode GetChildren(int i = 0)
        {
            return _children[i];
        }

        public void SetChild(UTNode childNode)
        {
            if (!_children.Contains(childNode))
            {
                _children.Add(childNode);
                childNode.SetParent(this);
            }
        }

        public void DeleteChild(UTNode parentNode)
        {
            if (_children.Contains(parentNode))
            {
                _children.Remove(parentNode);
            }
        }
        
        private bool isOpen;

        public bool IsOpen
        {
            get => isOpen;
            set => isOpen = value;
        }

        private string name;
        public string Name
        {
            get => name;
            set => name = value;
        }

        private string description;
        public string Description
        {
            get => description;
            set => description = value;
        }

        public override string ToString()
        {
            return $@"UTNode['{name}: parentscount: {_parent.Count}, childscount: {_children.Count}, isOpen: {IsOpen}']";
        }

        public UTNode()
        {
            _parent = new List<UTNode>();
            _children = new List<UTNode>();
        }
    }//class UTNode
}
