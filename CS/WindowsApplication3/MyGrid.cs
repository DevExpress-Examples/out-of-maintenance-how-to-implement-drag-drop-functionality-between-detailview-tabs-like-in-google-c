using System;
using System.Collections.Generic;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Registrator;
using DevExpress.XtraTab;
using DevExpress.XtraGrid.Tab;
using DevExpress.XtraGrid.Views.Grid;
using System.Drawing;
using DevExpress.Utils;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraTab.ViewInfo;
using DevExpress.XtraGrid.Views.Base.ViewInfo;
using System.Reflection;
using System.Linq;
using System.Runtime.InteropServices;

namespace DXSample
{
    public class MyGridControl : GridControl
    {
        private GridView gridView1;
        GridHitInfo downHitInfo = null;
        int oldIndex = -1;
        public List<int> tabOrderList;

        protected override BaseView CreateDefaultView()
        {
            return CreateView("MyGridView");
        }
        protected override void RegisterAvailableViewsCore(InfoCollection collection)
        {
            base.RegisterAvailableViewsCore(collection);
            collection.Add(new MyGridViewInfoRegistrator());
        }

        private void InitializeComponent()
        {
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            this.SuspendLayout();
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this;
            this.gridView1.Name = "gridView1";
            // 
            // MyGridControl
            // 
            this.MainView = this.gridView1;
            this.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
            this.ResumeLayout(false);

        }
        protected override void OnMouseDown(MouseEventArgs ev)
        {
            GridHitInfo hitInfo = ((GridView)this.MainView).CalcHitInfo(ev.Location);
            Console.WriteLine(hitInfo.HitTest);
            GridView view = (GridView)this.GetViewAt(ev.Location);

            downHitInfo = view.CalcHitInfo(ev.Location);
            GridDetailInfo gridDetailInfo = ((MyGridViewInfo)view.GetViewInfo()).gridDetailInfo;

            if (gridDetailInfo != null)
            {
                PropertyInfo pi = typeof(BaseView).GetProperty("TabControl", BindingFlags.NonPublic | BindingFlags.Instance);
                ViewTab tabControl = pi.GetValue(view, null) as ViewTab;               
                ViewTabPage page = tabControl.Pages.OfType<ViewTabPage>().First(p => p.DetailInfo == gridDetailInfo);
                oldIndex = ((IList)tabControl.Pages).IndexOf(page);
            }
            base.OnMouseDown(ev);
        }
        protected override void OnMouseMove(MouseEventArgs ev)
        {
            ViewTab tabControl = null;
            base.OnMouseMove(ev);
            GridView view = (GridView)this.GetViewAt((ev.Location));
            MyGridViewInfo gridViewInfo = view.GetViewInfo() as MyGridViewInfo;
            if (gridViewInfo == null) return;
            GridDetailInfo gridnewDetailInfo = gridViewInfo.gridDetailInfo as GridDetailInfo;
          
            if (gridnewDetailInfo != null)
            {
                if (ev.Button == MouseButtons.Left && gridnewDetailInfo != null)
                {
                    if (gridnewDetailInfo != null)
                    {
                        PropertyInfo pi = typeof(BaseView).GetProperty("TabControl", BindingFlags.NonPublic | BindingFlags.Instance);
                        tabControl = pi.GetValue(view, null) as ViewTab;
                        ViewTabPage pg = tabControl.Pages.OfType<ViewTabPage>().First(p => p.DetailInfo == gridnewDetailInfo);
                        int newIndex = ((IList)tabControl.Pages).IndexOf(pg);
                        if (newIndex != oldIndex && oldIndex != -1)
                        {
                            if (tabControl.Pages.Count > 1)
                            {
                                ViewTabPage page1 = tabControl.Pages[oldIndex];
                                ViewTabPage page2 = tabControl.Pages[newIndex];
                                GridDetailInfo info = page1.DetailInfo;
                                ((IList)tabControl.Pages).RemoveAt(oldIndex);
                                ((IList)tabControl.Pages).Insert(oldIndex, page2);
                                ((IList)tabControl.Pages).RemoveAt(newIndex);
                                ((IList)tabControl.Pages).Insert(newIndex, page1);

                                page1.DetailInfo = page2.DetailInfo;
                                page2.DetailInfo = info;

                                MyGridView myGridView = view as MyGridView;
                                tabOrderList = new List<int>();
                                foreach (ViewTabPage page in tabControl.Pages)
                                {
                                    tabOrderList.Add((int)page.Tag);
                                }
                                oldIndex = newIndex;
                                tabControl.LayoutChanged();
                            }
                        }
                    }
                }
            }
        }
    }

    public class MyGridViewInfoRegistrator : GridInfoRegistrator
    {
        public override string ViewName { get { return "MyGridView"; } }
        public override BaseView CreateView(GridControl grid) { return new MyGridView(grid as GridControl); }
        public override BaseViewInfo CreateViewInfo(BaseView view) { return new MyGridViewInfo(view as MyGridView); }

    }

    public class MyGridView : DevExpress.XtraGrid.Views.Grid.GridView
    {

        public MyGridView() : this(null) { }
        public MyGridView(DevExpress.XtraGrid.GridControl grid) : base(grid) { }
        protected override string ViewName { get { return "MyGridView"; } }


        protected override void PopulateTabMasterData(ViewTab tabControl, int rowHandle)
        {
            base.PopulateTabMasterData(tabControl, rowHandle);
            if (GridControl is MyGridControl)
            {
                MyGridControl gridControl = GridControl as MyGridControl;
                if (gridControl.tabOrderList != null)
                {
                    if (((MyGridControl)this.GridControl).tabOrderList != null)
                    {
                        for (int i = 0; i < ((MyGridControl)this.GridControl).tabOrderList.Count; i++)
                        {
                            if (tabControl.Pages[i].DetailInfo.RelationIndex != ((MyGridControl)this.GridControl).tabOrderList[i])
                            {
                                ViewTabPage page1 = tabControl.Pages[i];
                                ViewTabPage page2 = tabControl.Pages[((MyGridControl)this.GridControl).tabOrderList[i]];
                                ((IList)tabControl.Pages).RemoveAt(i);
                                ((IList)tabControl.Pages).Insert(i, page2);
                                ((IList)tabControl.Pages).RemoveAt(((MyGridControl)this.GridControl).tabOrderList[i]);
                                ((IList)tabControl.Pages).Insert(((MyGridControl)this.GridControl).tabOrderList[i], page1);
                            }
                        }
                    }
                }
            }
        }
    }
}