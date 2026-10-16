using sagut_packages_trpo6.Models;
using System.Collections.ObjectModel;

namespace sagut_packages_trpo6.Service
{
    public class PackageService
    {
        public WebApiDatabaseContext db = DBService.Instance.Context; 
        public ObservableCollection<Package> forms { get; set; } = new();
        public List<Package> FindProduct()
        {
            return db.Packages.ToList();
        }

        public Package FindProductId(int id) 
        {
            List<Package> allPackages = db.Packages.ToList();
            foreach(Package package in allPackages)
            {
                if(package.Id == id)
                {
                    return package;
                }
            }
            return null;
        }
        public Package Create(Package package)
        {
            db.Packages.Add(package);
            db.SaveChanges();
            return package;
        }
        public Package EditAll(int id, Package package)
        {
            Package item = FindProductId(id);
            if (item == null)
            {
                return null;
            }
            item.TrackingNumber = package.TrackingNumber;
            item.Recipient = package.Recipient;
            item.Weight= package.Weight;
            item.Status = package.Status;
            item.IsDelivered = package.IsDelivered;
            db.SaveChanges();
            return item;
        }
        public Package Edit(int id, Package package)
        {
            Package item = FindProductId(id);
            if (item == null)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(package.TrackingNumber))
                item.TrackingNumber = package.TrackingNumber;
            if (!string.IsNullOrEmpty(package.Recipient))
                item.TrackingNumber = package.Recipient;
            if (package.Weight>0)
                item.Weight = package.Weight;
            if (!string.IsNullOrEmpty(package.Status))
                item.TrackingNumber = package.Status;
            db.SaveChanges();
            return item;
        }

        public bool Delete(int id)
        {
            List<Package> allPackage = db.Packages.ToList();
            Package item = null;
            foreach(Package p in allPackage)
            {
                if (p.Id == id)
                {
                    item = p;
                    break;
                }
            }
            if (item == null)
                return false;
            db.Packages.Remove(item);
            db.SaveChanges();
            return true;
        }
    }
}
