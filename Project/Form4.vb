Imports System.Data.OleDb
Imports System.Diagnostics.Eventing
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form4

    Dim connection As OleDbConnection
    Dim ds As DataSet

    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim connection_string As New String("Data Source=localhost;Password=8466;User id=Kevil;Provider=OraOLEDB.Oracle")
        connection = New OleDbConnection(connection_string)

        fillData()
        fillStudDetails()
        fillCourses()

    End Sub

    Private Sub fillCourses()

        Dim adp As New OleDbDataAdapter("select * from COURSE ORDER BY C_ID", connection)

        adp.Fill(ds, "course")

    End Sub

    Private Sub fillStudDetails()

        Dim adp As New OleDbDataAdapter("select * from STUDENT ORDER BY S_ID", connection)

        adp.Fill(ds, "student")

        ComboBox1.DataSource = ds.Tables("student")
        ComboBox1.DisplayMember = "S_NAME"
        ComboBox1.ValueMember = "S_ID"

    End Sub

    Private Sub fillData()

        ds = New DataSet

        Dim adp As New OleDbDataAdapter("select * from FEES ORDER BY PAY_ID", connection)

        adp.Fill(ds, "fees")

        DataGridView1.DataSource = ds.Tables("fees")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()
        Form1.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try

            connection.Open()

            Dim query As New String("INSERT INTO Fees(PAY_ID,S_ID,SEM,COURSE_ID,PAY_DATE) Values (?,?,?,?,TO_DATE(?,'DD-MM-YYYY'))")

            Dim command As New OleDbCommand(query, connection)

            command.Parameters.AddWithValue("?", TextBox1.Text)
            command.Parameters.AddWithValue("?", CInt(ComboBox1.SelectedItem.item(0)))
            command.Parameters.AddWithValue("?", TextBox2.Text)
            command.Parameters.AddWithValue("?", TextBox4.Text)
            command.Parameters.AddWithValue("?", DateTimePicker1.Value.ToString("dd-MM-yyyy"))


            Dim res = command.ExecuteNonQuery()

            connection.Close()

            If res >= 1 Then

                MessageBox.Show("Data Inserted.....")
                fillData()

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try

            connection.Open()

            Dim query As New String("UPDATE Fees SET S_ID=?,SEM=?,COURSE_ID=?,PAY_DATE=TO_DATE(?,'DD-MM-YYYY') WHERE PAY_ID=?")

            Dim command As New OleDbCommand(query, connection)

            command.Parameters.AddWithValue("?", CInt(ComboBox1.SelectedItem.item(0)))
            command.Parameters.AddWithValue("?", TextBox2.Text)
            command.Parameters.AddWithValue("?", TextBox4.Text)
            command.Parameters.AddWithValue("?", DateTimePicker1.Value.ToString("dd-MM-yyyy"))
            command.Parameters.AddWithValue("?", TextBox1.Text)

            Dim res = command.ExecuteNonQuery()

            connection.Close()

            If res >= 1 Then

                MessageBox.Show("Data Updated.....")
                fillData()

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click

        Try

            connection.Open()

            Dim query As New String("DELETE FROM Fees where PAY_ID = '" + TextBox1.Text + "'")

            Dim command As New OleDbCommand(query, connection)

            Dim res = command.ExecuteNonQuery()

            connection.Close()

            If res >= 1 Then

                MessageBox.Show("Data Deleted.....")
                fillData()

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        TextBox1.Text = DataGridView1.SelectedCells.Item(0).Value

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        Try

            connection.Open()

            Dim query As String = "SELECT C_NAME FROM COURSE C,STUDENT S WHERE S.COURSE_ID = C_ID AND S.S_ID = ?"
            Dim command As New OleDbCommand(query, connection)

            command.Parameters.AddWithValue("?", CInt(ComboBox1.SelectedItem.item(0)))

            Dim result As Object = command.ExecuteScalar()
            If result IsNot Nothing Then
                TextBox3.Text = result.ToString()
            Else
                TextBox3.Text = "Name not found"
            End If

            Dim query2 As String = "SELECT SEM FROM STUDENT WHERE S_ID=?"
            Dim command2 As New OleDbCommand(query2, connection)

            command2.Parameters.AddWithValue("?", CInt(ComboBox1.SelectedItem.item(0)))

            Dim result2 As Object = command2.ExecuteScalar()
            If result2 IsNot Nothing Then
                TextBox2.Text = result2.ToString()
            Else
                TextBox2.Text = "Name not found"
            End If

            Dim query3 As String = "SELECT C_ID FROM COURSE WHERE C_NAME=?"
            Dim command3 As New OleDbCommand(query3, connection)

            command3.Parameters.AddWithValue("?", TextBox3.Text)

            Dim result3 As Object = command3.ExecuteScalar()
            If result3 IsNot Nothing Then
                TextBox4.Text = result3.ToString()
            Else
                TextBox4.Text = " "
            End If

        Catch ex As Exception
            MsgBox(ex.Message)


        Finally
            connection.Close()
        End Try

    End Sub

End Class