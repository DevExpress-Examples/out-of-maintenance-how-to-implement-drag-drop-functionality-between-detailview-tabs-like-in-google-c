Imports Microsoft.VisualBasic
Imports System
Namespace DXSample
	Partial Public Class Main
		''' <summary>
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary>
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Windows Form Designer generated code"

		''' <summary>
		''' Required method for Designer support - do not modify
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.components = New System.ComponentModel.Container()
			Me.defaultLookAndFeel1 = New DevExpress.LookAndFeel.DefaultLookAndFeel(Me.components)
			Me.myGridControl1 = New DXSample.MyGridControl()
			Me.myGridView1 = New DXSample.MyGridView()
			CType(Me.myGridControl1, System.ComponentModel.ISupportInitialize).BeginInit()
			CType(Me.myGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
			Me.SuspendLayout()
			' 
			' defaultLookAndFeel1
			' 
			Me.defaultLookAndFeel1.LookAndFeel.SkinName = "Blue"
			' 
			' myGridControl1
			' 
			Me.myGridControl1.Dock = System.Windows.Forms.DockStyle.Top
			Me.myGridControl1.EmbeddedNavigator.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
			Me.myGridControl1.Location = New System.Drawing.Point(0, 0)
			Me.myGridControl1.MainView = Me.myGridView1
			Me.myGridControl1.Margin = New System.Windows.Forms.Padding(3, 4, 3, 4)
			Me.myGridControl1.Name = "myGridControl1"

			Me.myGridControl1.Size = New System.Drawing.Size(811, 572)
			Me.myGridControl1.TabIndex = 1
			Me.myGridControl1.ViewCollection.AddRange(New DevExpress.XtraGrid.Views.Base.BaseView() { Me.myGridView1})
			' 
			' myGridView1
			' 
			Me.myGridView1.GridControl = Me.myGridControl1
			Me.myGridView1.Name = "myGridView1"
			Me.myGridView1.OptionsDetail.AllowExpandEmptyDetails = True
			' 
			' Main
			' 
			Me.AutoScaleDimensions = New System.Drawing.SizeF(7F, 16F)
			Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
			Me.ClientSize = New System.Drawing.Size(811, 571)
			Me.Controls.Add(Me.myGridControl1)
			Me.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
			Me.Name = "Main"
			Me.Text = "DetailTabStyle"
'			Me.Load += New System.EventHandler(Me.OnFormLoad);
			CType(Me.myGridControl1, System.ComponentModel.ISupportInitialize).EndInit()
			CType(Me.myGridView1, System.ComponentModel.ISupportInitialize).EndInit()
			Me.ResumeLayout(False)

		End Sub

		#End Region

		Private defaultLookAndFeel1 As DevExpress.LookAndFeel.DefaultLookAndFeel
		Private myGridControl1 As MyGridControl
		Private myGridView1 As MyGridView
	End Class
End Namespace

