namespace DiaryBase;

public partial class DetailPage : ContentPage
{
	public DetailPage(DetailViewModel vm)
	{
		InitializeComponent();
		vm = new DetailViewModel();
	}
}