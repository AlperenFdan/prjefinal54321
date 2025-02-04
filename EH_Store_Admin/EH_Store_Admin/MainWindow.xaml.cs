using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;

using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml;
using System.Xml.Linq;
using ADOCOKUGRASTIRIYOR;
using Microsoft.Win32;


namespace EH_Store_Admin
{
    public partial class MainWindow : Window
    {

        private Storyboard LoginAnimate;
        private Storyboard ErrorAnimate;
        private Storyboard Adm;


        DataRegion IHateAdo = new DataRegion();
        GraphicsCard UpdatingProduct = new GraphicsCard();
        Category UpdatingCategory = new Category();
        Store UpdatingUser = new Store();
        Brand UpdatingBrand = new Brand();
        Memory UpdatingMemory = new Memory();
        Motherboard UpdatingMotherboard = new Motherboard();
        Storage UpdatingStorage = new Storage();
        Processor UpdatingProcessor = new Processor();
        string MY = "C:\\Users\\lastg\\Desktop\\prjefinal54321\\Pictures\\";
         ObservableCollection<GraphicsCard> ProductsDataOC { get; set; }
         ObservableCollection<Brand> BrandsDataOC { get; set; }
         ObservableCollection<Category> CategoriesDataOC { get; set; }
         ObservableCollection<Store> UsersDataOC { get; set; }
         ObservableCollection<Processor> ProcessorOC { get; set; }
         ObservableCollection<Memory> MemoriesDataOC { get; set; }
         ObservableCollection<Motherboard> MotherboardDataOC { get; set; }
         ObservableCollection<Storage> StoragesDataOC { get; set; }

        bool IsDeleted = false;
        bool IsItBusy = false;
        bool IsUpdate = false;

        string AnimateSelection = "";
        string SelectedCurrentFileSaving = "";
        string SelectedCurrentFile = "";
        public MainWindow()
        {
            InitializeComponent(); 
            LoadingAppScreen.Visibility = Visibility.Visible;
            LoginAnimate = (Storyboard)FindResource("MyFadeOutStoryboard");
            ErrorAnimate = (Storyboard)FindResource("MyFadeOutErrorMassage");
            Adm = (Storyboard)FindResource("FadeAdminMessage");
            EmailTeaxtbox.Focus();
        }


      
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        

        #region - Login
        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
         
            if (e.Key == Key.Enter)
            {
            
                LoginCik();

               
            }
        }
        private void LoginCik()
        {
            Emp emp = IHateAdo.LoginControl(EmailTeaxtbox.Text, PasswordTeaxtbox.Text);
            if (emp != null)
            {
                CurrentAccount.ID = emp.ID;
                CurrentAccount.Name = emp.Name;
                CurrentAccount.SurnName = emp.SurnName;
                CurrentAccount.Email = emp.Email;
                CurrentAccount.Password = emp.Password;
                CurrentAccount.Phone = emp.Phone;
       
                MessageBox.Show($"Login Succes \n(Current Account; {CurrentAccount.Name} {CurrentAccount.SurnName})");
                LoginAnimate.Begin(LoadingAppScreen);
           
                MainCanvas.Visibility = Visibility.Visible;
                ProductsDataOC = new ObservableCollection<GraphicsCard>(IHateAdo.ListGraphicsCard());
                BrandsDataOC = new ObservableCollection<Brand>(IHateAdo.ListBrands());
                CategoriesDataOC = new ObservableCollection<Category>(IHateAdo.ListCategory());
                UsersDataOC = new ObservableCollection<Store>(IHateAdo.ListUsers());
                ProcessorOC = new ObservableCollection<Processor>(IHateAdo.ListProcessor());
                StoragesDataOC = new ObservableCollection<Storage>(IHateAdo.ListStorage());
                MotherboardDataOC = new ObservableCollection<Motherboard>(IHateAdo.ListMotherboard());
                MemoriesDataOC = new ObservableCollection<Memory>(IHateAdo.ListMemories());
                ProcessorDG.ItemsSource = ProcessorOC;
                ProductsD.ItemsSource = ProductsDataOC;
                CategoryDG.ItemsSource = CategoriesDataOC;
                BrandDG.ItemsSource = BrandsDataOC;
                UsersDG.ItemsSource = UsersDataOC;
                MemoriesDG.ItemsSource = MemoriesDataOC;
                MotherboardDG.ItemsSource = MotherboardDataOC;
                StorageDG.ItemsSource = StoragesDataOC;
            }
            else
            {
                if (ErrorMassage.Visibility == Visibility.Collapsed)
                {
                    ErrorMassage.Visibility = Visibility.Visible;
                    DispatcherTimer tm = new DispatcherTimer();
                    tm.Interval = TimeSpan.FromSeconds(2);
                    tm.Tick += AfterDeleteErrorMessage;
                    tm.IsEnabled = true;
                    AnimateSelection = "Login";
                    tm.Start();
                }
            }
        }

        private void LoginPageFadeOut(object sender, EventArgs e)
        {
            LoadingAppScreen.Visibility = Visibility.Collapsed;
            AdminMessage.Visibility = Visibility.Visible;
            Adm.Begin(AdminMessage);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            LoginCik();



        }

        private void AfterDeleteErrorMessage(object sender, EventArgs e)
        {
          
            if (AnimateSelection == "Login")
            {

                ErrorAnimate.Begin(ErrorMassage);
            }
            else if (AnimateSelection == "ChangeData")
            {
                ErrorAnimate.Begin(ErrorMassageTabItem);
            }
            DispatcherTimer tm = (DispatcherTimer)sender;
            tm.IsEnabled = false;
            tm.Stop();
            tm.Tick -= AfterDeleteErrorMessage;
            

        }

        private void ErrorMassageFadaeOut(object sender, EventArgs e)
        {
            ErrorAnimate.Stop();
            if (AnimateSelection == "Login")
            {
             
                ErrorMassage.Opacity = 1; ErrorMassage.Visibility = Visibility.Collapsed;
            }
            else if (AnimateSelection == "ChangeData")
            {
                ErrorMassageTabItem.Opacity = 1; ErrorMassageTabItem.Visibility = Visibility.Collapsed;
            }
           

        }

        #endregion

