# word

## 自动运行宏

宏的名字为特殊的名称

- AutoExec
- AutoNew/Open/Close/Exit

## Document

- Bookmarks 书签
- Fields 域
  - 111
- Hyperlinks 超链接
  - TextToDisplay 超链接的可见文档
  - Address 外部地址
  - SubAddress 文档内部目标
- InlineShapes
- ListParagraphs 已编号的段落
- Lists
- PageSetup 页面设置
- Paragraphs 段落
- Section 节
- Selection 选中的内容
- Shapes
  - Height
  - ID 只读
  - Name 返回或设置名称
  - TextFrame 返回 TextFrame 对象，包含指定形状的文本
  - Title 返回或设置标题
  - Visible
  - Delete()
  - IncrementTop() 垂直移动
  - IncrementLeft() 水平移动磅数
  - IncrementRotation()
- Tables

```vb
With ActiveDocument.Sections(1) 
 .Headers(wdHeaderFooterPrimary).Range.Text = "Header text" 
 .Footers(wdHeaderFooterPrimary).Range.Text = "Footer text" 
End With
```

```vb
ActiveDocument.Tables(1).Cell(Row:=1, Column:=1).Range
```

```vb
Dim range As Range
Set range = ActiveDocument.Range(Start:=0, End:=0) ' 开头位置

' 修改时互相影响
Set r1 = range
' 修改时不影响彼此
Set r2 = range.Duplicate
```

## ActiveDocument

返回一个 Document 对象

## Bookmark

## Paragraphs

- ParagraphFormat
  - LineSpacing
  - SpaceBefore

## Range

文档中一个连续的区域，由一个起始字符位置和终止字符位置定义

- Bookmarks 只读，返回区域内的所有书签
- Duplicate
- End 返回或设置结束字符的位置
- Fields 返回区域中所有的域
- Find
- Font
- Hyperlinks
- Start 返回或设置起始字符的位置
- Sections 返回范围内节的集合
- Tables
- Text 返回或设置制定区域中的文本
- Underline wdUnderlineDouble
- Words
- Collapse(direction) 将一段区间变成一个点，就变成类似鼠标插入
- InRange(range)
- InsertAfter(text)
- InsertBreak(type)
- MoveEnd()

## Section

Microsoft Word的页眉/页脚（包括页脚）是属于“页面”的属性，而不是严格属于“节”（Section）的属性。
即使你用连续分节符（Continuous Section Break）在同一页内创建了多个节，整张页面也只能显示一个统一的页脚。

- Footers 返回一个 HeaderFooters 集合, 代表指定节的页脚
  - wdHeaderFooterPrimary 返回文档或节中除第一页外所有页上的页眉或页脚
- Headers
- PageSetup 返回指定节相关的信息

## Selection

## TextFrame

- TextRange 返回一个 Range 对象，代表文本框中的文本

## Field

- Result 返回一个 Range 对象

## Table

- Columns
- Rows
- Range
- Title
- Cell(row, column) 介于 1 和 表格行列数之间的任意值

## HeaderFooter

- LinkToPrevious 页眉或页脚链接至前一节
