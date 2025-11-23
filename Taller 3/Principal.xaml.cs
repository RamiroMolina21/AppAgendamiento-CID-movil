namespace Taller_3;

public partial class Principal : FlyoutPage
{
	public Principal()
	{
		InitializeComponent();

		Flyout = new Maestro();
		Detail = new NavigationPage(new Detalle());
    }
}