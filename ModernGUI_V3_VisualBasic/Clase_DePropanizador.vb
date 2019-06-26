Public Class Clase_DePropanizador
    Private _D As Double
    Public Property D() As Double
        Get
            Return _D
        End Get
        Set(ByVal value As Double)
            _D = value
        End Set
    End Property
    Private _W As Double

    Public Property W() As Double
        Get
            Return _W
        End Get
        Set(ByVal value As Double)
            _W = value
        End Set
    End Property

    Private _Temp As Double
    Public Property TempCond() As Double
        Get
            Return _Temp
        End Get
        Set(ByVal value As Double)
            _Temp = value
        End Set
    End Property

    Private _TempW As Double
    Public Property TempW() As Double
        Get
            Return _TempW
        End Get
        Set(ByVal value As Double)
            _TempW = value
        End Set
    End Property

    Private _Presion As Double
    Public Property Presion() As Double
        Get
            Return _Presion
        End Get
        Set(ByVal value As Double)
            _Presion = value
        End Set
    End Property

    Private _alfaAVG As Double
    Public Property AlfaAVG() As Double
        Get
            Return _alfaAVG
        End Get
        Set(ByVal value As Double)
            _alfaAVG = value
        End Set
    End Property

    Private _PlatosMinimo As Double
    Public Property PlatosMinimo() As Double
        Get
            Return _PlatosMinimo
        End Get
        Set(ByVal value As Double)
            _PlatosMinimo = value
        End Set
    End Property

    Private _ReflujoMinimo As Double
    Public Property ReflujoMinimo() As Double
        Get
            Return _ReflujoMinimo
        End Get
        Set(ByVal value As Double)
            _ReflujoMinimo = value
        End Set
    End Property

    Private _KiDC3 As Double
    Public Property KiDC3() As Double
        Get
            Return _KiDC3
        End Get
        Set(ByVal value As Double)
            _KiDC3 = value
        End Set
    End Property

    Private _KiDC4 As Double
    Public Property KiDC4() As Double
        Get
            Return _KiDC4
        End Get
        Set(ByVal value As Double)
            _KiDC4 = value
        End Set
    End Property

    Private _KiWC3 As Double
    Public Property KiWC3() As Double
        Get
            Return _KiWC3
        End Get
        Set(ByVal value As Double)
            _KiWC3 = value
        End Set
    End Property

    Private _KiWC4 As Double
    Public Property KiWC4() As Double
        Get
            Return _KiWC4
        End Get
        Set(ByVal value As Double)
            _KiWC4 = value
        End Set
    End Property
    Public Sub BalanceMateria(ByVal num1 As NumericUpDown, ByVal num2 As NumericUpDown, ByVal num3 As NumericUpDown, ByVal num4 As NumericUpDown, ByVal num5 As NumericUpDown, ByVal num6 As NumericUpDown, ByVal num7 As NumericUpDown, ByVal numPropano As NumericUpDown, ByVal numButano As NumericUpDown, ByVal numD1 As NumericUpDown, ByVal numD2 As NumericUpDown, ByVal numD3 As NumericUpDown, ByVal numW1 As NumericUpDown, ByVal numW2 As NumericUpDown, ByVal numW3 As NumericUpDown, ByVal numW4 As NumericUpDown, ByVal numW5 As NumericUpDown, ByVal numW6 As NumericUpDown)
        _D = 0
        _W = 0
        'Calculo de flujos
        _D = num1.Value + (numPropano.Value / 100) * num2.Value + (numButano.Value / 100) * num3.Value
        _W = 100 - _D
        'Calculo de composiciones en Overhead
        If (_D > 0) Then
            numD1.Value = num1.Value * 100D / _D
            numD2.Value = (numPropano.Value * num2.Value) / _D
            numD3.Value = 100D - (numD1.Value + numD2.Value)
        Else
            numD1.Value = 0.0D
            numD2.Value = 0.0D
            numD3.Value = 0.0D
        End If
        'Calculo de composiciones en Bottom
        If (_W > 0) Then
            numW1.Value = ((100D - numPropano.Value) * num2.Value) / _W
            numW2.Value = ((100D - numButano.Value) * num3.Value) / _W
            numW3.Value = num4.Value * 100 / _W
            numW4.Value = num5.Value * 100 / _W
            numW5.Value = num6.Value * 100 / _W
            numW6.Value = 100D - (numW1.Value + numW2.Value + numW3.Value + numW4.Value + numW5.Value)
        Else
            numW1.Value = 0.0D
            numW2.Value = 0.0D
            numW3.Value = 0.0D
            numW4.Value = 0.0D
            numW5.Value = 0.0D
            numW6.Value = 0.0D
        End If
    End Sub
    Public Sub CalculoK(ByVal numD1 As NumericUpDown, ByVal numD2 As NumericUpDown, ByVal numD3 As NumericUpDown, ByVal numW1 As NumericUpDown, ByVal numW2 As NumericUpDown, ByVal numW3 As NumericUpDown, ByVal numW4 As NumericUpDown, ByVal numW5 As NumericUpDown, ByVal numW6 As NumericUpDown)
        ' Por la correlacion de Standing C2 - C6
        ' log10(Ki*P)=a+c*Fi <- Existe linealidad!
        ' Ver: Hidrocarbon Phase Behavior - Amed Tarek pag. 251
        ' Donde: Temp [°F]
        _Temp += 460 ' Convertimos a Rankine
