
namespace KlassiRahaHaldamine.Models
{
    public class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Amount { get; set; }
        public string ContactName { get; set; }
        public string ContactNumber { get; set; }

        public Student() { }

        public Student(string firstName, string lastName, decimal amount, string contactName, string contactNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            Amount = amount;
            ContactName = contactName;
            ContactNumber = contactNumber;
        }

        //public bool IsContactNumberValid()
        //{
        //    return !string.IsNullOrEmpty(ContactNumber) && ContactNumber.Length >= 7;
        //}

        public override string ToString()
        {
            return $"{FirstName} {LastName}, Contact: {ContactName}, Phone: {ContactNumber}, Amount: {Amount}";

        }
    }
}
