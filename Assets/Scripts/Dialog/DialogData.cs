using System.Collections.Generic;
using UnityEngine;

namespace Ekonomika.Dialog
{
    [System.Serializable]
    public struct Person
    {
        public string name
        {
            get
            {
                return _name;
            }
        }
        
        [SerializeField]
        private string _name;
    }

    public struct Replica
    {
        public Person person { get; }
        public string text { get; }
        
        public Replica(Person person, string text)
        {
            this.person = person;
            this.text = text;
        }
    }

    [CreateAssetMenu(fileName = "New Dialog Data", menuName = "Create DialogData")]
    public class DialogData : ScriptableObject
    {
        public int NumOfReplicas
        {
            get
            {
                return dialog.Count;
            }
        }

        [SerializeField]
        [System.Serializable]
        private struct _Replica
        {
            public int personId;
            [Space]
            [TextArea(3, 5)]
            public string text;
        }

        [SerializeField]
        private List<Person> people;

        [SerializeField]
        private List<_Replica> dialog;

        public Replica GetReplica(int id)
        {
            return new Replica(people[dialog[id].personId], dialog[id].text);
        }
    }
}
