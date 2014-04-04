using System.Collections.Generic;
using System.Linq;

namespace PetriNet.Entity
{
    class Transition
    {
        public readonly List<Node> Inputs = new List<Node>();
        public readonly List<Node> Outputs = new List<Node>();
        public string Name { get; set; }
        public Transition()
        {
            
        }

        public Transition(string name)
        {
            Name = name;
        }

        public bool ReadyForTransition(Dictionary<string, int> marking)
        {
            return Inputs.All(inp => inp.HasToken(marking));
        }

        public Dictionary<string, int> Transit(Dictionary<string, int> marking)
        {
            var ret = new Dictionary<string, int>(marking);
            ret = Inputs.Aggregate(ret, (current, inp) => inp.GetToken(current));

            return Outputs.Aggregate(ret, (current, outp) => outp.SetTokens(current, 1));
        }


        public void AddInput(Node trans)
        {
            Inputs.Add(trans);
        }


        public void AddOutput(Node trans)
        {
            Outputs.Add(trans);
        }

    }
}
