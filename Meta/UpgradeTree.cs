using System;
using System.Collections.Generic;

namespace TowerDefence
{
    [Serializable]
    public class UpgradeTree
    {
        private UTNode root;
        public UTNode Root
        {
            get => root;
            set
            {
                root = value;
                tree.ForEach( x => x.Value.Root = value);
            } 
        }
        
        public List<UTreeKVPair> tree = new List<UTreeKVPair>();
        
        public UTNode this[int i]
        {
            get => tree[i].Value;
        }

        public UTNode this[string key]
        {
            get
            {
                UTreeKVPair pair = tree.Find(x => x.Key == key);
                return pair == null ? null : pair.Value;
            }
        }

        public int NodesCount
        {
            get => tree.Count;
        }

        public void AddNode( UTreeKVPair kvPair)
        {
            UTreeKVPair pair = tree.Find(x => x.Key == kvPair.Key);
            if (pair == null)
            {
                tree.Add( kvPair );
            }
        }

        public void AddNode(string key, UTNode value)
        {
            AddNode( new UTreeKVPair( key, value) );
        }

        public void RemoveNode( string key)
        {
            tree.RemoveAll(x => x.Key == key);
        }
    }// class UpgradeTree
    
    [Serializable]
    public class UTNode
    {
        private UTNode _root;
        public UTNode Root
        {
            get => _root; 
            internal set => _root = value;
        }
        
        private List<UTNode> _parent = new List<UTNode>();
        public List<UTNode> GetAllParents
        {
            get => _parent;
        }
        public UTNode GetParent(int i = 0) { return _parent[i]; }

        public void SetParent(UTNode parentNode)
        {
            if (!_parent.Contains(parentNode))
            {
                _parent.Add(parentNode);
            }
        }
        
        public void DeleteParent(UTNode parentNode)
        {
            if (_parent.Contains(parentNode))
            {
                _parent.Remove(parentNode);
            }
        }
        
        private List<UTNode> _children;
        
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
    }//class UTNode
    
    [Serializable]
    public class UTreeKVPair
    {
        public string Key;
        public UTNode Value;

        public UTreeKVPair(string key, UTNode value)
        {
            Key = key;
            Value = value;
        }
    }//class UTreeKVPair
}
