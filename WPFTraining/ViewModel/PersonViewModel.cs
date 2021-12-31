using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTraining.Model;

namespace WPFTraining.ViewModel
{
    public class PersonViewModel
    {
        public ObservableCollection<Person> people;
        public ObservableCollection<Person> People { get; set; }

        public PersonViewModel()
        {
            People = new ObservableCollection<Person>(getPerson());
        }
        public static ObservableCollection<Person> getPerson()
        {
            var person = new ObservableCollection<Person>();
            person.Add(new Person() { Name = "Alice", Roll = "Customer" });
            person.Add(new Person() { Name = "Naruto", Roll = "Customer" });
            person.Add(new Person() { Name = "Luffy", Roll = "Customer" });
            person.Add(new Person() { Name = "Nami", Roll = "Sale" });
            person.Add(new Person() { Name = "Khanh", Roll = "Sale" });
            person.Add(new Person() { Name = "Hoang", Roll = "Sale" });
            return person;
        }
    }
}
