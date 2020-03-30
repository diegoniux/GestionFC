using GestionFC.Models.DTO;
using GestionFC.Models.Login;
using GestionFC.ViewModels.Share;
using System.Windows.Input;
using Xamarin.Forms;

namespace GestionFC.ViewModels.Login
{
    public class LoginViewModel : ViewModelBase // está dentro de Share y contiene el código para Notify Property changed 
    {
		private LoginDTO loginDTO;

		public LoginDTO LoginDTO
		{
			get { return loginDTO; }
			set 
			{
				loginDTO = value;
				RaisePropertyChanged(nameof(LoginModel));
			}
		}

		private bool isRunning;

		public bool IsRunning
		{
			get { return isRunning; }
			set 
			{ 
				isRunning = value;
				RaisePropertyChanged(nameof(IsRunning));
			}
		}

	}
}
