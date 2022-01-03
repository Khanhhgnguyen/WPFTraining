using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFTrainningCSharp.Model;

namespace WPFTrainningCSharp.ViewModel
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
            person.Add(new Person() { Name = "Khanh", Roll = "Customer" });
            person.Add(new Person() { Name = "Khanh Nguyen", Roll = "Customer" });
            person.Add(new Person() { Name = "Hoang Gia Khanh", Roll = "Customer" });
            person.Add(new Person() { Name = "Gia", Roll = "Sale" });
            person.Add(new Person() { Name = "Khanh", Roll = "Sale" });
            person.Add(new Person() { Name = "Hoang", Roll = "Sale" });
            person.Add(new Person() { Name = "Gia Khanh", Roll = "ShipTo" });
            person.Add(new Person() { Name = "Khanh Nguyen", Roll = "ShipTo" });
            person.Add(new Person() { Name = "Hoang Gia Khanh", Roll = "ShipTo" });
            person.Add(new Person() { Name = "Gia", Roll = "BillTo" });
            person.Add(new Person() { Name = "Khanh", Roll = "BillTo" });
            person.Add(new Person() { Name = "Hoang", Roll = "BillTo" });
            person.Add(new Person() { Name = "Hoang", Roll = "BillTo" });
            return person;
        }
    }
}
