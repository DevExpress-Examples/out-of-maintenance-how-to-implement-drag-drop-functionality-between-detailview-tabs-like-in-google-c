using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;

namespace DXSample {
    public class MyGridViewInfo : GridViewInfo {
        public GridDetailInfo gridDetailInfo;

        protected override DevExpress.XtraGrid.Views.Grid.GridDetailInfo CalcTabControlHeaderHitInfo(System.Drawing.Point pt) {
            
            gridDetailInfo = base.CalcTabControlHeaderHitInfo(pt);
            return gridDetailInfo;
        }
        public MyGridViewInfo(DevExpress.XtraGrid.Views.Grid.GridView gridView) : base(gridView) { }

    }
}
