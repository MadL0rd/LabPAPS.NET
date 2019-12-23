using AutoMapper;
using HotelBooking.Entities;
using HotelBooking.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelBooking.Controllers
{
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext context { get; set; }
        public AdminController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Admin
        public ActionResult OrderList()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Order, OrderListViewModel>()).CreateMapper();
            var orders = config.Map<IQueryable<Order>, List<OrderListViewModel>>(context.Orders);
            for (int i = 0; i < orders.Count; i++)
            {
                var room = context.Orders.Find(orders[i].Id).Room;
                orders[i].Hotel = room.Hotel.Name;
                orders[i].Category = room.Category.Name;
            }
            orders.OrderBy(x => x.ArrivalTime);
            return View(orders);
        }
        public ActionResult RoomList(int? SelectedCategoryId, int? SelectedHotelId)
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
        public ActionResult CategoryList()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Category, CategoryListViewModel>()).CreateMapper();
            var categories = config.Map<IQueryable<Category>, List<CategoryListViewModel>>(context.Categories);
            return View(categories);
        }
        public ActionResult HotelList()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Hotel, HotelListViewModel>()).CreateMapper();
            var hotels = config.Map<IQueryable<Hotel>, List<HotelListViewModel>>(context.Hotels);
            return View(hotels);
        }



        public ActionResult CreateRoom()
        {
            var model = new CreateRoomViewModel();
            model.Categories = context.Categories.ToList();
            model.Hotels = context.Hotels.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult CreateRoom(CreateRoomViewModel room)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(x => x.CreateMap<CreateRoomViewModel, Room>()).CreateMapper();
                var newRoom = config.Map<CreateRoomViewModel, Room>(room);
                newRoom.Category = context.Categories.Find(room.SelectedCategoryId);
                newRoom.Hotel = context.Hotels.Find(room.SelectedHotelId);
                context.Rooms.Add(newRoom);
                context.SaveChanges();
                TempData["color"] = "green";
                TempData["message"] = string.Format(" Комната категории \"{0}\" добавлена в отель \"{1}\"", newRoom.Category.Name, newRoom.Hotel.Name);
            }
            room = new CreateRoomViewModel();
            room.Categories = context.Categories.ToList();
            room.Hotels = context.Hotels.ToList();
            return View(room);
        }
        public ActionResult DeleteRoom(int id)
        {
            var room = context.Rooms.Find(id);
            if (room != null)
            {
                context.Rooms.Remove(room);
                context.SaveChanges();
                TempData["message"] = string.Format(" Комната категории \"{0}\" удалена из отеля \"{1}\"", room.Category.Name, room.Hotel.Name);
            }
            return RedirectToAction("RoomList");
        }



        public ActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateCategory(CreateCategoryViewModel category)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(x => x.CreateMap<CreateCategoryViewModel, Category>()).CreateMapper();
                var newCategory = config.Map<CreateCategoryViewModel, Category>(category);
                context.Categories.Add(newCategory);
                context.SaveChanges();
                TempData["color"] = "green";
                TempData["message"] = string.Format(" Категория комнат \"{0}\" добавлена", newCategory.Name);
            }
            return View();
        }
        public ActionResult DeleteCategory(int id)
        {
            var category = context.Categories.Find(id);
            if (category != null)
            {
                context.Categories.Remove(category);
                context.SaveChanges();
                TempData["message"] = string.Format(" Категория \"{0}\" удалена", category.Name);
            }
            return RedirectToAction("CategoryList");
        }



        public ActionResult CreateHotel()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CreateHotel(CreateHotelViewModel hotel)
        {
            if (ModelState.IsValid)
            {
                var config = new MapperConfiguration(x => x.CreateMap<CreateHotelViewModel, Hotel>()).CreateMapper();
                var newHotel = config.Map<CreateHotelViewModel, Hotel>(hotel);
                context.Hotels.Add(newHotel);
                context.SaveChanges();
                TempData["color"] = "green";
                TempData["message"] = string.Format(" Категория комнат \"{0}\" добавлена", newHotel.Name);
            }
            return View();
        }
        public ActionResult DeleteHotel(int id)
        {
            var hotel = context.Categories.Find(id);
            if (hotel != null)
            {
                context.Categories.Remove(hotel);
                context.SaveChanges();
                TempData["message"] = string.Format(" Категория \"{0}\" удалена", hotel.Name);
            }
            return RedirectToAction("HotelList");
        }



        public ActionResult ResultListAdmin()
        {
            var config = new MapperConfiguration(x => x.CreateMap<Room, RoomListViewModel>()).CreateMapper();
            var rooms = config.Map<IQueryable<Room>, List<RoomListViewModel>>(context.Rooms.Include("User"));
            return View(rooms);
        }
    }
}
