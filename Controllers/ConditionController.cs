using log4net;
using OrbitCovidConditionConfigurator.DataStore;
using OrbitCovidConditionConfigurator.Enums;
using OrbitCovidConditionConfigurator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace OrbitCovidConditionConfigurator.Controllers
{
    public class ConditionController : Controller
    {
        private static readonly ILog _log = LogManager.GetLogger(typeof(ConditionController));
        /// <summary>
        /// Get All condition to show on the main page
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
            _log.Info("Inside Index");
            return View(await ConditionStore.GetConditions(id: null, ""));
        }
        /// <summary>
        /// Get All conditions
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpPost]
        public async Task<ActionResult> Index(FormCollection form)
        {
            string searchText = string.Empty;
            if (Request.Form["selectedIds"] != null)
            {
                string selectedIds = Request.Form["selectedIds"].ToString();
                _log.Info($"Inside Index - Deleting {selectedIds}");

                await ConditionStore.DeleteSelectedConditions(selectedIds);
            }
            if (Request.Form["Search"] != null)
            {
                searchText = Request.Form["Search"];
                _log.Info($"Inside Index - Getting Condition matching searchText {searchText}");

            }
            return View(await ConditionStore.GetConditions(id: null, searchText));
        }
        /// <summary>
        /// Get the condition Details for Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Condition/Details/5
        public async Task<ActionResult> Details(int id)
        {
            List<Condition> conditions = await ConditionStore.GetConditions(id);
            if (conditions != null)
            {
                return View(conditions.FirstOrDefault());
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }

        // GET: Condition/Create
        public async Task<ActionResult> Create()
        {
            Condition condition = new Condition(); 
            await InitializeCondition(condition);

            return View(condition);
        }
        /// <summary>
        /// Insert New Condition
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Create(Condition condition)
        {
            try
            {
                await ConditionStore.SaveCondition(condition);
                await InitializeCondition(condition);
                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                _log.Error($" Create New Condition  {ex.ToString()}");
                return View();
            }
        }
        /// <summary>
        /// Load the View to get updated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Condition/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if(id == null)
            {
                return new HttpNotFoundResult();
            }
            List<Condition> conditions = await ConditionStore.GetConditions(id);

            if (conditions != null)
            {
                await InitializeCondition(conditions.FirstOrDefault());
                return View(conditions.FirstOrDefault());
            }
            else
            {
                _log.Error($" Edit Id {id} - Not found or failed to initialise");
                return new HttpNotFoundResult();
            }
        }
        /// <summary>
        /// Update the Condition
        /// </summary>
        /// <param name="id"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Edit(int? id, Condition condition)
        {
            if (id == null)
            {
                return new HttpNotFoundResult();
            }
            List<Condition> conditions = await ConditionStore.GetConditions(id);
            if (conditions != null)
            {
                try
                {
                    condition.OutletCode = conditions.FirstOrDefault().OutletCode;
                    await ConditionStore.SaveCondition(condition);
                    if(!await InitializeCondition(condition))
                    {
                        _log.Error($" Initialize Condition Id {id} - failed to initialise ");
                        return new HttpNotFoundResult();
                    }
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    _log.Error($" Initialize Condition Id {id} - failed to initialise {ex.ToString()}");
                    return new HttpNotFoundResult();
                }
            }
            else
            {
                _log.Error($" Edit Id {id} - Not found or failed to initialise");
                return new HttpNotFoundResult();
            }
        }
        /// <summary>
        /// Initialise all Required properties
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>

        private static async Task<bool> InitializeCondition(Condition condition)
        {
            try
            {
                condition.Outlets = await Outlet.GetOutlets();
                condition.Routes = RouteList.GetRoutes();
                condition.SegmentTypes = SegmentTypeList.GetSegmentTypes();
                condition.Brands = BrandList.GetBrands();
                if (condition.Id == 0)
                {
                    condition.IsActive = true;
                    condition.ValidFrom = DateTime.Now;
                    condition.ValidTo = DateTime.Now.AddYears(1);
                }
                return true;
            }
            catch(Exception ex)
            {
                _log.Error($" Initialize Condition Id {condition?.Id} - failed to initialise {ex.ToString()}");
                return false;
            }
        }
        /// <summary>
        /// Get Branch List Matching Outlet Code
        /// </summary>
        /// <param name="outlet"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetOutletBranchList(string outletCode, int brand)
        {
            List<string> branchList = new List<string>();
            List<Outlet> outlets = await OutletStore.GetOutlets(brand);
            List<Outlet> matchOutlets = string.IsNullOrEmpty(outletCode) || outletCode.Equals("null", StringComparison.OrdinalIgnoreCase) ? outlets : outlets.FindAll(o => o.OutletCode.Equals(outletCode, StringComparison.OrdinalIgnoreCase));
            foreach (Outlet outlet in matchOutlets)
            {
                branchList.Add(outlet.BranchCode);
            }
            return Json(branchList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Get Distinct Outlet Codes
        /// </summary>
        /// <param name="outlet"></param>
        /// <returns></returns>
        [AcceptVerbs(HttpVerbs.Get)]
        public async Task<JsonResult> GetOutletList(int brand)
        {
            List<string> outletList = new List<string>();
            List<Outlet> outlets = await OutletStore.GetOutlets(brand);
            foreach (string outletCode in outlets.GroupBy(o => o.OutletCode).Select(o => o.Key))
            {
                outletList.Add(outletCode);
            }
            return Json(outletList, JsonRequestBehavior.AllowGet);
        }
       
       
    }
}
