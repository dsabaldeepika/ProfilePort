using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfilePort.DataModel
{
    public class Layout :Edit
    {

        public int Id { get; set; }
        public string LayoutName { get; set; }
        public string HeaderColor { get; set; }
        public string BodyColor { get; set; }
        public string FooterColor { get; set; }
        public string NavigationBarColor { get; set; }
        public string BackgroundColor { get; set; }
     

        [Required]
        public string DashboardId { get; set; }
        [ForeignKey("DashboardId")]
        public virtual Dashboard Dashboard { get; set; }
    }
}
