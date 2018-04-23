Imports Microsoft.VisualBasic
Imports System
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Text
Imports System.Windows.Forms
Imports DevExpress.XtraEditors
Imports System.Reflection
Imports DevExpress.XtraGrid.Tab
Imports DevExpress.XtraGrid.Views.Grid.ViewInfo
Imports DevExpress.XtraGrid.Views.Grid


Namespace DXSample
	Partial Public Class Main
		Inherits XtraForm
		Private downHitInfo As GridHitInfo = Nothing
		Private upHitInfo As GridHitInfo = Nothing
		Public Sub New()
			InitializeComponent()
		End Sub
		Private Sub OnFormLoad(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
			myGridControl1.DataSource = Master.FillMaster()
		End Sub
	End Class

	Public Class Master
		Private _Value As String
		Private _Details As List(Of Detail)
		Private _Details1 As List(Of Detail)
		Private _Details2 As List(Of Detail)
		Private _Details3 As List(Of Detail)

		Public Property Details() As List(Of Detail)
			Get
				Return _Details
			End Get
			Set(ByVal value As List(Of Detail))
				_Details = value
			End Set
		End Property
		Public Property Details1() As List(Of Detail)
			Get
				Return _Details1
			End Get
			Set(ByVal value As List(Of Detail))
				_Details1 = value
			End Set
		End Property
		Public Property Details2() As List(Of Detail)
			Get
				Return _Details2
			End Get
			Set(ByVal value As List(Of Detail))
				_Details2 = value
			End Set
		End Property
		Public Property Details3() As List(Of Detail)
			Get
				Return _Details3
			End Get
			Set(ByVal value As List(Of Detail))
				_Details3 = value
			End Set
		End Property
		Public Sub New()

		End Sub

		Public Property Value() As String
			Get
				Return _Value
			End Get
			Set(ByVal value As String)
				_Value = value
			End Set
		End Property


		Public Shared Function FillMaster() As List(Of Master)
			Dim masters As New List(Of Master)()
			For i As Integer = 0 To 4
				Dim master As New Master()
				master.Value = i.ToString()
				Dim details As New List(Of Detail)()
				For j As Integer = 0 To 4
					Dim detail As New Detail()
					detail.Value_Detail = i.ToString()
					detail.Value_Detail2 = i.ToString()
					details.Add(detail)
				Next j
				master.Details = details
				master.Details1 = details
				master.Details2 = details
				master.Details3 = details
				masters.Add(master)
			Next i
			Return masters
		End Function

	End Class

	Public Class Detail

		' Fields...
		Private _Value_Detail As String
		Private _Value_Detail2 As String

		Public Property Value_Detail() As String
			Get
				Return _Value_Detail
			End Get
			Set(ByVal value As String)
				_Value_Detail = value
			End Set
		End Property

		Public Property Value_Detail2() As String
			Get
				Return _Value_Detail2
			End Get
			Set(ByVal value As String)
				_Value_Detail2 = value
			End Set
		End Property


		Public Sub New()

		End Sub
	End Class
End Namespace