        #region - Category
        private void DeleteCategories_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                CategoryDG.IsReadOnly = false;
                DeleteCategories.Click -= DeleteCategories_Click;
                DeleteCategories.Click += CancelDeleteForCategory;
                IsItBusy = true;
                IsDeleted = true;
                TabItemControl(false);
                DeleteCategories.Content = "İptal et";
            }
        }
        private void CancelDeleteForCategory(object sender, RoutedEventArgs e)
        {
            CategoryDG.IsReadOnly = true;
            DeleteCategories.Click -= CancelDeleteForCategory;
            DeleteCategories.Click += DeleteCategories_Click;
            IsItBusy = false;
            IsDeleted = false; 
            TabItemControl(true);
            DeleteCategories.Content = "Kategori sil";
        }

        private void CategoryDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsUpdate)
            {

             

                var selectedCellInfo = CategoryDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {
                    ProcessCancelForCategoriesEditBtn.Visibility = Visibility.Visible;

                    Category selectedRowItem = selectedCellInfo.Item as Category;

                    EditCategoriesBtn.Click -= EditCategoriesBtn_Click;
                    EditCategoriesBtn.Click += UpdateCategoryBtn;
                    EditCategoriesBtn.Content = "Kategoriyi Kaydet?";
                    UpdatingCategory = selectedRowItem;


                    int rowIndex = CategoryDG.Items.IndexOf(selectedRowItem);

                    CategoriesDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {
              
       

                var selectedCellInfo = CategoryDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Category selectedRowItem = selectedCellInfo.Item as Category;

                    int rowIndex = CategoryDG.Items.IndexOf(selectedRowItem);

                    CategoriesDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li Ürünü Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        CategoryDG.IsReadOnly = true;
                        CategoriesDataOC.Remove(selectedRowItem);
                        IsDeleted = false; Uyarici();
                        IsItBusy = false; TabItemControl(true);
                        DeleteCategories.Click -= CancelDeleteForCategory;
                        DeleteCategories.Click += DeleteCategories_Click;
                        DeleteCategories.Content = "Kategori Sil";
                        if (IHateAdo.DeleteCategory(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        CategoriesDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }
        private void UpdateCategoryBtn(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingCategory))
            {
                if (IHateAdo.UpdateCategory(UpdatingCategory))
                {
                    MessageBox.Show("İşlem Başarılı"); Uyarici();
                }
                else
                {
                    MessageBox.Show("İşlem Başarısız");
                }
                ProcessCancelForCategoriesEditBtn.Visibility = Visibility.Collapsed;
                EditCategoriesBtn.Content = "Kategoriyi Düzenle";
                EditCategoriesBtn.Click -= UpdateCategoryBtn;
                EditCategoriesBtn.Click += EditCategoriesBtn_Click;
                IsUpdate = false;
                CategoryDG.IsReadOnly = true;
                IsItBusy = false;
                TabItemControl(true);
                foreach (Category item in CategoriesDataOC)
                {

                    item.IsItReadOnly = false;

                }
            }
            else
            {
                MessageBox.Show("Lütfen Boşlukları doldurunuz!");
            }

        }
        private void EditCategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForCategoriesEditBtn.Visibility = Visibility.Visible;
                CategoryDG.IsReadOnly = false;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);

            }
            else
            {
                MessageBox.Show("Şuan Başka bir işlem gerçekleştiriyorsun!");
            }

        }

        private void ProcessCancelForCategoriesEditBtn_Click(object sender, RoutedEventArgs e)
        {
            CategoryDG.IsReadOnly = true;
            ProcessCancelForCategoriesEditBtn.Visibility = Visibility.Collapsed;
            EditCategoriesBtn.Content = "Kategoriyi Düzenle"; 
            EditCategoriesBtn.Click -= EditCategoriesBtn_Click;
            EditCategoriesBtn.Click -= UpdateCategoryBtn;
            EditCategoriesBtn.Click += EditCategoriesBtn_Click;
            IsUpdate = false;
            IsItBusy = false; TabItemControl(true);
            foreach (Category item in CategoriesDataOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingCategory = new Category();
            CategoriesDataOC.Clear();
            AddCategoriesForSource();
        }
        private void AddCategoriesForSource()
        {
            foreach (Category item in IHateAdo.ListCategory())
            {
                CategoriesDataOC.Add(item);
            }
        }
        private void AddNewItemToSqlCategories(object sender, RoutedEventArgs e)
        {

            Category NewPr = CategoriesDataOC[CategoriesDataOC.Count - 1];
            
            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewCategoriesBtn.Click -= AddNewItemToSqlCategories;
                    AddNewCategoriesBtn.Click += AddNewCategoriesBtn_Click;
                    CategoryDG.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddCategory(NewPr))
                    {
                        MessageBox.Show("Başarıyla eklendi!");  
                        CategoriesDataOC[CategoriesDataOC.Count - 1].IsItReadOnly = false;
                        CategoriesDataOC.Clear(); Uyarici();
                        AddCategoriesForSource();
                        CancelProcessForCategories.Visibility = Visibility.Collapsed;
                        AddNewCategoriesBtn.Content = "Yeni Kategori Ekle";
                    }
                    else
                    {
                        MessageBox.Show("Bir yerde sorun var");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
            }
        }
        private void AddNewCategoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                CategoriesDataOC.Add(new Category());
                foreach (var item in CategoriesDataOC)
                {

                    if (CategoriesDataOC[CategoriesDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                CancelProcessForCategories.Visibility = Visibility.Visible;
                AddNewCategoriesBtn.Click -= AddNewCategoriesBtn_Click;
                AddNewCategoriesBtn.Click += AddNewItemToSqlCategories;
                AddNewCategoriesBtn.Content = "Kategoriyi ekle?";
                IsItBusy = true;
                CategoryDG.IsReadOnly = false;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Bir işlem yapıyorusnuz!");
            }
        }

        private void CancelProcessForCategories_Click(object sender, RoutedEventArgs e)
        {
            Category NewPr = CategoriesDataOC[CategoriesDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    CategoryDG.IsReadOnly = true;
                    CategoriesDataOC.Remove(NewPr);
                    CategoriesDataOC.Clear();
                    AddCategoriesForSource();
                    CancelProcessForCategories.Visibility = Visibility.Collapsed;
                    AddNewCategoriesBtn.Click -= AddNewItemToSqlCategories;
                    AddNewCategoriesBtn.Click += AddNewCategoriesBtn_Click;
                    AddNewCategoriesBtn.Content = "Yeni Kategori ekle";
                    IsItBusy = false; 
                    TabItemControl(true);
                }

            }
        }

        #endregion

        #region - Brands

        private void DeleteBrand_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                DeleteBrand.Content = "İptal";
                IsItBusy = true;
                BrandDG.IsReadOnly = false;
                IsDeleted = true;
                TabItemControl(false);
                DeleteBrand.Click -= DeleteBrand_Click;
                DeleteBrand.Click += ProcessCancelForDeleteBrand;
            }
        }

        private void ProcessCancelForDeleteBrand(object sender, RoutedEventArgs e)
        {
            DeleteBrand.Content = "Marka Sil";
            IsItBusy = false;
            BrandDG.IsReadOnly = true;
            IsDeleted = false;
            TabItemControl(true);
            DeleteBrand.Click -= ProcessCancelForDeleteBrand;
            DeleteBrand.Click += DeleteBrand_Click;
        }

        private void BrandDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsUpdate)
            {

            
                var selectedCellInfo = BrandDG.SelectedCells.FirstOrDefault();
                ProcessCancelForBrandEditBtn.Visibility = Visibility.Visible;

                if (selectedCellInfo.Item != null)
                {

                    Brand selectedRowItem = selectedCellInfo.Item as Brand;

                    EditBrandBtn.Click -= EditBrandBtn_Click;
                    EditBrandBtn.Click += UpdateBrandBtn;
                    EditBrandBtn.Content = "Markayı Kaydet?";
                    UpdatingBrand = selectedRowItem;


                    int rowIndex = BrandDG.Items.IndexOf(selectedRowItem);

                    BrandsDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {
              


                var selectedCellInfo = BrandDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Brand selectedRowItem = selectedCellInfo.Item as Brand;

                    int rowIndex = BrandDG.Items.IndexOf(selectedRowItem);

                    ProductsDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li Markayı Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        BrandsDataOC.Remove(selectedRowItem);
                        IsDeleted = false;
                        Uyarici();
                        IsItBusy = false;
                        BrandDG.IsReadOnly = true;
                        TabItemControl(true);
                        DeleteBrand.Click -= ProcessCancelForDeleteBrand;
                        DeleteBrand.Click += DeleteBrand_Click;
                        DeleteBrand.Content = "Marka Sil";
                        if (IHateAdo.DeleteBrand(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!"); 
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        BrandsDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }

        private void EditBrandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForBrandEditBtn.Visibility = Visibility.Visible;
                IsItBusy = true;
                IsUpdate = true;
                BrandDG.IsReadOnly = false;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Başka bir İşlem yapıyorsun!1");
            }
        }

        public void UpdateBrandBtn(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingBrand))
            {
                if (IHateAdo.UpdateBrands(UpdatingBrand))
                {
                    MessageBox.Show("İşlem Başarılı"); Uyarici();
                }
                else
                {
                    MessageBox.Show("İşlem Başarısız");
                }
                ProcessCancelForBrandEditBtn.Visibility = Visibility.Collapsed;
                EditBrandBtn.Content = "Marka Düzenle";
                EditBrandBtn.Click -= UpdateBrandBtn;
                EditBrandBtn.Click += EditBrandBtn_Click;
                IsUpdate = false;
                BrandDG.IsReadOnly = true;

                IsItBusy = false; 
                TabItemControl(true);
                foreach (Brand item in BrandsDataOC)
                {

                    item.IsItReadOnly = false;

                }
            }
            else
            {
                MessageBox.Show("Lütfen Boşlukları doldurunuz!");
            }
        }

        private void ProcessCancelForEditBrand(object sender, RoutedEventArgs e)
        {
            IsItBusy = false;
            BrandDG.IsReadOnly = true;
            ProcessCancelForBrandEditBtn.Visibility = Visibility.Collapsed;
            IsUpdate = false;
            TabItemControl(true);
            EditBrandBtn.Content = "Marka Düzenle";
            EditBrandBtn.Click -= EditBrandBtn_Click;
            EditBrandBtn.Click -= UpdateBrandBtn;
            EditBrandBtn.Click += EditBrandBtn_Click;
        }
        private void AddNewBrandBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                BrandsDataOC.Add(new Brand());
                foreach (var item in BrandsDataOC)
                {

                    if (BrandsDataOC[BrandsDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                CancelProcessForBrand.Visibility = Visibility.Visible;
                AddNewBrandBtn.Click -= AddNewBrandBtn_Click;
                AddNewBrandBtn.Click += AddNewBrandToSql;
                AddNewBrandBtn.Content = "Markayı ekle?";
                IsItBusy = true;
                BrandDG.IsReadOnly = false;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Bir işlem yapıyorusnuz!");
            }
        }
        private void RefershBrands()
        {
            foreach (Brand item in IHateAdo.ListBrands())
            {
                BrandsDataOC.Add(item);
            }
        }
        private void CancelProcessForBrand_Click(object sender, RoutedEventArgs e)
        {

            Brand NewPr = BrandsDataOC[BrandsDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    BrandsDataOC.Remove(NewPr);
                    BrandsDataOC.Clear();
                    RefershBrands();
                    BrandDG.IsReadOnly = true;
                    CancelProcessForBrand.Visibility = Visibility.Collapsed;
                    AddNewBrandBtn.Click -= AddNewBrandToSql;
                    AddNewBrandBtn.Click += AddNewBrandBtn_Click;
                    IsItBusy = false; TabItemControl(true); AddNewBrandBtn.Content = "Yeni Marka Ekle";
                }

            }
        }
        private void AddNewBrandToSql(object sender, RoutedEventArgs e)
        {
            Brand NewPr = BrandsDataOC[BrandsDataOC.Count - 1];

            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewBrandBtn.Click -= AddNewBrandToSql;
                    AddNewBrandBtn.Click += AddNewBrandBtn_Click;
                    BrandDG.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddBrand(NewPr))
                    {
                        MessageBox.Show("Başarıyla eklendi!");
                        BrandsDataOC[BrandsDataOC.Count - 1].IsItReadOnly = false;
                        BrandsDataOC.Clear();
                        Uyarici();
                        RefershBrands();
                        CancelProcessForBrand.Visibility = Visibility.Collapsed;
                        AddNewBrandBtn.Content = "Yeni Marka Ekle";
                    }
                    else
                    {
                        MessageBox.Show("Bir yerde sorun var");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
            }
        }
        #endregion

        #region - GraphicsCards
        private void NewItemClick(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                ProductsDataOC.Add(new GraphicsCard());
                foreach (var item in ProductsDataOC)
                {

                    if (ProductsDataOC[ProductsDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                ProductsD.IsReadOnly = false;
                CancelProcessForProduct.Visibility = Visibility.Visible;
                AddNewProductBtn.Click -= NewItemClick;
                AddNewProductBtn.Click += AddNewItemToSqlProduct;
                AddNewProductBtn.Content = "Ürünü ekle?";
                IsItBusy = true; TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Bir işlem yapıyorusnuz!");
            }

        }
        private void AddProductForSource()
        {
            foreach (GraphicsCard item in IHateAdo.ListGraphicsCard())
            {
                ProductsDataOC.Add(item);
            }
        }
        private void AddNewItemToSqlProduct(object sender, RoutedEventArgs e)
        {

            GraphicsCard NewPr = ProductsDataOC[ProductsDataOC.Count - 1];
          
            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewProductBtn.Click -= AddNewItemToSqlProduct;
                    AddNewProductBtn.Click += NewItemClick;
                    ProductsD.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddGraphicsCard(NewPr))
                    {
                        if (!string.IsNullOrEmpty(SelectedCurrentFile))
                        {
                            FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                            fileInfo.CopyTo(SelectedCurrentFileSaving);
                            SelectedCurrentFileSaving = null;
                            SelectedCurrentFile = null;
                        }
                        MessageBox.Show("Başarıyla eklendi!");
                        ProductsDataOC[ProductsDataOC.Count - 1].IsItReadOnly = false;
                        ProductsDataOC.Clear();
                        Uyarici();
                        AddProductForSource();
                        CancelProcessForProduct.Visibility = Visibility.Collapsed;
                        AddNewProductBtn.Content = "Yeni Ürün Ekle";
                    }
                    else
                    {
                        MessageBox.Show("Bir yerde sorun var");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
            }
        }

        private void CancelProcessForEdit_Click(object sender, RoutedEventArgs e)
        {
            ProcessCancelForEditBtn.Visibility = Visibility.Collapsed;
            EditProductBtn.Content = "Ürün Düzenle";
            EditProductBtn.Click -= EditProductBtn_Click;
            EditProductBtn.Click -= UpdateItemForEditPbtn;
            EditProductBtn.Click += EditProductBtn_Click;
            IsUpdate = false;
            IsItBusy = false; TabItemControl(true);
            ProductsD.IsReadOnly = true;
            foreach (GraphicsCard item in ProductsDataOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingProduct = new GraphicsCard();
            ProductsDataOC.Clear();
            AddProductForSource();

        }
        private void UpdateItemForEditPbtn(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingProduct))
            {
                if (IHateAdo.UpdateGraphicsCard(UpdatingProduct))
                {
                    MessageBox.Show("İşlem Başarılı");
                    if (!string.IsNullOrEmpty(SelectedCurrentFile))
                    {
                        FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                        fileInfo.CopyTo(SelectedCurrentFileSaving);
                        SelectedCurrentFileSaving = null;
                        SelectedCurrentFile = null;
                    }
                    Uyarici();
                }
                else
                {
                    MessageBox.Show("İşlem Başarısız");
                }
                ProductsD.IsReadOnly = true;
                ProcessCancelForEditBtn.Visibility = Visibility.Collapsed;
                EditProductBtn.Content = "Ürün Düzenle";
                EditProductBtn.Click -= UpdateItemForEditPbtn;
                EditProductBtn.Click += EditProductBtn_Click;
                IsUpdate = false;
                IsItBusy = false;
                TabItemControl(true);
                foreach (GraphicsCard item in ProductsDataOC)
                {

                    item.IsItReadOnly = false;

                }
                UpdatingProduct = new GraphicsCard();
                ProductsDataOC.Clear();
                AddProductForSource();
            }
            else
            {
                MessageBox.Show("Lütfen Boşlukları doldurunuz!");
            }

        }
        private void EditProductBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForEditBtn.Visibility = Visibility.Visible;
                ProductsD.IsReadOnly = false;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuan Başka bir işlem gerçekleştiriyorsun!");
            }
        }


        private void CancelProcessForProduct_Click(object sender, RoutedEventArgs e)
        {

            GraphicsCard NewPr = ProductsDataOC[ProductsDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    ProductsDataOC.Remove(NewPr);
                    ProductsDataOC.Clear();
                    AddProductForSource(); 
                    ProductsD.IsReadOnly = true;
                    CancelProcessForProduct.Visibility = Visibility.Collapsed;
                    AddNewProductBtn.Click -= AddNewItemToSqlProduct;
                    AddNewProductBtn.Click += NewItemClick;
                    IsItBusy = false; 
                    TabItemControl(true); 
                    AddNewProductBtn.Content = "Yeni Ürün Ekle";
                }

            }
        }

        private void DeleteProduct_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                IsItBusy = true;
                IsDeleted = true; TabItemControl(false);
                DeleteProduct.Content = "İptal et";
                DeleteProduct.Click -= DeleteProduct_Click;
                DeleteProduct.Click += ProcessCancelForDelete;
            }
        }
        private void ProcessCancelForDelete(object sender, RoutedEventArgs e)
        {
            IsItBusy = false;
            IsDeleted = false; TabItemControl(true);
            DeleteProduct.Content = "Ürünü Sil";
            DeleteProduct.Click -= ProcessCancelForDelete;
            DeleteProduct.Click += DeleteProduct_Click;
        }

        private void ProductsD_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsUpdate)
            {

               

                var selectedCellInfo = ProductsD.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {


                    GraphicsCard selectedRowItem = selectedCellInfo.Item as GraphicsCard;

                    EditProductBtn.Click -= EditProductBtn_Click;
                    EditProductBtn.Click += UpdateItemForEditPbtn;
                    EditProductBtn.Content = "Ürünü Kaydet?";
                    UpdatingProduct = selectedRowItem;
                    ProcessCancelForEditBtn.Visibility = Visibility.Visible;

                    int rowIndex = ProductsD.Items.IndexOf(selectedRowItem);

                    ProductsDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false; 
                }
            }
            else if (IsDeleted)
            {
               


                var selectedCellInfo = ProductsD.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    GraphicsCard selectedRowItem = selectedCellInfo.Item as GraphicsCard;

                    int rowIndex = ProductsD.Items.IndexOf(selectedRowItem);

                    ProductsDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li Ürünü Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        ProductsDataOC.Remove(selectedRowItem);
                        IsDeleted = false; 
                        Uyarici();
                        IsItBusy = false;
                        TabItemControl(true);
                        ProductsD.IsReadOnly = true;
                        DeleteProduct.Click -= ProcessCancelForDelete;
                        DeleteProduct.Click += DeleteProduct_Click;
                        DeleteProduct.Content = "Ürünü Sil";
                        if (IHateAdo.DeleteGraphicsCard(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        ProductsDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }

        private void ImageChange_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            GraphicsCard currentItem = button.DataContext as GraphicsCard;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                ProductsDataOC.FirstOrDefault(x => x.ID == currentItem.ID).imgs = guid + fileInfo.Extension;
                button.Content = guid + fileInfo.Extension;
                
                SelectedCurrentFileSaving = MY + guid + fileInfo.Extension;
                SelectedCurrentFile = openFileDialog.FileName;

            }
        }
        #endregion

        #region - Stores
        private void RefersUsers()
        {
            foreach (Store item in IHateAdo.ListUsers())
            {
                UsersDataOC.Add(item);
            }
        }
        private void AddNewMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                UsersDataOC.Add(new Store());
                foreach (var item in UsersDataOC)
                {

                    if (UsersDataOC[UsersDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                CancelProcessForMember.Visibility = Visibility.Visible;
                AddNewMemberBtn.Click -= AddNewMemberBtn_Click;
                AddNewMemberBtn.Click += AddNewUserToSql;
                AddNewMemberBtn.Content = "Bayiyi ekle?";
                IsItBusy = true;
                UsersDG.IsReadOnly = false;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Bir işlem yapıyorusnuz!");
            }
        }
        private void CancelProcessForMember_Click(object sender, RoutedEventArgs e)
        {
            Store NewPr = UsersDataOC[UsersDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    UsersDG.IsReadOnly = true;
                    UsersDataOC.Remove(NewPr);
                    UsersDataOC.Clear();
                    RefersUsers();
                    CancelProcessForMember.Visibility = Visibility.Collapsed;
                    AddNewMemberBtn.Click -= AddNewUserToSql; AddNewMemberBtn.Content = "Yeni Bayi Ekle";
                    AddNewMemberBtn.Click += AddNewMemberBtn_Click;
                    IsItBusy = false; TabItemControl(true);
                }

            }
        }
        private void AddNewUserToSql(object sender, RoutedEventArgs e)
        {
            Store NewPr = UsersDataOC[UsersDataOC.Count - 1];


            if (MyLibrary.PropControl(NewPr))
            {

                AddNewMemberBtn.Click -= AddNewUserToSql;
                AddNewMemberBtn.Click += AddNewMemberBtn_Click;
                AddNewMemberBtn.Content = "Yeni Bayi Ekle";
                IsItBusy = false;
                UsersDG.IsReadOnly = true;
                TabItemControl(true);
                if (IHateAdo.AddUser(NewPr))
                {
                    MessageBox.Show("Başarıyla eklendi!");
                    UsersDataOC[UsersDataOC.Count - 1].IsItReadOnly = false;
                    UsersDataOC.Clear();
                    RefersUsers(); Uyarici();
                    CancelProcessForMember.Visibility = Visibility.Collapsed;
                   
                }
                else
                {
                    MessageBox.Show("Bir yerde sorun var");
                }

            }
            else
            {
                MessageBox.Show("Lütfen tüm boşluklları kontrol ediniz!");
            }
          
          
        }
        private void UpdateMember(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingUser))
            {
                if (IHateAdo.UpdateUser(UpdatingUser))
                {
                    MessageBox.Show("İşlem Başarılı"); 
                    Uyarici();
                }
                else
                {
                    MessageBox.Show("İşlem Başarısız");
                }
                ProcessCancelForMemberEditBtn.Visibility = Visibility.Collapsed;
                EditMemberBtn.Content = "Bayi Düzenle";
                EditMemberBtn.Click -= UpdateMember;
                EditMemberBtn.Click += EditMemberBtn_Click;
                IsUpdate = false;
                IsItBusy = false;
                UsersDG.IsReadOnly = true;
                TabItemControl(true);
                foreach (Store item in UsersDataOC)
                {

                    item.IsItReadOnly = false;

                }
            }
            else
            {
                MessageBox.Show("Lütfen Boşlukları doldurunuz!");
            }
        }
        private void EditMemberBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForMemberEditBtn.Visibility = Visibility.Visible;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);
                UsersDG.IsReadOnly = false;
            }
            else
            {
                MessageBox.Show("Şuan Başka bir işlem gerçekleştiriyorsun!");
            }
        }

        private void UsersDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (IsUpdate)
            {

           

                var selectedCellInfo = UsersDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {
                    ProcessCancelForMemberEditBtn.Visibility = Visibility.Visible;

                    Store selectedRowItem = selectedCellInfo.Item as Store;

                    EditMemberBtn.Click -= EditMemberBtn_Click;
                    EditMemberBtn.Click += UpdateMember;
                    EditMemberBtn.Content = "Bayiyi Kaydet?";
                    UpdatingUser = selectedRowItem;


                    int rowIndex = UsersDG.Items.IndexOf(selectedRowItem);

                    UsersDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {


                var selectedCellInfo = UsersDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Store selectedRowItem = selectedCellInfo.Item as Store;

                    int rowIndex = UsersDG.Items.IndexOf(selectedRowItem);

                    UsersDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li Ürünü Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        UsersDataOC.Remove(selectedRowItem);
                        IsDeleted = false;
                        Uyarici();
                        IsItBusy = false;
                        UsersDG.IsReadOnly = true;
                        TabItemControl(true);
                        DeleteMember.Click -= CancelDeleteForCategory;
                        DeleteMember.Click += DeleteCategories_Click;
                        DeleteMember.Content = "Bayi Sil";
                        if (IHateAdo.DeleteUser(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        UsersDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }

        private void ProcessCancelForMemberEditBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessCancelForMemberEditBtn.Visibility = Visibility.Collapsed;
            EditMemberBtn.Content = "Bayi Düzenle";
            EditMemberBtn.Click -= EditMemberBtn_Click;
            EditMemberBtn.Click -= UpdateMember;
            EditMemberBtn.Click += EditMemberBtn_Click;
            IsUpdate = false;
            IsItBusy = false;
            UsersDG.IsReadOnly = true;
            TabItemControl(true);
            foreach (Store item in UsersDataOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingUser = new Store();
            UsersDataOC.Clear();
            RefersUsers();
        }

        private void DeleteMember_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                UsersDG.IsReadOnly = false;
                DeleteMember.Click -= DeleteMember_Click;
                DeleteMember.Click += PropcessCancelDeleteForMember;
                IsItBusy = true;
                IsDeleted = true; 
                TabItemControl(false);
                DeleteMember.Content = "İptal et";
            }
        }

        private void PropcessCancelDeleteForMember(object sender, RoutedEventArgs e)
        {
            UsersDG.IsReadOnly = true;
            DeleteMember.Click -= PropcessCancelDeleteForMember;
            DeleteMember.Click += DeleteMember_Click;
            IsItBusy = false;
            IsDeleted = false; 
            TabItemControl(true);
            DeleteMember.Content = "Üye sil";
        }
        #endregion

        #region - Processors
        private void DeleteProcessor_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                IsItBusy = true;
                IsDeleted = true; TabItemControl(false);
                DeleteProcessor.Content = "Cancel Transaction";
                DeleteProcessor.Click -= DeleteProcessor_Click;
                DeleteProcessor.Click += ProcessCancelForProcessorDelete;
            }
        }

        private void ProcessCancelForProcessorDelete(object sender, RoutedEventArgs e)
        {
            IsItBusy = false;
            IsDeleted = false; TabItemControl(true);
            DeleteProcessor.Content = "Delete Processor";
            DeleteProcessor.Click -= ProcessCancelForProcessorDelete;
            DeleteProcessor.Click += DeleteProcessor_Click;
        }

        private void AddNewProcessorBtn_Click(object sender, RoutedEventArgs e)
        {

            if (!IsItBusy)
            {

                ProcessorOC.Add(new Processor());
                foreach (var item in ProcessorOC)
                {

                    if (ProcessorOC[ProcessorOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                ProcessorDG.IsReadOnly = false;
                CancelProcessForProcessor.Visibility = Visibility.Visible;
                AddNewProcessorBtn.Click -= AddNewProcessorBtn_Click;
                AddNewProcessorBtn.Click += AddNewItemToSqlProcessor;
                AddNewProcessorBtn.Content = "Add Processor?";
                IsItBusy = true; TabItemControl(false);
            }
            else
            {
                MessageBox.Show("You are busy!");
            }
        }
        private void ImageChange_ClickForProcessor(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Processor currentItem = button.DataContext as Processor;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                ProcessorOC.FirstOrDefault(x => x.ID == currentItem.ID).imgs = guid + fileInfo.Extension;
                button.Content = guid + fileInfo.Extension;

                SelectedCurrentFileSaving = MY + guid + fileInfo.Extension;
                SelectedCurrentFile = openFileDialog.FileName;

            }
        }
        private void AddProcessorForSource()
        {
            foreach (Processor item in IHateAdo.ListProcessor())
            {
                ProcessorOC.Add(item);
            }
        }
        private void AddNewItemToSqlProcessor(object sender, RoutedEventArgs e)
        {

            Processor NewPr = ProcessorOC[ProcessorOC.Count - 1];

            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewProcessorBtn.Click -= AddNewItemToSqlProcessor;
                    AddNewProcessorBtn.Click += AddNewProcessorBtn_Click;
                    ProcessorDG.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddProcessor(NewPr))
                    {
                        if (!string.IsNullOrEmpty(SelectedCurrentFile))
                        {
                            FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                            fileInfo.CopyTo(SelectedCurrentFileSaving);
                            SelectedCurrentFileSaving = null;
                            SelectedCurrentFile = null;
                        }
                        MessageBox.Show("Başarıyla eklendi!");
                        ProcessorOC[ProcessorOC.Count - 1].IsItReadOnly = false;
                        ProcessorOC.Clear();
                        Uyarici();
                        AddProcessorForSource();
                        CancelProcessForProcessor.Visibility = Visibility.Collapsed;
                        AddNewProcessorBtn.Content = "Add Processor";
                    }
                    else
                    {
                        MessageBox.Show("ÖZÜR DİLERİM HOCAM BİR YERDE BATIRDIM");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
                }
            }
            else
            {
                MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
            }
        }

        private void ProcessorDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

            if (IsUpdate)
            {

             

                var selectedCellInfo = ProcessorDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {


                    Processor selectedRowItem = selectedCellInfo.Item as Processor;

                    EditProcessorBtn.Click -= EditProcessorBtn_Click;
                    EditProcessorBtn.Click += UpdateProcessor;
                    EditProcessorBtn.Content = "Save processor?";
                    UpdatingProcessor = selectedRowItem;
                    ProcessCancelForProcessorEditBtn.Visibility = Visibility.Visible;

                    int rowIndex = ProcessorDG.Items.IndexOf(selectedRowItem);

                    ProcessorOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {


                var selectedCellInfo = ProcessorDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Processor selectedRowItem = selectedCellInfo.Item as Processor;

                    int rowIndex = ProcessorDG.Items.IndexOf(selectedRowItem);

                    ProcessorOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li İşlemciyi Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        ProcessorOC.Remove(selectedRowItem);
                        IsDeleted = false;
                        Uyarici();
                        IsItBusy = false;
                        TabItemControl(true);
                        ProcessorDG.IsReadOnly = true;
                        DeleteProcessor.Click -= ProcessCancelForProcessorDelete;
                        DeleteProcessor.Click += DeleteProcessor_Click;
                        DeleteProcessor.Content = "Delete Processor";
                        if (IHateAdo.DeleteProcessor(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        ProcessorOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }

        private void CancelProcessForProcessor_Click(object sender, RoutedEventArgs e)
        {
            Processor NewPr = ProcessorOC[ProcessorOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    ProcessorDG.IsReadOnly = true;
                    ProcessorOC.Remove(NewPr);
                    ProcessorOC.Clear();
                    AddProcessorForSource();
                    CancelProcessForProcessor.Visibility = Visibility.Collapsed;
                    AddNewProcessorBtn.Click -= AddNewItemToSqlProcessor;
                    AddNewProcessorBtn.Click += AddNewProcessorBtn_Click;
                    AddNewProcessorBtn.Content = "Add Processor";
                    IsItBusy = false;
                    TabItemControl(true);
                }

            }
        }

        private void ProcessCancelForProcessorEditBtn_Click(object sender, RoutedEventArgs e)
        {
            ProcessorDG.IsReadOnly = true;
            ProcessCancelForProcessorEditBtn.Visibility = Visibility.Collapsed;
            EditProcessorBtn.Content = "Edit Processor"; 
            EditProcessorBtn.Click -= EditProcessorBtn_Click;
            EditProcessorBtn.Click -= UpdateProcessor;
            EditProcessorBtn.Click += EditProcessorBtn_Click;
            IsUpdate = false;
            IsItBusy = false; TabItemControl(true);
            foreach (Processor item in ProcessorOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingProcessor = new Processor();
            ProcessorOC.Clear();
            AddProcessorForSource();
        }
        private void UpdateProcessor(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingProcessor))
            {
                string oldimg = UpdatingProcessor.imgs;
                if (IHateAdo.UpdateProcessor(UpdatingProcessor))
                {

                    MessageBox.Show("İşlem Başarılı");
                    if (!string.IsNullOrEmpty(SelectedCurrentFile))
                    {
                        FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                        FileInfo file = new FileInfo(MY + oldimg);
                        file.Delete();
                        fileInfo.CopyTo(SelectedCurrentFileSaving);
                        SelectedCurrentFileSaving = null;
                        SelectedCurrentFile = null;
                    }
                    Uyarici();
                }
                else
                {
                    MessageBox.Show("İşlem Başarısız");
                }
                ProcessorDG.IsReadOnly = true;
                ProcessCancelForProcessorEditBtn.Visibility = Visibility.Collapsed;
                EditProcessorBtn.Content = "Edit Processor";
                EditProcessorBtn.Click -= UpdateProcessor;
                EditProcessorBtn.Click += EditProcessorBtn_Click;
                IsUpdate = false;
                IsItBusy = false;
                TabItemControl(true);
                foreach (Processor item in ProcessorOC)
                {

                    item.IsItReadOnly = false;

                }
                UpdatingProcessor = new Processor();
                ProcessorOC.Clear();
                AddProcessorForSource();
            }
            else
            {
                MessageBox.Show("Lütfen Boşlukları doldurunuz!");
            }
        }
        private void EditProcessorBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForProcessorEditBtn.Visibility = Visibility.Visible;
                ProcessorDG.IsReadOnly = false;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);

            }
            else
            {
                MessageBox.Show("Şuan Başka bir işlem gerçekleştiriyorsun!");
            }
        }
        #endregion

        #region  - Memories
        private void AddMemoryForSource()
        {
            foreach (Memory item in IHateAdo.ListMemories())
            {
                MemoriesDataOC.Add(item);
            }
        }

        private void UpdateMemories(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingMemory))
            {
                string oldimg = UpdatingMemory.imgs;
                if (IHateAdo.UpdateMemory(UpdatingMemory))
                {
                    MessageBox.Show("İşlem Başarılı");
                    if (!string.IsNullOrEmpty(SelectedCurrentFile))
                    {
                        FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                        FileInfo file = new FileInfo(MY + oldimg);
                        file.Delete();
                        fileInfo.CopyTo(SelectedCurrentFileSaving);
                        SelectedCurrentFileSaving = null;
                        SelectedCurrentFile = null;
                    }
                    Uyarici();
                }
                else
                {
                    MessageBox.Show("İşlem Başarısız");
                }
                MemoriesDG.IsReadOnly = true;
                ProcessCancelForMemoriesEditBtn.Visibility = Visibility.Collapsed;
                EditMemoriesBtn.Content = "Edit Memory";
                EditMemoriesBtn.Click -= UpdateMemories;
                EditMemoriesBtn.Click += EditMemoriesBtn_Click;
                IsUpdate = false;
                IsItBusy = false;
                TabItemControl(true);
                foreach (Memory item in MemoriesDataOC)
                {

                    item.IsItReadOnly = false;

                }
                UpdatingMemory = new Memory();
                MemoriesDataOC.Clear();
                AddMemoryForSource();
            }
            else
            {
                MessageBox.Show("Lütfen Boşlukları doldurunuz!");
            }

        }

        private void EditMemoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                MemoriesDG.IsReadOnly = false;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuan Başka bir işlem gerçekleştiriyorsun!");
            }
        }

        private void ProcessCancelForMemoriesEditBtn_Click(object sender, RoutedEventArgs e)
        {
            IsItBusy = false;
            BrandDG.IsReadOnly = true;
            ProcessCancelForMemoriesEditBtn.Visibility = Visibility.Collapsed;
            IsUpdate = false;
            TabItemControl(true);
            EditMemoriesBtn.Content = "Edit Memory";
            EditMemoriesBtn.Click -= UpdateMemories;
            EditMemoriesBtn.Click += EditMemoriesBtn_Click;
            foreach (Memory item in MemoriesDataOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingMemory = new Memory();
            MemoriesDataOC.Clear();
            AddMemoryForSource();

        }

        private void CancelProcessForMemories_Click(object sender, RoutedEventArgs e)
        {
            Memory NewPr = MemoriesDataOC[MemoriesDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    MemoriesDataOC.Remove(NewPr);
                    MemoriesDataOC.Clear();
                    AddMemoryForSource();
                    MemoriesDG.IsReadOnly = true;
                    CancelProcessForMemories.Visibility = Visibility.Collapsed;
                    AddNewMemoriesBtn.Click -= AddNewItemToSqlMemory;
                    AddNewMemoriesBtn.Click += AddNewMemoriesBtn_Click;
                    IsItBusy = false;
                    TabItemControl(true);
                    AddNewMemoriesBtn.Content = "Add Memory";
                }

            }
        }
        private void AddNewItemToSqlMemory(object sender, RoutedEventArgs e)
        {

            Memory NewPr = MemoriesDataOC[MemoriesDataOC.Count - 1];

            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewMemoriesBtn.Click -= AddNewItemToSqlMemory;
                    AddNewMemoriesBtn.Click += AddNewMemoriesBtn_Click;
                    MemoriesDG.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddMemories(NewPr))
                    {
                        if (!string.IsNullOrEmpty(SelectedCurrentFile))
                        {
                            FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                            fileInfo.CopyTo(SelectedCurrentFileSaving);
                            SelectedCurrentFileSaving = null;
                            SelectedCurrentFile = null;
                        }
                        MessageBox.Show("Başarıyla eklendi!");
                        MemoriesDataOC[MemoriesDataOC.Count - 1].IsItReadOnly = false;
                        MemoriesDataOC.Clear();
                        Uyarici();
                        AddMemoryForSource();
                        CancelProcessForMemories.Visibility = Visibility.Collapsed;
                        AddNewMemoriesBtn.Content = "Add Memory";
                    }
                    else
                    {
                        MessageBox.Show("Bir yerde sorun var");
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen boşluklara dikkat ediniz. Bir yerde sorun var!");
                }
            }
            else
            {
                MessageBox.Show("İsim Boş bırakılınamaz!");

            }
        }
        private void AddNewMemoriesBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                MemoriesDataOC.Add(new Memory());
                foreach (var item in MemoriesDataOC)
                {

                    if (MemoriesDataOC[MemoriesDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                MemoriesDG.IsReadOnly = false;
                CancelProcessForMemories.Visibility = Visibility.Visible;
                AddNewMemoriesBtn.Click -= AddNewMemoriesBtn_Click;
                AddNewMemoriesBtn.Click += AddNewItemToSqlMemory;
                AddNewMemoriesBtn.Content = "Add Memory";
                IsItBusy = true; TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Bir işlem yapıyorusnuz!");
            }

        }

        private void DeleteMemories_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                IsItBusy = true;
                IsDeleted = true; TabItemControl(false);
                DeleteMemories.Content = "Cancel Transaction";
                DeleteMemories.Click -= DeleteMemories_Click;
                DeleteMemories.Click += ProcessCancelForMemoryDelete;
            }
        }

        private void ProcessCancelForMemoryDelete(object sender, RoutedEventArgs e)
        {
            IsItBusy = false;
            IsDeleted = false; TabItemControl(true);
            DeleteMemories.Content = "Cancel Transaction";
            DeleteMemories.Click -= ProcessCancelForMemoryDelete;
            DeleteMemories.Click += DeleteMemories_Click;

        }

        private void MemoriesDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsUpdate)
            {



                var selectedCellInfo = MemoriesDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {


                    Memory selectedRowItem = selectedCellInfo.Item as Memory;

                    EditMemoriesBtn.Click -= EditMemoriesBtn_Click;
                    EditMemoriesBtn.Click += UpdateMemories;
                    EditMemoriesBtn.Content = "Memory Save?";
                    UpdatingMemory = selectedRowItem;
                    ProcessCancelForMemoriesEditBtn.Visibility = Visibility.Visible;

                    int rowIndex = MemoriesDG.Items.IndexOf(selectedRowItem);

                    MemoriesDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {


                var selectedCellInfo = MemoriesDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Memory selectedRowItem = selectedCellInfo.Item as Memory;

                    int rowIndex = MemoriesDG.Items.IndexOf(selectedRowItem);

                    MemoriesDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li Ürünü Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        MemoriesDataOC.Remove(selectedRowItem);
                        IsDeleted = false;
                        Uyarici();
                        IsItBusy = false;
                        TabItemControl(true);
                        MemoriesDG.IsReadOnly = true;
                        DeleteMemories.Click -= ProcessCancelForMemoryDelete;
                        DeleteMemories.Click += DeleteMemories_Click;
                        DeleteMemories.Content = "Delete Memory";
                        if (IHateAdo.DeleteMemory(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        MemoriesDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }
        private void ImageChangeForMemory_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Memory currentItem = button.DataContext as Memory;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                MemoriesDataOC.FirstOrDefault(x => x.ID == currentItem.ID).imgs = guid + fileInfo.Extension;
                button.Content = guid + fileInfo.Extension;

                SelectedCurrentFileSaving = MY + guid + fileInfo.Extension;
                SelectedCurrentFile = openFileDialog.FileName;

            }
        }
        #endregion

        #region - Motherboards
        private void DeleteMotherboard_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                MotherboardDG.IsReadOnly = false;
                DeleteMotherboard.Click -= DeleteMotherboard_Click;
                DeleteMotherboard.Click += CancelDeleteForMotherboard;
                IsItBusy = true;
                IsDeleted = true;
                TabItemControl(false);
                DeleteMotherboard.Content = "Cancel Transaction";
            }
        }
        private void AddMotheboardForSource()
        {
            foreach (Motherboard item in IHateAdo.ListMotherboard())
            {
                MotherboardDataOC.Add(item);
            }
        }

        private void CancelDeleteForMotherboard(object sender, RoutedEventArgs e)
        {
            MotherboardDG.IsReadOnly = true;
            DeleteMotherboard.Click -= CancelDeleteForMotherboard;
            DeleteMotherboard.Click += DeleteMotherboard_Click;
            IsItBusy = false;
            IsDeleted = false;
            TabItemControl(true);
            DeleteMotherboard.Content = "Delet Motherboard";
        }

        private void MotherboardDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsUpdate)
            {



                var selectedCellInfo = MotherboardDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {
                    ProcessCancelForMotherboardEditBtn.Visibility = Visibility.Visible;

                    Motherboard selectedRowItem = selectedCellInfo.Item as Motherboard;

                    EditMotherboardBtn.Click -= EditMotherboardBtn_Click;
                    EditMotherboardBtn.Click += UpdateMotherboardBtn;
                    EditMotherboardBtn.Content = "Save Motherboard?";
                    UpdatingMotherboard = selectedRowItem;


                    int rowIndex = MotherboardDG.Items.IndexOf(selectedRowItem);

                    MotherboardDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {



                var selectedCellInfo = MotherboardDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Motherboard selectedRowItem = selectedCellInfo.Item as Motherboard;

                    int rowIndex = MotherboardDG.Items.IndexOf(selectedRowItem);

                    MotherboardDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"{selectedRowItem.Name} İsimli ve {selectedRowItem.ID} ID'li Ürünü Silmek istiyormusunuz?" +
                        $" SİLME İŞLEMİ GERİ ALINAMAZ", "Dikkat!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        MotherboardDG.IsReadOnly = true;
                        MotherboardDataOC.Remove(selectedRowItem);
                        IsDeleted = false; Uyarici();
                        IsItBusy = false; TabItemControl(true);
                        DeleteMotherboard.Click -= CancelDeleteForCategory;
                        DeleteMotherboard.Click += DeleteCategories_Click;
                        DeleteMotherboard.Content = "Delete Motherboard";
                        if (IHateAdo.DeleteMotherboard(selectedRowItem.ID))
                        {
                            MessageBox.Show("İşlem başarılı!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        MotherboardDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }

        private void UpdateMotherboardBtn(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingMotherboard))
            {
                string oldimg = UpdatingMotherboard.imgs;
                if (IHateAdo.UpdateMotherboard(UpdatingMotherboard))
                {
                    
                    if (!string.IsNullOrEmpty(SelectedCurrentFile))
                    {
                        FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                        FileInfo file = new FileInfo(MY + oldimg);
                        file.Delete();
                        fileInfo.CopyTo(SelectedCurrentFileSaving);
                        SelectedCurrentFileSaving = null;
                        SelectedCurrentFile = null;
                    }
                    MessageBox.Show("Transaction Successful"); Uyarici();
                }
                else
                {
                    MessageBox.Show("Something went wrong");
                }
                ProcessCancelForMotherboardEditBtn.Visibility = Visibility.Collapsed;
                EditMotherboardBtn.Content = "Edit Motherboard";
                EditMotherboardBtn.Click -= UpdateMotherboardBtn;
                EditMotherboardBtn.Click += EditMotherboardBtn_Click;
                IsUpdate = false;
                MotherboardDG.IsReadOnly = true;
                IsItBusy = false;
                TabItemControl(true);
                foreach (Motherboard item in MotherboardDataOC)
                {

                    item.IsItReadOnly = false;

                }
            }
            else
            {
                MessageBox.Show("Please fill in the blanks!");
            }

        }

        private void EditMotherboardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForMotherboardEditBtn.Visibility = Visibility.Visible;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);
                MotherboardDG.IsReadOnly = false;
            }
            else
            {
                MessageBox.Show("Now you are performing another action!");
            }
        }

        private void ProcessCancelForMotherboardEditBtn_Click(object sender, RoutedEventArgs e)
        {
            MotherboardDG.IsReadOnly = true;
            ProcessCancelForMotherboardEditBtn.Visibility = Visibility.Collapsed;
            EditMotherboardBtn.Content = "Edit Motherboard";
            EditMotherboardBtn.Click -= EditMotherboardBtn_Click;
            EditMotherboardBtn.Click -= UpdateMotherboardBtn;
            EditMotherboardBtn.Click += EditMotherboardBtn_Click;
            IsUpdate = false;
            IsItBusy = false; TabItemControl(true);
            foreach (Motherboard item in MotherboardDataOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingMotherboard = new Motherboard();
            MotherboardDataOC.Clear();
            AddMotheboardForSource();
        }
        private void AddNewItemToSqlMotherboard(object sender, RoutedEventArgs e)
        {

            Motherboard NewPr = MotherboardDataOC[MotherboardDataOC.Count - 1];

            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewMotherboardBtn.Click -= AddNewItemToSqlMotherboard;
                    AddNewMotherboardBtn.Click += AddNewMotherboardBtn_Click;
                    MotherboardDG.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddMotherboard(NewPr))
                    {
                        MessageBox.Show("Added successfully!");
                        if (!string.IsNullOrEmpty(SelectedCurrentFile))
                        {
                            FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                            fileInfo.CopyTo(SelectedCurrentFileSaving);
                            SelectedCurrentFileSaving = null;
                            SelectedCurrentFile = null;
                        }
                        MotherboardDataOC[MotherboardDataOC.Count - 1].IsItReadOnly = false;
                        MotherboardDataOC.Clear();
                        Uyarici();
                        AddMotheboardForSource();
                        CancelProcessForMotherboard.Visibility = Visibility.Collapsed;
                        AddNewMotherboardBtn.Content = "Add Motherboard";
                    }
                    else
                    {
                        MessageBox.Show("Someting ewnt wrong");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in the blanks!");
                }
            }
            else
            {
                MessageBox.Show("Please fill in the blanks!");
            }
        }
        private void AddNewMotherboardBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                MotherboardDataOC.Add(new Motherboard());
                foreach (var item in MotherboardDataOC)
                {

                    if (MotherboardDataOC[MotherboardDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                CancelProcessForMotherboard.Visibility = Visibility.Visible;
                AddNewMotherboardBtn.Click -= AddNewMotherboardBtn_Click;
                AddNewMotherboardBtn.Click += AddNewItemToSqlMotherboard;
                AddNewMotherboardBtn.Content = "Add Motherboard?";
                IsItBusy = true;
                MotherboardDG.IsReadOnly = false;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("Şuanda Bir işlem yapıyorusnuz!");
            }
        }

        private void CancelProcessForMotherboard_Click(object sender, RoutedEventArgs e)
        {
            Motherboard NewPr = MotherboardDataOC[MotherboardDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    MotherboardDG.IsReadOnly = true;
                    MotherboardDataOC.Remove(NewPr);
                    MotherboardDataOC.Clear();
                    AddMotheboardForSource();
                    CancelProcessForMotherboard.Visibility = Visibility.Collapsed;

                    AddNewMotherboardBtn.Click -= AddNewItemToSqlMotherboard;
                    AddNewMotherboardBtn.Click += AddNewMotherboardBtn_Click;
                    AddNewMotherboardBtn.Content = "Add Motherboard";
                    IsItBusy = false;
                    TabItemControl(true);
                }

            }
        }
        private void ImageChangeForMotherboard_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Motherboard currentItem = button.DataContext as Motherboard;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                MotherboardDataOC.FirstOrDefault(x => x.ID == currentItem.ID).imgs = guid + fileInfo.Extension;
                button.Content = guid + fileInfo.Extension;

                SelectedCurrentFileSaving = MY + guid + fileInfo.Extension;
                SelectedCurrentFile = openFileDialog.FileName;

            }
        }
        #endregion

        #region - Storages
        private void AddStorageForSource()
        {
            foreach (Storage item in IHateAdo.ListStorage())
            {
                StoragesDataOC.Add(item);
            }
        }
        private void DeleteStorage_Click(object sender, RoutedEventArgs e)
        {

            if (!IsItBusy)
            {
                StorageDG.IsReadOnly = false;
                DeleteStorage.Click -= DeleteStorage_Click;
                DeleteStorage.Click += CancelDeleteForStorage;
                IsItBusy = true;
                IsDeleted = true;
                TabItemControl(false);
                DeleteStorage.Content = "Cancel Transaction";
            }
        }

        private void CancelDeleteForStorage(object sender, RoutedEventArgs e)
        {
            StorageDG.IsReadOnly = true;
            DeleteStorage.Click -= CancelDeleteForStorage;
            DeleteStorage.Click += DeleteStorage_Click;
            IsItBusy = false;
            IsDeleted = false;
            TabItemControl(true);
            DeleteStorage.Content = "Delete Product";
        }

        private void StorageDG_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (IsUpdate)
            {



                var selectedCellInfo = StorageDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {


                    Storage selectedRowItem = selectedCellInfo.Item as Storage;

                    EditStorageBtn.Click -= EditStorageBtn_Click;
                    EditStorageBtn.Click += UpdateStorageDGBtn;
                    EditStorageBtn.Content = "Save Storage?";
                    UpdatingStorage = selectedRowItem;


                    int rowIndex = StorageDG.Items.IndexOf(selectedRowItem);

                    StoragesDataOC[rowIndex].IsItReadOnly = true;
                    IsUpdate = false;
                }
            }
            else if (IsDeleted)
            {



                var selectedCellInfo = StorageDG.SelectedCells.FirstOrDefault();
                if (selectedCellInfo.Item != null)
                {

                    Storage selectedRowItem = selectedCellInfo.Item as Storage;

                    int rowIndex = StorageDG.Items.IndexOf(selectedRowItem);

                    StoragesDataOC[rowIndex].IsItReadOnly = true;

                    MessageBoxResult uyr = MessageBox.Show($"Do you want the Delete product with Name {selectedRowItem.Name} and ID {selectedRowItem.ID}? " +
                        $" DELETION CANNOT BE UNDOED", "Attention!", MessageBoxButton.YesNo);
                    if (uyr == MessageBoxResult.Yes)
                    {
                        StorageDG.IsReadOnly = true;
                        StoragesDataOC.Remove(selectedRowItem);
                        IsDeleted = false; Uyarici();
                        IsItBusy = false; TabItemControl(true);
                        DeleteStorage.Click -= CancelDeleteForStorage;
                        DeleteStorage.Click += DeleteStorage_Click;
                        DeleteStorage.Content = "Delte Storage";
                        if (IHateAdo.DeleteStorage(selectedRowItem.ID))
                        {
                            MessageBox.Show("Transaction successful!");
                        }
                        else
                        {
                            MessageBox.Show("BİR ŞEYLER YANLIŞ ALPEREN BATIRDIN!");
                        }
                    }
                    else
                    {
                        StoragesDataOC[rowIndex].IsItReadOnly = false;
                    }


                }
            }
        }
        private void UpdateStorageDGBtn(object sender, RoutedEventArgs e)
        {
            if (MyLibrary.PropControl(UpdatingStorage))
            {
                string oldimg = UpdatingStorage.imgs;
                if (IHateAdo.UpdateStorage(UpdatingStorage))
                {
                    MessageBox.Show("Transaction Successful"); Uyarici(); 
                    if (!string.IsNullOrEmpty(SelectedCurrentFile))
                    {
                        FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                        FileInfo file = new FileInfo(MY + oldimg);
                        file.Delete();
                        fileInfo.CopyTo(SelectedCurrentFileSaving);
                        SelectedCurrentFileSaving = null;
                        SelectedCurrentFile = null;
                    }
                }
                else
                {
                    MessageBox.Show("Something went worng!");
                }
                ProcessCancelForStorageEditBtn.Visibility = Visibility.Collapsed;
                EditStorageBtn.Content = "Edit Product";
                EditStorageBtn.Click -= UpdateStorageDGBtn;
                EditStorageBtn.Click += EditStorageBtn_Click;
                IsUpdate = false;
                StorageDG.IsReadOnly = true;
                IsItBusy = false;
                TabItemControl(true);
                foreach (Storage item in StoragesDataOC)
                {

                    item.IsItReadOnly = false;

                }
                StoragesDataOC.Clear();
                AddStorageForSource();
            }
            else
            {
                MessageBox.Show("Please fill in the blanks!");
            }

        }

        private void EditStorageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {
                ProcessCancelForStorageEditBtn.Visibility = Visibility.Visible;
                StorageDG.IsReadOnly = false;
                IsUpdate = true;
                IsItBusy = true;
                TabItemControl(false);

            }
            else
            {
                MessageBox.Show("You are currently performing another action!");
            }
        }

        private void ProcessCancelForStorageEditBtn_Click(object sender, RoutedEventArgs e)
        {
            StorageDG.IsReadOnly = true;
            ProcessCancelForStorageEditBtn.Visibility = Visibility.Collapsed;
            EditStorageBtn.Content = "Edit Storage";
            EditStorageBtn.Click -= EditStorageBtn_Click;
            EditStorageBtn.Click -= UpdateStorageDGBtn;
            EditStorageBtn.Click += EditStorageBtn_Click;
            IsUpdate = false;
            IsItBusy = false;
            TabItemControl(true);
            foreach (Storage item in StoragesDataOC)
            {

                item.IsItReadOnly = false;

            }
            UpdatingStorage = new Storage();
            StoragesDataOC.Clear();
            AddStorageForSource();
        }
        private void AddNewItemToSqlStorage(object sender, RoutedEventArgs e)
        {

            Storage NewPr = StoragesDataOC[StoragesDataOC.Count - 1];

            if (!string.IsNullOrEmpty(NewPr.Name))
            {
                if (MyLibrary.PropControl(NewPr))
                {

                    AddNewStorageBtn.Click -= AddNewItemToSqlStorage;
                    AddNewStorageBtn.Click += AddNewStorageBtn_Click;
                    StorageDG.IsReadOnly = true;
                    IsItBusy = false;
                    TabItemControl(true);
                    if (IHateAdo.AddStorage(NewPr))
                    {
                        MessageBox.Show("Transaction Sucessful!");
                        if (!string.IsNullOrEmpty(SelectedCurrentFile))
                        {
                            FileInfo fileInfo = new FileInfo(SelectedCurrentFile);
                            fileInfo.CopyTo(SelectedCurrentFileSaving);
                            SelectedCurrentFileSaving = null;
                            SelectedCurrentFile = null;
                        }
                        StoragesDataOC[StoragesDataOC.Count - 1].IsItReadOnly = false;
                        StoragesDataOC.Clear(); Uyarici();
                        AddStorageForSource();
                        CancelProcessForStorage.Visibility = Visibility.Collapsed;
                        AddNewStorageBtn.Content = "Add Storage";
                    }
                    else
                    {
                        MessageBox.Show("Something Went wrong");
                    }
                }
                else
                {
                    MessageBox.Show("Please fill in the blanks!");
                }
            }
            else
            {
                MessageBox.Show("Please fill in the blanks!");
            }
        }
        private void AddNewStorageBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!IsItBusy)
            {

                StoragesDataOC.Add(new Storage());
                foreach (var item in StoragesDataOC)
                {

                    if (StoragesDataOC[StoragesDataOC.Count - 1] == item)
                    {
                        item.IsItReadOnly = true;
                    }
                    else
                    {
                        item.IsItReadOnly = false;
                    }

                }
                CancelProcessForStorage.Visibility = Visibility.Visible;
                AddNewStorageBtn.Click -= AddNewStorageBtn_Click;
                AddNewStorageBtn.Click += AddNewItemToSqlStorage;
                AddNewStorageBtn.Content = "Add Product?";
                IsItBusy = true;
                StorageDG.IsReadOnly = false;
                TabItemControl(false);
            }
            else
            {
                MessageBox.Show("You are currently performing another action!");
            }
        }
        private void CancelProcessForStorage_Click(object sender, RoutedEventArgs e)
        {
            Storage NewPr = StoragesDataOC[StoragesDataOC.Count - 1];
            if (NewPr.ID == 0)
            {
                MessageBoxResult uyr = MessageBox.Show("Bu işlemi iptal etmek istediğinizden eminmisini? SİLME İŞLEMİ GERİ ALINAMAZ!", "Dikkat!", MessageBoxButton.YesNo);
                if (uyr == MessageBoxResult.Yes)
                {
                    StorageDG.IsReadOnly = true;
                    StoragesDataOC.Remove(NewPr);
                    StoragesDataOC.Clear();
                    AddStorageForSource();
                    CancelProcessForStorage.Visibility = Visibility.Collapsed;
                    AddNewStorageBtn.Click -= AddNewItemToSqlCategories;
                    AddNewStorageBtn.Click += AddNewCategoriesBtn_Click;
                    AddNewStorageBtn.Content = "Add Product";
                    IsItBusy = false;
                    TabItemControl(true);
                }

            }
        }
        private void ImageChangeForStorage_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Storage currentItem = button.DataContext as Storage;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                Guid guid = Guid.NewGuid();
                FileInfo fileInfo = new FileInfo(openFileDialog.FileName);
                StoragesDataOC.FirstOrDefault(x => x.ID == currentItem.ID).imgs = guid + fileInfo.Extension;
                button.Content = guid + fileInfo.Extension;

                SelectedCurrentFileSaving = MY + guid + fileInfo.Extension;
                SelectedCurrentFile = openFileDialog.FileName;

            }
        }
        #endregion

        #region MyHelpers

        public void TabItemControl(bool V)
        {
            foreach (TabItem item in Pages.Items)
            {
                item.IsEnabled = V;
            }
        } 
        private void Uyarici()
        {
            if (ErrorMassageTabItem.Visibility == Visibility.Collapsed)
            {
                ErrorMassageTabItem.Visibility = Visibility.Visible;
                DispatcherTimer tm = new DispatcherTimer();
                AnimateSelection = "ChangeData";
                tm.Interval = TimeSpan.FromSeconds(5);
                tm.Tick += AfterDeleteErrorMessage;
                tm.IsEnabled = true;
                tm.Start();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(DataContext.ToString());
           
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

            
        }









        #endregion





        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            XDocument xDocument = XDocument.Load("C:\\Users\\lastg\\Desktop\\prjefinal54321\\Products.xml");
            XElement xel = xDocument.Root;
            XElement elmntremoveparent = xDocument.Descendants("Products").FirstOrDefault();
            var productsToRemove = elmntremoveparent.Elements("Product").ToList();


            foreach (var elmnt in productsToRemove)
            {
                elmnt.Remove();
            }
            xDocument.Save("C:\\Users\\lastg\\Desktop\\prjefinal54321\\Products.xml");

            foreach (TabItem tabItem in Pages.Items)
            {

                if (tabItem.Content is Grid grid)
                {

                    foreach (var child in grid.Children)
                    {
                        if (child is DataGrid dg)
                        {
                            Type type = dg.GetType();
                            List<object> itemsoruce = new List<object>();
                            itemsoruce.AddRange(dg.ItemsSource.Cast<object>());

                            foreach (object item in itemsoruce)
                            {
                                Type typep = item.GetType();
                                if (typep != typeof(Store) && typep != typeof(Emp))
                                {

                                    PropertyInfo[] props = typep.GetProperties();
                                    XElement xele = new XElement("Product");
                                    XAttribute attype = new XAttribute("Type", typep.Name.Split('_')[0]);
                                    xele.Add(attype);
                                    foreach (var prop in props)
                                    {
                                        object value = prop.GetValue(item);
                                        if (prop.Name == "ID")
                                        {
                                            XAttribute id = new XAttribute("id", value);
                                            xele.Add(id);

                                        }
                                        else if (prop.Name == "BrandID")
                                        {
                                            XElement nowxe = new XElement("BrandName", IHateAdo.ListBrands().FirstOrDefault(x => x.ID == Convert.ToInt32(value)).Name);
                                            xele.Add(nowxe);
                                        }
                                        else if (prop.Name == "CategoryID")
                                        {
                                            XElement nowxe = new XElement("CategoryName", IHateAdo.ListCategory().FirstOrDefault(x => x.ID == Convert.ToInt32(value)).Name);
                                            xele.Add(nowxe);
                                        }
                                        else if (value != null)
                                        {
                                            XElement nowxe = new XElement(prop.Name.ToString(), value);
                                            xele.Add(nowxe);
                                        }

                                    }
                                    xel.Add(xele);
                                    xDocument.Save("C:\\Users\\lastg\\Desktop\\prjefinal54321\\Products.xml");
                                }
                            }
                        }
                    }
                }
            }
            int sa = int.Parse(xel.Attribute("Version").Value);
        
            xel.SetAttributeValue("Version",sa +1);
            
            xDocument.Save("C:\\Users\\lastg\\Desktop\\prjefinal54321\\Products.xml");

        }
        

        

       

    }
}
