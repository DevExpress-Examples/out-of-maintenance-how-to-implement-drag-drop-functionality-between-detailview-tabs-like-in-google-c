Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.Windows.Forms
Imports DevExpress.Skins
Imports DevExpress.XtraGrid
Imports DevExpress.XtraGrid.Views.Base
Imports DevExpress.XtraGrid.Registrator
Imports DevExpress.XtraTab
Imports DevExpress.XtraGrid.Tab
Imports DevExpress.XtraGrid.Views.Grid
Imports System.Drawing
Imports DevExpress.Utils
Imports System.Collections
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraTab.ViewInfo
Imports DevExpress.XtraGrid.Views.Base.ViewInfo
Imports System.Reflection
Imports System.Linq
Imports System.Runtime.InteropServices

Namespace DXSample
	Public Class MyGridControl
		Inherits GridControl
		Private gridView1 As GridView
		Private downHitInfo As GridHitInfo = Nothing
		Private oldIndex As Integer = -1
		Public tabOrderList As List(Of Integer)

		Protected Overrides Function CreateDefaultView() As BaseView
			Return CreateView("MyGridView")
		End Function
		Protected Overrides Sub RegisterAvailableViewsCore(ByVal collection As InfoCollection)
			MyBase.RegisterAvailableViewsCore(collection)
			collection.Add(New MyGridViewInfoRegistrator())
		End Sub

		Private Sub InitializeComponent()
			Me.gridView1 = New DevExpress.XtraGrid.Views.Grid.GridView()
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' gridView1
			' 
			Me.gridView1.GridControl = Me
			Me.gridView1.Name = "gridView1"
			' 
			' MyGridControl
			' 
			Me.MainView = Me.gridView1
			Me.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.gridView1})
			CType(Me.gridView1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub
		Protected Overrides Sub OnMouseDown(ByVal ev As MouseEventArgs)
			Dim hitInfo As GridHitInfo = (CType(Me.MainView, GridView)).CalcHitInfo(ev.Location)
			Console.WriteLine(hitInfo.HitTest)
			Dim view As GridView = CType(Me.GetViewAt(ev.Location), GridView)

			downHitInfo = view.CalcHitInfo(ev.Location)
			Dim gridDetailInfo As GridDetailInfo = (CType(view.GetViewInfo(), MyGridViewInfo)).gridDetailInfo

			If gridDetailInfo IsNot Nothing Then
				Dim pi As PropertyInfo = GetType(BaseView).GetProperty("TabControl", BindingFlags.NonPublic Or BindingFlags.Instance)
				Dim tabControl As ViewTab = TryCast(pi.GetValue(view, Nothing), ViewTab)
				Dim page As ViewTabPage = tabControl.Pages.OfType(Of ViewTabPage)().First(Function(p) p.DetailInfo Is gridDetailInfo)
				oldIndex = (CType(tabControl.Pages, IList)).IndexOf(page)
			End If
			MyBase.OnMouseDown(ev)
		End Sub
		Protected Overrides Sub OnMouseMove(ByVal ev As MouseEventArgs)
			Dim tabControl As ViewTab = Nothing
			MyBase.OnMouseMove(ev)
			Dim view As GridView = CType(Me.GetViewAt((ev.Location)), GridView)
			Dim gridViewInfo As MyGridViewInfo = TryCast(view.GetViewInfo(), MyGridViewInfo)
			If gridViewInfo Is Nothing Then
				Return
			End If
			Dim gridnewDetailInfo As GridDetailInfo = TryCast(gridViewInfo.gridDetailInfo, GridDetailInfo)

			If gridnewDetailInfo IsNot Nothing Then
				If ev.Button = MouseButtons.Left AndAlso gridnewDetailInfo IsNot Nothing Then
					If gridnewDetailInfo IsNot Nothing Then
						Dim pi As PropertyInfo = GetType(BaseView).GetProperty("TabControl", BindingFlags.NonPublic Or BindingFlags.Instance)
						tabControl = TryCast(pi.GetValue(view, Nothing), ViewTab)
						Dim pg As ViewTabPage = tabControl.Pages.OfType(Of ViewTabPage)().First(Function(p) p.DetailInfo Is gridnewDetailInfo)
						Dim newIndex As Integer = (CType(tabControl.Pages, IList)).IndexOf(pg)
						If newIndex <> oldIndex AndAlso oldIndex <> -1 Then
							If tabControl.Pages.Count > 1 Then
								Dim page1 As ViewTabPage = tabControl.Pages(oldIndex)
								Dim page2 As ViewTabPage = tabControl.Pages(newIndex)
								Dim info As GridDetailInfo = page1.DetailInfo
								CType(tabControl.Pages, IList).RemoveAt(oldIndex)
								CType(tabControl.Pages, IList).Insert(oldIndex, page2)
								CType(tabControl.Pages, IList).RemoveAt(newIndex)
								CType(tabControl.Pages, IList).Insert(newIndex, page1)

								page1.DetailInfo = page2.DetailInfo
								page2.DetailInfo = info

								Dim myGridView As MyGridView = TryCast(view, MyGridView)
								tabOrderList = New List(Of Integer)()
								For Each page As ViewTabPage In tabControl.Pages
									tabOrderList.Add(CInt(Fix(page.Tag)))
								Next page
								oldIndex = newIndex
								tabControl.LayoutChanged()
							End If
						End If
					End If
				End If
			End If
		End Sub
	End Class

	Public Class MyGridViewInfoRegistrator
		Inherits GridInfoRegistrator
		Public Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property
		Public Overrides Function CreateView(ByVal grid As GridControl) As BaseView
			Return New MyGridView(TryCast(grid, GridControl))
		End Function
		Public Overrides Function CreateViewInfo(ByVal view As BaseView) As BaseViewInfo
			Return New MyGridViewInfo(TryCast(view, MyGridView))
		End Function

	End Class

	Public Class MyGridView
		Inherits DevExpress.XtraGrid.Views.Grid.GridView

		Public Sub New()
			Me.New(Nothing)
		End Sub
		Public Sub New(ByVal grid As DevExpress.XtraGrid.GridControl)
			MyBase.New(grid)
		End Sub
		Protected Overrides ReadOnly Property ViewName() As String
			Get
				Return "MyGridView"
			End Get
		End Property


		Protected Overrides Sub PopulateTabMasterData(ByVal tabControl As ViewTab, ByVal rowHandle As Integer)

            MyBase.PopulateTabMasterData(tabControl, rowHandle)
            If TypeOf GridControl Is MyGridControl Then
                Dim gridControl__1 As MyGridControl = TryCast(GridControl, MyGridControl)
                If gridControl__1.tabOrderList IsNot Nothing Then
                    If DirectCast(Me.GridControl, MyGridControl).tabOrderList IsNot Nothing Then
                        For i As Integer = 0 To DirectCast(Me.GridControl, MyGridControl).tabOrderList.Count - 1
                            If tabControl.Pages(i).DetailInfo.RelationIndex <> DirectCast(Me.GridControl, MyGridControl).tabOrderList(i) Then
                                Dim page1 As ViewTabPage = tabControl.Pages(i)
                                Dim page2 As ViewTabPage = tabControl.Pages(DirectCast(Me.GridControl, MyGridControl).tabOrderList(i))
                                DirectCast(tabControl.Pages, IList).RemoveAt(i)
                                DirectCast(tabControl.Pages, IList).Insert(i, page2)
                                DirectCast(tabControl.Pages, IList).RemoveAt(DirectCast(Me.GridControl, MyGridControl).tabOrderList(i))
                                DirectCast(tabControl.Pages, IList).Insert(DirectCast(Me.GridControl, MyGridControl).tabOrderList(i), page1)
                            End If
                        Next
                    End If
                End If
            End If

		End Sub
	End Class
End Namespace