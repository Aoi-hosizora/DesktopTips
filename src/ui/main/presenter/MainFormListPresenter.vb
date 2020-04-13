Public Class MainFormListPresenter
    Implements MainFormContract.IListPresenter

    Private ReadOnly _view As MainFormContract.IView

    Public Sub New(view As MainFormContract.IView)
        _view = view
    End Sub

End Class
