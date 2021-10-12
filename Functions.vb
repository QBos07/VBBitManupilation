Module BitManipulation

	Function GetBit(input As Byte(), position As Integer) As Boolean
		Return (input(position \ 8) And (1 << (position Mod 8))) <> 0
	End Function

	Function GetBit(input As UShort(), position As Integer) As Boolean
		Return (input(position \ 16) And (1 << (position Mod 16))) <> 0
	End Function

	Function GetBit(input As Char(), position As Integer, Optional alternateMethod As Boolean = False) As Boolean
		Return If(Not alternateMethod, (AscW(input(position \ 16)) And (1 << (position Mod 16)) <> 0), GetBit(New String(input), position, False))
	End Function

	Function GetBit(input As String, position As Integer, Optional alternateMethod As Boolean = False) As Boolean
		Return If(Not alternateMethod, ((AscW(input(position \ 16)) And (1 << (position Mod 16))) <> 0), GetBit(input.ToCharArray(), position, False))
	End Function


	Function SetBit(input As Byte(), position As Integer, value As Boolean) As Byte()
		input.SetValue(If(value, input(position \ 8) Or (1 << (position Mod 8)), input(position \ 8) And (Not CByte(1 << (position Mod 8)))), position \ 8)
		Return input
	End Function

	Function SetBit(input As UShort(), position As Integer, value As Boolean) As UShort()
		input.SetValue(If(value, input(position \ 16) Or (1 << (position Mod 16)), input(position \ 16) And (Not CUShort(1 << (position Mod 16)))), position \ 16)
		Return input
	End Function

	Function SetBit(input As Char(), position As Integer, value As Boolean, Optional alternateMethod As Boolean = False) As Char()
		If Not alternateMethod Then
			input.SetValue(If(value, ChrW(AscW(input(position \ 16)) Or (1 << (position Mod 16))), ChrW(AscW(input(position \ 16)) And (Not 1 << (position Mod 16)))), position \ 16)
			Return input
		End if
		Return SetBit(New String(input), position, value, False)
	End Function

	Function SetBit(input As String, position As Integer, value As Boolean, Optional alternateMethod As Boolean = False) As String
		Return If(Not alternateMethod, (input.Remove(position \ 16, 1).Insert(position \ 16, If(value, ChrW(AscW(input(position \ 16)) Or (1 << (position Mod 16))), ChrW(AscW(input(position \ 16)) And (Not 1 << (position Mod 16)))))), SetBit(input.ToCharArray(), position, value, False))
	End Function


	' Function GetBit1(input As String, position As Integer) As Boolean
	' 	Return GetBit(input.ToCharArray(), position)
	' End Function

	' Function SetBit1(input As String, position As Integer, value As Boolean) As String
	' 	Return SetBit(input.ToCharArray(), position, value)
	' End Function


	Sub Main()
		Console.WriteLine("    '        Byte() (8bit)")
		Dim ByteArray() As Byte = {&B1010010, &B1010001, &B10101101, &B10101110} ' 01010010 01010001 10101101 10101110
		For index = 0 To ByteArray.Length * 8 - 1
			Dim out As Boolean = GetBit(ByteArray, index)
			Console.Write(If(out, 1, 0))
			Console.Write("    ")
			Console.WriteLine(out)
			If index Mod 8 = 7 Then
				Console.WriteLine()
			End If
		Next
		Console.WriteLine("    '        UShort() (16bit)")
		Dim UShortArray() As UShort = {&B101001001010001, &B1010110110101110} ' 0101001001010001 1010110110101110
		For index = 0 To UShortArray.Length * 16 - 1
			Dim out As Boolean = GetBit(UShortArray, index)
			Console.Write(If(out, 1, 0))
			Console.Write("    ")
			Console.WriteLine(out)
			If index Mod 16 = 15 Then
				Console.WriteLine()
			End If
		Next
		Console.WriteLine("    '        End")
		Console.WriteLine(" (press a key to exit)")
		Dim s = "Hi"
		Console.Write(s)
		Console.Write(" -> ")
		Console.WriteLine(SetBit(s, 16, False))
		Console.ReadKey(False)

	End Sub

End Module