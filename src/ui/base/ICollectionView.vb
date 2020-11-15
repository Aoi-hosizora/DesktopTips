''' <summary>
''' 抽象容器类的 ControlView
''' 要求必须提供 baseItems 只读属性
''' 以便 ControlView 能够快速重载 ItemsCollection
''' </summary>
Public Interface ICollectionView
    ReadOnly Property baseItems As IList
End Interface