#Region "Constantes necesarias para la correlacion"
        ' Notacion:
        '   C2   0
        '   C3   1
        '   iC4  2
        '   nC4  3
        '   iC5  4
        '   nC5  5
        '   C6   6
        ' bi
        Dim bi() As Double = {1.145, 1.799, 2.037, 2.153, 2.368, 2.48, 2.738}

        ' Tbi °R
        Dim Tbi() As Integer = {303, 416, 471, 491, 542, 557, 610}
#End Region
        ' Declaracion de variables
        Dim i As UInteger = 0, j As UInteger = 0, k As UInteger = 0
        Dim _alfaD As Double = 0, _alfaW As Double = 0
        Dim a As Double = 0, c As Double = 0
        Dim sumPresBurbuj As Double = 0, sumTempBurbuj As Double = 0
        'Valores iniciales de las incognitas (Modificar si es necesario)
        _Presion = 280.0
        _TempW = 710.0
        ' Destilado
        Dim KiD(2) As Double
        Dim FiD(2) As Double
        Dim CompD = New Double() {numD1.Value / 100, numD2.Value / 100, numD3.Value / 100}
        'Colas
        Dim KiW(5) As Double
        Dim FiW(5) As Double
        Dim CompW = New Double() {numW1.Value / 100, numW2.Value / 100, numW3.Value / 100, numW4.Value / 100, numW5.Value / 100, numW6.Value / 100}
        'Calculo en "D"/ Presion de burbuja
        For k = 0 To 2
            FiD(k) = bi(k) * (1 / Tbi(k) + 1 / _Temp)
        Next
        Do While (True)
            sumPresBurbuj = 0
            a = 1.2 + 0.0045 * _Presion + 0.00000015 * Math.Pow(_Presion, 2)
            c = 0.89 - 0.00017 * _Presion - 0.000000035 * Math.Pow(_Presion, 2)
            For i = 0 To 2
                KiD(i) = Math.Pow(10.0, a + c * FiD(i)) / (_Presion)
            Next
            For j = 0 To 2
                sumPresBurbuj += CompD(j) * KiD(j)
            Next
            If (sumPresBurbuj > 0.999999 And sumPresBurbuj < 1.000001) Then
                'Salir
                Exit Do
            ElseIf (sumPresBurbuj >= 1.000001) Then
                _Presion *= 0.9
            ElseIf (sumPresBurbuj <= 0.999999 And sumPresBurbuj > 0) Then
                _Presion *= 1.1
            Else
                MsgBox("Caso no definido" & vbCrLf & sumPresBurbuj & vbCrLf & _Presion & " psia.", MsgBoxStyle.Information, "PRESION")
                Exit Sub
            End If
        Loop
        MsgBox("PRESION DE BURBUJA HALLADA!" & vbCrLf & sumPresBurbuj & vbCrLf & _Presion & " psia.", MsgBoxStyle.Information, "CONVERGE")
        ' Calculo en "W" / Temperatura de Burbuja
        Do While (True)
            sumTempBurbuj = 0
            For i = 0 To 5
                FiW(i) = bi(i + 1) * (1 / Tbi(i + 1) + 1 / TempW)
            Next
            For j = 0 To 5
                KiW(j) = Math.Pow(10.0, a + c * FiW(j)) / (_Presion)
            Next
            For k = 0 To 5
                sumTempBurbuj += CompW(k) * KiW(k)
            Next
            If (sumTempBurbuj > 0.999999 And sumTempBurbuj < 1.000001) Then
                'Salir
                Exit Do
            ElseIf (sumTempBurbuj >= 1.000001) Then
                TempW *= 1.1
            ElseIf (sumTempBurbuj <= 0.999999 And sumTempBurbuj > 0) Then
                TempW *= 0.9
            Else
                MsgBox("Caso no definido" & vbCrLf & sumTempBurbuj & vbCrLf & TempW - 460 & " °F", MsgBoxStyle.Information, "TEMPERATURA")
                Exit Sub
            End If
        Loop
        MsgBox("TEMPERATURA DE BURBUJA HALLADA!" & vbCrLf & sumTempBurbuj & vbCrLf & TempW - 460 & " °F", MsgBoxStyle.Information, "CONNVERGE")
        'Valores alfa
        _alfaD = KiD(1) / KiD(2)
        _alfaW = KiW(0) / KiW(1)
        _alfaAVG = (_alfaD + _alfaW) / 2
        'Valores K necesarios
        _KiDC3 = KiD(1)
        _KiDC4 = KiD(2)
        _KiWC3 = KiW(0)
        _KiWC4 = KiW(1)
        'For i = 0 To 2
        '    MsgBox(KiD(i), MsgBoxStyle.SystemModal, "D")
        'Next
        'For j = 0 To 5
        '    MsgBox(KiW(j), MsgBoxStyle.SystemModal, "W")
        'Next
    End Sub
    Public Sub CalcPlatosReflujo(ByVal numD1 As NumericUpDown, ByVal numD2 As NumericUpDown, ByVal numD3 As NumericUpDown, ByVal numW1 As NumericUpDown, ByVal numW2 As NumericUpDown, ByVal num1 As NumericUpDown, ByVal num2 As NumericUpDown, ByVal num3 As NumericUpDown, ByVal num4 As NumericUpDown, ByVal num5 As NumericUpDown, ByVal num6 As NumericUpDown, ByVal num7 As NumericUpDown)
        Dim Sm As Double = 0
        Dim Beta As Double = 0, bdata As Double = 0
        Dim theta As Double = 0, sum As Double = 0, sumD As Double = 0, i As Integer = 0
        Dim Iteraciones As UInteger = 0, es As Double = 0.0001, ea As Double = 0
        Dim LV As Double = 0, LVm As Double = 0, Sm_S As Double = 0
        Dim CompF = New Double() {num1.Value / 100, num2.Value / 100, num3.Value / 100, num4.Value / 100, num5.Value / 100, num6.Value / 100, num7.Value / 100}
        Dim CompD = New Double() {numD1.Value / 100, numD2.Value / 100, numD3.Value / 100}
        Dim AlfaF = New Double() {68.33, 26.67, 13.83, 11.0, 5.83, 5.0, 1.0}
        ' VALOR INICIAL <----
        Dim theta_i As Double = 0.0 'Valor inicial de la NR
        'Calculo de beta y bdata // Condenser y reboiler
        'bdata = Math.Abs((Math.Log(KiWC3 / KiDC3)) / (Math.Log(KiWC4 / KiDC4)))
        'Beta = Math.Abs(KiDC3 / Math.Pow(KiDC4, bdata))
        bdata = 0.798
        Beta = 1.759
        'Calculo de Sm
        Sm = Math.Log10((numD2.Value / numW1.Value) * Math.Pow(numW2.Value / numD3.Value, bdata) * Math.Pow(W / D, bdata - 1)) / Math.Log10(Beta)

        'Calculo del reflujo minimo // NR
        theta_i = InputBox("Introduzca el valor inicial de Theta (Use coma decimal!)")
        Do While (True)
            sum = 0
            sumD = 1
            For i = 0 To 6
                sum += (CompF(i) * AlfaF(i)) / (AlfaF(i) - theta_i)
                sumD += (CompF(i) * AlfaF(i)) / Math.Pow(AlfaF(i) - theta_i, 2)
            Next
            theta = theta_i - (sum) / (sumD)
            ea = Math.Abs(theta - theta_i) * 100
            If (ea < es Or Iteraciones > 40) Then Exit Do
            Iteraciones += 1
            theta_i = theta
        Loop
        If Iteraciones > 40 Then
            MsgBox("Solucion no hallada")
            Exit Sub
        End If
        'Para el Destilado
        sumD = 0
        For i = 0 To 2
            sumD += (CompD(i) * AlfaF(i)) / (AlfaF(i) - theta)
        Next
        ReflujoMinimo = sumD - 1

        'Calculo del numero minimo de platos
        LV = (1.3 * ReflujoMinimo) / (1.3 * ReflujoMinimo + 1)
        LVm = (ReflujoMinimo) / (ReflujoMinimo + 1)
        Sm_S = InputBox("En la figura 19-7 GPSA con:" & vbCrLf & "L0/V1 = " & LV & vbCrLf & "(L0/V1)m = " & LVm & vbCrLf & "Halle Sm/S: " & Sm)
        PlatosMinimo = Sm / Sm_S
        'MsgBox("Ingresar el numero con coma: ,")
    End Sub
End Class
