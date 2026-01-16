# word

## 自动运行宏

宏的名字为特殊的名称

- AutoExec
- AutoNew/Open/Close/Exit

## Document

## ActiveDocument

- Bookmarks
- PageSetup 页面设置
- Paragraphs 段落
- Range
- Section 节
- Selection 选中的内容
- Tables
- Cell
- Headers 页头
- Fields
  - Type wdFieldRef `交叉引用必须看 Field 不看文本`
- Footers 页脚
  - LinkToPrevious 链接到前一节，默认 True
  - Range
  - Shapes
- Hyperlinks 超链接
  - TextToDisplay 超链接的可见文档
  - Address 外部地址
  - SubAddress 文档内部目标
- Lists
- Shapes
  - IncrementLeft() 水平移动磅数
  - IncrementRotation()

```vb
With ActiveDocument.Sections(1) 
 .Headers(wdHeaderFooterPrimary).Range.Text = "Header text" 
 .Footers(wdHeaderFooterPrimary).Range.Text = "Footer text" 
End With
```

- a

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

## Bookmark

## Paragraphs

- ParagraphFormat
  - LineSpacing
  - SpaceBefore

## Section

使用分节符划分文档区域，每个节可以有独立的页眉/页脚等

`布局-分隔符-分节符`

- 下一页：强制启动新页。新节从新页开始，页脚完全独立。
- 连续：不启动新页，页脚是按页面的主要节渲染

Microsoft Word的页眉/页脚（包括页脚）是属于“页面”的属性，而不是严格属于“节”（Section）的属性。
即使你用连续分节符（Continuous Section Break）在同一页内创建了多个节，整张页面也只能显示一个统一的页脚。

```vb
.Hyperlinks.Add Anchor:=targetRange, Address:="", _
  SubAddress:=bookmarkName & "\t", TextToDisplay:="下一节"
```

## Selection

- InsertParagraphAfter 插入一个新的段落
