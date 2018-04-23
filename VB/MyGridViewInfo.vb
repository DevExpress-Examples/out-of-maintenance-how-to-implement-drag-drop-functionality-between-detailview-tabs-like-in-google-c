Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid

Namespace DXSample
	Public Class MyGridViewInfo
		Inherits GridViewInfo
		Public gridDetailInfo As GridDetailInfo

		Protected Overrides Function CalcTabControlHeaderHitInfo(ByVal pt As System.Drawing.Point) As DevExpress.XtraGrid.Views.Grid.GridDetailInfo

			gridDetailInfo = MyBase.CalcTabControlHeaderHitInfo(pt)
			Return gridDetailInfo
		End Function
		Public Sub New(ByVal gridView As DevExpress.XtraGrid.Views.Grid.GridView)
			MyBase.New(gridView)
		End Sub

	End Class
End Namespace
