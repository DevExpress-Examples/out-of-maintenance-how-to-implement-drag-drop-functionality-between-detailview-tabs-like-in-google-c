using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Reflection;
using DevExpress.XtraGrid.Tab;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;


namespace DXSample {
    public partial class Main: XtraForm {
        GridHitInfo downHitInfo = null;
        GridHitInfo upHitInfo = null;
        public Main() {
            InitializeComponent();
            
          
        }

       

        private void OnFormLoad(object sender, EventArgs e)
        {
            myGridControl1.DataSource = Master.FillMaster(); 
            //// TODO: This line of code loads data into the 'nwindDataSet.Customers' table. You can move, or remove it, as needed.
            //this.customersTableAdapter.Fill(this.nwindDataSet.Customers);
            //// TODO: This line of code loads data into the 'nwindDataSet.Orders' table. You can move, or remove it, as needed.
            //this.ordersTableAdapter.Fill(this.nwindDataSet.Orders);            
       }

        private void myGridView1_MasterRowExpanded(object sender, CustomMasterRowEventArgs e) {

        }
    }

    public class Master
    {

        private string _Value;
        private List<Detail> _Details;
        private List<Detail> _Details1;
        private List<Detail> _Details2;
        private List<Detail> _Details3;

        public List<Detail> Details
        {
            get { return _Details; }
            set { _Details = value; }
        }
        public List<Detail> Details1
        {
            get { return _Details1; }
            set { _Details1 = value; }
        }
        public List<Detail> Details2
        {
            get { return _Details2; }
            set { _Details2 = value; }
        }
        public List<Detail> Details3
        {
            get { return _Details3; }
            set { _Details3 = value; }
        }
        public Master()
        {

        }

        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }


        public static List<Master> FillMaster()
        {
            List<Master> masters = new List<Master>();
            for (int i = 0; i < 5; i++)
            {
                Master master = new Master();
                master.Value = i.ToString();
                List<Detail> details = new List<Detail>();
                for (int j = 0; j < 5; j++)
                {
                    Detail detail = new Detail();
                    detail.Value_Detail = i.ToString();
                    detail.Value_Detail2 = i.ToString();
                    details.Add(detail);
                }
                master.Details = details;
                master.Details1 = details;
                master.Details2 = details;
                master.Details3 = details;
                masters.Add(master);
            }
            return masters;
        }

    }

    public class Detail
    {

        // Fields...
        private string _Value_Detail;
        private string _Value_Detail2;

        public string Value_Detail
        {
            get { return _Value_Detail; }
            set { _Value_Detail = value; }
        }

        public string Value_Detail2
        {
            get { return _Value_Detail2; }
            set { _Value_Detail2 = value; }
        }


        public Detail()
        {

        }
    }
}
