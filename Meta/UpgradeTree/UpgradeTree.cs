using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class UpgradeTree
    {
        [SerializeField]
        private string root;
        public string Root
        {
            get => root;
            set
            {
                root = value;
            } 
        }

        public void SetRoot( UTNode node )
        {
            Root = node.Name;
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
                return tree.Find(x => x.Key == key)?.Value;
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

            if (root == null || root == "")
            {
                root = kvPair.Value.Name;
            }
        }

        public void AddNode(UTNode value)
        {
            AddNode( new UTreeKVPair( value) );
        }

        public void RemoveNode( string key)
        {
            tree.RemoveAll(x => x.Key == key);
            foreach (var pair in tree)
            {
                pair.Value.GetAllParents.RemoveAll(x => x == key);
                pair.Value.GetAllChildrens.RemoveAll(x => x == key);
            }
        }
        
        public void RemoveNode( UTNode node )
        {
            RemoveNode( node.Name );
        }

        public override string ToString()
        {
            return $"UTree with Root: {Root}";
        }
    }// class UpgradeTree
    
    [Serializable]
    public class UTreeKVPair
    {
        [SerializeField]
        public string Key;
        [SerializeField]
        public UTNode Value;

        public UTreeKVPair( UTNode node )
        {
            Key = node.Name;
            Value = node;
        }
    }//class UTreeKVPair
}
