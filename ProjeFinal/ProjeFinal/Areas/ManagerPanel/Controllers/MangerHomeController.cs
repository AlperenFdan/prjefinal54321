using ProjeFinal.Areas.ManagerPanel.Data;
using ProjeFinal.Areas.ManagerPanel.MyAttributes;
using ProjeFinal.Models;
using ProjeFinal.MyHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.ModelBinding;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.Expressions;
using System.Xml;
using System.Xml.Linq;

namespace ProjeFinal.Areas.ManagerPanel.Controllers
{

    public class MangerHomeController : Controller
    {
        string SIMG = "C:\\Users\\lastg\\Desktop\\prjefinal54321\\Pictures\\";
        string MY = @"C:\Users\lastg\Desktop\prjefinal54321\ProjeFinal\ProjeFinal\MyDataForFinalProject\Photos\";
        // GET: ManagerPanel/MangerHome
        EH_Store db = new EH_Store();
        Mylib ml = new Mylib();
        public ActionResult Index()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult Index(ViewModelForMangerLogin mdl)
        {
            if (ModelState.IsValid)
            {
                Emp e = db.Emps.FirstOrDefault(x => x.Email == mdl.EMail && x.Password == mdl.Password);
                if (e != null)
                {
                    if (!e.isBanned)
                    {
                        Session["Manager"] = e;
                        return RedirectToAction("Home", "MangerHome");
                    }
                    else
                    {
                        ViewBag.Warning = "Bir sorun var gibi duruyor bu sizden ötürümü yoksa bizdenmi?";
                    }
                }
                else
                {
                    ViewBag.Warning = "Kulalnıcı bulanamdı!";
                }
            }
            return View();
        }
        public ActionResult TakeProducts(bool way)
        {
            int ProductCount =0;
            int CategoryCount = 0;
            int BrandCount = 0;
            decimal TotalPrice = 0; ;
            XDocument xmlDoc = XDocument.Load("C:\\Users\\lastg\\Desktop\\prjefinal54321\\Products.xml");
         

            if (way)
            {
                List<object> bnc = new List<object>();
                List<object> pr = new List<object>();
                pr.AddRange(db.Products);
                bnc.AddRange(db.Categories);
                bnc.AddRange(db.Brands);
               

                foreach (Product item in pr)
                {
                    
                    if (item.ProductType == "Memory")
                    {
                        Memory b = ml.GetArticle(item.ProductType, item.ProductID) as Memory;
                        Memory prd = db.Memorys.Find(b.ID);
                        FileInfo fi = new FileInfo(MY + prd.imgs);
                        fi.Delete();
                        db.Memorys.Remove(prd);
                    }
                    else if (item.ProductType == "Processor")
                    {
                        Processor b = ml.GetArticle(item.ProductType, item.ProductID) as Processor;
                        Processor prd = db.Processors.Find(b.ID); 
                        FileInfo fi = new FileInfo(MY + prd.imgs);
                        fi.Delete();
                        db.Processors.Remove(prd);
                    }
                    else if(item.ProductType == "Storage")
                    {
                        Storage b = ml.GetArticle(item.ProductType, item.ProductID) as Storage;
                      
                        Storage prd = db.Storages.Find(b.ID); 
                        FileInfo fi = new FileInfo(MY + prd.imgs);
                        fi.Delete();
                        db.Storages.Remove(prd);
                    }
                    else if(item.ProductType == "Motherboard")
                    {
                        Motherboard b = ml.GetArticle(item.ProductType, item.ProductID) as Motherboard;
                        Motherboard prd = db.Motherboards.Find(b.ID);
                        FileInfo fi = new FileInfo(MY + prd.imgs);
                        fi.Delete();
                        db.Motherboards.Remove(prd);
                    }
                    else if(item.ProductType == "GraphicsCard")
                    {
                        GraphicsCard b = ml.GetArticle(item.ProductType, item.ProductID) as GraphicsCard;
                        GraphicsCard prd = db.GraphicsCards.Find(b.ID);
                        FileInfo fi = new FileInfo(MY + prd.imgs);
                        fi.Delete();
                        db.GraphicsCards.Remove(prd);
                    }
                    db.SaveChanges();
                }
                foreach (object item in bnc)
                {
                    if (item.GetType().Name.ToString().Split('_')[0] == "Brand")
                    {
                        Brand b = item as Brand;
                        Brand prd = db.Brands.Find(b.ID);
                        BrandCount++;
                        db.Brands.Remove(prd);
                        db.SaveChanges();
                    }
                    else if (item.GetType().Name.ToString().Split('_')[0] == "Category")
                    {
                        Category b = item as Category;
                        Category prd = db.Categories.Find(b.ID);
                        CategoryCount++;
                        db.Categories.Remove(prd);
                        db.SaveChanges();
                    }

                }
            }
 
            foreach (XElement productElement in xmlDoc.Descendants("Product"))
            {
                string Type = productElement.Attribute("Type").Value;
                string productName = productElement.Element("Name").Value;
                string Description = productElement.Element("Description").Value;
                bool IsActived = true;
               
                DateTime CreationTime = DateTime.Now;
                if (Type == "Brand" && db.Brands.FirstOrDefault(x => x.Name == productName) == null)
                {
                    Brand brnd = new Brand();
                    brnd.Name = productName;
                    brnd.Description = Description;
               
                    brnd.CreationTime = CreationTime;
                    brnd.IsActive = IsActived;
                    db.Brands.Add(brnd);
                    db.SaveChanges();
                }
                else if (Type == "Category" && db.Categories.FirstOrDefault(x=> x.Name == productName) == null)
                {
                    Category Category = new Category();
                    Category.Name = productName;
                    Category.Description = Description;
                 
                    Category.CreationTime = CreationTime;
                    Category.IsActive = IsActived;
                    db.Categories.Add(Category);
                    db.SaveChanges();
                }
            }
            ProductCount = 0;
            foreach (XElement productElement in xmlDoc.Descendants("Product"))
            {
                
                string Type = productElement.Attribute("Type").Value;
                if (Type !=  "Category" && Type != "Brand")
                {
                    
                    string productName = productElement.Element("Name").Value;
                    string Description = productElement.Element("Description").Value;
                    double pricep = double.Parse(productElement.Element("Price").Value) / 100;
                    decimal price = Convert.ToDecimal(pricep - (pricep *0.2));
                    int stock = int.Parse(productElement.Element("stock").Value);
                    int sstock = int.Parse(productElement.Element("SStock").Value) / 10;
                    string category1 = productElement.Element("CategoryName").Value;
                    string Brand1 = productElement.Element("BrandName").Value;
                    int category = db.Categories.FirstOrDefault(x => x.Name == category1).ID;
                    int Brand = db.Brands.FirstOrDefault(x => x.Name == Brand1).ID;
                    string imgs = productElement.Element("imgs").Value;
                    bool IsActived = true;
                    bool IsDeleted = false;
                    DateTime CreationTime = DateTime.Now;
                    if (Type == "GraphicsCard")
                    {
                      
                        GraphicsCard pr = new GraphicsCard();
                        pr.Name = productName;
                        pr.Description = Description;
                        pr.Price = price; pr.imgs = imgs;
                        pr.stock = stock;
                        pr.SStock = sstock;
                        pr.BrandID = Brand;
                        pr.CategoryID = category;
                        pr.IsActived = IsActived;
                        pr.IsDeleted = IsDeleted;
                        pr.CreationTime = CreationTime;
                        pr.VRAM = int.Parse(productElement.Element("VRAM").Value);
                        pr.Series = productElement.Element("Series").Value;
                        pr.bitnumber = productElement.Element("bitnumber").Value;
                        pr.CompatibleConnect = productElement.Element("CompatibleConnect").Value;
                        pr.Connects = productElement.Element("Connects").Value;
                        pr.StorgeType = productElement.Element("StorgeType").Value;
                        GraphicsCard md = db.GraphicsCards.FirstOrDefault(x => x.Name == pr.Name);
                        if (md == null)
                        {
                            TotalPrice += price*stock;
                            ProductCount += stock;
                            db.GraphicsCards.Add(pr);
                            db.SaveChanges();
                        }
                        else
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.GraphicsCards.Remove(md);
                            db.SaveChanges();
                            db.GraphicsCards.Add(pr); db.SaveChanges();
                        }
                    }
                    else if (Type == "Processor")
                    {
                      
                        Processor pr = new Processor();
                        pr.Name = productName;
                        pr.Description = Description;
                        pr.Price = price;
                        pr.imgs = imgs;
                        pr.stock = stock;
                        pr.SStock = sstock;
                        pr.BrandID = Brand;
                        pr.CategoryID = category;
                        pr.IsActived = IsActived;
                        pr.IsDeleted = IsDeleted;
                        pr.CreationTime = CreationTime;
                        pr.MakximumFrequency = double.Parse(productElement.Element("MakximumFrequency").Value);
                        pr.L3Cache = productElement.Element("L3Cache").Value;
                        pr.L2Cahce = productElement.Element("L2Cahce").Value;
                        pr.NumberOfCores = productElement.Element("NumberOfCores").Value;
                        pr.ClockFrequency = double.Parse(productElement.Element("ClockFrequency").Value);
                        pr.BusSpeed = productElement.Element("BusSpeed").Value;
                        pr.ProcessorType = productElement.Element("ProcessorType").Value;
                        Processor md = db.Processors.FirstOrDefault(x => x.Name == pr.Name);
                        if (md == null)
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Processors.Add(pr);
                            db.SaveChanges();
                        }
                        else
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Processors.Remove(md);
                            db.SaveChanges();
                            db.Processors.Add(pr); db.SaveChanges();
                        }
                    }
                    else if (Type == "Motherboard")
                    {
                        Motherboard pr = new Motherboard();
                        pr.Name = productName;
                        pr.Description = Description;
                        pr.Price = price;
                        pr.stock = stock; pr.imgs = imgs;
                        pr.SStock = sstock;
                        pr.BrandID = Brand;
                        pr.CategoryID = category;
                        pr.IsActived = IsActived;
                        pr.IsDeleted = IsDeleted;
                        pr.CreationTime = CreationTime;
                        pr.ChipsetManufacturer = productElement.Element("ChipsetManufacturer").Value;
                        pr.MemoryTechnology = productElement.Element("MemoryTechnology").Value;
                        pr.MemorySlot = productElement.Element("MemorySlot").Value;
                        pr.MemoryClockSpeed = productElement.Element("MemoryClockSpeed").Value;
                        pr.PCIVersion = productElement.Element("PCIVersion").Value;
                        pr.MultiGPU = bool.Parse(productElement.Element("MultiGPU").Value);

                        Motherboard md = db.Motherboards.FirstOrDefault(x => x.Name == pr.Name);
                        if (md == null)
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Motherboards.Add(pr);
                            db.SaveChanges();
                        }
                        else
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Motherboards.Remove(md);
                            db.SaveChanges();
                            db.Motherboards.Add(pr);
                            db.SaveChanges();
                        }
                    }
                    else if (Type == "Memory")
                    {
                    
                        Memory pr = new Memory();
                        pr.Name = productName;
                        pr.Description = Description;
                        pr.Price = price; pr.imgs = imgs;
                        pr.stock = stock;
                        pr.SStock = sstock;
                        pr.BrandID = Brand;
                        pr.CategoryID = category;
                        pr.IsActived = IsActived;
                        pr.IsDeleted = IsDeleted;
                        pr.CreationTime = CreationTime;
                        pr.MemoryType = productElement.Element("MemoryType").Value;
                        pr.CapacityPerModule = productElement.Element("CapacityPerModule").Value;
                        pr.MemoryFrequency = productElement.Element("MemoryFrequency").Value;
                        pr.TotalMemoryCapacity = productElement.Element("TotalMemoryCapacity").Value;

                        Memory md = db.Memorys.FirstOrDefault(x => x.Name == pr.Name);
                        if (md == null)
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Memorys.Add(pr);
                            db.SaveChanges();
                        }
                        else
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Memorys.Remove(md);
                            db.SaveChanges();
                            db.Memorys.Add(pr);
                            db.SaveChanges();
                        }
                    }
                    else if (Type == "Storage")
                    {
                           
                        Storage pr = new Storage();
                        pr.Name = productName;
                        pr.Description = Description;
                        pr.Price = price;
                        pr.stock = stock; pr.imgs = imgs;
                        pr.SStock = sstock;
                        pr.BrandID = Brand;
                        pr.CategoryID = category;
                        pr.IsActived = IsActived;
                        pr.IsDeleted = IsDeleted;
                        pr.CreationTime = CreationTime;
                        pr.StorageCapacity = productElement.Element("StorageCapacity").Value;
                        pr.SequentialWriting = productElement.Element("SequentialWriting").Value;
                        pr.SequentialReading = productElement.Element("SequentialReading").Value;
                        pr.connections = productElement.Element("connections").Value;
                        pr.Height = productElement.Element("Height").Value;
                        pr.Width = productElement.Element("Width").Value;
                        pr.Type = productElement.Element("Type").Value;

                        Storage md = db.Storages.FirstOrDefault(x => x.Name == pr.Name);
                        if (md == null)
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Storages.Add(pr);
                            db.SaveChanges();
                        }
                        else
                        {
                            TotalPrice += price * stock;
                            ProductCount += stock;
                            db.Storages.Remove(md);
                            db.SaveChanges();
                            db.Storages.Add(pr);
                            db.SaveChanges();
                        }
                    }
                    FileInfo fi = new FileInfo(MY + imgs);
                    if (!fi.Exists)
                    {
                       
                        FileInfo fileInfo = new FileInfo(SIMG + imgs);
                        fileInfo.CopyTo(MY + imgs);
                    }
                }

            }
            
            ViewBag.BrandCount = db.Brands.Count(x => x.IsActive == true);
            ViewBag.CategoryCount = db.Categories.Count(x => x.IsActive == true);
            ViewBag.UserCount = db.Users.Count(x => x.IsActive == true);
            ViewBag.GraphicsCardCount = db.GraphicsCards.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.PrecessorCount = db.Processors.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.RamCount = db.Memorys.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.MotehboardCount = db.Motherboards.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.StorageCount = db.Storages.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.ProductCount = ViewBag.StorageCount + ViewBag.MotehboardCount + ViewBag.RamCount + ViewBag.PrecessorCount + ViewBag.GraphicsCardCount;
            ViewBag.TotalPrice = (TotalPrice * Convert.ToDecimal(TakeDiscount(4)) / 100).ToString("C", new System.Globalization.CultureInfo("en-US"));
            ViewBag.Discount = TakeDiscount(4);
            ViewBag.CountP = ProductCount;
            ViewBag.CountC = CategoryCount;
            ViewBag.CountB = BrandCount;
            string path = Server.MapPath("~/Models/StoreInformation.xml");
            XDocument xd = XDocument.Load(path);
            XElement xel = xd.Root;
       
            XElement xeldc = xmlDoc.Root;
            int serverversion = int.Parse(xeldc.Attribute("Version").Value);
            xel.SetAttributeValue("Version", serverversion);
            xd.Save(path);
            return View("Home");
        }
        [MangerLoginControl]
        public ActionResult Home()
        {
          
            XDocument xmlDoc = XDocument.Load("C:\\Users\\lastg\\Desktop\\prjefinal54321\\Products.xml");
            string path = Server.MapPath("~/Models/StoreInformation.xml");
            XDocument xd = XDocument.Load(path);
            XElement xel = xd.Root;
            XElement xel2 = xmlDoc.Root;
            int Myversion = int.Parse(xel.Attribute("Version").Value);
            int ServerVersion = int.Parse(xel2.Attribute("Version").Value);
            if (Myversion < ServerVersion)
            {
                ViewBag.Infromation = "Güncelleme gerekli!";
            }

            ViewBag.BrandCount = db.Brands.Count(x => x.IsActive == true );
            ViewBag.CategoryCount = db.Categories.Count(x =>  x.IsActive == true);
            ViewBag.UserCount = db.Users.Count(x => x.IsActive == true);
            ViewBag.GraphicsCardCount = db.GraphicsCards.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.PrecessorCount = db.Processors.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.RamCount = db.Memorys.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.MotehboardCount = db.Motherboards.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.StorageCount = db.Storages.Count(x => x.IsActived == true && x.IsDeleted == false);
            ViewBag.ProductCount = ViewBag.StorageCount + ViewBag.MotehboardCount + ViewBag.RamCount + ViewBag.PrecessorCount + ViewBag.GraphicsCardCount;
            return View();
        }

        private decimal TakeDiscount(int id)
        {
           SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ReadyForEH;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();

            try
            {
                cmd.CommandText = "Select * From Stores where ID = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                int TierID = 0;
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {

                    TierID = reader.GetInt32(4);
                }
                return FindTier(TierID);

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
           return -1;
        }
        private decimal FindTier(int id)
        {
            SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ReadyForEH;Integrated Security=True");
            SqlCommand cmd = con.CreateCommand();
            try
            {

                cmd.CommandText = "Select * From Tiers where ID = @id";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("id", id);
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                decimal ds = 0;
                while (reader.Read())
                {
                    ds = reader.GetInt32(2);
                }
                return ds;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return 1;
          
        }
    }


    
}