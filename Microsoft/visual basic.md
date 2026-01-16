# Visual Basic

## 变量

```vb
Dim v ' 默认类型 Variant
Dim obj As Object ' Currency Decimal Date Byte Boolean Long

Set o = New Object ' 或 Nothing

' 数组
Option Base 0 ' 默认索引从 0 开始
Dim arr() As String
' 更改维度，元素数，上下限
ReDim arr(6) As Double ' Single

Public Const age As Integer = 18
```

行连续符：前导空格后跟一个下划线

## 函数

```vb
Sub Main()
  HouseCalc 99800, 43100 
  Call HouseCalc(380950, 49500)
End Sub

' 可选参数
Sub  HouseCalc(price As Single, wage As Single， Optional s As String = "China")
  If wage <= 0.8 Then
    MsgBox ""
  Else
    MsgBox ""
  End If
End Sub

' 传递命名参数
MsgBox(Title:="Visual Basic")
```

## Sub

Sub 过程没参数的时候必须包括一堆空括号

执行语句但是不返回值

## Function

所有参数按引用传递, 可以有返回值(将值分配给函数名称来返回值)

`ByVal` 创建参数的副本

```vb
Function Factorial(ByVal v As Integer)
  v = v - 1
  If v = 0 then
    Factorial = 1
    Exit Function
  End If
  Factorial = Factorial(v) * (v + 1)
End Function
```

## 语句

```vb
With

End With

Do ' while Until
  ' Exit Do
Loop ' while Until


Dim i As Integer
For i = 1 to 10 Step 2 '默认 Step 1
  Debug.Print i
Next

Dim range As Range
For Each r In range

Next

Select Case i
  Case 1 To 5

  Case Is > 10

  Case Else

End Select
```
