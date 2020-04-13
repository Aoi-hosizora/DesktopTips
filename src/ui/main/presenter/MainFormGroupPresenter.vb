Public Class MainFormGroupPresenter
    Implements MainFormContract.IGroupPresenter

    Private ReadOnly _view As MainFormContract.IView

    Public Sub New(view As MainFormContract.IView)
        _view = view
    End Sub

End Class
