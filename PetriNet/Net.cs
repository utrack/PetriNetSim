﻿using System.Collections.Generic;
using System.Linq;
using PetriNet.Entity;

namespace PetriNet
{
    /// <summary>
    /// This class is used as primitive simulation of user-defined Petri Net.
    /// </summary>
    public class Net
    {
        public event DMarkFound MarkFound;
        public delegate void DMarkFound(Dictionary<string, int> marking, Dictionary<string, int> parentMarking);

        private readonly Dictionary<string, Node> _nodes = new Dictionary<string, Node>();
        private readonly Dictionary<string, Transition> _transitions = new Dictionary<string, Transition>(); 


        private readonly Dictionary<string, int> _defMarking = new Dictionary<string, int>();
        private readonly Dictionary<string, int> _maxMarks = new Dictionary<string, int>(); 
        private Tree.Node _startNode;
        private readonly List<Dictionary<string, int>> _foundMarkings = new List<Dictionary<string, int>>();
        public Net(string file)
        {
            var dFile = new FLDataFile.DataFile(file);


            foreach (var set in dFile.GetSections("Node"))
            {
                var name = set.GetFirstOf("name")[0];
                var newNode = new Node(name);
                _defMarking[name] = int.Parse(set.GetFirstOf("tokens")[0]);
                _maxMarks[name] = _defMarking[name];
                _nodes.Add(name,newNode);
            }




            foreach (var name in dFile.GetSections("Transition").Select(sec => sec.GetFirstOf("name")[0]))
            {
                _transitions.Add(name, new Transition(name));
            }

            foreach (var set in dFile.GetSettings("NodeToTransition", "connection"))
            {
                _nodes[set[0]].AddOutput(_transitions[set[1]]);
                _transitions[set[1]].AddInput(_nodes[set[0]]);
            }

            foreach (var set in dFile.GetSettings("TransitionToNode", "connection"))
            {
                _transitions[set[0]].AddOutput(_nodes[set[1]]);
                _nodes[set[1]].AddInput(_transitions[set[0]]);
            }

        }


        public void Traverse()
        {
            _startNode = new Tree.Node(_defMarking, "Root");
            Traverse(new Dictionary<string, int>(_defMarking),_startNode);
        }

        private void Traverse(Dictionary<string, int> marking, Tree.Node tNode)
        {

            if (MarkFound != null)
                MarkFound(marking, tNode.Marking);

            if (StateExists(marking)) return;
            _foundMarkings.Add(marking);

            foreach (var newMarking in from trans in _transitions.Values 
                                       where trans.ReadyForTransition(marking) 
                                       select new { Mark = trans.Transit(marking), netNode = trans})
            {
                SetMaxMarks(newMarking.Mark);
                var node = new Tree.Node(newMarking.Mark, newMarking.netNode.Name);
                tNode.Children.Add(node);
                
                Traverse(newMarking.Mark,node);
            }
        }

        public bool StateExists(Dictionary<string, int> state)
        {
            return _foundMarkings.Any(dict => MarkingDict.Compare(dict, state));
        }

        private void SetMaxMarks(Dictionary<string, int> marking)
        {
            foreach (var val in marking.Where(val => val.Value > _maxMarks[val.Key]))
            {
                _maxMarks[val.Key] = val.Value;
            }
        }
    }
}
