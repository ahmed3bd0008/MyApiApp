namespace Entity.Paging
{
    public abstract class RequestPrameter
    {
        const int MaxPageSize=50;
        public int PageNumber { get; set; }
        private int _PageSize=10;
        public int PageSize
        {
            get { return  _PageSize; }
            set
             {
                         _PageSize = (value>MaxPageSize)?MaxPageSize:value; 
             }
        }
        public string OrderString { get; set; }

    }
    public class EmployeePrameter:RequestPrameter
    {
        public EmployeePrameter()
        {
            OrderString = "Name";
        }
        public string  Name { get; set; }
        public int MinAge { get; set; }=int.MinValue;
        public int MaxAge { get; set; }=int.MaxValue;
        public string Fields { get; set; }
        public bool IsIvalidAge
        { 
            get
            {
                 {
                     return MinAge > MaxAge;
                 }
            }
        }

    }
    public class CompanyPrameter : RequestPrameter
    {
        public CompanyPrameter()
        {
            OrderString = "Name";
        }
    }
}