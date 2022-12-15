using System.Text.RegularExpressions;

namespace PersonBuilder;

public class Person
{
    // firstname, lastname, age, phone, address

    private string _firstname = null!;
    private string _lastname = null!;
    private int? _age;
    private string? _phone;
    private string? _address;

    public string FirstName => _firstname;
    public string LastName => _lastname;
    public int? Age => _age;
    public string? Phone => _phone;
    public string? Address => _address;

    private Person(Builder builder)
    {
        _firstname = builder._firstname;
        _lastname = builder._lastname;
        _age = builder._age;
        _phone = builder._phone;
        _address = builder._address;
    }

    public class Builder
    {
        internal string _firstname = null!;
        internal string _lastname = null!;
        internal int? _age;
        internal string? _phone;
        internal string? _address;

        public Builder(string firstname, string lastname)
        {
            _firstname = firstname;
            _lastname = lastname;
        }

        public Builder WithAge(int age)
        {
            _age = age;
            return this;
        }

        public Builder WithPhone(string phone)
        {
            _phone = phone;
            return this;
        }

        public Builder WithAddress(string address)
        {
            _address = address;
            return this;
        }

        public override string ToString() => $"{_firstname} {_lastname} {_age} {_phone} {_address}";

        public Person Build()
        {
            var pers = new Person(this);
            if (pers._age > 115)
                throw new ArgumentException($"Age {pers._age} is too high for {pers._firstname} {pers._lastname}");

            if (pers._firstname.Length < 3)
                throw new ArgumentException($"Firstname {_firstname} is too short");
            
            if (!new Regex(@"\b\d{1,5}").Match(pers._address ??= String.Empty).Success)
                throw new ArgumentException($"HouseNR in address {_address} is not valid");
            
            if (!new Regex(@"\+\b\d{2,}").Match(pers._phone ??= String.Empty).Success)
                throw new ArgumentException($"Phone {_phone} is not valid");

            return pers;
        }
    }
}