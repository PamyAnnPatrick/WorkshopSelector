using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WorkshopSelector
{
    class VM : INotifyPropertyChanged
    {
        private ObservableCollection<WorkShop> objWorkShop;
        public ObservableCollection<WorkShop> WorkShops
        {
            get { return objWorkShop; }
        }

        private ObservableCollection<Location> objLocation;
        public ObservableCollection<Location> Locations
        {
            get { return objLocation; }
        }

        private string regFeesContent = "";
        public string RegFeesContent
        {
            get { return regFeesContent; }
            set { regFeesContent = value; notifyChange(); }
        }

        private string lodgeFeesContent = "";
        public string LodgeFeesContent
        {
            get { return lodgeFeesContent; }
            set { lodgeFeesContent = value; notifyChange(); }
        }

        private string lodgeCalContent = "";
        public string LodgeCalContent
        {
            get { return lodgeCalContent; }
            set { lodgeCalContent = value; notifyChange(); }
        }

        private string totalContent = "";
        public string TotalContent
        {
            get { return totalContent; }
            set { totalContent = value; notifyChange(); }
        }

        private WorkShop objSelectWorkShop = null;
        public WorkShop SelectedWorkShop
        {
            get { return objSelectWorkShop; }
            set
            {
                objSelectWorkShop = value;
                ShowCustomMessage();
                notifyChange();
            }
        }

        private Location objSelectLocation = null;
        public Location SelectedLocation
        {
            get { return objSelectLocation; }
            set { objSelectLocation = value; notifyChange(); }
        }

        private string msgText;
        public string MsgText
        {
            get { return msgText; }
            set { msgText = value; notifyChange(); }
        }

        private string subContent;
        public string SubContent
        {
            get { return subContent; }
            set { subContent = value; notifyChange(); }
        }

        private string resVisibility = "Collapsed";
        public string ResVisibility
        {
            get { return resVisibility; }
            set { resVisibility = value; notifyChange(); }
        }

        private string conVisibility = "Visible";
        public string ConVisibility
        {
            get { return conVisibility; }
            set { conVisibility = value; notifyChange(); }
        }
        //constructor
        public VM()
        {
            // for Workshop
            var stress = new WorkShop() { Name = "Handling Stress", NoOfDays = 3, Fees = 1500 };
            var times = new WorkShop() { Name = "Time Management", NoOfDays = 3, Fees = 800 };
            var sup = new WorkShop() { Name = "Supervision Skills", NoOfDays = 3, Fees = 1500 };
            var negotiation = new WorkShop() { Name = "Negotiation", NoOfDays = 5, Fees = 1300 };
            var interview = new WorkShop() { Name = "How to Interview", NoOfDays = 1, Fees = 500 };
            objWorkShop = new ObservableCollection<WorkShop>() { stress, times, sup, negotiation, interview };
            //for location
            var austin = new Location() { Name = "Austin", Fees = 150 };
            var chicago = new Location() { Name = "Chicago", Fees = 225 };
            var dallas = new Location() { Name = "Dallas", Fees = 175 };
            var orlando = new Location() { Name = "Orlando", Fees = 300 };
            var Phoenix = new Location() { Name = "Phoenix", Fees = 175 };
            var raleigh = new Location() { Name = "Raleigh", Fees = 150 };
            objLocation = new ObservableCollection<Location>() { austin, chicago, dallas, orlando, Phoenix, raleigh };
        }

        //To clear data
        public void ClearAll()
        {
            try
            {
                RegFeesContent = "";
                LodgeCalContent = "";
                LodgeFeesContent = "";
                TotalContent = "";
                SelectedLocation = null;
                SelectedWorkShop = null;
                ResVisibility = "Collapsed";
                ConVisibility = "Visible";
                MsgText = "Please select a workshop and lodging location.";
            }
            catch (Exception)
            {
                MsgText = "Something went wrong.Please try again.";
            }
        }

        //To calculate the fees
        public void Calculate()
        {
            decimal total, lodgingFees, regFees;
            try
            {
                if (SelectedWorkShop != null && SelectedLocation != null)
                {
                    regFees = SelectedWorkShop.Fees;
                    RegFeesContent = regFees.ToString("C");
                    LodgeCalContent = "Lodge Fees (" + SelectedWorkShop.NoOfDays + " x " + SelectedLocation.Fees + ")";
                    lodgingFees = (SelectedWorkShop.NoOfDays * SelectedLocation.Fees);
                    LodgeFeesContent = lodgingFees.ToString("C");
                    total = regFees + lodgingFees;
                    TotalContent = Math.Round(total, 2).ToString("C");
                    ResVisibility = "Visible";
                    ConVisibility = "Collapsed";
                }
            }
            catch (Exception)
            {
                ClearAll();
                MsgText = "Something went wrong.Please try again.";
            }
        }

        //to register for the workshop
        public void Register()
        {
            try
            {
                if (SelectedWorkShop != null && SelectedLocation != null)
                {
                    MsgText = "Thank you for your interest. You have been registered for '" + SelectedWorkShop.Name
                    + "' workshop in " + SelectedLocation.Name + ".";
                    SubContent = "Workshop Fees";
                    Calculate();
                }
            }
            catch (Exception)
            {
                ClearAll();
                MsgText = "Something went wrong.Please try again.";
            }
        }

        // to show custom message according to the selection of listbox
        public void ShowCustomMessage()
        {
            try
            {
                MsgText = "";
                if (SelectedWorkShop != null && SelectedLocation != null)
                {
                    MsgText = "You have selected '" + SelectedWorkShop.Name + "' workshop in " + SelectedLocation.Name
                        + ". Click Register button to register for the workshop.";
                    SubContent = "Estimated Fees";
                    Calculate();
                }
                else if (SelectedWorkShop != null && SelectedLocation == null)
                    MsgText = "Please select a lodging location for '" + SelectedWorkShop.Name + "' workshop. ";
                else if (SelectedWorkShop == null && SelectedLocation != null)
                    MsgText = "Please select a workshop that you want to attend in " + SelectedLocation.Name + ".";
                else
                    MsgText = "Please select a workshop and lodging location.";
            }
            catch (Exception)
            {
                ClearAll();
                MsgText = "Something went wrong.Please try again.";
            }
        }

        //event registration
        public event PropertyChangedEventHandler PropertyChanged;

        private void notifyChange([CallerMemberName] string property = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }

    //seperate class for workshop
    public class WorkShop
    {
        public string Name
        {
            get;
            set;
        }
        public int NoOfDays
        {
            get;
            set;
        }
        public int Fees
        {
            get;
            set;
        }
    }

    //seperate class for location
    public class Location
    {
        public string Name
        {
            get;
            set;
        }
        public int Fees
        {
            get;
            set;
        }
    }
}
