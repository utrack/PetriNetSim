using System.Collections.Generic;

namespace PetriNet.Entity
{
    class Node
    {

        private readonly List<Transition> _inputs = new List<Transition>();
        private readonly List<Transition> _outputs = new List<Transition>();
        
        public string Name { get; set; }

        public Node()
        {
            
        }

        public Node(string name)
        {
            Name = name;
        }

        public bool HasToken(Dictionary<string, int> marking)
        {
            return marking[Name] > 0;
        }

        public bool HasToken(Dictionary<string, int> marking, int num)
        {
            return marking[Name] >= num;
        }


        public Dictionary<string, int> GetToken(Dictionary<string, int> marking)
        {
            marking[Name]--;
            return marking;
        }

        public Dictionary<string, int> SetTokens(Dictionary<string, int> marking, int increment)
        {
            marking[Name] += increment;
            return marking;
        }

        public void AddInput(Transition trans)
        {
            _inputs.Add(trans);
        }


        public void AddOutput(Transition trans)
        {
            _outputs.Add(trans);
        }

    }
}
