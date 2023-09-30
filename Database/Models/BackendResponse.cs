using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Models;

public class BackendResponse
{
    public Fridge Fridge {  get; set; }
    public User User { get; set; }
    public ProductFridge Product { get; set; }
    public List<ProductFridge> Products { get; set; }
    public ErrorMessage Error { get; set; }
}

public class ErrorMessage
{
    public string Message { get; set; }
}
