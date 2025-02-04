using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ADOCOKUGRASTIRIYOR
{
    public class DataRegion
    {
        SqlConnection con; SqlCommand cmd;
        public DataRegion() {
            con = new SqlConnection(@"Data Source=.\SQLEXPRESS;Initial Catalog=ReadyForEH;Integrated Security=True");
            cmd = con.CreateCommand();
        }

        public Emp LoginControl(string Email, string Password)
        {
            List<Emp> empList = new List<Emp>();
            try
            {
                cmd.CommandText = "Select * From Emps";
                cmd.Parameters.Clear();
                con.Open(); 
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Emp emp = new Emp();
                    emp.ID = reader.GetInt32(0);
                    emp.Name = reader.GetString(1);
                    emp.SurnName = reader.GetString(2);
                    emp.Email = reader.GetString(3);
                    emp.Password = reader.GetString(4);
                    emp.Phone = reader.GetString(5);
                   
                    empList.Add(emp);
                }


            }
            catch (Exception)
            {

            }
            finally
            { 
                con.Close();
            }

            foreach (Emp emp in empList)
            {
                if (emp.Email == Email && emp.Password == Password)
                {
                    
                    return emp;
                }
            }
            return null;
        }
        #region Brands
        public bool AddBrand(Brand b)
        {
            try
            {
                cmd.CommandText = "Insert Into Brands(Name,Description,IsActive,IsDeleted,CreationTime) Values(@n,@d,@a,@de,@c)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n",b.Name);
                cmd.Parameters.AddWithValue("@d",b.Description);
                cmd.Parameters.AddWithValue("@a",b.IsActive);
                cmd.Parameters.AddWithValue("@de",b.IsDeleted);
                cmd.Parameters.AddWithValue("@c",b.CreationTime);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
      
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                con.Close() ;
            }
        }
        public bool DeleteBrand(int BrandID)
        {
            try
            {
                cmd.CommandText = "Delete From Brands Where ID = @ID ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", BrandID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateBrands(Brand Item)
        {
            try
            {
                cmd.CommandText = "Update Brands SET Name =@n, Description = @d, IsActive = @IsA, IsDeleted = @IsD Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", Item.Name);
                cmd.Parameters.AddWithValue("@d", Item.Description);
                cmd.Parameters.AddWithValue("@IsA", Item.IsActive);
                cmd.Parameters.AddWithValue("@IsD", Item.IsDeleted);
                cmd.Parameters.AddWithValue("@ID", Item.ID);

                con.Open();
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return false;
        }
        public ObservableCollection<Brand> ListBrands()
        {
            ObservableCollection<Brand> brotherList = new ObservableCollection<Brand>();
            try
            {
                cmd.CommandText = "Select * From Brands";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Brand brother = new Brand();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Description = reader.GetString(2);
                    brother.IsActive = reader.GetBoolean(3);
                    brother.IsDeleted = reader.GetBoolean(4);
                    brother.CreationTime = reader.GetDateTime(5);
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

        #region Categories
        public ObservableCollection<Category> ListCategory()
        {
            ObservableCollection<Category> brotherList = new ObservableCollection<Category>();
            try
            {
                cmd.CommandText = "Select * From Categories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Category brother = new Category();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Description = reader.GetString(2);
                    brother.IsDeleted = reader.GetBoolean(3);
                    brother.IsActive = reader.GetBoolean(4);
                    brother.CreationTime = reader.GetDateTime(5);
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        public bool AddCategory(Category c)
        {
            try
            {
                cmd.CommandText = "Insert Into Categories(Name,Description,IsActive,IsDeleted,CreationTime) Values(@n,@d,@a,@de,@c)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n",c.Name);
                cmd.Parameters.AddWithValue("@d", c.Description);
                cmd.Parameters.AddWithValue("@a", c.IsActive);
                cmd.Parameters.AddWithValue("@de", c.IsDeleted);
                cmd.Parameters.AddWithValue("@c", c.CreationTime);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool DeleteCategory(int CategoryID)
        {
            try
            {
                cmd.CommandText = "Delete From Categories Where ID = @ID ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", CategoryID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateCategory(Category Item)
        {
            try
            {
                cmd.CommandText = "Update Categories SET Name =@n, Description = @d, IsActive = @IsA, IsDeleted = @IsD Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", Item.Name);
                cmd.Parameters.AddWithValue("@d", Item.Description);
                cmd.Parameters.AddWithValue("@IsA", Item.IsActive);
                cmd.Parameters.AddWithValue("@IsD", Item.IsDeleted);
                cmd.Parameters.AddWithValue("@ID", Item.ID);

                con.Open();
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return false;
        }
        #endregion

        #region GraphicsCard
        public bool DeleteGraphicsCard(int ID)
        {
            try
            {
                cmd.CommandText = "Delete From GraphicsCards Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateGraphicsCard(GraphicsCard p)
        {
            try
            {
                cmd.CommandText = "Update GraphicsCards SET Name = @n, Price = @p, ListPrice = @lp, BrandID = @bi, CategoryID = @ci, Description = @d, stock = @s, SStock = @ss, imgs = @i, IsActived = @ia, IsDeleted = @id, CreationTime = @ct , VRAM = @VRAM ,Series = @Series, bitnumber = @bitnumber, CompatibleConnect = @CompatibleConnect, Connects = @Connects,StorgeType = @StorgeType Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@i", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@ID", p.ID); 
                cmd.Parameters.AddWithValue("@VRAM", p.VRAM);
                cmd.Parameters.AddWithValue("@Series", p.Series);
                cmd.Parameters.AddWithValue("@bitnumber", p.bitnumber);
                cmd.Parameters.AddWithValue("@CompatibleConnect", p.CompatibleConnect);
                cmd.Parameters.AddWithValue("@Connects", p.Connects);
                cmd.Parameters.AddWithValue("@StorgeType", p.StorgeType);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool AddGraphicsCard(GraphicsCard p)
        {
            try
            {
                cmd.CommandText = "INSERT INTO GraphicsCards(Name,Price,ListPrice,BrandID,CategoryID,Description,stock,SStock,imgs,IsActived,IsDeleted,CreationTime,VRAM,Series,bitnumber,CompatibleConnect,Connects,StorgeType)" +
                    "Values(@n,@p,@lp,@bi,@ci,@d,@s,@ss,@is,@ia,@id,@ct,@VRAM,@Series,@bitnumber,@CompatibleConnect,@Connects,@StorgeType)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@is", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime); 
                cmd.Parameters.AddWithValue("@VRAM", p.VRAM);
                cmd.Parameters.AddWithValue("@Series", p.Series);
                cmd.Parameters.AddWithValue("@bitnumber", p.bitnumber);
                cmd.Parameters.AddWithValue("@CompatibleConnect", p.CompatibleConnect);
                cmd.Parameters.AddWithValue("@Connects", p.Connects);
                cmd.Parameters.AddWithValue("@StorgeType", p.StorgeType);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public ObservableCollection<GraphicsCard> ListGraphicsCard()
        {
            ObservableCollection<GraphicsCard> brotherList = new ObservableCollection<GraphicsCard>();
            try
            {
                cmd.CommandText = "Select * From GraphicsCards";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    GraphicsCard brother = new GraphicsCard();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Price = reader.GetDecimal(2);
                    brother.ListPrice = reader.GetDecimal(3);
                    brother.BrandID = reader.GetInt32(4);
                    brother.CategoryID = reader.GetInt32(5);
                    brother.Description = reader.GetString(6);
                    brother.stock = reader.GetInt32(7);
                    brother.SStock = reader.GetInt32(8);
                    brother.imgs = reader.GetString(9);
                    brother.VRAM = reader.IsDBNull(10) ? 0 : reader.GetInt32(10);
                    brother.Series = reader.IsDBNull(11) ? "" : reader.GetString(11);
                    brother.bitnumber = reader.IsDBNull(12) ? "" : reader.GetString(12);
                    brother.CompatibleConnect = reader.IsDBNull(13) ? "" : reader.GetString(13);
                    brother.Connects = reader.IsDBNull(14) ? "" : reader.GetString(14);
                    brother.StorgeType = reader.IsDBNull(15) ? "" : reader.GetString(15);
                    brother.IsActived = reader.GetBoolean(16);
                    brother.IsDeleted = reader.GetBoolean(17);
                    brother.CreationTime = reader.GetDateTime(18);
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

        #region Stores
        public ObservableCollection<Store> ListUsers()
        {
            ObservableCollection<Store> brotherList = new ObservableCollection<Store>();
            try
            {
                cmd.CommandText = "Select * From Stores";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Store brother = new Store();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Phone = reader.GetString(2);
                    brother.Tier = reader.GetInt32(4);
                    brother.Email = reader.GetString(3);
                    brother.IsBanned = reader.GetBoolean(5);
                    brother.SignUpTime = reader.GetDateTime(6);
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        public bool AddUser(Store c)
        {
            try
            {
                cmd.CommandText = "Insert Into Stores(Name,Phone,Email,Tier,IsBanned,SignUpTime) Values(@n,@p,@em,@t,@b,@sut)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", c.Name);
                cmd.Parameters.AddWithValue("@p", c.Phone);
                cmd.Parameters.AddWithValue("@em", c.Email);
                cmd.Parameters.AddWithValue("@t", c.Tier);
                cmd.Parameters.AddWithValue("@b", c.IsBanned);
                cmd.Parameters.AddWithValue("@sut", c.SignUpTime);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;

            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool DeleteUser(int UserID)
        {
            try
            {
                cmd.CommandText = "Delete From Stores Where ID = @ID ";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", UserID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateUser(Store c)
        {
            try
            {
                cmd.CommandText = "Update Stores SET Name =@n, Phone = @p, Email = @em, Tier = @t, IsBanned = @b, SignUpTime = @sut Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", c.Name);
                cmd.Parameters.AddWithValue("@p", c.Phone);
                cmd.Parameters.AddWithValue("@em", c.Email);
                cmd.Parameters.AddWithValue("@t", c.Tier);
                cmd.Parameters.AddWithValue("@b", c.IsBanned);
                cmd.Parameters.AddWithValue("@sut", c.SignUpTime);

                con.Open();
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return false;
        }
        #endregion

        #region Tiers
            
        public ObservableCollection<Tier> ListTier()
        {
            ObservableCollection<Tier> brotherList = new ObservableCollection<Tier>();
            try
            {
                cmd.CommandText = "Select * From Tiers";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Tier brother = new Tier();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Sale = reader.GetDecimal(2);
                    
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

        #region Processor
        public bool DeleteProcessor(int ID)
        {
            try
            {
                cmd.CommandText = "Delete From Processors Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateProcessor(Processor p)
        {
            try
            {
                cmd.CommandText = "Update Processors SET Name = @n, Price = @p, ListPrice = @lp, BrandID = @bi, CategoryID = @ci, Description = @d, stock = @s, SStock = @ss, imgs = @i, IsActived = @ia, IsDeleted = @id, CreationTime = @ct , MakximumFrequency = @MakximumFrequency ,L3Cache = @L3Cache, L2Cahce = @L2Cahce, NumberOfCores = @NumberOfCores, ClockFrequency = @ClockFrequency,BusSpeed = @BusSpeed,ProcessorType = @ProcessorType Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@i", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@ID", p.ID);
                cmd.Parameters.AddWithValue("@MakximumFrequency", p.MakximumFrequency);
                cmd.Parameters.AddWithValue("@L3Cache", p.L3Cache);
                cmd.Parameters.AddWithValue("@L2Cahce", p.L2Cahce);
                cmd.Parameters.AddWithValue("@NumberOfCores", p.NumberOfCores);
                cmd.Parameters.AddWithValue("@ClockFrequency", p.ClockFrequency);
                cmd.Parameters.AddWithValue("@BusSpeed", p.BusSpeed);
                cmd.Parameters.AddWithValue("@ProcessorType", p.ProcessorType);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool AddProcessor(Processor p)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Processors(Name,Price,ListPrice,BrandID,CategoryID,Description,stock,SStock,imgs,IsActived,IsDeleted,CreationTime,MakximumFrequency,L3Cache,L2Cahce,NumberOfCores,ClockFrequency,BusSpeed,ProcessorType)" +
                    "Values(@n,@p,@lp,@bi,@ci,@d,@s,@ss,@is,@ia,@id,@ct,@MakximumFrequency,@L3Cache,@L2Cahce,@NumberOfCores,@ClockFrequency,@BusSpeed,@ProcessorType)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@is", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@MakximumFrequency", p.MakximumFrequency);
                cmd.Parameters.AddWithValue("@L3Cache", p.L3Cache);
                cmd.Parameters.AddWithValue("@L2Cahce", p.L2Cahce);
                cmd.Parameters.AddWithValue("@NumberOfCores", p.NumberOfCores);
                cmd.Parameters.AddWithValue("@ClockFrequency", p.ClockFrequency);
                cmd.Parameters.AddWithValue("@BusSpeed", p.BusSpeed);
                cmd.Parameters.AddWithValue("@ProcessorType", p.ProcessorType);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public ObservableCollection<Processor> ListProcessor()
        {
            ObservableCollection<Processor> brotherList = new ObservableCollection<Processor>();
            try
            {
                cmd.CommandText = "Select * From Processors";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Processor brother = new Processor();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Price = reader.GetDecimal(2);
                    brother.ListPrice = reader.GetDecimal(3);
                    brother.BrandID = reader.GetInt32(4);
                    brother.CategoryID = reader.GetInt32(5);
                    brother.Description = reader.GetString(6);
                    brother.stock = reader.GetInt32(7);
                    brother.SStock = reader.GetInt32(8);
                    brother.imgs = reader.GetString(9);
                    brother.MakximumFrequency = reader.IsDBNull(10) ? 0 : reader.GetDouble(10);
                    brother.L3Cache = reader.IsDBNull(11) ? "" : reader.GetString(11);
                    brother.L2Cahce = reader.IsDBNull(12) ? "" : reader.GetString(12);
                    brother.NumberOfCores = reader.IsDBNull(13) ? "" : reader.GetString(13);
                    brother.ClockFrequency = reader.IsDBNull(14) ? 0 : reader.GetDouble(14);
                    brother.BusSpeed = reader.IsDBNull(15) ? "" : reader.GetString(15);
                    brother.ProcessorType = reader.IsDBNull(15) ? "" : reader.GetString(16);
                    brother.IsActived = reader.GetBoolean(17);
                    brother.IsDeleted = reader.GetBoolean(18);
                    brother.CreationTime = reader.GetDateTime(19);
                  
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

        #region MotherBoard
        public bool DeleteMotherboard(int ID)
        {
            try
            {
                cmd.CommandText = "Delete From Motherboards Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateMotherboard(Motherboard p)
        {
            try
            {
                cmd.CommandText = "Update Motherboards SET Name = @n, Price = @p, ListPrice = @lp, BrandID = @bi, CategoryID = @ci, Description = @d, stock = @s, SStock = @ss, imgs = @i, IsActived = @ia, IsDeleted = @id, CreationTime = @ct , MemoryTechnology = @MemoryTechnology ,MemorySlot = @MemorySlot, MemoryClockSpeed = @MemoryClockSpeed, PCIVersion = @PCIVersion, MultiGPU = @MultiGPU,ChipsetManufacturer = @ChipsetManufacturer Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@i", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@ID", p.ID);
                cmd.Parameters.AddWithValue("@MemoryTechnology", p.MemoryTechnology);
                cmd.Parameters.AddWithValue("@MemorySlot", p.MemorySlot);
                cmd.Parameters.AddWithValue("@MemoryClockSpeed", p.MemoryClockSpeed);
                cmd.Parameters.AddWithValue("@PCIVersion", p.PCIVersion);
                cmd.Parameters.AddWithValue("@MultiGPU", p.MultiGPU);
                cmd.Parameters.AddWithValue("@ChipsetManufacturer", p.ChipsetManufacturer);
      
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool AddMotherboard(Motherboard p)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Motherboards(Name,Price,ListPrice,BrandID,CategoryID,Description,stock,SStock,imgs,IsActived,IsDeleted,CreationTime,MemoryTechnology,MemorySlot,MemoryClockSpeed,PCIVersion,MultiGPU,ChipsetManufacturer)" +
                    "Values(@n,@p,@lp,@bi,@ci,@d,@s,@ss,@is,@ia,@id,@ct,@MemoryTechnology,@MemorySlot,@MemoryClockSpeed,@PCIVersion,@MultiGPU,@ChipsetManufacturer)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@is", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@MemoryTechnology", p.MemoryTechnology);
                cmd.Parameters.AddWithValue("@MemorySlot", p.MemorySlot);
                cmd.Parameters.AddWithValue("@MemoryClockSpeed", p.MemoryClockSpeed);
                cmd.Parameters.AddWithValue("@PCIVersion", p.PCIVersion);
                cmd.Parameters.AddWithValue("@MultiGPU", p.MultiGPU);
                cmd.Parameters.AddWithValue("@ChipsetManufacturer", p.ChipsetManufacturer);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public ObservableCollection<Motherboard> ListMotherboard()
        {
            ObservableCollection<Motherboard> brotherList = new ObservableCollection<Motherboard>();
            try
            {
                cmd.CommandText = "Select * From Motherboards";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Motherboard brother = new Motherboard();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Price = reader.GetDecimal(2);
                    brother.ListPrice = reader.GetDecimal(3);
                    brother.BrandID = reader.GetInt32(4);
                    brother.CategoryID = reader.GetInt32(5);
                    brother.Description = reader.GetString(6);
                    brother.stock = reader.GetInt32(7);
                    brother.SStock = reader.GetInt32(8);
                    brother.imgs = reader.GetString(9);
                    brother.MemoryTechnology = reader.IsDBNull(10) ? "NULL" : reader.GetString(10);
                    brother.MemorySlot = reader.IsDBNull(11) ? "0" : reader.GetString(11);
                    brother.MemoryClockSpeed = reader.IsDBNull(12) ? "0" : reader.GetString(12);
                    brother.PCIVersion = reader.IsDBNull(13) ? "0" : reader.GetString(13);
                    brother.MultiGPU = reader.IsDBNull(14) ? false : reader.GetBoolean(14);
                    brother.ChipsetManufacturer = reader.IsDBNull(15) ? "" : reader.GetString(15);
                    brother.IsActived = reader.GetBoolean(16);
                    brother.IsDeleted = reader.GetBoolean(17);
                    brother.CreationTime = reader.GetDateTime(18);
                 
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

        #region Memory
        public bool DeleteMemory(int ID)
        {
            try
            {
                cmd.CommandText = "Delete From Memories Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateMemory(Memory p)
        {
            try
            {
                cmd.CommandText = "Update Memories SET Name = @n, Price = @p, ListPrice = @lp, BrandID = @bi, CategoryID = @ci, Description = @d, stock = @s, SStock = @ss, imgs = @i, IsActived = @ia, IsDeleted = @id, CreationTime = @ct , MemoryType = @MemoryType ,CapacityPerModule = @CapacityPerModule, MemoryFrequency = @MemoryFrequency, TotalMemoryCapacity = @TotalMemoryCapacity Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@i", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@ID", p.ID);
                cmd.Parameters.AddWithValue("@MemoryType", p.MemoryType);
                cmd.Parameters.AddWithValue("@CapacityPerModule", p.CapacityPerModule);
                cmd.Parameters.AddWithValue("@MemoryFrequency", p.MemoryFrequency);
                cmd.Parameters.AddWithValue("@TotalMemoryCapacity", p.TotalMemoryCapacity);

                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool AddMemories(Memory p)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Memories(Name,Price,ListPrice,BrandID,CategoryID,Description,stock,SStock,imgs,IsActived,IsDeleted,CreationTime,MemoryType,CapacityPerModule,MemoryFrequency,TotalMemoryCapacity)" +
                    "Values(@n,@p,@lp,@bi,@ci,@d,@s,@ss,@is,@ia,@id,@ct,@MemoryType,@CapacityPerModule,@MemoryFrequency,@TotalMemoryCapacity)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@is", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@MemoryType", p.MemoryType);
                cmd.Parameters.AddWithValue("@CapacityPerModule", p.CapacityPerModule);
                cmd.Parameters.AddWithValue("@MemoryFrequency", p.MemoryFrequency);
                cmd.Parameters.AddWithValue("@TotalMemoryCapacity", p.TotalMemoryCapacity);
        
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public ObservableCollection<Memory> ListMemories()
        {
            ObservableCollection<Memory> brotherList = new ObservableCollection<Memory>();
            try
            {
                cmd.CommandText = "Select * From Memories";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Memory brother = new Memory();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Price = reader.GetDecimal(2);
                    brother.ListPrice = reader.GetDecimal(3);
                    brother.BrandID = reader.GetInt32(4);
                    brother.CategoryID = reader.GetInt32(5);
                    brother.Description = reader.GetString(6);
                    brother.stock = reader.GetInt32(7);
                    brother.SStock = reader.GetInt32(8);
                    brother.imgs = reader.GetString(9);
                    brother.MemoryType = reader.IsDBNull(10) ? "NULL" : reader.GetString(10);
                    brother.CapacityPerModule = reader.IsDBNull(11) ? "0" : reader.GetString(11);
                    brother.MemoryFrequency = reader.IsDBNull(12) ? "0" : reader.GetString(12);
                    brother.TotalMemoryCapacity = reader.IsDBNull(13) ? "0" : reader.GetString(13);
                    brother.IsActived = reader.GetBoolean(14);
                    brother.IsDeleted = reader.GetBoolean(15);
                    brother.CreationTime = reader.GetDateTime(16);

                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

        #region Storage
        public bool DeleteStorage(int ID)
        {
            try
            {
                cmd.CommandText = "Delete From Storages Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ID", ID);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
        }
        public bool UpdateStorage(Storage p)
        {
            try
            {
                cmd.CommandText = "Update Storages SET Name = @n, Price = @p, ListPrice = @lp, BrandID = @bi, CategoryID = @ci, Description = @d, stock = @s, SStock = @ss, imgs = @i, IsActived = @ia, IsDeleted = @id, CreationTime = @ct , StorageCapacity = @StorageCapacity ,SequentialWriting = @SequentialWriting, SequentialReading = @SequentialReading, Height = @Height, Width = @Width, Type = @Type,connections = @connections Where ID = @ID";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@i", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@ID", p.ID); 
                cmd.Parameters.AddWithValue("@Type", p.Type);
                cmd.Parameters.AddWithValue("@StorageCapacity", p.StorageCapacity);
                cmd.Parameters.AddWithValue("@SequentialReading", p.SequentialReading);
                cmd.Parameters.AddWithValue("@SequentialWriting", p.SequentialWriting);
                cmd.Parameters.AddWithValue("@Height", p.Height);
                cmd.Parameters.AddWithValue("@Width", p.Width);
                cmd.Parameters.AddWithValue("@connections", p.connections);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public bool AddStorage(Storage p)
        {
            try
            {
                cmd.CommandText = "INSERT INTO Storages(Name,Price,ListPrice,BrandID,CategoryID,Description,stock,SStock,imgs,IsActived,IsDeleted,CreationTime,StorageCapacity,SequentialReading,SequentialWriting,Height,Width,Type,connections)" +
                    "Values(@n,@p,@lp,@bi,@ci,@d,@s,@ss,@is,@ia,@id,@ct,@StorageCapacity,@SequentialReading,@SequentialWriting,@Height,@Width,@Type,@connections)";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@n", p.Name);
                cmd.Parameters.AddWithValue("@p", p.Price);
                cmd.Parameters.AddWithValue("@lp", p.ListPrice);
                cmd.Parameters.AddWithValue("@bi", p.BrandID);
                cmd.Parameters.AddWithValue("@ci", p.CategoryID);
                cmd.Parameters.AddWithValue("@d", p.Description);
                cmd.Parameters.AddWithValue("@s", p.stock);
                cmd.Parameters.AddWithValue("@ss", p.SStock);
                cmd.Parameters.AddWithValue("@is", p.imgs);
                cmd.Parameters.AddWithValue("@ia", p.IsActived);
                cmd.Parameters.AddWithValue("@id", p.IsDeleted);
                cmd.Parameters.AddWithValue("@ct", p.CreationTime);
                cmd.Parameters.AddWithValue("@Type", p.Type);
                cmd.Parameters.AddWithValue("@StorageCapacity", p.StorageCapacity);
                cmd.Parameters.AddWithValue("@SequentialReading", p.SequentialReading);
                cmd.Parameters.AddWithValue("@SequentialWriting", p.SequentialWriting);
                cmd.Parameters.AddWithValue("@Height", p.Height);
                cmd.Parameters.AddWithValue("@Width", p.Width);
                cmd.Parameters.AddWithValue("@connections", p.connections);
                con.Open();
                cmd.ExecuteNonQuery();
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
            finally
            {
                con.Close();
            }
        }
        public ObservableCollection<Storage> ListStorage()
        {
            ObservableCollection<Storage> brotherList = new ObservableCollection<Storage>();
            try
            {
                cmd.CommandText = "Select * From Storages";
                cmd.Parameters.Clear();
                con.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Storage brother = new Storage();
                    brother.ID = reader.GetInt32(0);
                    brother.Name = reader.GetString(1);
                    brother.Price = reader.GetDecimal(2);
                    brother.ListPrice = reader.GetDecimal(3);
                    brother.BrandID = reader.GetInt32(4);
                    brother.CategoryID = reader.GetInt32(5);
                    brother.Description = reader.GetString(6);
                    brother.stock = reader.GetInt32(7);
                    brother.SStock = reader.GetInt32(8);
                    brother.imgs = reader.GetString(9);
                    brother.StorageCapacity = reader.IsDBNull(10) ? "0MB" : reader.GetString(10);
                    brother.SequentialWriting = reader.IsDBNull(11) ? "0MB" : reader.GetString(11);
                    brother.SequentialReading = reader.IsDBNull(12) ? "0MB" : reader.GetString(12);
                    brother.Height = reader.IsDBNull(13) ? "0mm" : reader.GetString(13);
                    brother.Width = reader.IsDBNull(14) ? "0mm" : reader.GetString(14);
                    brother.IsActived = reader.GetBoolean(15);
                    brother.IsDeleted = reader.GetBoolean(16);
                    brother.CreationTime = reader.GetDateTime(17);
                    brother.Type = reader.GetString(18);
                    brother.connections = reader.GetString(19);
                    brotherList.Add(brother);
                }
                return brotherList;

            }
            catch (Exception)
            {

            }
            finally
            {
                con.Close();
            }
            return null;
        }
        #endregion

    }
}
