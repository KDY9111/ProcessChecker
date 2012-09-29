Imports System.Net.Sockets
Public Class Form1
    Dim OTime As Integer = 0
    Dim host As String
    Dim port As Integer
    Dim counter As Integer
    'Dim Seconds As Integer = Timer1.Interval * 10000
    'Dim Minutes As Integer = Timer1.Interval * 60000
    'Dim Hours As Integer = Timer1.Interval * 3600000
    'Dim Days As Integer
    Public Function IsProcessRunning(name As String) As Boolean
        'here we're going to get a list of all running processes on
        'the computer
        For Each clsProcess As Process In Process.GetProcesses()
            If clsProcess.ProcessName.StartsWith(name) Then
                'process found so it's running so return true
                Return True
            End If
        Next
        ToolStripProgressBar1.Increment(100)
        ToolStripProgressBar1.Increment(-100)
        'process not found, return false
        Return False
    End Function
    Dim Answer As String
    Public Sub CheckForUpdates()
        'This is the updater, I use dropbox because I can easily change the old and replace them with new ones v
        Dim request As System.Net.HttpWebRequest = System.Net.HttpWebRequest.Create("https://dl.dropbox.com/u/14318663/Process%20Checker%20Update/version.txt")
        Dim response As System.Net.HttpWebResponse = request.GetResponse()

        Dim sr As System.IO.StreamReader = New System.IO.StreamReader(response.GetResponseStream())

        Dim newestversion As String = sr.ReadToEnd()
        Dim currentversion As String = Application.ProductVersion
        If newestversion.Contains(currentversion) Then

        Else
            Console.Beep()
            Answer = MsgBox("There is a new update for Process Checker, would you like to download it?", vbYesNo)
            If Answer = vbYes Then System.Diagnostics.Process.Start("https://dl.dropbox.com/u/14318663/Process%20Checker%20Update/Process%20Checker.zip")
        End If
    End Sub
    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        CheckForUpdates()
    End Sub
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    'Check if process MCForge.exe is running and some label coloring : P
        If TextBox3.Text = "" Then
            TextBox3.Text = "30000"
        End If
        If IsProcessRunning(TextBox3.Text) Then
            Label1.ForeColor = Color.Green
            Label1.Text = TextBox3.Text + " is running"
        Else
            Try
                Process.Start(TextBox1.Text)
                Label1.ForeColor = Color.Red
                Label1.Text = TextBox3.Text + " is not running"
            Catch ex As Exception
                TextBox1.Visible = False
                Label1.ForeColor = Color.Red
                Label1.BackColor = Color.Black
                Label1.Text = "oops something went wrong"
                Label5.Text = "You entered an invaild process name: " + TextBox3.Text & vbCrLf & ex.Message + " Please Restart Process Checker"
                Label5.Visible = True
            End Try
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        'Online time 
        OTime = OTime + 1
        Label2.Text = counter
    End Sub

    Private Sub Button2_Click(sender As System.Object, e As System.EventArgs) Handles Button2.Click
        Dim intValue As Integer
        'This portion of the code will check if you entered a number or not if you enter something like poop you will get a message box
        If Not Integer.TryParse(TextBox2.Text, intValue) Then
            MsgBox("You must enter a number FOOL!")
        Else
            'Millisecond to seconds converter Below
            'Takes whatever is in the timer interval textbox and multiples it with 1000
            TextBox2.Text = TextBox2.Text * 1000 ' <--
            Timer1.Interval = TextBox2.Text
        End If
    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        MsgBox("In the textbox you can set the amount of seconds Process Checker should wait to check again")
    End Sub

    Private Sub Button5_Click(sender As System.Object, e As System.EventArgs) Handles Button5.Click
        MsgBox("In the white text box below you will put your MCForge.exe full location, an example would be C:\Users\Bob\Desktop\MyServer\MCForge.exe")
    End Sub
    Private Sub Button6_Click(sender As System.Object, e As System.EventArgs) Handles Button6.Click
        TextBox3.Text = InputBox("Type the name of the program you want Process Checker to check for, Example: MCForge")
        If TextBox3.Text = "" Then
            TextBox3.Text = "MCForge"
        End If
    End Sub

    Private Sub Button7_Click(sender As System.Object, e As System.EventArgs) Handles Button7.Click
        MsgBox("Inside this textbox you can change the name of the program process checker should check for, by default it is MCForge")
    End Sub

    Private Sub StartToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StartToolStripMenuItem.Click
        If TextBox1.Text = "" Then
            MsgBox("Enter File Location of your .EXE Program")
        Else
            Timer1.Start()
            Timer1.Interval = TextBox2.Text
            Label1.ForeColor = Color.Black
            Label1.Text = "Started: Checking..."
        End If
    End Sub

    Private Sub StopToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles StopToolStripMenuItem.Click
        Label1.ForeColor = Color.Black
        Label1.Text = "Not Started: Waiting..."
        Timer1.Interval = 30000
        Timer1.Stop()
    End Sub

    Private Sub RestartToolStripMenuItem_Click(sender As System.Object, e As System.EventArgs) Handles RestartToolStripMenuItem.Click
        Timer1.Stop()
        Application.Restart()
    End Sub
End Class
