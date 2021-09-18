using System;

namespace Entity.DataTransferObject
{
    public class EmployeeDto
{
        public Guid Id { get; set; } 
        public string Name { get; set; } 
        public int Age { get; set; } public 
        string Position { get; set; }    
}
    public class AddEmployeeDto
{
        public string Name { get; set; } 
        public int Age { get; set; } public 
        string Position { get; set; }    
}
 public class UpdateEmployeeDto
{
        public string Name { get; set; } 
        public int Age { get; set; } public 
        string Position { get; set; }    
}
}