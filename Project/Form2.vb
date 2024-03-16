Imports System.CodeDom
Imports System.Data.OleDb

Public Class Form2

    Dim connection As OleDbConnection
    Dim ds As DataSet

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim connection_string As New String("Data source=localhost;Password=8466;User id=Kevil;Provider=OraOLEDB.Oracle")
        connection = New OleDbConnection(connection_string)

        fillData()
        fillCourses()

    End Sub

    Private Sub fillCourses()

        'ds = New DataSet

        Dim adp As New OleDbDataAdapter("select * from COURSE ORDER BY C_ID", connection)

        adp.Fill(ds, "course")

        ComboBox2.DataSource = ds.Tables("course")
        ComboBox2.DisplayMember = "C_NAME"
        ComboBox2.ValueMember = "C_ID"

    End Sub

    Private Sub fillData()

        ds = New DataSet

        Dim adp As New OleDbDataAdapter("select * from STUDENT ORDER BY S_ID", connection)

        adp.Fill(ds, "student")

        DataGridView1.DataSource = ds.Tables("student")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()
        Form1.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try

            connection.Open()

            Dim gen As String

            If (RadioButton1.Checked) Or (RadioButton2.Checked) Then
                If RadioButton1.Checked Then
                    gen = "Male"
                Else
                    gen = "Female"
                End If

                Dim query As New String("INSERT INTO STUDENT(S_ID,S_NAME,SEM,DIV,COURSE_ID,CITY,GENDER) Values (?,?,?,?,?,?,?)")

                Dim command As New OleDbCommand(query, connection)

                command.Parameters.AddWithValue("?", TextBox1.Text)
                command.Parameters.AddWithValue("?", TextBox2.Text)
                command.Parameters.AddWithValue("?", NumericUpDown1.Value)
                command.Parameters.AddWithValue("?", ComboBox1.SelectedItem)
                command.Parameters.AddWithValue("?", CInt(ComboBox2.SelectedValue))
                command.Parameters.AddWithValue("?", TextBox3.Text)
                command.Parameters.AddWithValue("?", gen)

                Dim res = command.ExecuteNonQuery()

                TextBox1.Clear()

                If res >= 1 Then
                    MsgBox("Data Inserted.....")
                    fillData()
                End If

            Else
                MsgBox("Select Gender")
            End If

            connection.Close()

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try

            connection.Open()

            Dim gen As String

            If (RadioButton1.Checked) Or (RadioButton2.Checked) Then
                If RadioButton1.Checked Then
                    gen = "Male"
                Else
                    gen = "Female"
                End If

                Dim query As New String("UPDATE STUDENT SET S_NAME=?,SEM=?,DIV=?,COURSE_ID=?,CITY=?,GENDER=? where S_ID=?")

                Dim command As New OleDbCommand(query, connection)

                command.Parameters.AddWithValue("?", TextBox2.Text)
                command.Parameters.AddWithValue("?", NumericUpDown1.Value)
                command.Parameters.AddWithValue("?", ComboBox1.SelectedItem)
                command.Parameters.AddWithValue("?", CInt(ComboBox2.SelectedValue))
                command.Parameters.AddWithValue("?", TextBox3.Text)
                command.Parameters.AddWithValue("?", gen)
                command.Parameters.AddWithValue("?", TextBox1.Text)

                Dim res = command.ExecuteNonQuery()

                TextBox1.Clear()

                If res >= 1 Then
                    MsgBox("Data Updated.....")
                    fillData()
                End If

            Else
                MsgBox("Select Gender")
            End If

            connection.Close()

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try

            connection.Open()

            Dim query As New String("DELETE FROM STUDENT where S_ID='" + TextBox1.Text + "'")

            Dim command As New OleDbCommand(query, connection)

            Dim res = command.ExecuteNonQuery()

                If res >= 1 Then
                MsgBox("Data Deleted.....")
                fillData()
                End If

            connection.Close()

            TextBox1.Clear()

        Catch ex As Exception

            MsgBox(ex.Message)

        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        TextBox1.Text = DataGridView1.SelectedCells.Item(0).Value

    End Sub
End Class