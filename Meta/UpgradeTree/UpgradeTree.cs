using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

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

            if (root == null)
            {
                root = kvPair.Value;
            }
        }

        public void AddNode(UTNode value)
        {
            AddNode( new UTreeKVPair( value) );
        }

        public void RemoveNode( string key)
        {
            tree.RemoveAll(x => x.Key == key);
            CleanTreeByKey(key);
        }
        
        public void RemoveNode( UTNode node )
        {
            string key = node.Name;
            tree.RemoveAll(x => x.Key == node.Name);
            CleanTreeByKey( key );
        }

        private void CleanTreeByKey( string key)
        {
            foreach (var pair in tree)
            {
                pair.Value.GetAllParents.RemoveAll(x => x.Name == key);
                pair.Value.GetAllChildrens.RemoveAll(x => x.Name == key);
            }
        }

        public override string ToString()
        {
            return $@"UTree with Root: {Root}; childscount: {Root.GetAllChildrens.Count}";
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
