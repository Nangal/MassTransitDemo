namespace Demo.Web.Controllers
{
    using System;
    using System.Web.Mvc;
    using Contracts;
    using MassTransit;
    using Models;


    public class AddressController : Controller
    {
        //
        // GET: /Address/

        public ActionResult Index()
        {
            return View("AddAddress");
        }

        //
        // GET: /Address/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Address/Create

        public ActionResult Create()
        {
            return View("AddAddress");
        }

        //
        // POST: /Address/Create

        [HttpPost]
        public ActionResult Create(Address address)
        {
            try
            {
                Bus.Instance.GetEndpoint(new Uri("rabbitmq://localhost/demo/member-service"))
                    .Send(new SaveMemberAddressMessage
                        {
                            CommandId = NewId.NextGuid(),
                            Timestamp = DateTime.UtcNow,
                            MemberId = address.MemberId,
                            Street = address.Street,
                            City = address.City,
                            State = address.State,
                            Zip = address.Zip,
                        });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public class SaveMemberAddressMessage :
            SaveMemberAddress
        {
            public Guid CommandId { get;  set; }
            public DateTime Timestamp { get;  set; }
            public string MemberId { get;  set; }
            public string Street { get;  set; }
            public string City { get;  set; }
            public string State { get;  set; }
            public string Zip { get;  set; }
        }

        public ActionResult Approve()
        {
            return View();
        }

        //
        // POST: /Address/Create

        [HttpPost]
        public ActionResult Approve(ApproveAddress address)
        {
            try
            {
                Bus.Instance.Publish(new MemberAddressApprovedMessage
                    {
                        EventId = NewId.NextGuid(),
                        Timestamp = DateTime.UtcNow,
                        MemberId = address.MemberId,
                        Approver = address.Approver,
                    });

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }


        public class MemberAddressApprovedMessage :
            MemberAddressApproved
        {
            public string MemberId { get;  set; }
            public Guid EventId { get;  set; }
            public DateTime Timestamp { get;  set; }
            public string Approver { get;  set; }
        }


        //
        // GET: /Address/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Address/Edit/5

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

        //
        // GET: /Address/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Address/Delete/5

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