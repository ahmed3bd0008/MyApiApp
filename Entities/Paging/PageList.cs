using System;
using System.Collections.Generic;
using System.Linq;

namespace Entity.Paging
{
    public class PageList<T>:List<T>
    {
                public MetaData metaData { get; set; }
                public PageList(List<T>Item, int pageSize,int PageNumber,int Account)
                {
                    metaData=new MetaData()
                    {          
                                Account=Account,
                                TotalPages=(int)Math.Ceiling(Account/(double)pageSize),
                                CurrentPage=PageNumber,
                                PageSize=pageSize

                    };
                    this.AddRange(Item);
                }
                public static PageList<T>ToPageList(List<T>Source,int PageSize,int PageNumber)
                {
                            var Acount=Source.Count;
                            var item =Source.
                            Skip((PageNumber-1)*PageSize).
                            Take(PageSize).ToList();
                        return new PageList<T>(item,PageSize,PageNumber,Acount);
                }
                    
    }
    }
