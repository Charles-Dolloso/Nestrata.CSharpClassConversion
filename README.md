This project is to convert C# Class Definition to Typescript Class.

Here's the sample request: (**FYI: Make sure to follow this format and as you notice string is in one line since console application can't accept new line as input**)
public class PersonDto { public string Name { get; set; } public int Age { get; set; } public string Gender { get; set; } public long? DriverLicenceNumber { get; set; } public List<Address> Addresses { get; set; } public class Address { public int StreetNumber { get; set; } public string StreetName { get; set; } public string Suburb { get; set; } public int PostCode { get; set; } } }
