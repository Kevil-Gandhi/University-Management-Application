Imports System.Data.OleDb

Public Class Form3

    Dim connection As OleDbConnection
    Dim ds As DataSet

    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Dim connection_string As New String("Data sourse=localhost;Password=8466;User id=Kevil;Provider=OraOLEDB.Oracle")
        connection = New OleDbConnection(connection_string)

        fillData()

    End Sub

    Private Sub fillData()

        ds = New DataSet

        Dim adp As New OleDbDataAdapter("select * from COURSE ORDER BY C_ID", connection)

        adp.Fill(ds, "course")

        DataGridView1.DataSource = ds.Tables("course")

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Me.Close()
        Form1.Show()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click

        Try

            connection.Open()

            Dim query As New String("INSERT INTO COURSE(C_ID,C_NAME) Values ('" + TextBox1.Text + "','" + TextBox2.Text + "')")

            Dim command As New OleDbCommand(query, connection)

            Dim res = command.ExecuteNonQuery()

            connection.Close()

            TextBox1.Clear()
            TextBox2.Clear()

            If res >= 1 Then

                MessageBox.Show("Data Inserted.....")
                fillData()

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click

        Try

            connection.Open()

            Dim query As New String("UPDATE COURSE SET C_NAME='" + TextBox2.Text + "' where C_ID='" + TextBox1.Text + "'")

            Dim command As New OleDbCommand(query, connection)

            Dim res = command.ExecuteNonQuery()

            connection.Close()

            TextBox1.Clear()
            TextBox2.Clear()

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

            Dim query As New String("DELETE FROM COURSE Where C_ID = '" + TextBox1.Text + "' ")

            Dim command As New OleDbCommand(query, connection)

            Dim res = command.ExecuteNonQuery()

            connection.Close()

            TextBox1.Clear()
            TextBox2.Clear()

            If res >= 1 Then

                MessageBox.Show("Data Deleted.....")
                fillData()

            End If

        Catch ex As Exception

            MessageBox.Show(ex.Message)

        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub DataGridView1_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        TextBox1.Text = DataGridView1.SelectedCells.Item(0).Value

    End Sub
End Class