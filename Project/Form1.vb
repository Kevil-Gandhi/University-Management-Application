Imports System.Data.OleDb

Public Class Form1

    Dim connection As OleDbConnection
    Dim ds As DataSet

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        ComboBox1.Items.AddRange({"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        ComboBox1.SelectedIndex = 0

        ComboBox3.Items.AddRange({"1", "2", "3", "4", "5", "6", "7", "8", "9", "10"})
        ComboBox3.SelectedIndex = 0

        'Me.WindowState = FormWindowState.Maximized

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Hide()
        Form2.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Me.Hide()
        Form3.Show()

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Me.Hide()
        Form4.Show()

    End Sub

    Private Sub TabPage2_Click(sender As Object, e As EventArgs) Handles TabPage2.Click

        Dim connection_string As New String("Data Source=localhost;Password=8466;User Id=Kevil;Provider=OraOLEDB.Oracle")

        connection = New OleDbConnection(connection_string)

        fillData()
        fillCourses()

    End Sub

    Private Sub fillCourses()

        Dim adp As New OleDbDataAdapter("select * from COURSE ORDER BY C_ID", connection)

        adp.Fill(ds, "course")

        ComboBox2.DataSource = ds.Tables("course")
        ComboBox2.DisplayMember = "C_NAME"
        ComboBox2.ValueMember = "C_ID"

    End Sub

    Private Sub fillData()

        ds = New DataSet

        Dim adp As New OleDbDataAdapter("SELECT * FROM STUDENT order by S_ID", connection)

        adp.Fill(ds, "stud")

        DataGridView1.DataSource = ds.Tables("stud")

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        If ComboBox1.SelectedItem = "" Then

            MsgBox("Please Select Semester")

        Else

            Dim connection_string As New String("Data Source=localhost;Password=8466;User Id=Kevil;Provider=OraOLEDB.Oracle")

            connection = New OleDbConnection(connection_string)

            connection.Open()

            Dim query As String = "SELECT * FROM Student WHERE sem = '" + ComboBox1.SelectedItem + "'"
            Dim adapter As New OleDbDataAdapter(query, connection)

            Dim dt As New DataTable()
            adapter.Fill(dt)

            DataGridView1.DataSource = dt

            connection.Close()

        End If

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click

        connection.Open()

        Dim query2 As String = "SELECT S.* FROM STUDENT S,COURSE C where S.COURSE_ID = C.C_ID AND C.C_ID = ?"

        Dim adapter2 As New OleDbDataAdapter(query2, connection)

        adapter2.SelectCommand.Parameters.AddWithValue("?", CInt(ComboBox2.SelectedItem.item(0)))

        Dim dt As New DataTable()

        adapter2.Fill(dt)

        DataGridView2.DataSource = dt

        connection.Close()

    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click

        connection.Open()

        Dim query3 As String = "SELECT S.* FROM STUDENT S,FEES F where S.S_ID=F.S_ID AND F.SEM = ?"

        Dim adapter3 As New OleDbDataAdapter(query3, connection)

        adapter3.SelectCommand.Parameters.AddWithValue("?", CInt(ComboBox3.SelectedItem))

        Dim dt As New DataTable()

        adapter3.Fill(dt)

        DataGridView3.DataSource = dt

        connection.Close()

    End Sub

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

        PictureBox1.Image = Image.FromFile("D:\Sem 4\VB.net\Student_5.png")

    End Sub
End Class
