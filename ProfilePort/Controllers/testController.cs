using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProfilePort.Controllers
{
    public class testController : Controller
    {
        // GET: test
        public ActionResult Index()
        {
            return View();
        }
    }
}

//A static class is basically the same as a non-static class, but there is one difference: a static class cannot be instantiated. In other words, you cannot use the new keyword to create a variable of the class type. Because there is no instance variable, you access the members of a static class by using the class name itself. For example, if you have a static class that is named UtilityClass that has a public method named MethodA, you call the method as shown in the following example: UtilityClass.MethodA();
//A static class can be used as a convenient container for sets of methods that just operate on input parameters and do not have to get or set any internal instance fields. For example, in the .NET Framework Class Library, the static System.Math class contains methods that perform mathematical operations, without any requirement to store or retrieve data that is unique to a particular instance of the Math class. That is, you apply the members of the class by specifying the class name and the method name, as shown in the following examp

//double dub = -3.14;
//Console.WriteLine(Math.Abs(dub));
//Console.WriteLine(Math.Floor(dub));
//Console.WriteLine(Math.Round(Math.Abs(dub)));

// Output:
// 3.14
// -4
// 3