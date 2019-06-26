Public Class Form1
    Private obj As New Clase_DePropanizador

    Private Sub BtnCerrarForm_Click(sender As Object, e As EventArgs) Handles BtnCerrarForm.Click
        Close()
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Composicion inicial
        Num1.Value = 2.07
        Num2.Value = 48.67
        Num3.Value = 10.11
        Num4.Value = 24.08
        Num5.Value = 5.41
        Num6.Value = 4.81
        TxtTemperaturaCond.Text = 120.0
    End Sub

    Private Sub Num1_ValueChanged(sender As Object, e As EventArgs) Handles Num1.ValueChanged
        Num7.Value = 100 - (Num1.Value + Num2.Value + Num3.Value + Num4.Value + Num5.Value + Num6.Value)
    End Sub

    Private Sub Num2_ValueChanged(sender As Object, e As EventArgs) Handles Num2.ValueChanged
        Num7.Value = 100 - (Num1.Value + Num2.Value + Num3.Value + Num4.Value + Num5.Value + Num6.Value)
    End Sub

    Private Sub Num3_ValueChanged(sender As Object, e As EventArgs) Handles Num3.ValueChanged
        Num7.Value = 100 - (Num1.Value + Num2.Value + Num3.Value + Num4.Value + Num5.Value + Num6.Value)
    End Sub

    Private Sub Num4_ValueChanged(sender As Object, e As EventArgs) Handles Num4.ValueChanged
        Num7.Value = 100 - (Num1.Value + Num2.Value + Num3.Value + Num4.Value + Num5.Value + Num6.Value)
    End Sub

    Private Sub Num5_ValueChanged(sender As Object, e As EventArgs) Handles Num5.ValueChanged
        Num7.Value = 100 - (Num1.Value + Num2.Value + Num3.Value + Num4.Value + Num5.Value + Num6.Value)
    End Sub

    Private Sub Num6_ValueChanged(sender As Object, e As EventArgs) Handles Num6.ValueChanged
        Num7.Value = 100 - (Num1.Value + Num2.Value + Num3.Value + Num4.Value + Num5.Value + Num6.Value)
    End Sub

    Private Sub NumPropRecup_ValueChanged(sender As Object, e As EventArgs) Handles NumPropRecup.ValueChanged
        obj.BalanceMateria(Num1, Num2, Num3, Num4, Num5, Num6, Num7, NumPropRecup, NumButRecup, NumD1, NumD2, NumD3, NumW1, NumW2, NumW3, NumW4, NumW5, NumW6)
    End Sub

    Private Sub NumButRecup_ValueChanged(sender As Object, e As EventArgs) Handles NumButRecup.ValueChanged
        obj.BalanceMateria(Num1, Num2, Num3, Num4, Num5, Num6, Num7, NumPropRecup, NumButRecup, NumD1, NumD2, NumD3, NumW1, NumW2, NumW3, NumW4, NumW5, NumW6)
    End Sub

    Private Sub BtnCalcular_Click(sender As Object, e As EventArgs) Handles BtnCalcular.Click
        'Try
        If NumD1.Value = 0 Or NumW1.Value = 0 Then
            MsgBox("Realice el balance de materia")
            Exit Sub
        End If
        obj.TempCond = TxtTemperaturaCond.Text
        obj.CalculoK(NumD1, NumD2, NumD3, NumW1, NumW2, NumW3, NumW4, NumW5, NumW6)
        obj.CalcPlatosReflujo()
        'Catch ex As Exception
        '    MsgBox("Los datos no convergen.")
        'End Try

    End Sub

    Private Sub BtnTXT_Click(sender As Object, e As EventArgs) Handles BtnTXT.Click
        Try
            MsgBox("TXT..")
        Catch ex As Exception
            MsgBox("No se puede generar reporte.")
        End Try
    End Sub
End Class