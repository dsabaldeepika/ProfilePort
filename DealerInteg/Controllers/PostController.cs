
using System;
using System.IO;
using System.Web.Mvc;

namespace DealerPortalCRM.Controllers
{
    public class PostController : Controller
    {
        [HttpPost]
        public string Accept()
        {
            try
            {
                string xmlPayload;
                using (var reader = new StreamReader(Request.InputStream))
                {
                    xmlPayload = reader.ReadToEnd();
                }

                // get XML and save the copy for testing
                System.IO.File.WriteAllText(@"C:\\Temp\testPost.xml", xmlPayload.ToString());

                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
