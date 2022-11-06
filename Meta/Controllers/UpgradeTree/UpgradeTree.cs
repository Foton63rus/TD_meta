using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class UpgradeTree
    {
        [SerializeField]
        private int upgradePoints;
        
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
            }
        }
        
        public void RemoveNode( UTNode node )
        {
            RemoveNode( node.Name );
        }

        public void AddPoints(int count)
        {
            upgradePoints += count;
        }

        public void OpenNode( string nodeName)
        {
            UTNode node = tree.Find(x => x.Key == nodeName)?.Value;

            if (node == null)
            {
                //Debug.Log("Не нашлось ноды с тким именем");
                return;
            }

            if (node.IsOpen)
            {
                //Debug.Log("Такой апгрейд уже есть");
                return;
            }

            bool allParentsIsOpen = true;
            if (node.GetAllParents.Count > 0)
            {
                try
                {
                    allParentsIsOpen = node.GetAllParents.TrueForAll
                        (x => tree.Find(y => y.Key == x).Value.IsOpen);
                    //Debug.Log($"allParentsIsOpen: {allParentsIsOpen}");
                }
                catch (Exception e)
                {
                    allParentsIsOpen = false;
                    //Debug.Log($"OpenNode exception: ");
                    return;
                }
            }
            
            if (upgradePoints >= node.Cost && allParentsIsOpen)
            {
                upgradePoints -= node.Cost;
                node.IsOpen = true;
                //Debug.Log($"node {node} is open: {node.IsOpen}");
            }
        }

        public override string ToString()
        {
            return $"UTree with Root: {Root}, {tree}";
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
