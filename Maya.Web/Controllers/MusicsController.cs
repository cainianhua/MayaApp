using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Maya.Services.VO;
using Maya.Services.BO;
using Maya.Web.Models;
using Maya.Services;
using System.ComponentModel;
using Webdiyer.WebControls.Mvc;
using Maya.Web.FilterAttributes;

namespace Maya.Web.Controllers
{
	[LoginChecker]
    public class MusicsController : ControllerBase
    {
        // GET: Musics
        public ActionResult Index([DefaultValue(1)] int page, int? did)
        {
			List<MusicVO> musics = MusicBO.GetInstance().GetItems(did);
            var items = new PagedList<MusicVO>(musics.Skip((page - 1) * PageSize).Take(PageSize), page, PageSize, musics.Count);

            List<SelectListItem> selectItems = new List<SelectListItem>();
            DistrictBO.GetInstance().GetItems().ForEach(item =>
            {
                selectItems.Add(new SelectListItem()
                {
                    Text = item.Name,
                    Value = Url.Action("Index", new { page = page, did = item.DistrictId }),
                    Selected = did.HasValue && did.Value == item.DistrictId
                });
            });

            ViewBag.Districts = selectItems;

			return View( items );
        }

        // GET: Musics/Create
        public ActionResult Create(int? id)
        {
			CreateOrEditMusicModel item = new CreateOrEditMusicModel();
			if (id.HasValue) {
				MusicVO m = MusicBO.GetInstance().GetItem( id.Value );
				if (m != null) {
					item = m.To<CreateOrEditMusicModel>();
				}
			}

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );

			return View( item );
        }

        // POST: Musics/Create
        [HttpPost]
        public ActionResult Create(CreateOrEditMusicModel item)
        {
			if (ModelState.IsValid) {
				MusicVO m = new MusicVO();
				m.MusicId = item.MusicId;
				m.Name = item.Name;
				m.Description = item.Description;
				m.LinkTo = item.LinkTo;
				m.SortOrder = item.SortOrder;
				m.DistrictId = item.DistrictId;

				m.ActionDate = DateTime.Now;
				m.ActionBy = UserContext.Current.User.UserName;

				try {
					m.MusicId = MusicBO.GetInstance().SaveOrUpdateItem( m );
				}
				catch (Exception ex) {
					ModelState.AddModelError( "", ex );
				}

				if (ModelState.IsValid) {
					return RedirectToAction( "Index" );
				}
			}

			ViewBag.Districts = new SelectList( DistrictBO.GetInstance().GetItems(), "DistrictId", "Name" );

			return View( item );
        }

        // GET: Musics/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Musics/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // POST: Musics/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            // TODO: Add delete logic here
            MusicBO.GetInstance().DeleteItem(id);

            return RedirectToAction("Index");
        }
    }
}
