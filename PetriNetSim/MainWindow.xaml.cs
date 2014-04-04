using System;
using System.Collections.Generic;
using PetriNet;

namespace PetriNetSim
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var net = new Net(@"g:\sampleticker.petrini");
            net.MarkFound += net_MarkFound;
            net.Traverse();
        }

        static void net_MarkFound(Dictionary<string, int> marking, Dictionary<string, int> parentMarking)
        {
            Console.Write(String.Join(", ", marking.Values));
            //throw new NotImplementedException();
        }
    }
}
