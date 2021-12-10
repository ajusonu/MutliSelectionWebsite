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
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> Index()
        {
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
                await ConditionStore.DeleteSelectedConditions(selectedIds);
            }
            if (Request.Form["Search"] != null)
            {
                searchText = Request.Form["Search"];
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
            catch
            {
                return View();
            }
        }
        /// <summary>
        /// Load the View to get updated
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        // GET: Condition/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            List<Condition> conditions = await ConditionStore.GetConditions(id);

            if (conditions != null)
            {
                await InitializeCondition(conditions.FirstOrDefault());
                return View(conditions.FirstOrDefault());
            }
            else
            {
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
        public async Task<ActionResult> Edit(int id, Condition condition)
        {
            List<Condition> conditions = await ConditionStore.GetConditions(id);
            if (conditions != null)
            {
                try
                {
                    //condition.BranchCode = conditions.FirstOrDefault().BranchCode;
                    condition.OutletCode = conditions.FirstOrDefault().OutletCode;
                    await ConditionStore.SaveCondition(condition);
                    await InitializeCondition(condition);
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
            else
            {
                return new HttpNotFoundResult();
            }
        }
        /// <summary>
        /// Initialise all Required properties
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>

        private static async Task InitializeCondition(Condition condition)
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
        // GET: Condition/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Condition/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
       
    }
}
