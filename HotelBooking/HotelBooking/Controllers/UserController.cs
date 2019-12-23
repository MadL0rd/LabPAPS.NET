using HotelBooking.Entities;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.EntityFramework;
using HotelBooking.Models;
using AutoMapper;

namespace HotelBooking.Controllers
{
    [Authorize(Roles = "user")]
    public class UserController : Controller
    {
        private ApplicationDbContext context { get; set; }
        private ApplicationUserManager UserManager { get; set; }
        public UserController()
        {
            context = new ApplicationDbContext();
            UserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
        }



        public ActionResult RoomListUser(int? SelectedCategoryId, int? SelectedHotelId)
        {
            var model = new RoomListViewModel();

            var categories = context.Categories.ToList();
            categories.Insert(0, new Category { Id = -1, Name = "Все" });
            model.Categories = categories;

            var hotels = context.Hotels.ToList();
            hotels.Insert(0, new Hotel { Id = -1, Name = "Все" });
            model.Hotels = hotels;

            model.Rooms = context.Rooms.ToList();
            if (SelectedHotelId != -1 && SelectedHotelId != null)
            {
                model.Rooms = model.Rooms.Where(x => x.Hotel.Id == SelectedHotelId);
            }
            if (SelectedCategoryId != -1 && SelectedCategoryId != null)
            {
                model.Rooms = model.Rooms.Where(x => x.Category.Id == SelectedCategoryId);
            }
            return View(model);
        }
        public ActionResult ReserveRoom(int? id)
        {
            var model = new RoomReserveViewModel();

            var room = context.Rooms.Find(id);
            if (room != null)
            {
                model.Room = room;
                var orders = context.Orders.Where(x => x.Room.Id == id).ToList();
                orders.OrderBy(x => x.ArrivalTime);
                model.Orders = orders;
            }
            
            return View(model);
        }

        [HttpPost]
        public ActionResult ReserveRoom(int? id, RoomReserveViewModel reservation)
        {
            if (ModelState.IsValid)
            {
                var begin = reservation.ArrivalTime;
                var end = reservation.DepartureTime;
                if (begin < DateTime.Today)
                {
                    TempData["message"] = "Вы не можете отправиться в прошлое";
                }
                else
                {
                    if (end < begin)
                    {
                        TempData["message"] = "Дата выселения не может быть раньше даты заселения";
                    }
                    else
                    {
                        var check = context.Orders.Where(x => x.Room.Id == id &&
                           (x.ArrivalTime >= begin && x.ArrivalTime <= end || x.DepartureTime >= begin && x.DepartureTime <= end)).ToList();
                        if (check.Count > 0)
                        {
                            TempData["message"] = "Комната уже забронирована (возможно частично) на это время";
                        }
                        else
                        {
                            var config = new MapperConfiguration(x => x.CreateMap<RoomReserveViewModel, Order>()).CreateMapper();
                            var order = config.Map<RoomReserveViewModel, Order>(reservation);
                            order.Room = context.Rooms.Find(id);
                            order.User = UserManager.FindById(User.Identity.GetUserId());
                            context.Orders.Add(order);
                            context.SaveChanges();
                            TempData["color"] = "green";
                            TempData["message"] = string.Format(" Вы забронировали комнату на период с {0} до {1}", order.ArrivalTime.Date, order.DepartureTime.Date);
                        }
                    }
                }
            }
            var room = context.Rooms.Find(id);
            if (room != null)
            {
                reservation.Room = room;
                var orders = context.Orders.Where(x => x.Room.Id == id && x.DepartureTime >= DateTime.Today).ToList();
                orders.OrderBy(x => x.ArrivalTime);
                reservation.Orders = orders;
            }
            return View(reservation);
        }

        public ActionResult OrderList()
        {
            var user = UserManager.FindById(User.Identity.GetUserId());

            var config = new MapperConfiguration(x => x.CreateMap<Order, OrderListViewModel>()).CreateMapper();
            var orders = config.Map<IQueryable<Order>, List<OrderListViewModel>>(context.Orders.Where(x => x.User.Id == user.Id));
            for (int i = 0; i < orders.Count; i++)
            {
                var room = context.Orders.Find(orders[i].Id).Room;
                orders[i].Hotel = room.Hotel.Name;
                orders[i].Category = room.Category.Name;
            }
            return View(orders);
        }
        [HttpGet]
        public ActionResult CreateOrder()
        {
            return View();
        }
       
        public ActionResult DeleteOrder(int id)
        {
            var ord = context.Orders.Find(id);
            if (ord != null)
            {
                context.Orders.Remove(ord);
                context.SaveChanges();
                TempData["message"] = string.Format(" Бронирование отменено");
            }
            return RedirectToAction("OrderList");
        }
    }
}