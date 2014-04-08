using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using PetriNet.Annotations;

namespace PetriNet.Tree
{
    class Node: INotifyPropertyChanged
    {
        public string TransitName { get; set; }
        private ObservableCollection<Node> _children;
        public Dictionary<string, int> Marking;

        public ObservableCollection<Node> Children
        {
            get { return _children; }
            set
            {
                _children = value;
                OnPropertyChanged("Children");
            }
        }


        public Node(Dictionary<string, int> marking, string trName)
        {
            Marking = marking;
            TransitName = trName;
            _children = new ObservableCollection<Node>();
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
