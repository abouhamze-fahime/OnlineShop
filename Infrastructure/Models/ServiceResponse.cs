using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Models;

public class ServiceResponse<T>
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
    public T Data { get; set; }
    public int Total {  get; set; }//1469
    public int Page { get; set; }// 3
    public int PageCount   // Total /size => 73.45 => Math.Ceiling => 74
    { 
        get
        {
            if(Total == 0) return 0;
            return Convert.ToInt32(Math.Ceiling(Total / (double)Size));
        }
    }
    public int Size { get; set; }// 20
}
