Partial Public Class MainForm
    Implements MainFormContract.IView

    Private ReadOnly _globalPresenter As MainFormContract.IGlobalPresenter = New MainFormGlobalPresenter(Me)
    Private ReadOnly _listPresenter As MainFormContract.IListPresenter = New MainFormListPresenter(Me)
    Private ReadOnly _groupPresenter As MainFormContract.IGroupPresenter = New MainFormGroupPresenter(Me)

    ''' <summary>
    ''' 引用 Form
    ''' </summary>
    Public Function GetMe() As MainForm Implements MainFormContract.IView.GetMe
        Return Me
    End Function

End Class
