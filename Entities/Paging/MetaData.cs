namespace Entity.Paging
{
public class MetaData
{
      public int CurrentPage { get; set; }
      public int PageSize { get; set; }
      public int Account { get; set; }
      public int TotalPages { get; set; }
      public bool HasPrivouseValue
      {
          get { return CurrentPage>1; }
      }
      public bool HasNextValue
      {
          get { return CurrentPage<TotalPages; }
      }

            
}
    
}