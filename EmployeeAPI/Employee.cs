using MongoDB.Bson;
using System.Text.Json.Serialization;

namespace EmployeeAPI
{
    public class Employee
    {
        [JsonIgnore]
        public ObjectId _id { get; set; }
        public string? Name { get; set; }

        public string? Department { get; set; }

        public int Salary { get; set; }

    }
}
