using OrbitCovidConditionConfigurator.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace OrbitCovidConditionConfigurator.Models
{
    public class Condition
    {
        /// <summary>
        /// Row Id
        /// </summary>
        /// 
        [Display(Name="Id")]
        
        public int Id { get; set; }
        /// <summary>
        /// Brand - Orbit Or Retail
        /// </summary>
        public Brand Brand { get; set; }

        /// <summary>
        /// Outlet Code
        /// </summary>
        /// 
        [Display(Name = "Outlet")]
        public String OutletCode { get; set; }
        /// <summary>
        /// Branch Code
        /// </summary>
        /// 
        [Display(Name = "Branch List")]
        public String BranchCode { get; set; }
        /// <summary>
        /// Airline Code
        /// </summary>
        /// 
        [Display(Name = "Airline")]
        public String Airline { get; set; }
        /// <summary>
        /// Item type - Hotel, Air etc
        /// </summary>
        /// 
        [Display(Name = "Item")]
        public SegmentType SegmentType { get; set; }
        /// <summary>
        /// Domestic/ International
        /// </summary>
        /// 
        [Display(Name = "Route")]
        public Route Route { get; set; }
        /// <summary>
        /// Multiline Covid Conditions
        /// </summary>
        ///
        [Display(Name = "Condition")]
        [UIHint("MultilineText")]
        public string CovidConditon { get; set; }
        /// <summary>
        /// Valid From Date
        /// </summary>
        /// 
        [Display(Name = "Valid From")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.DateTime), Required]
        public DateTime ValidFrom { get; set; }
        /// <summary>
        /// Valid To Date
        /// </summary>
        /// 
       
        [Display(Name = "Valid To")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.DateTime), Required]
        public DateTime ValidTo { get; set; }
        /// <summary>
        /// Is Condition Active
        /// </summary>
        /// 
        [Display(Name = "Active")]
        public bool IsActive { get; set; }
        /// <summary>
        /// List of Outlets - used in drop downs
        /// </summary>
        public List<Outlet> Outlets { get; set; }

        /// <summary>
        /// List of Selected Row ids  - used for Selection in the list
        /// </summary>
        public List<string> SelectedRowIds { get; set; }
        /// <summary>
        /// Segment Type List for Dropdown
        /// </summary>
        public List<SegmentType> SegmentTypes { get; set; }
        /// <summary>
        /// Route List - Domestic/ International
        /// </summary>
        public List<Route> Routes { get; set; }
        /// <summary>
        /// Brand List
        /// </summary>
        public List<Brand> Brands { get; set; }
        /// <summary>
        /// Search Parameter passed
        /// </summary>
        public SearchParameter SearchParameter { get; set; }
    }
}