using System;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence
{
    [Serializable]
    public class UTNode
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private string description;

        [SerializeField]
        private string image;

        [SerializeField]
        private int cost;

        [SerializeField]
        private bool isOpen;

        [SerializeField]
        private List<string> _parent;

        public List<string> GetAllParents
        {
            get => _parent;
        }

        public string GetParent(int i = 0)
        {
            return _parent[i];
        }

        private void setParent(string parentName)
        {
            _parent.Add(parentName);
        }

        public void SetParent(UTNode parentNode)
        {
            if (!_parent.Contains(parentNode.Name))
            {
                setParent(parentNode.Name);
            }
        }

        public void DeleteParent(UTNode parentNode)
        {
            if (_parent.Contains(parentNode.Name))
            {
                _parent.Remove(parentNode.Name);
            }
        }

        public string Name
        {
            get => name;
            set => name = value;
        }

        public string Description
        {
            get => description;
            set => description = value;
        }

        public int Cost
        {
            get => cost;
            internal set => cost = value;
        }

        public bool IsOpen
        {
            get => isOpen;
            set => isOpen = value;
        }

        public override string ToString()
        {
            return $@"UTNode: {name}";
        }

        public UTNode()
        {
            _parent = new List<string>();
        }
    }//class UTNode
}
