using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Empolyeee
{
    public class Employee : INotifyPropertyChanged
    {
        public Employee() { }
        public Employee(int i, string n, float s)
        {
            id = i;
            name = n;
            salary = s;

        }
        private string name;
        private int id;
        private float salary;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            set
            {
                name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
            get => name;
        }
        public float Salary
        {
            set
            {
                salary = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Salary"));


            }
            get => salary;
        }
        public int Id
        {
            set
            {
                id = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Id"));
            }
            get => id;
        }
        public override string ToString()
        {
            return $"Name is {name} has id: {id} \t salary: {Salary}";

        }
    }
}
