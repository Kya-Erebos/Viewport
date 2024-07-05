<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnLoadScene = New System.Windows.Forms.Button()
        Me.btnSaveScene = New System.Windows.Forms.Button()
        Me.btnManageScenes = New System.Windows.Forms.Button()
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip()
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSaveScene = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuLoadScene = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReset = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem()
        Me.btnOpenControls = New System.Windows.Forms.Button()
        Me.btnChangeControlMode = New System.Windows.Forms.Button()
        Me.tmrUpdate = New System.Windows.Forms.Timer(Me.components)
        Me.opnFileDialog = New System.Windows.Forms.OpenFileDialog()
        Me.savFileDialog = New System.Windows.Forms.SaveFileDialog()
        Me.btnChangeViewMode = New System.Windows.Forms.Button()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnLoadScene
        '
        Me.btnLoadScene.Location = New System.Drawing.Point(12, 191)
        Me.btnLoadScene.Name = "btnLoadScene"
        Me.btnLoadScene.Size = New System.Drawing.Size(246, 23)
        Me.btnLoadScene.TabIndex = 4
        Me.btnLoadScene.Text = "Load Scene"
        Me.btnLoadScene.UseVisualStyleBackColor = True
        '
        'btnSaveScene
        '
        Me.btnSaveScene.Location = New System.Drawing.Point(12, 162)
        Me.btnSaveScene.Name = "btnSaveScene"
        Me.btnSaveScene.Size = New System.Drawing.Size(246, 23)
        Me.btnSaveScene.TabIndex = 3
        Me.btnSaveScene.Text = "Save Scene"
        Me.btnSaveScene.UseVisualStyleBackColor = True
        '
        'btnManageScenes
        '
        Me.btnManageScenes.Location = New System.Drawing.Point(12, 27)
        Me.btnManageScenes.Name = "btnManageScenes"
        Me.btnManageScenes.Size = New System.Drawing.Size(246, 23)
        Me.btnManageScenes.TabIndex = 2
        Me.btnManageScenes.Text = "Manage Scenes"
        Me.btnManageScenes.UseVisualStyleBackColor = True
        '
        'MenuStrip1
        '
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuHelp})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(270, 24)
        Me.MenuStrip1.TabIndex = 2
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuSaveScene, Me.mnuLoadScene, Me.mnuReset, Me.mnuExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(37, 20)
        Me.mnuFile.Text = "File"
        '
        'mnuSaveScene
        '
        Me.mnuSaveScene.Name = "mnuSaveScene"
        Me.mnuSaveScene.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.S), System.Windows.Forms.Keys)
        Me.mnuSaveScene.Size = New System.Drawing.Size(177, 22)
        Me.mnuSaveScene.Text = "Save Scene"
        '
        'mnuLoadScene
        '
        Me.mnuLoadScene.Name = "mnuLoadScene"
        Me.mnuLoadScene.ShortcutKeys = CType((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.O), System.Windows.Forms.Keys)
        Me.mnuLoadScene.Size = New System.Drawing.Size(177, 22)
        Me.mnuLoadScene.Text = "Load Scene"
        '
        'mnuReset
        '
        Me.mnuReset.Name = "mnuReset"
        Me.mnuReset.Size = New System.Drawing.Size(177, 22)
        Me.mnuReset.Text = "Reset"
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.ShortcutKeys = CType((System.Windows.Forms.Keys.Alt Or System.Windows.Forms.Keys.F4), System.Windows.Forms.Keys)
        Me.mnuExit.Size = New System.Drawing.Size(177, 22)
        Me.mnuExit.Text = "Exit"
        '
        'mnuHelp
        '
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(44, 20)
        Me.mnuHelp.Text = "Help"
        '
        'btnOpenControls
        '
        Me.btnOpenControls.Location = New System.Drawing.Point(12, 56)
        Me.btnOpenControls.Name = "btnOpenControls"
        Me.btnOpenControls.Size = New System.Drawing.Size(246, 23)
        Me.btnOpenControls.TabIndex = 5
        Me.btnOpenControls.Text = "Open Controls"
        Me.btnOpenControls.UseVisualStyleBackColor = True
        '
        'btnChangeControlMode
        '
        Me.btnChangeControlMode.Location = New System.Drawing.Point(12, 85)
        Me.btnChangeControlMode.Name = "btnChangeControlMode"
        Me.btnChangeControlMode.Size = New System.Drawing.Size(246, 23)
        Me.btnChangeControlMode.TabIndex = 6
        Me.btnChangeControlMode.Text = "Control Mode: None"
        Me.btnChangeControlMode.UseVisualStyleBackColor = True
        '
        'tmrUpdate
        '
        Me.tmrUpdate.Enabled = True
        '
        'opnFileDialog
        '
        Me.opnFileDialog.DefaultExt = "vport"
        Me.opnFileDialog.FileName = "Scene"
        Me.opnFileDialog.Filter = "Viewport Files|*.vport|All Files|*.*"
        '
        'btnChangeViewMode
        '
        Me.btnChangeViewMode.Location = New System.Drawing.Point(12, 114)
        Me.btnChangeViewMode.Name = "btnChangeViewMode"
        Me.btnChangeViewMode.Size = New System.Drawing.Size(246, 23)
        Me.btnChangeViewMode.TabIndex = 7
        Me.btnChangeViewMode.Text = "View Mode: Wireframe"
        Me.btnChangeViewMode.UseVisualStyleBackColor = True
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(270, 226)
        Me.Controls.Add(Me.btnChangeViewMode)
        Me.Controls.Add(Me.btnChangeControlMode)
        Me.Controls.Add(Me.btnOpenControls)
        Me.Controls.Add(Me.btnLoadScene)
        Me.Controls.Add(Me.btnSaveScene)
        Me.Controls.Add(Me.btnManageScenes)
        Me.Controls.Add(Me.MenuStrip1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MainMenuStrip = Me.MenuStrip1
        Me.Name = "Form1"
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Viewport"
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents MenuStrip1 As MenuStrip
    Friend WithEvents mnuFile As ToolStripMenuItem
    Friend WithEvents mnuSaveScene As ToolStripMenuItem
    Friend WithEvents mnuLoadScene As ToolStripMenuItem
    Friend WithEvents mnuExit As ToolStripMenuItem
    Friend WithEvents mnuHelp As ToolStripMenuItem
    Friend WithEvents mnuReset As ToolStripMenuItem
    Friend WithEvents btnSaveScene As Button
    Friend WithEvents btnManageScenes As Button
    Friend WithEvents btnLoadScene As Button
    Friend WithEvents btnOpenControls As Button
    Friend WithEvents btnChangeControlMode As Button
    Friend WithEvents tmrUpdate As Timer
    Friend WithEvents opnFileDialog As OpenFileDialog
    Friend WithEvents savFileDialog As SaveFileDialog
    Friend WithEvents btnChangeViewMode As Button
End Class
